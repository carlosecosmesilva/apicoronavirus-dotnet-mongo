using Coronavirus.Domain.ValueObjects;

namespace Coronavirus.Application.DTOs.Requests;

public class CreateInfectadoRequest
{
    public DateTime DataNascimento { get; set; }
    public Sexo Sexo { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
