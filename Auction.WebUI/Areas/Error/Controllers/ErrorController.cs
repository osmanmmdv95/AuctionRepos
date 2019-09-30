using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Auction.WebUI.Controllers
{
    [Area("Error")]
    
    public class ErrorController : Controller
    {
        [Route("/Error/UnknownError")]
        public IActionResult UnknownError()
        {
            return View();
        }
    }
}