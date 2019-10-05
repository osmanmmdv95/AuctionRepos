using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.Application;
using Auction.Application.BrandServices;
using Auction.Application.CityService;
using Auction.Application.ProductServices;
using Auction.Application.SubCategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Auction.WebUI.Areas.Product.Controllers
{

    [Area("Product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryservice _categoryService;
        private readonly IBrandService _brandService;
        private readonly ICityService _cityService;
        public ProductController(IProductService productService, ISubCategoryService subCategoryService, ICategoryservice categoryService, IBrandService brandService, ICityService cityService)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _cityService = cityService;
        }

        [Route("Product")]
        public async Task<IActionResult> Index()
        {
            var getAllService = await _productService.GetAll();
            return View(getAllService.Result);
        }

        [Route("Product/Details")]
        public async Task<IActionResult> Details(Guid id)
        {
            var getService = await _productService.Get(id);
            return View(getService.Result);
        }

        [Route("/Product/Create")]
        public async Task<IActionResult> Create()
        {
            var brandList = await _brandService.GetAll();
            ViewBag.brandDDL = brandList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.BrandName,
                Value = x.Id.ToString()

            });

            var cityList = await _cityService.GetAll();
            ViewBag.cityDDL = cityList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.CityName,
                Value = x.Id.ToString()

            });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Product/Create")]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            var returnErrorModel = model;
            if (ModelState.IsValid)
            {
                model.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = await _productService.Create(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(String.Empty, result.ErrorMessage);
            }

            var brandList = await _brandService.GetAll();
            ViewBag.brandDDL = brandList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.BrandName,
                Value = x.Id.ToString()

            });

            var cityList = await _cityService.GetAll();
            ViewBag.cityDDL = cityList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.CityName,
                Value = x.Id.ToString()

            });

            return View(returnErrorModel);
        }
    }
}