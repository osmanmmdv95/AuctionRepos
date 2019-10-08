using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.Application.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.WebUI.Areas.Admin.Controllers
{


    [Authorize(Roles = "Admin,Editor")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        public AdminController(IProductService productService)
        {
            _productService = productService;
        }
               
        [Route("Admin")]
        public IActionResult Index()
        {
            return View();
        }
                
        [Route("Admin/AdminProfile")]
        public async Task<IActionResult> AdminProfile(string CreatedById)
        {
             CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var getAllService = await _productService.GetAllById(CreatedById);
            return View(getAllService.Result);
        }
    }
}