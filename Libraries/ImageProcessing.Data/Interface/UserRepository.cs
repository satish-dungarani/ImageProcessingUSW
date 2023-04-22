using ImageProcessing.Data.DataContext;
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

        private readonly ImageProcessingDBContext _dbContext;

        public UserRepository(ImageProcessingDBContext dbContext)
        {
            _dbContext = dbContext;
        }
         
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();    
        }
    }
}
