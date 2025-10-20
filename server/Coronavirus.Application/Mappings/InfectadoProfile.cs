using AutoMapper;
using Coronavirus.Application.DTOs.Responses;
using Coronavirus.Domain.Entities;

namespace Coronavirus.Application.Mappings;

public class InfectadoProfile : Profile
{
    public InfectadoProfile()
    {
        CreateMap<Infectado, InfectadoResponse>()
            .ForMember(d => d.Sexo, opt => opt.MapFrom(s => s.Sexo.ToString()))
            .ForMember(d => d.Latitude, opt => opt.MapFrom(s => s.Localizacao.Latitude))
            .ForMember(d => d.Longitude, opt => opt.MapFrom(s => s.Localizacao.Longitude));
    }
}
