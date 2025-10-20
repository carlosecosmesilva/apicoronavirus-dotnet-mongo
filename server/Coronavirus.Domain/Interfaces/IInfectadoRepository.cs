using Coronavirus.Domain.Entities;
using Coronavirus.Domain.ValueObjects;
namespace Coronavirus.Domain.Interfaces;

public interface IInfectadoRepository
{
    Task<Infectado> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Infectado>> ObterTodosAsync();
    Task<IEnumerable<Infectado>> ObterPorFiltroAsync(
        DateTime? dataInicio, DateTime? dataFim, Sexo? sexo);
    Task AdicionarAsync(Infectado infectado);
    Task AtualizarAsync(Infectado infectado);
    Task RemoverAsync(Guid id);
}