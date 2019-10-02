using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.Application;
using Auction.Application.BrandServices;
using Auction.Application.SubCategoryServices;
using Auction.Application.SubCategoryServices.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Auction.WebUI.Areas.Brand.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    [Area("Brand")]
    public class BrandController : Controller
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryservice _categoryService;
        private readonly IBrandService _brandService;

        public BrandController(ISubCategoryService subCategoryService, ICategoryservice categoryService,IBrandService brandService)
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        [Route("Brand")]
        public async Task<IActionResult> Index()
        {
            var getAllService = await _brandService.GetAll();
            return View(getAllService.Result);
        }

        [Route("/Brand/Details")]
        public async Task<IActionResult> Details(int id)
        {
            var getService = await _brandService.Get(id);
            return View(getService.Result);
        }


        [Route("/Brand/Create")]
        public async Task<IActionResult> Create()
        {
            var subCategoryList = await _subCategoryService.GetAll();
            ViewBag.subCategoryDDL = subCategoryList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.SubCategoryName,
                Value = x.Id.ToString()
            }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Brand/Create")]
        public async Task<IActionResult> Create(CreateBrandViewModel model)
        {
            var returnErrorModel = model;
            if (ModelState.IsValid)
            {
                model.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = await _brandService.Create(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, result.ErrorMessage);

            }
            var subCategoryList = await _subCategoryService.GetAll();
            ViewBag.subCategoryDDL = subCategoryList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.CategoryName,
                Value = x.Id.ToString()
            }).ToList();
            return View(returnErrorModel);
        }

        [Route("/Brand/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var getService = await _brandService.Get(id);
            return View(getService.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Brand/Delete/{id}")]
        public async Task<IActionResult> Delete(int id, BrandDto model)
        {
            var errorReturnModel = model;
            if (ModelState.IsValid & id == model.Id)
            {
                var deleteService = await _brandService.Delete(id);
                if (deleteService.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, deleteService.ErrorMessage);

            }

            return View(errorReturnModel);
        }


        [Route("/Brand/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var getService = await _brandService.Get(id);

            var subCategoryList = await _subCategoryService.GetAll();
            ViewBag.subCategoryDDL = subCategoryList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.SubCategoryName,
                Value = x.Id.ToString()
            }).ToList();

            UpdateBrandViewModel model = new UpdateBrandViewModel()
            {
                Id = getService.Result.Id,
                CreatedById = getService.Result.CreatedById,
                ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                BrandName = getService.Result.BrandName,
                BrandUrlName = getService.Result.BrandUrlName,
                SubCategoryId = getService.Result.SubCategoryId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Brand/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, UpdateBrandViewModel model)
        {
            //Hata olursa geri dönecek model
            var errorReturnModel = model;

            if (ModelState.IsValid)
            {
                if (id != model.Id)
                {
                    ModelState.AddModelError(string.Empty, "Bir hata oluştu!");
                }
                else
                {
                    var getService = await _brandService.Get(id);
                    model.CreatedById = getService.Result.CreatedById;
                    model.Id = getService.Result.Id;
                    model.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var updateService = await _brandService.Update(model);
                    if (updateService.Succeeded)
                    {
                        return RedirectToAction("Details", new { id });
                    }
                    ModelState.AddModelError(string.Empty, updateService.ErrorMessage);
                }

            }
            var subCategoryList = await _subCategoryService.GetAll();
            ViewBag.subCategoryDDL = subCategoryList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.SubCategoryName,
                Value = x.Id.ToString()
            }).ToList();

            return View(errorReturnModel);
        }
    }
}