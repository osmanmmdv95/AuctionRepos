using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auction.Application.SubCategoryServices.Dtos;

namespace Auction.Application.SubCategoryServices
{
    public class SubCategoryService : ISubCategoryService
    {
        public Task<ApplicationResult<SubCategoryDto>> Create(CreateSubCategoryViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<SubCategoryDto>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<List<SubCategoryDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<SubCategoryDto>> Update(UpdateSubCategoryViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
