using Auction.Application.ProductServices;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace Auction.WebUI.ViewComponents
{
    [ViewComponent]
    public class ProductListViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public ProductListViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var productList = await _productService.GetAll();
            return View(productList.Result);
        }
    }
}
