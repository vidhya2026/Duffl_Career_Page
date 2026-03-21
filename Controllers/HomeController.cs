using Duffl_career.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Duffl_career.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Career_Detail()
        {
            return View();
        }
        public IActionResult Career()
        {
            return View();
        }
        
        public IActionResult Index()
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
