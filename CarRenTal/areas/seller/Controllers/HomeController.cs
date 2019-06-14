using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarRenTal.Areas.seller.Controllers
{
    public class HomeController : BaseSellerController
    {
        [Area("seller")]
        public IActionResult Index()
        {
            return View();
        }
    }
}