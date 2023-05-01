using ImageProcessing.Data;
using ImageProcessing.Services;
using ImageProcessing.Web.Helpers;
using ImageProcessing.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageProcessing.Web.Controllers
{
    public class ImageProcessingController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageProcessingService _imageProcessingService;

        public ImageProcessingController(IUserService userService, IHttpContextAccessor httpContextAccessor, IImageProcessingService imageProcessingService)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _imageProcessingService = imageProcessingService;
        }
        public IActionResult Index()
        {
            var model = new ImageProcessingModel();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(ImageProcessingModel model)
        {
            try
            {
                //Checking for field validation in Model ......
                if (!ModelState.IsValid)
                    return View(model);

                if (model.RequestedImage != null && model.RequestedImage.Length > 0)
                {
                    var fileName = DateTime.Now.Ticks + "_" + Path.GetFileName(model.RequestedImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images/requested", fileName);

                    bool exists = System.IO.Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "requested"));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "requested"));
                    exists = System.IO.Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "processed"));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "processed"));

                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    await model.RequestedImage.CopyToAsync(fileStream);
                    model.RequestedImageUrl = Path.Combine("images", "requested", fileName);
                    model.ProcessedImageUrl = Path.Combine("images", "processed", fileName);
                }
                TempData["ResizeSDone"] = await _imageProcessingService.ResizeImage(new Data.Entities.ImageProcessingHistory()
                {
                    Height = model.Height,
                    Width = model.Width,
                    IPQuality = model.IPQuality,
                    RequestedImageUrl = model.RequestedImageUrl,
                    ProcessedImageUrl = model.ProcessedImageUrl,
                    UserId = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetInt32("Id")),
                    CreatedOn = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                LogHelper.logger.Error($"{nameof(UserController)},{" - "},{nameof(Index)},{" - "},{ex.Message},{ex.InnerException},{ex.StackTrace}");
            }

            return View(model);
        }
    }
}
