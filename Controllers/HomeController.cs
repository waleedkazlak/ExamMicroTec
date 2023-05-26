using ExamMicroTec.Models;
using ExamMicroTec.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamMicroTec.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _accountService;

        public HomeController(ILogger<HomeController> logger , IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            var model = _accountService.PrepareAccountReport();
            return View(model);
        }


        public IActionResult AccountDetails(string ID)
        {
            var model = _accountService.PrepareAccountDetails(ID);
            return Ok(model);
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