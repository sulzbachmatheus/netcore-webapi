using MongoDB.Bson;
using System.Threading.Tasks;
using WebApi.Data.Models.Validations;
using WebApi.Data.Protocols;
using WebApi.Models;

namespace WebApi.Services
{
    public class ApplicationService : BaseService, IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(
            IApplicationRepository applicationRepository,
            INotifier notifier) : base(notifier)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<bool> Post(Application app)
        {
            if (!ExecuteValidation(new ApplicationValidation(), app)) return false;
            if (_applicationRepository.GetApplication(app.ApplicationId).Result != null)
            {
                Notify("It already exists");
                return false;
            }

            await _applicationRepository.Create(app);
            return true;
        }

        public async Task<bool> Put(Application app, ObjectId internalId)
        {
            app.InternalId = internalId;

            if (!ExecuteValidation(new ApplicationValidation(), app)) return false;

            await _applicationRepository.Update(app);
            return true;
        }

        public async Task<bool> Patch(Application app, ObjectId internalId)
        {
            app.InternalId = internalId;

            if (!ExecuteValidation(new ApplicationValidation(), app)) return false;

            await _applicationRepository.PartialUpdate(app);
            return true;
        }

        
    }
}
