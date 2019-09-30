using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Auction.Application.CategoryServices.Dtos;
using Auction.Domain.Category;
using Auction.Domain.Identity;
using Auction.EntityFramework.Context;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auction.Application.CategoryServices
{
    public class CategoryService : ICategoryservice
    {
        private readonly ApplicationUserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationUserDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }


        public async Task<ApplicationResult<CategoryDto>> Get(int Id)
        {
            try
            {
                Category category = await _context.Categories.FindAsync(Id);
                CategoryDto dto = new CategoryDto
                {
                    CreatedBy = category.CreatedBy,
                    CreatedById = category.CreatedById,
                    CreatedDate = category.CreatedDate,
                    Id = category.Id,
                    ModifiedBy = category.ModifiedBy,
                    ModifiedById = category.ModifiedById,
                    ModifiedDate = category.ModifiedDate,
                    CategoryName = category.CategoryName,
                    CategoryUrlName = category.CategoryUrlName

                };
                return new ApplicationResult<CategoryDto>
                {
                    Result = dto,
                    Succeeded = true
                };
            }
            catch (Exception e)
            {
                return new ApplicationResult<CategoryDto>
                {
                    Result = new CategoryDto(),
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }


        }

        public async Task<ApplicationResult<List<CategoryDto>>> GetAll()
        {
            try
            {
                //List<Category> CategoryList = await _context.Categories.ToListAsync();
                //List<CategoryDto> mapCategory = _mapper.Map<List<CategoryDto>>(CategoryList);

                List<CategoryDto> categoryList = await _context.Categories.Select(category => new CategoryDto
                {
                    CreatedBy = category.CreatedBy,
                    CreatedById = category.CreatedById,
                    CreatedDate = category.CreatedDate,
                    Id = category.Id,
                    ModifiedBy = category.ModifiedBy,
                    ModifiedById = category.ModifiedById,
                    ModifiedDate = category.ModifiedDate,
                    CategoryName = category.CategoryName,
                    CategoryUrlName = category.CategoryUrlName,

                    

            }).ToListAsync();

                return new ApplicationResult<List<CategoryDto>>
                {
                    Succeeded = true,
                    Result = categoryList
                };
            }
            catch (Exception e)
            {
                return new ApplicationResult<List<CategoryDto>>
                {
                    Succeeded = false,
                    ErrorMessage = e.Message,
                    Result = new List<CategoryDto>()
                };
            }
        }

        public async Task<ApplicationResult<CategoryDto>> Create(CreateCategoryViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.CreatedById);
                Category mapCategory = _mapper.Map<Category>(model);
                mapCategory.CreatedById = model.CreatedById;
                mapCategory.CreatedBy = user.UserName;
                _context.Categories.Add(mapCategory);
                await _context.SaveChangesAsync();
                ApplicationResult<CategoryDto> result = new ApplicationResult<CategoryDto>
                {
                    Result = _mapper.Map<CategoryDto>(mapCategory),
                    Succeeded = true
                };

                return result;

            }
            catch (Exception e)
            {
                ApplicationResult<CategoryDto> result = new ApplicationResult<CategoryDto>();
                result.Succeeded = false;
                result.ErrorMessage = e.Message;
                return result;
            }
        }

        public async Task<ApplicationResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationResult<CategoryDto>> Update(UpdateCategoryViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}