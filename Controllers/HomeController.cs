using BOXCricket.BAL;
using BOXCricket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace BOXCricket.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [CheckAccess]
        public IActionResult Index()
        {
            ViewBag.UserID = HttpContext.Session.GetString("UserID");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.ProfilePhotoPath = HttpContext.Session.GetString("ProfilePhotoPath");

            return View();
        }

        public IActionResult Table()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}