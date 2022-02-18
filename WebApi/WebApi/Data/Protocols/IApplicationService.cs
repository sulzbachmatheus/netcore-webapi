using MongoDB.Bson;
using System;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data.Protocols
{
    public interface IApplicationService
    {
        Task<bool> Post(Application app);
        Task<bool> Put(Application app, ObjectId internalId);
        Task<bool> Patch(Application app, ObjectId internalId);
    }
}
