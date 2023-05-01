using ImageProcessing.Data;
using ImageProcessing.Data.Entities;
using ImageProcessing.Data.Interface;

namespace ImageProcessing.Services
{
    public class UserService : IUserService
    {
        #region Props
        private readonly IUserRepository _userRepository;
        #endregion

        #region Ctors
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region Methods
        public Task<IEnumerable<User>> GetUserAsync()
        {
            return _userRepository.GetUsersAsync();
        }
        public Task<User> GetUserByIdAsync(int Id)
        {
            return _userRepository.GetUserByIdAsync(Id);
        }
        public Task<int> InsertUserAsync(User user)
        {
            return _userRepository.InsertUserAsync(user);
        }
        public Task<int> InsertRequestsAuditAsync(RequestsAudit requestsAudit)
        {
            return _userRepository.InsertRequestsAuditAsync(requestsAudit);
        }
        public async Task<bool> CheckUser(string email,string password)
        {
            return await _userRepository.CheckUser(email, password) is not null;
        }
        public async Task<User> GetUserByEmailAndPassword(string email,string password)
        {
            return await _userRepository.CheckUser(email, password);
        }
        public async Task<IEnumerable<RequestsAudit>> GetUserRequestAuditAsync()
        {
            return await _userRepository.GetRequestsAuditsAsync();
        }
        #endregion

    }
}