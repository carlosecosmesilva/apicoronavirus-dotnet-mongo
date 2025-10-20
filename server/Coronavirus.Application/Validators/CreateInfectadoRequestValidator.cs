using Coronavirus.Application.DTOs.Requests;
using FluentValidation;

namespace Coronavirus.Application.Validators;

public class CreateInfectadoRequestValidator : AbstractValidator<CreateInfectadoRequest>
{
    public CreateInfectadoRequestValidator()
    {
        RuleFor(x => x.DataNascimento)
            .LessThan(DateTime.UtcNow)
            .WithMessage("Data de nascimento deve ser no passado");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude inválida");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude inválida");
    }
}
