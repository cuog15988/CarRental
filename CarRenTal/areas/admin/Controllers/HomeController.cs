using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarRenTal.areas.admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("admin")]

        public IActionResult Index()
        {
            return View();
        }

    }
}