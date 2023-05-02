using ImageProcessing.Data.DataContext;
using ImageProcessing.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Data.Interface
{
    public class UserRepository : IUserRepository
    {
        #region Properties
        private readonly ImageProcessingDBContext _dbContext;
        #endregion


        #region Ctor
        public UserRepository(ImageProcessingDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods

        // To Get User List
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var list =  await _dbContext.Users.ToListAsync();
            return list;
        }

        // To Get User By Id
        public async Task<User> GetUserByIdAsync(int Id)
        {
            User? model = await _dbContext.Users.FirstOrDefaultAsync(m => m.Id == Id);
            return model;
        }

        // To Get User Insert/Update User
        public async Task<int> InsertUserAsync(User user)
        {
            if (user.Id > 0)
            {
                var existingUser = _dbContext.Users.Find(user.Id);
                _dbContext.Entry(existingUser).CurrentValues.SetValues(user);
            }
            else
            {
                _dbContext.Users.Add(user);
            }
            return await _dbContext.SaveChangesAsync();
        }

        //To Save User Request Audit Records
        public async Task<int> InsertRequestsAuditAsync(RequestsAudit requestsAudit)
        {
            _dbContext.RequestsAudits.Add(requestsAudit);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RequestsAudit>> GetRequestsAuditsAsync()
        {
            var list = await _dbContext.RequestsAudits.ToListAsync();
            return list;
        }


        //To Check User
        public async Task<User> CheckUser(string email, string password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
        #endregion

    }
}
