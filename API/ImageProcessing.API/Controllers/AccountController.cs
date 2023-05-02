using ImageProcessing.API.Models;
using ImageProcessing.Data;
using ImageProcessing.Services;
using ImageProcessing.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageProcessing.API.Controllers
{

    /// <summary>
    /// Account Controller - User Releated End-Points
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

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

        /// <summary>
        /// Get USer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [Authorize]
        [Route("Getuser")]
        [HttpGet]
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
            catch (Exception)
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Sign-in Action 
        /// </summary>
        /// <param name="signin"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Signin")]
        [HttpPost]
        public virtual async Task<IActionResult> Signin([FromBody] SigninModel signin)
        {

            var result = await _userService.GetUserByEmailAndPassword(signin.Email, signin.Password);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        /// <summary>
        /// Register User Action
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public virtual async Task<IActionResult> Register([FromBody] UserModel user)
        {
            var model = await _userService.GetUserAsync();
            var IsAlreadyRegistered = model.Where(x => x.Email == user.Email).Any();

            //Already register user checking ......
            if (IsAlreadyRegistered)
                ModelState.AddModelError(nameof(user.Email), "Email is already registered in system.");

            //Password matching .......
            if (user.Password != user.ConfirmPassword)
                ModelState.AddModelError(nameof(user.ConfirmPassword), "Password and confirm password does not match.");

            //Checking for field validation in Model ......
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            //Calling Insert service to register user in database........
            int result = await _userService.InsertUserAsync(new Data.User()
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Password = SecurityHelper.Encrypt(user.Password),
                CreatedOn = DateTime.Now,
                IsActive = true,
                IsAdmin = false
            });
            if (result > 0)
            {
                user.Id = result;

                return Ok(user);
            }
            else
                return BadRequest();
        }
    }
}
