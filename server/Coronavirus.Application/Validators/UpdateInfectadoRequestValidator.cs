using Coronavirus.Application.DTOs.Requests;
using FluentValidation;

namespace Coronavirus.Application.Validators;

public class UpdateInfectadoRequestValidator : AbstractValidator<UpdateInfectadoRequest>
{
    public UpdateInfectadoRequestValidator()
    {
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude inválida");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude inválida");
    }
}
