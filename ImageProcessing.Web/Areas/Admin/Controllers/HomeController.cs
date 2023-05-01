using ImageProcessing.Data;
using ImageProcessing.Services;
using ImageProcessing.Web.Helpers;
using ImageProcessing.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ImageProcessing.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    public class HomeController : Controller
    {
        #region Pros

        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageProcessingService _imageProcessingService;

        #endregion

        #region Ctors
        public HomeController(IUserService userService, IHttpContextAccessor httpContextAccessor, IImageProcessingService imageProcessingService)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _imageProcessingService = imageProcessingService;
        }

        #endregion

        #region Methods

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Users()
        {
            var userlist = _userService.GetUserAsync().GetAwaiter().GetResult();
            return View(userlist.ToList());
        }

        public ActionResult UserTracker()
        {
            var tracklist = _userService.GetUserRequestAuditAsync().GetAwaiter().GetResult();
            return View(tracklist.ToList());
        }

        public async Task<ActionResult> History()
        {
            var list = await _imageProcessingService.GetHistoriesAsync();
            return View(list.ToList());
        }

        #endregion
    }
}