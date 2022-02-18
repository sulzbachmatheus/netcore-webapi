using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Dto;

namespace WebApi.Models
{
    
    public interface IApplicationRepository
    {       
        Task<IEnumerable<Application>> GetAllApplications();       
        Task<Application> GetApplication(int id);        
        Task Create(Application app);   
        Task<bool> Update(Application app);
        Task<bool> PartialUpdate(Application app);
        Task<bool> Delete(long id);
        Task<int> GetNextId();
    }
}
