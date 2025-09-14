using MongoDB.Driver;

namespace to_do_minimal_api_mongodb.Infrastructure;

public class MongoDbService
{
    private readonly IMongoDatabase? _database;
    
    public MongoDbService(IConfiguration config)
    {
        var connectionString = config.GetConnectionString("MongoDb");

        var url = MongoUrl.Create(connectionString);
        var client = new MongoClient(url);
        _database = client.GetDatabase(url.DatabaseName);
    }
    
    public IMongoDatabase Database => _database;
}