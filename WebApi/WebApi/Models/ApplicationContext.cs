using MongoDB.Driver;
using WebApi.Config;

namespace WebApi.Models
{   

    public class ApplicationContext : IApplicationContext
    {
        private readonly IMongoDatabase _db;
        public ApplicationContext(MongoDbConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<Application> Applications => _db.GetCollection<Application>("Applications");
    }
}
