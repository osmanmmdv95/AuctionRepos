using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.Application.CategoryServices.Dtos;
using Auction.Application.SubCategoryServices.Dtos;

namespace Auction.Application.SubCategoryServices
{
    public interface ISubCategoryService
    {
        Task<ApplicationResult<SubCategoryDto>> Get(int id);
        Task<ApplicationResult<List<SubCategoryDto>>> GetAll();
        Task<ApplicationResult<SubCategoryDto>> Create(CreateSubCategoryViewModel model);
        Task<ApplicationResult<SubCategoryDto>> Update(UpdateSubCategoryViewModel model);
        Task<ApplicationResult> Delete(int id);
    }
}
