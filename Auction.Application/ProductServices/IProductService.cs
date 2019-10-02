using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Application.ProductServices
{
    public interface IProductService
    {
        Task<ApplicationResult<ProductDto>> Get(int id);
        Task<ApplicationResult<List<ProductDto>>> GetAll();
        Task<ApplicationResult<ProductDto>> Create(CreateProductViewModel model);
        Task<ApplicationResult<ProductDto>> Update(UpdateProductViewModel model);
        Task<ApplicationResult> Delete(int id);
    }
}
