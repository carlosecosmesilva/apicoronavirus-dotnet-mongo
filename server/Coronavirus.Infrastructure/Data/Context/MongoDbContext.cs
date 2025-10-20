using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using Coronavirus.Domain.Entities;
using Coronavirus.Infrastructure.Configuration;

namespace Coronavirus.Infrastructure.Data.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
        ConfigureMappings();
    }

    public IMongoCollection<T> GetCollection<T>(string name)
        => _database.GetCollection<T>(name);

    private void ConfigureMappings()
    {
        // Configurações de mapeamento
        BsonClassMap.RegisterClassMap<Infectado>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(c => c.Id);
            cm.MapProperty(c => c.DataNascimento);
            // etc...
        });
    }
}