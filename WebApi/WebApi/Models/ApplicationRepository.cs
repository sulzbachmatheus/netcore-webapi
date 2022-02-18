using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace WebApi.Models
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IApplicationContext _context;
        public ApplicationRepository(IApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Application>> GetAllApplications()
        {
            return await _context
                            .Applications
                            .Find(_ => true)
                            .ToListAsync();
        }
        public Task<Application> GetApplication(int id)
        {
            FilterDefinition<Application> filter = Builders<Application>.Filter.Eq(m => m.ApplicationId, id);

            return _context
                    .Applications
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
        public async Task Create(Application application)
        {
            await _context.Applications.InsertOneAsync(application);
        }
        public async Task<bool> Update(Application application)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Applications
                        .ReplaceOneAsync(
                            filter: g => g.ApplicationId == application.ApplicationId,
                            replacement: application);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> PartialUpdate(Application application)
        {
            var update = 
                Builders<Application>
                    .Update
                    .Set(p => p.Url, application.Url)
                    .Set(p => p.PathLocal, application.PathLocal)
                    .Set(p => p.ApplicationId, application.ApplicationId)
                    .Set(p => p.DebuggingMode, application.DebuggingMode);

            var updateResult = await _context.Applications.UpdateOneAsync(filter: g => g.ApplicationId == application.ApplicationId, update);
                        
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(long id)
        {
            FilterDefinition<Application> filter = Builders<Application>.Filter.Eq(m => m.ApplicationId, id);
            DeleteResult deleteResult = await _context
                                                .Applications
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public async Task<int> GetNextId()
        {
            return (int)await _context.Applications.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}
