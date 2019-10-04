using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Application.BrandServices.Dtos;
using Auction.Domain.Category;
using Auction.Domain.Identity;
using Auction.EntityFramework.Context;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auction.Application.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationUserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public BrandService(ApplicationUserDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApplicationResult<BrandDto>> Get(Guid id)
        {
            try
            {
                //Brand brand = await _context.Brands.FindAsync(id);
                //BrandDto mapBrand = _mapper.Map<BrandDto>(brand);

                var brandList = await (from s in _context.Brands
                                       join c in _context.SubCategories on s.SubCategoryId equals c.Id
                                       select new
                                       {

                                           c.SubCategoryName,
                                           s.SubCategoryId,
                                           s.Id,
                                           s.BrandName,
                                           s.BrandUrlName,
                                           s.CreatedBy,
                                           s.CreatedById,
                                           s.CreatedDate,
                                           s.ModifiedBy,
                                           s.ModifiedById,
                                           s.ModifiedDate

                                       }).ToListAsync();
                var selectBrand = brandList.Where(x => x.Id == id).FirstOrDefault();
                BrandDto mapBrand = new BrandDto();

                mapBrand = new BrandDto
                {

                    SubCategoryName = selectBrand.SubCategoryName,
                    SubCategoryId = selectBrand.SubCategoryId,
                    Id = selectBrand.Id,
                    BrandName = selectBrand.BrandName,
                    BrandUrlName = selectBrand.BrandUrlName,
                    CreatedBy = selectBrand.CreatedBy,
                    CreatedById = selectBrand.CreatedById,
                    CreatedDate = selectBrand.CreatedDate,
                    ModifiedBy = selectBrand.ModifiedBy,
                    ModifiedById = selectBrand.ModifiedById,
                    ModifiedDate = selectBrand.ModifiedDate

                };

                return new ApplicationResult<BrandDto>
                {
                    Result = mapBrand,
                    Succeeded = true
                };
            }
            catch (Exception e)
            {
                return new ApplicationResult<BrandDto>
                {
                    Result = new BrandDto(),
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<ApplicationResult<List<BrandDto>>> GetAll()
        {
            try
            {

                var brandList = await (from s in _context.Brands
                                       join c in _context.SubCategories on s.SubCategoryId equals c.Id
                                       select new
                                       {

                                           c.SubCategoryName,
                                           s.SubCategoryId,
                                           s.Id,
                                           s.BrandName,
                                           s.BrandUrlName,
                                           s.CreatedBy,
                                           s.CreatedById,
                                           s.CreatedDate,
                                           s.ModifiedBy,
                                           s.ModifiedById,
                                           s.ModifiedDate

                                       }).ToListAsync();

                List<BrandDto> listBrand = new List<BrandDto>();
                foreach (var item in brandList)
                {
                    BrandDto mapBrand = new BrandDto
                    {

                        SubCategoryName = item.SubCategoryName,
                        SubCategoryId = item.SubCategoryId,
                        Id = item.Id,
                        BrandName = item.BrandName,
                        BrandUrlName = item.BrandUrlName,
                        CreatedBy = item.CreatedBy,
                        CreatedById = item.CreatedById,
                        CreatedDate = item.CreatedDate,
                        ModifiedBy = item.ModifiedBy,
                        ModifiedById = item.ModifiedById,
                        ModifiedDate = item.ModifiedDate

                    };
                    listBrand.Add(mapBrand);
                }

                return new ApplicationResult<List<BrandDto>>
                {
                    Succeeded = true,
                    Result = listBrand
                };

            }
            catch (Exception e)
            {
                return new ApplicationResult<List<BrandDto>>
                {
                    Succeeded = false,
                    ErrorMessage = e.Message,
                    Result = new List<BrandDto>()
                };
            }
        }

        public async Task<ApplicationResult<BrandDto>> Create(CreateBrandViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.CreatedById);
                Brand mapBrand = _mapper.Map<Brand>(model);
                mapBrand.CreatedById = model.CreatedById;
                mapBrand.CreatedBy = user.UserName;
                mapBrand.SubCategoryId = model.SubCategoryId;
                _context.Brands.Add(mapBrand);
                await _context.SaveChangesAsync();

                ApplicationResult<BrandDto> result = new ApplicationResult<BrandDto>
                {
                    Result = _mapper.Map<BrandDto>(mapBrand),
                    Succeeded = true
                };

                return result;

            }
            catch (Exception e)
            {
                ApplicationResult<BrandDto> result = new ApplicationResult<BrandDto>();
                result.Succeeded = false;
                result.ErrorMessage = e.Message;
                return result;
            }
        }

        public async Task<ApplicationResult> Delete(Guid id)
        {
            try
            {
                var willDelete = await _context.Brands.FindAsync(id);

                if (willDelete != null)
                {
                    _context.Brands.Remove(willDelete);
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


        public async Task<ApplicationResult<BrandDto>> Update(UpdateBrandViewModel model)
        {
            try
            {
                var getExistBrand = await _context.Brands.FindAsync(model.Id);
                if (getExistBrand == null)
                {
                    return new ApplicationResult<BrandDto>
                    {
                        Result = new BrandDto(),
                        Succeeded = false,
                        ErrorMessage = "Böyle bir Marka bulunamadı!"
                    };
                }
                var modifierUser = await _userManager.FindByIdAsync(model.ModifiedById);
                getExistBrand.ModifiedBy = modifierUser.UserName;
                _mapper.Map(model, getExistBrand);
                _context.Update(getExistBrand);
                await _context.SaveChangesAsync();
                return await Get(getExistBrand.Id);

            }
            catch (Exception e)
            {
                return new ApplicationResult<BrandDto>
                {
                    Result = new BrandDto(),
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}
