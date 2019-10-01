using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Application.SubCategoryServices.Dtos;
using Auction.Domain.Category;
using Auction.Domain.Identity;
using Auction.EntityFramework.Context;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auction.Application.SubCategoryServices
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ApplicationUserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public SubCategoryService(ApplicationUserDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApplicationResult<SubCategoryDto>> Get(int id)
        {
            try
            {
                SubCategory subCategory = await _context.SubCategories.FindAsync(id);
                SubCategoryDto mapSubCategory = _mapper.Map<SubCategoryDto>(subCategory);

                return new ApplicationResult<SubCategoryDto>
                {
                    Result = mapSubCategory,
                    Succeeded = true
                };
            }
            catch (Exception e)
            {
                return new ApplicationResult<SubCategoryDto>
                {
                    Result = new SubCategoryDto(),
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }
        }
        public async Task<ApplicationResult<List<SubCategoryDto>>> GetAll()
        {
            try
            {
                List<SubCategory> SubCategoryList = await _context.SubCategories.ToListAsync();
                List<SubCategoryDto> mapSubCategory = _mapper.Map<List<SubCategoryDto>>(SubCategoryList);

                return new ApplicationResult<List<SubCategoryDto>>
                {
                    Succeeded = true,
                    Result = mapSubCategory
                };
            }
            catch (Exception e)
            {
                return new ApplicationResult<List<SubCategoryDto>>
                {
                    Succeeded = false,
                    ErrorMessage = e.Message,
                    Result = new List<SubCategoryDto>()
                };
            }
        }
        public async Task<ApplicationResult<SubCategoryDto>> Create(CreateSubCategoryViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.CreatedById);
                SubCategory mapSubCategory = _mapper.Map<SubCategory>(model);
                mapSubCategory.CreatedById = model.CreatedById;
                mapSubCategory.CreatedBy = user.UserName;
                mapSubCategory.CategoryId = model.CategoryId;
                _context.SubCategories.Add(mapSubCategory);
                await _context.SaveChangesAsync();
                ApplicationResult<SubCategoryDto> result = new ApplicationResult<SubCategoryDto>
                {
                    Result = _mapper.Map<SubCategoryDto>(mapSubCategory),
                    Succeeded = true
                };

                return result;

            }
            catch (Exception e)
            {
                ApplicationResult<SubCategoryDto> result = new ApplicationResult<SubCategoryDto>();
                result.Succeeded = false;
                result.ErrorMessage = e.Message;
                return result;
            }

        }

        public async Task<ApplicationResult> Delete(int id)
        {
            try
            {
                var willDelete = await _context.SubCategories.FindAsync(id);
              
                if (willDelete != null)
                {
                    _context.SubCategories.Remove(willDelete);
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

       



        public async Task<ApplicationResult<SubCategoryDto>> Update(UpdateSubCategoryViewModel model)
        {
            try
            {
                var getExistSubCategory = await _context.SubCategories.FindAsync(model.Id);
                if (getExistSubCategory == null)
                {
                    return new ApplicationResult<SubCategoryDto>
                    {
                        Result = new SubCategoryDto(),
                        Succeeded = false,
                        ErrorMessage = "Böyle bir Kategori bulunamadı"
                    };
                }
                var modifierUser = await _userManager.FindByIdAsync(model.ModifiedById);
                getExistSubCategory.ModifiedBy = modifierUser.UserName;
                _mapper.Map(model, getExistSubCategory);
                _context.Update(getExistSubCategory);
                await _context.SaveChangesAsync();
                return await Get(getExistSubCategory.Id);

            }
            catch (Exception e)
            {
                return new ApplicationResult<SubCategoryDto>
                {
                    Result = new SubCategoryDto(),
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}
