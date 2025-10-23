namespace Coronavirus.API.Contracts;

public class ApiResponse
{
    public bool Success { get; set; }
    public object? Data { get; set; }
    public string? Message { get; set; }
    public string? TraceId { get; set; }
    public object? Meta { get; set; }

    public static ApiResponse FromSuccess(object? data, string? message, string traceId, object? meta = null)
        => new ApiResponse { Success = true, Data = data, Message = message, TraceId = traceId, Meta = meta };
}
