using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Auction.WebUI.Models;
using Auction.Application.ProductServices;
using System.Security.Claims;
using Auction.Application;
using Auction.Application.SubCategoryServices;
using Auction.Application.BrandServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Auction.Domain.Category;
using Auction.WebUI.ViewModels.AjaxPost;
using Auction.Application.CategoryServices.Dtos;
using Auction.Application.SubCategoryServices.Dtos;

namespace Auction.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryservice _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IBrandService _brandService;
        public HomeController(IProductService productService, ICategoryservice categoryService, ISubCategoryService subCategoryService, IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            var category = await _categoryService.GetAll();
            ViewBag.Category = category.Result.Select(c => new SelectListItem
            {
                Selected = false,
                Text = c.CategoryName,
                Value = c.Id.ToString()
            }).ToList();
            return View();
        }


        public async Task<JsonResult> GetSubCategory([FromBody] CategoryDto Category)
        {
                var subCategory = await _subCategoryService.GetSubCategory(Category.Id);
           
                return Json(subCategory);
    
        }

        public async Task<JsonResult> GetBrand([FromBody] SubCategoryDto subCategory)
        {
            var brand = await _brandService.GetBrand(subCategory.Id);

            return Json(brand);
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

        public async Task<IActionResult> Profile(string createdById)
        {
            createdById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var getAllService = await _productService.GetAllById(createdById);
            return View(getAllService.Result);
        }
    }
}
