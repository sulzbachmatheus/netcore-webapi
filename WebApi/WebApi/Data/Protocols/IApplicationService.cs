using System;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data.Protocols
{
    public interface IApplicationService : IDisposable
    {
        Task<bool> Post(Application app);
    }
}
