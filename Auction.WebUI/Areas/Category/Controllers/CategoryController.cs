using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.Application;
using Auction.Application.CategoryServices.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.WebUI.Areas.Category.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    [Area("Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryservice _categoryService;
        public CategoryController(ICategoryservice categoryService)
        {
            _categoryService = categoryService;
        }


        [Route("Category")]
        public async Task<IActionResult> Index()
        {
            var getAllService = await _categoryService.GetAll();
            return View(getAllService.Result);
        }

        [Route("/Category/Details")]
        public async Task<IActionResult> Details(int id)
        {
            var getService = await _categoryService.Get(id);
            return View(getService.Result);
        }

        [Route("/Category/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Category/Create")]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = await _categoryService.Create(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, result.ErrorMessage);

            }
            return View(model);
        }

        [Route("/Category/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var getService = await _categoryService.Get(id);
            return View(getService.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Category/Delete/{id}")]
        public async Task<IActionResult> Delete(int id, CategoryDto model)
        {
            var errorReturnModel = model;
            if (ModelState.IsValid)
            {
                var deleteService = await _categoryService.Delete(id);
                if (deleteService.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, deleteService.ErrorMessage);
                ViewBag.DeleteError = deleteService.ErrorMessage;
            }
          return View(errorReturnModel);

        }

        [Route("/Category/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var getService = await _categoryService.Get(id);
            UpdateCategoryViewModel model = new UpdateCategoryViewModel
            {
                Id = getService.Result.Id,
                CreatedById = getService.Result.CreatedById,
                ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                CategoryName = getService.Result.CategoryName,
                CategoryUrlName = getService.Result.CategoryUrlName
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Category/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, UpdateCategoryViewModel model)
        {
            //Hata durumunda geri dönecek model
            var errorReturnModel = model;
            if (ModelState.IsValid)
            {
                if (id != model.Id)
                {
                    ModelState.AddModelError(string.Empty, "Bir hata oluştu!");
                }
                else
                {
                    var getService = await _categoryService.Get(id);
                    model.CreatedById = getService.Result.CreatedById;
                    model.Id = getService.Result.Id;
                    model.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var updateService = await _categoryService.Update(model);
                    if (updateService.Succeeded)
                    {
                        return RedirectToAction("Details", new { id });
                    }
                    ModelState.AddModelError(string.Empty, updateService.ErrorMessage);
                }

            }

            return View(errorReturnModel);
        }

    }
}