using Microsoft.AspNetCore.Mvc;

namespace ImageProcessing.Web.Controllers
{
    public class AccountController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
