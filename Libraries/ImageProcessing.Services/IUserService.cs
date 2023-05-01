using ImageProcessing.Data;
using ImageProcessing.Data.Entities;

namespace ImageProcessing.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUserAsync();
        Task<User> GetUserByIdAsync(int Id);
        Task<int> InsertUserAsync(User user);
        Task<int> InsertRequestsAuditAsync(RequestsAudit requestsAudit);
        Task<bool> CheckUser(string email, string password);
        Task<User> GetUserByEmailAndPassword(string email, string password);
        Task<IEnumerable<RequestsAudit>> GetUserRequestAuditAsync();
    }
}