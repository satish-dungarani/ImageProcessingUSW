using ImageProcessing.API.Models;
using ImageProcessing.Services;
using ImageProcessing.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImageProcessing.API.Controllers
{

    /// <summary>
    /// Account Controller - User Releated End-Points
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Proeerties
        /// </summary>
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        /// <summary>
        /// Constrocution
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="httpContextAccessor"></param>
        public AccountController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);

                var model = new UserModel()
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    Gender = user.Gender,
                    DOB = user.DOB,
                    Image = user.Image,
                    IsActive = user.IsActive,
                    IsAdmin = user.IsAdmin,
                    Password = SecurityHelper.Decrypt(user.Password),
                };
                return model;
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }

            return NoContent(); 
        }
    }
}
