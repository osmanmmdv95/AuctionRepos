using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class AdminController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}