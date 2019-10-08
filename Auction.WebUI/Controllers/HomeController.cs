using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Auction.WebUI.Models;
using Auction.Application.ProductServices;
using System.Security.Claims;

namespace Auction.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
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

        public async Task<IActionResult> Profile(string CreatedById)
        {
            CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var getAllService = await _productService.GetAllById(CreatedById);
            return View(getAllService.Result);
        }
    }
}
