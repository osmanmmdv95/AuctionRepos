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
                CategoryDto mapCategory = _mapper.Map<CategoryDto>(category);

                return new ApplicationResult<CategoryDto>
                {
                    Result = mapCategory,
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
                List<Category> categoryList = await _context.Categories.ToListAsync();
                List<CategoryDto> mapCategory = _mapper.Map<List<CategoryDto>>(categoryList);

                return new ApplicationResult<List<CategoryDto>>
                {
                    Succeeded = true,
                    Result = mapCategory
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
            try
            {
                var willDelete = await _context.Categories.FindAsync(id);
                if (_context.Categories.Where(x => x.Id == id).Any())
                {
                    return new ApplicationResult { Succeeded = false, ErrorMessage = "Bu kategoriye bağlı alt kategoriler olduğundan silinemez!" };
                }
                if (willDelete != null)
                {
                    _context.Categories.Remove(willDelete);
                    await _context.SaveChangesAsync();
                    return new ApplicationResult { Succeeded = true };
                }
                return new ApplicationResult { Succeeded = false, ErrorMessage = "Bir hata oluştu lütfen kontrol edip tekrar deneyiniz" };
            }
            catch (Exception e)
            {
                return new ApplicationResult { Succeeded = false, ErrorMessage = e.Message };
            }
        }

        public async Task<ApplicationResult<CategoryDto>> Update(UpdateCategoryViewModel model)
        {
            try
            {
                var getExistCategory = await _context.Categories.FindAsync(model.Id);
                if (getExistCategory == null)
                {
                    return new ApplicationResult<CategoryDto>
                    {
                        Result = new CategoryDto(),
                        Succeeded = false,
                        ErrorMessage = "Böyle bir Kategori bulunamadı"
                    };
                }
                var modifierUser = await _userManager.FindByIdAsync(model.ModifiedById);
                getExistCategory.ModifiedBy = modifierUser.UserName;
                _mapper.Map(model, getExistCategory);
                _context.Update(getExistCategory);
                await _context.SaveChangesAsync();
                return await Get(getExistCategory.Id);

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
    }
}