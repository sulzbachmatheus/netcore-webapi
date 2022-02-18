using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApi.Models
{
    
    public class Application
    {       
        [BsonId]
        public ObjectId InternalId { get; set; }
        public int ApplicationId { get; set; }
        public string Url { get; set; }
        public string PathLocal { get; set; }
        public bool DebuggingMode { get; set; }
    }
}
