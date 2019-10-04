using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.Application;
using Auction.Application.SubCategoryServices;
using Auction.Application.SubCategoryServices.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Auction.WebUI.Areas.SubCategory.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    [Area("SubCategory")]
    public class SubCategoryController : Controller
    {

        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryservice _categoryService;

        public SubCategoryController(ISubCategoryService subCategoryService, ICategoryservice categoryService)
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        [Route("SubCategory")]
        public async Task<IActionResult> Index()
        {
            var getAllService = await _subCategoryService.GetAll();
            return View(getAllService.Result);
        }

        [Route("/SubCategory/Details")]
        public async Task<IActionResult> Details(Guid id)
        {
            var getService = await _subCategoryService.Get(id);
            return View(getService.Result);
        }

        [Route("/SubCategory/Create")]
        public async Task<IActionResult> Create()
        {
            var categoryList = await _categoryService.GetAll();
            ViewBag.CategoryDDL = categoryList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.CategoryName,
                Value = x.Id.ToString()
            }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/SubCategory/Create")]
        public async Task<IActionResult> Create(CreateSubCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = await _subCategoryService.Create(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, result.ErrorMessage);

            }
            var categoryList = await _categoryService.GetAll();
            ViewBag.CategoryDDL = categoryList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.CategoryName,
                Value = x.Id.ToString()
            }).ToList();
            return View(model);
        }

        [Route("/SubCategory/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var getService = await _subCategoryService.Get(id);
            return View(getService.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/SubCategory/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id, SubCategoryDto model)
        {
            var errorReturnModel = model;
            if (ModelState.IsValid & id == model.Id)
            {
                var deleteService = await _subCategoryService.Delete(id);
                if (deleteService.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, deleteService.ErrorMessage);

            }

            return View(errorReturnModel);
        }

        [Route("/SubCategory/Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var getService = await _subCategoryService.Get(id);

            var categoryList = await _categoryService.GetAll();
            ViewBag.CategoryDDL = categoryList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.CategoryName,
                Value = x.Id.ToString()
            }).ToList();

            UpdateSubCategoryViewModel model = new UpdateSubCategoryViewModel
            {
                Id = getService.Result.Id,
                CreatedById = getService.Result.CreatedById,
                ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                SubCategoryName = getService.Result.SubCategoryName,
                SubCategoryUrlName = getService.Result.SubCategoryName,
                CategoryId = getService.Result.CategoryId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/SubCategory/Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, UpdateSubCategoryViewModel model)
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
                    var getService = await _subCategoryService.Get(id);
                    model.CreatedById = getService.Result.CreatedById;
                    model.Id = getService.Result.Id;
                    model.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var updateService = await _subCategoryService.Update(model);
                    if (updateService.Succeeded)
                    {
                        return RedirectToAction("Details", new { id });
                    }
                    ModelState.AddModelError(string.Empty, updateService.ErrorMessage);
                }

            }
            var categoryList = await _categoryService.GetAll();
            ViewBag.CategoryDDL = categoryList.Result.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.CategoryName,
                Value = x.Id.ToString()
            }).ToList();

            return View(errorReturnModel);
        }
    }

}