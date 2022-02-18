using MongoDB.Driver;

namespace WebApi.Models
{    
    public interface IApplicationContext
    {
        IMongoCollection<Application> Applications { get; }
    }
}
