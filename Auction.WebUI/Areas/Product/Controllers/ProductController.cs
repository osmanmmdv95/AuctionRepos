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

        [Route("Product/Create")]
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
        [Route("Product/Create")]
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

            }).ToList();

            var cityList = await _cityService.GetAll();
            ViewBag.cityDDL = cityList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.CityName,
                Value = x.Id.ToString()

            }).ToList();

            return View(returnErrorModel);
        }

        [Route("Product/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var getService = await _productService.Get(id);
            return View(getService.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Product/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id, ProductDto model)
        {
            var errorReturnModel = model;
            if (ModelState.IsValid && id == model.Id)
            {
                var deleteService = await _productService.Delete(model.Id);
                if (deleteService.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, deleteService.ErrorMessage);
            }
            return View(errorReturnModel);
        }

        [Route("Product/Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var getService = await _productService.Get(id);

            UpdateProductViewModel model = new UpdateProductViewModel()
            {
                Id = getService.Result.Id,
                ProductName = getService.Result.ProductName,
                CreatedById = getService.Result.CreatedById,
                ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                ProductDetail = getService.Result.ProductDetail,
                ProductFuelType = getService.Result.ProductFuelType,
                ProductGearType = getService.Result.ProductGearType,
                ProductImageUrl = getService.Result.ProductImageUrl,
                ProductMinPrice = getService.Result.ProductMinPrice,
                ProductPrice = getService.Result.ProductPrice,
                ProductKm = getService.Result.ProductKm,
                ProductYear = getService.Result.ProductYear,
                ProductCityId = getService.Result.ProductCityId,
                ProductBrandId = getService.Result.ProductBrandId

            };


            var brandList = await _brandService.GetAll();
            ViewBag.brandDDL = brandList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.BrandName,
                Value = x.Id.ToString()

            }).ToList();

            var cityList = await _cityService.GetAll();
            ViewBag.cityDDL = cityList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.CityName,
                Value = x.Id.ToString()

            }).ToList();


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Product/Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, UpdateProductViewModel model)
        {
            //Hata olursa geri dönecek model
            var errorReturnModel = model;
            if (ModelState.IsValid && id == model.Id)
            {
                var getService = await _productService.Get(id);
                model.CreatedById = getService.Result.CreatedById;
                model.Id = getService.Result.Id;
                model.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var updateService = await _productService.Update(model);
                if (updateService.Succeeded)
                {
                    return RedirectToAction("Details", new { id });
                }
                ModelState.AddModelError(string.Empty, updateService.ErrorMessage);
            }
            var brandList = await _brandService.GetAll();
            ViewBag.brandDDL = brandList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.BrandName,
                Value = x.Id.ToString()

            }).ToList();

            var cityList = await _cityService.GetAll();
            ViewBag.cityDDL = cityList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.CityName,
                Value = x.Id.ToString()

            }).ToList();


            return View(errorReturnModel);
        }
    }
}