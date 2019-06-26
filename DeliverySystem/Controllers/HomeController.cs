namespace DeliverySystem.Controllers
{
    #region Usings

    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using DeliverySystem.Models;

    #endregion

    public class HomeController : Controller
    {
        #region Methods

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error(int? statusCode)
        {
            var errorViewModel = new ErrorViewModel
            {
                Response = statusCode?.ToString() ?? "-",
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(errorViewModel);
        }

        #endregion
    }
}