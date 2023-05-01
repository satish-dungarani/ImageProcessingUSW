using ImageProcessing.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ImageProcessing.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> Users()
        {
            return View();
        }
        public async Task<ActionResult> UserTrafficTracking()
        {
            return View();
        }
        public async Task<ActionResult> ImaageProcessingHistory()
        {
            return View();
        }
    }
}