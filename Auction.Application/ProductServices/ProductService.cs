using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Application.ProductServices
{
    public class ProductService : IProductService
    {
        public Task<ApplicationResult<ProductDto>> Create(CreateProductViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<ProductDto>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<List<ProductDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<ProductDto>> Update(UpdateProductViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
