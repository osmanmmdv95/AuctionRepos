using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Application.ProductServices;
using Auction.Application.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Auction.WebUI.ViewComponents
{
    [ViewComponent]
    public class GetProductsByFilterViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public GetProductsByFilterViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(FilterViewModel model)
        {
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
            var productList = await _productService.GetByFilter(filterViewModel);
            return View(productList.Result);
        }
    }

}
