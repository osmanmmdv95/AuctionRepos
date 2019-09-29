using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Auction.Application.CategoryServices.CategoryViewModels;
using Auction.Domain.Category;
using Auction.Domain.Identity;
using Auction.EntityFramework.Context;
using Microsoft.AspNetCore.Identity;

namespace Auction.Application.CategoryServices
{
    public class CategoryService : ICategoryservice
    {
        private readonly ApplicationUserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoryService(ApplicationUserDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        public async Task<ApplicationResult<CategoryDto>> Get(int id)
        {
            try
            {
                Category category = await _context.Categories.FindAsync(id);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Task<ApplicationResult<List<CategoryDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<CategoryDto>> Create(CreateCategoryViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult<CategoryDto>> Update(UpdateCategoryViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
