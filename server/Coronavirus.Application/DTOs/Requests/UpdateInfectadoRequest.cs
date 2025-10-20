using Coronavirus.Domain.ValueObjects;

namespace Coronavirus.Application.DTOs.Requests;

public class UpdateInfectadoRequest
{
    public Sexo Sexo { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
