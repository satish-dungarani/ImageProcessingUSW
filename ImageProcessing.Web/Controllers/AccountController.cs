using ImageProcessing.Data.Interface;
using ImageProcessing.Services;
using ImageProcessing.Web.Helpers;
using ImageProcessing.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageProcessing.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signin()
        {
            return View(new UserModel());
        }
        public IActionResult Register()
        {
            return View(new UserModel());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Signin(UserModel user)
        {
            var model = await _userService.GetUserAsync();
            bool result = model.Where(x => x.Password == SecurityHelper.Encrypt(user.Password) && x.Email == user.Email).Any();
            if (result)
            {

            }
            var pass = SecurityHelper.Encrypt(user.Password);
            var dys = SecurityHelper.Decrypt(pass);
            return View();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Register(UserModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(user);

                if (user.Password != user.ConfirmPassword)
                {
                    ModelState.AddModelError(nameof(user.ConfirmPassword), "Password are not matched.");
                    return View(user);
                }

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
                    return View(user);
            }
            catch (Exception ex)
            {
                LogHelper.logger.Error($"{nameof(AccountController)},{" - "},{nameof(Register)},{" - "},{ex.Message},{ex.InnerException},{ex.StackTrace}");
            }
            return View();
        }
    }
}
