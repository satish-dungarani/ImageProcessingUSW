using ImageProcessing.Data;
using ImageProcessing.Services;
using ImageProcessing.Web.Helpers;
using ImageProcessing.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace ImageProcessing.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            try
            {
                var users = _userService.GetUserAsync();
                return View(users);
            }
            catch (Exception ex)
            {
                LogHelper.logger.Error($"{nameof(UserController)},{" - "},{nameof(Index)},{" - "},{ex.Message},{ex.InnerException},{ex.StackTrace}");
            }
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetInt32("Id")));
                return View(new UserModel()
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
                });
            }
            catch (Exception ex)
            {
                LogHelper.logger.Error($"{nameof(UserController)},{" - "},{nameof(Profile)},{" - "},{ex.Message},{ex.InnerException},{ex.StackTrace}");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Profile(UserModel user)
        {
            try
            {
                var model = await _userService.GetUserAsync();
                var IsAlreadyRegistered = model.Where(x => x.Email == user.Email).Any();

                //Already register user checking ......
                if (IsAlreadyRegistered && _httpContextAccessor.HttpContext.Session.GetInt32("Id") == null)
                    ModelState.AddModelError(nameof(user.Email), "Email is already registered in system.");

                //Password matching .......
                if (user.Password != user.ConfirmPassword)
                    ModelState.AddModelError(nameof(user.ConfirmPassword), "Password and confirm password does not match.");

                //Checking for field validation in Model ......
                if (!ModelState.IsValid)
                    return View(user);

                if (user.ProfilePicture != null && user.ProfilePicture.Length > 0)
                {
                    var fileName = Path.GetFileName(user.ProfilePicture.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    await user.ProfilePicture.CopyToAsync(fileStream);
                    user.ProfilePictureUrl = "/images/" + fileName;
                }

                //Calling Insert service to register user in database........
                int result = await _userService.InsertUserAsync(new Data.User()
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    DOB = user.DOB,
                    Gender = user.Gender,
                    Password = SecurityHelper.Encrypt(user.Password),
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    //IsAdmin = f,
                    Image = user.ProfilePictureUrl

                });

                if (result > 0)
                    return Json(new { result = true, msg = "Profile Updated Successfully." });
            }
            catch (Exception ex)
            {
                LogHelper.logger.Error($"{nameof(UserController)},{" - "},{nameof(Profile)},{" - "},{ex.Message},{ex.InnerException},{ex.StackTrace}");
            }
            return Json(new { result = false, msg = "Something went wrong." });
        }
    }
}
