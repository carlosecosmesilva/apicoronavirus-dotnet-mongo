using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Coronavirus.Domain.Interfaces;
using Coronavirus.Infrastructure.Configuration;
using Coronavirus.Infrastructure.Data.Context;
using Coronavirus.Infrastructure.Data.Repositories;

namespace Coronavirus.Infrastructure.CrossCutting;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        // MongoDB
        services.Configure<MongoDbSettings>(
            configuration.GetSection("MongoDb"));
        services.AddSingleton<MongoDbContext>();

        // Repositories
        services.AddScoped<IInfectadoRepository, InfectadoRepository>();

        return services;
    }
}