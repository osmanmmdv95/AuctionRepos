using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.Application.BrandServices.Dtos;

namespace Auction.Application.BrandServices
{
   public interface IBrandService
    {
        Task<ApplicationResult<BrandDto>> Get(Guid id);
        Task<ApplicationResult<BrandDto>> GetBrand(Guid id);
        Task<ApplicationResult<List<BrandDto>>> GetAll();
        Task<ApplicationResult<BrandDto>> Create(CreateBrandViewModel model);
        Task<ApplicationResult<BrandDto>> Update(UpdateBrandViewModel model);
        Task<ApplicationResult> Delete(Guid id);
    }

}
