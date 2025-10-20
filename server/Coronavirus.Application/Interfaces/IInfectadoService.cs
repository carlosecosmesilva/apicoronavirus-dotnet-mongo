using Coronavirus.Application.DTOs.Requests;
using Coronavirus.Application.DTOs.Responses;

namespace Coronavirus.Application.Interfaces;

public interface IInfectadoService
{
    Task<InfectadoResponse> CriarAsync(CreateInfectadoRequest request);
    Task<IEnumerable<InfectadoResponse>> ObterTodosAsync();
    Task<InfectadoResponse?> ObterPorIdAsync(Guid id);
    Task<InfectadoResponse> AtualizarAsync(Guid id, UpdateInfectadoRequest request);
    Task RemoverAsync(Guid id);
}
