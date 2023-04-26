using ImageProcessing.Data;
using ImageProcessing.Data.Interface;

namespace ImageProcessing.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public Task<IEnumerable<User>> GetUserAsync()
        {
           return _userRepository.GetUsersAsync();
        }
        public Task<int> InsertUserAsync(User user)
        {
           return _userRepository.InsertUserAsync(user);
        }
    }
}