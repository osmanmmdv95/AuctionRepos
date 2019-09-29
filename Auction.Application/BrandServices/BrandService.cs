using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.Application.BrandServices.Dtos;

namespace Auction.Application.BrandServices
{
    public class BrandService : IBrandService
    {
        public Task<ApplicationResult<BrandDto>> Create(CreateBrandViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<BrandDto>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<List<BrandDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<BrandDto>> Update(UpdateBrandViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
