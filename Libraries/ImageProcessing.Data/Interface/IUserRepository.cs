using ImageProcessing.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Data.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int Id);
        Task<int> InsertUserAsync(User user);
        Task<int> InsertRequestsAuditAsync(RequestsAudit requestsAudit);
        Task<User> CheckUser(string email, string password);

        Task<IEnumerable<RequestsAudit>> GetRequestsAuditsAsync();
    }
}
