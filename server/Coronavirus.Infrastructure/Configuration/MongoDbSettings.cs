namespace Coronavirus.Infrastructure.Configuration;

/// <summary>
/// Configurações de conexão com MongoDB.
/// </summary>
public class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
}
