using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppAdminRequestRepository : IRepository<AdminRequests> {
        Task<object> AddRequest(Guid id);
        
     }
}