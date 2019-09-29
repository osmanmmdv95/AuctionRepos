using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.Application.CategoryServices.CategoryViewModels;

namespace Auction.Application.CategoryServices
{
    public interface ICategoryservice
    {
        Task<ApplicationResult<CategoryDto>> Get(int id);
        Task<ApplicationResult<List<CategoryDto>>> GetAll();
        Task<ApplicationResult<CategoryDto>> Create(CreateCategoryViewModel model);
        Task<ApplicationResult<CategoryDto>> Update(UpdateCategoryViewModel model);
        Task<ApplicationResult> Delete(int id);
    }
}
