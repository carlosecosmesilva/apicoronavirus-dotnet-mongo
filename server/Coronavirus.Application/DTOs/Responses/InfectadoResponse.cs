namespace Coronavirus.Application.DTOs.Responses;

public class InfectadoResponse
{
    public Guid Id { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Sexo { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime DataRegistro { get; set; }
    public DateTime? DataAtualizacao { get; set; }
}
