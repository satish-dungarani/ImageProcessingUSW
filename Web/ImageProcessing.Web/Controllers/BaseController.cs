using Microsoft.AspNetCore.Mvc;

namespace ImageProcessing.Web.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
