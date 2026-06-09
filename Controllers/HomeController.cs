using Citas_App.Interfaces.Models;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CitasApp.Web.Models;

namespace Citas_App.Controllers
{
    public class HomeController : Controller
    {
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
            return View(new Interfaces.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
