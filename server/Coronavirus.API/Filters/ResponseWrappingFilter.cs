using Coronavirus.API.Attributes;
using Coronavirus.API.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Coronavirus.API.Filters;

public class ResponseWrappingFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var endpoint = context.HttpContext.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<NoWrapAttribute>() is not null)
        {
            await next();
            return;
        }

        // Skip for ProblemDetails or non-success statuses
        if (context.Result is ObjectResult objRes)
        {
            if (objRes.Value is ProblemDetails)
            {
                await next();
                return;
            }
        }

        // Skip for File/NoContent
        if (context.Result is FileResult || context.Result is NoContentResult)
        {
            await next();
            return;
        }

        // Handle CreatedAtAction specially (wrap value and preserve Location)
        if (context.Result is CreatedAtActionResult created)
        {
            var traceId = context.HttpContext.TraceIdentifier;
            var api = ApiResponse.FromSuccess(created.Value, message: null, traceId: traceId);

            // Resolve Location using LinkGenerator if possible
            var linkGenerator = context.HttpContext.RequestServices.GetService<LinkGenerator>();
            string? location = null;
            if (linkGenerator is not null)
            {
                var path = linkGenerator.GetPathByAction(context.HttpContext,
                    action: created.ActionName,
                    controller: created.ControllerName,
                    values: created.RouteValues);
                if (!string.IsNullOrEmpty(path)) location = path;
            }

            if (!string.IsNullOrEmpty(location))
            {
                context.HttpContext.Response.Headers.Location = location;
            }

            context.Result = new ObjectResult(api) { StatusCode = StatusCodes.Status201Created };
            await next();
            return;
        }

        // Wrap 2xx Object results
        int? status = context switch
        {
            { Result: OkObjectResult } => StatusCodes.Status200OK,
            { Result: ObjectResult o } => o.StatusCode,
            _ => null
        };

        int code = status ?? (context.Result is ObjectResult ? StatusCodes.Status200OK : 0);
        bool isSuccess = code >= 200 && code < 300;

        if (isSuccess)
        {
            object? value = context.Result switch
            {
                OkObjectResult ok => ok.Value,
                ObjectResult o => o.Value,
                _ => null
            };

            // If already ApiResponse, do nothing
            if (value is ApiResponse)
            {
                await next();
                return;
            }

            var traceId = context.HttpContext.TraceIdentifier;
            var api = ApiResponse.FromSuccess(value, message: null, traceId: traceId);
            context.Result = new ObjectResult(api) { StatusCode = code };
        }

        await next();
    }
}
