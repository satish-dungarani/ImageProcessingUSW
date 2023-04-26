using ImageProcessing.Data;

namespace ImageProcessing.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUserAsync();
        Task<int> InsertUserAsync(User user);
    }
}