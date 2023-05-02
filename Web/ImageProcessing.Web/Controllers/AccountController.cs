using ImageProcessing.Data.Interface;
using ImageProcessing.Services;
using ImageProcessing.Web.Helpers;
using ImageProcessing.Web.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Licenses;
using System.Web;

namespace ImageProcessing.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signin()
        {
            if (_httpContextAccessor.HttpContext.Session.GetInt32("Id") > 0)
                return RedirectToAction("Index", "Home");
            else
                return View(new UserModel());
        }
        public IActionResult Register()
        {
            return View(new UserModel());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Signin(UserModel user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                {
                    return View(user);
                }

                var model = await _userService.GetUserAsync();
                var result = model.Where(x => x.Password == SecurityHelper.Encrypt(user.Password) && x.Email == user.Email).FirstOrDefault();
                if (result != null)
                {
                    _httpContextAccessor.HttpContext.Session.SetInt32(nameof(result.Id), result.Id);
                    _httpContextAccessor.HttpContext.Session.SetString(nameof(result.Firstname), result.Firstname);
                    _httpContextAccessor.HttpContext.Session.SetString(nameof(result.Lastname), value: result.Lastname);
                    _httpContextAccessor.HttpContext.Session.SetString(nameof(result.Email), result.Email);
                    _httpContextAccessor.HttpContext.Session.SetString(nameof(result.IsAdmin), result.IsAdmin.ToString());

                    //return RedirectToAction("Index", "Home");
                    return Json(new { result = true, msg = "Sign-in Successfully." });
                }
            }
            catch (Exception ex)
            {
                LogHelper.logger.Error($"{nameof(AccountController)},{" - "},{nameof(SignIn)},{" - "},{ex.Message},{ex.InnerException},{ex.StackTrace}");
            }
            return Json(new { result = false, msg = "Something went wrong." });

        }

        [HttpPost]
        public virtual async Task<IActionResult> Register(UserModel user)
        {
            try
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
                    return View(user);

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
                    return Json(new { result = true, msg = "User Registred Successfully." });
            }
            catch (Exception ex)
            {
                LogHelper.logger.Error($"{nameof(AccountController)},{" - "},{nameof(Register)},{" - "},{ex.Message},{ex.InnerException},{ex.StackTrace}");
            }
            return Json(new { result = false, msg = "Something went wrong." });
        }

        public virtual IActionResult Signout()
        {
            try
            {
                if (_httpContextAccessor.HttpContext.Session.GetInt32("Id") != null)
                {
                    _httpContextAccessor.HttpContext.Session.Clear();
                }
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                LogHelper.logger.Error($"{nameof(AccountController)},{" - "},{nameof(SignIn)},{" - "},{ex.Message},{ex.InnerException},{ex.StackTrace}");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
