using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PremiumUnit.Models;

namespace PremiumUnit.Controllers
{
    public class ReceiveListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
