using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        [Authorize]
        [Route("Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminProfile()
        {
            return View();
        }
    }
}