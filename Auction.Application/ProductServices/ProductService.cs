using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Domain.Identity;
using Auction.Domain.Product;
using Auction.EntityFramework.Context;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auction.Application.ProductServices
{

    public class ProductService : IProductService
    {
        private readonly ApplicationUserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ProductService(ApplicationUserDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApplicationResult<ProductDto>> Create(CreateProductViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.CreatedById);

                var newProduct = _mapper.Map<Product>(model);
                newProduct.CreatedBy = user.UserName;
                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                return new ApplicationResult<ProductDto>
                {
                    Succeeded = true
                };

            }
            catch (Exception e)
            {
                return new ApplicationResult<ProductDto>
                {
                    Succeeded = true,
                    Result = new ProductDto(),
                    ErrorMessage = e.Message

                };
            }
        }

        public async Task<ApplicationResult> Delete(Guid id)
        {
            try
            {
                var willDelete = await _context.Products.FindAsync(id);
                if (willDelete != null)
                {
                    _context.Products.Remove(willDelete);
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

        public async Task<ApplicationResult<ProductDto>> Get(Guid id)
        {
            try
            {
                var product = await _context.Products.Include(b => b.Brand).Include(c => c.City).FirstOrDefaultAsync(x => x.Id == id);

                var mapProduct = _mapper.Map<ProductDto>(product);
                mapProduct.BrandName = product.Brand.BrandName;
                mapProduct.CityName = product.City.CityName;

                return new ApplicationResult<ProductDto>
                {
                    Result = mapProduct,
                    Succeeded = true
                };
            }
            catch (Exception e)
            {
                return new ApplicationResult<ProductDto>
                {
                    Result = new ProductDto(),
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<ApplicationResult<List<ProductDto>>> GetAll()
        {
            try
            {
                var listProduct = await _context.Products.Include(b => b.Brand).Include(c => c.City).ToListAsync();

                List<ProductDto> mapProduct = _mapper.Map<List<ProductDto>>(listProduct);

                int i = 0;
                foreach (var item in mapProduct)
                {
                    item.CityName = listProduct[i].City.CityName;
                    item.BrandName = listProduct[i].Brand.BrandName;
                    i++;
                }


                return new ApplicationResult<List<ProductDto>>
                {
                    Succeeded = true,
                    Result = mapProduct
                };
            }
            catch (Exception e)
            {
                return new ApplicationResult<List<ProductDto>>
                {
                    Succeeded = false,
                    ErrorMessage = e.Message,
                    Result = new List<ProductDto>()
                };
            }
        }

        public async Task<ApplicationResult<ProductDto>> Update(UpdateProductViewModel model)
        {
            try
            {
                var willUpdate = await _context.Products.FindAsync(model.Id);
                if (willUpdate != null)
                {
                    var modifierUser = await _userManager.FindByIdAsync(model.ModifiedById);
                    willUpdate.ModifiedBy = modifierUser.UserName;
                    _mapper.Map(model, willUpdate);
                    _context.Products.Update(willUpdate);
                    await _context.SaveChangesAsync();
                    return new ApplicationResult<ProductDto>
                    {
                        Succeeded = true,

                    };
                }

                return new ApplicationResult<ProductDto> { Succeeded = false, ErrorMessage = "Bir hata oluştu lütfen kontrol edip tekrar deneyiniz" };
            }
            catch (Exception e)
            {
                return new ApplicationResult<ProductDto>
                {
                    Result = new ProductDto(),
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }
            return new ApplicationResult<ProductDto>();
        }
    }
}
