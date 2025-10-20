using Coronavirus.Domain.Exceptions;

namespace Coronavirus.Domain.ValueObjects;

public class Localizacao : ValueObject
{
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public Localizacao(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
            throw new DomainException("Latitude inválida");
        if (longitude < -180 || longitude > 180)
            throw new DomainException("Longitude inválida");

        Latitude = latitude;
        Longitude = longitude;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}