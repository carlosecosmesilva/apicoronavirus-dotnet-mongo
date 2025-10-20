using Coronavirus.Domain.Entities;
using Coronavirus.Domain.Interfaces;
using Coronavirus.Domain.ValueObjects;
using Coronavirus.Infrastructure.Data.Context;
using MongoDB.Driver;

namespace Coronavirus.Infrastructure.Data.Repositories;

public class InfectadoRepository : IInfectadoRepository
{
    private readonly IMongoCollection<Infectado> _collection;

    public InfectadoRepository(MongoDbContext context)
    {
        _collection = context.GetCollection<Infectado>("infectados");
    }

    public async Task<Infectado> ObterPorIdAsync(Guid id)
    {
        return await _collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Infectado>> ObterTodosAsync()
    {
        return await _collection
            .Find(_ => true)
            .ToListAsync();
    }

    public async Task<IEnumerable<Infectado>> ObterPorFiltroAsync(
        DateTime? dataInicio, DateTime? dataFim, Sexo? sexo)
    {
        var filterBuilder = Builders<Infectado>.Filter;
        var filters = new List<FilterDefinition<Infectado>>();

        if (dataInicio.HasValue)
            filters.Add(filterBuilder.Gte(x => x.DataNascimento, dataInicio.Value));

        if (dataFim.HasValue)
            filters.Add(filterBuilder.Lte(x => x.DataNascimento, dataFim.Value));

        if (sexo.HasValue)
            filters.Add(filterBuilder.Eq(x => x.Sexo, sexo.Value));

        var filter = filters.Any()
            ? filterBuilder.And(filters)
            : filterBuilder.Empty;

        return await _collection
            .Find(filter)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Infectado infectado)
    {
        await _collection.InsertOneAsync(infectado);
    }

    public async Task AtualizarAsync(Infectado infectado)
    {
        await _collection.ReplaceOneAsync(
            x => x.Id == infectado.Id,
            infectado
        );
    }

    public async Task RemoverAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}