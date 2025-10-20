using AutoMapper;
using Coronavirus.Application.Interfaces;
using Coronavirus.Application.Mappings;
using Coronavirus.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Coronavirus.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(InfectadoProfile).Assembly);

        // Validators
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Services
        services.AddScoped<IInfectadoService, InfectadoService>();

        return services;
    }
}
