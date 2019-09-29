using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.Application.BrandServices.Dtos;
using Auction.Application.CategoryServices.CategoryViewModels;

namespace Auction.Application.BrandServices
{
   public interface IBrandService
    {
        Task<ApplicationResult<BrandDto>> Get(int id);
        Task<ApplicationResult<List<BrandDto>>> GetAll();
        Task<ApplicationResult<BrandDto>> Create(CreateBrandViewModel model);
        Task<ApplicationResult<BrandDto>> Update(UpdateBrandViewModel model);
        Task<ApplicationResult> Delete(int id);
    }

}
