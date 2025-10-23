using Coronavirus.API.Middlewares;
using Coronavirus.Application;
using Coronavirus.Infrastructure.CrossCutting;
using Microsoft.OpenApi.Models;
using Coronavirus.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResponseWrappingFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Coronavirus API",
        Version = "v1",
        Description = "API para gerenciamento de dados de infectados"
    });
});

// Custom services
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Seu futuro client
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Middleware pipeline
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Redirect root to Swagger UI in development to avoid opening default localhost root
    app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
}

app.UseHttpsRedirection();
app.UseCors("AllowClient");
app.UseAuthorization();
app.MapControllers();

app.Run();