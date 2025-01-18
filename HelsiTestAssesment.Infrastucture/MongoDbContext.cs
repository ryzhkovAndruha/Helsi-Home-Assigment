using MongoDB.Driver;

namespace HelsiTestAssesment.Infrastucture;

public class MongoDbContext(IMongoDatabase mongoDatabase)
{
    public IMongoDatabase MongoDatabase { get; set; } = mongoDatabase;
}
