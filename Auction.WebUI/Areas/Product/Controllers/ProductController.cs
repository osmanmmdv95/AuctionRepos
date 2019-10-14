using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.Application;
using Auction.Application.BrandServices;
using Auction.Application.BrandServices.Dtos;
using Auction.Application.CityService;
using Auction.Application.ProductServices;
using Auction.Application.ProductServices.Dtos;
using Auction.Application.Shared;
using Auction.Application.SubCategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace Auction.WebUI.Areas.Product.Controllers
{

    [Area("Product")]
    public class ProductController : Controller
    {
        #region CTOR
        private readonly IProductService _productService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryservice _categoryService;
        private readonly IBrandService _brandService;
        private readonly ICityService _cityService;
        private readonly IFileProvider _fileProvider;
        private readonly IHostingEnvironment _env;
        public ProductController(IProductService productService, ISubCategoryService subCategoryService, ICategoryservice categoryService, IBrandService brandService, ICityService cityService, IFileProvider fileProvider, IHostingEnvironment env)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _cityService = cityService;
            _fileProvider = fileProvider;
            _env = env;
        }
        #endregion

        #region GetAll(Index)
        [Route("Product")]
        public async Task<IActionResult> Index()
        {
            var getAllService = await _productService.GetAll();
            return View(getAllService.Result);
        }
        #endregion

        #region Get (Details)
        [Route("Product/Details")]
        public async Task<IActionResult> Details(Guid id, string returnUrl = "")
        {
            ViewBag.ReturnUrl = string.IsNullOrEmpty(returnUrl) ? "" : returnUrl;
            var getService = await _productService.Get(id);
            return View(getService.Result);
        }
        #endregion

        #region Create
        [Route("Product/Create")]
        public async Task<IActionResult> Create(string returnUrl = "")
        {
            ViewBag.ReturnUrl = string.IsNullOrEmpty(returnUrl) ? "" : returnUrl;
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
        public async Task<IActionResult> Create(CreateProductViewModel model, IFormFile file, string returnUrl = "")
        {
            var returnErrorModel = model;
            model.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (file != null)
            {

                FileInfo fi = new FileInfo(file.FileName);

                var newFileName = model.CreatedById.ToString() + "_" + String.Format("{0:d}", (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;

                var webPath = _env.WebRootPath;
                var path = Path.Combine("", webPath + @"\images\" + newFileName);
                var pathToSave = @"/images/" + newFileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                }
                model.ProductImageUrl = pathToSave;
            }
            else
            {
                model.ProductImageUrl = "https://api.cobalt.com/social/1.0/image/caravatar.png";
            }

            if (ModelState.IsValid)
            {
                var result = await _productService.Create(model);
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
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
        #endregion

        #region Delete
        [Route("Product/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id, string returnUrl = "")
        {
            ViewBag.ReturnUrl = string.IsNullOrEmpty(returnUrl) ? "" : returnUrl;
            var getService = await _productService.Get(id);
            return View(getService.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Product/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id, ProductDto model, string returnUrl = "")
        {
            var errorReturnModel = model;
            if (ModelState.IsValid && id == model.Id)
            {
                var deleteService = await _productService.Delete(model.Id);
                if (deleteService.Succeeded)
                {
                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, deleteService.ErrorMessage);
            }
            return View(errorReturnModel);
        }
        #endregion

        #region Edit (Update)
        [Route("Product/Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, string returnUrl = "")
        {
            ViewBag.ReturnUrl = string.IsNullOrEmpty(returnUrl) ? "" : returnUrl;
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
        public async Task<IActionResult> Edit(Guid id, UpdateProductViewModel model, IFormFile file, string returnUrl = "")
        {
            //Hata olursa geri dönecek model
            var errorReturnModel = model;
            var getService = await _productService.Get(id);
            model.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (file != null)
            {

                FileInfo fi = new FileInfo(file.FileName);

                var newFileName = model.CreatedById.ToString() + "_" + String.Format("{0:d}", (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;

                var webPath = _env.WebRootPath;
                var path = Path.Combine("", webPath + @"\images\" + newFileName);
                var pathToSave = @"/images/" + newFileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                }
                model.ProductImageUrl = pathToSave;
            }
            else
            {
                model.ProductImageUrl = getService.Result.ProductImageUrl;
            }

            if (ModelState.IsValid && id == model.Id)
            {

                model.CreatedById = getService.Result.CreatedById;
                model.Id = getService.Result.Id;
                var updateService = await _productService.Update(model);
                if (updateService.Succeeded)
                {
                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
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
        #endregion



        #region GetByFilter



        public async Task<IActionResult> GetByFilter(FilterViewModel model)
        {
            var category = await _categoryService.GetAll();
            ViewBag.Category = category.Result.Select(c => new SelectListItem
            {
                Selected = false,
                Text = c.CategoryName,
                Value = c.Id.ToString()
            }).ToList();

            FilterViewModel filterViewModel = new FilterViewModel()
            {
                BrandName = model.BrandName,
                Brand = model.Brand,
                ProductBrandId = model.ProductBrandId,
                SubCategoryName = model.SubCategoryName,
                SubCategory = model.SubCategory,
                SubCategoryId = model.SubCategoryId,
                Category = model.Category,
                CategoryName = model.CategoryName,
                CategoryId = model.CategoryId
            };

            //var getService = await _productService.GetByFilter(filterViewModel);
            return View(filterViewModel);
        }


        #endregion
    }
}