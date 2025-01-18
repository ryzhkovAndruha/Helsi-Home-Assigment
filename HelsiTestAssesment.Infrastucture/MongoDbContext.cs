using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelsiTestAssesment.Infrastucture;

public class MongoDbContext(IMongoDatabase mongoDatabase)
{
    public IMongoDatabase MongoDatabase { get; set; } = mongoDatabase;
}
