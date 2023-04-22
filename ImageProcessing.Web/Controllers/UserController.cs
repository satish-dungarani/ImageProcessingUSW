using ImageProcessing.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageProcessing.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetUserAsync();
            return View(users);
        }
    }
}
