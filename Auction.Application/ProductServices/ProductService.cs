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
            throw new NotImplementedException();
        }

        public async Task<ApplicationResult<ProductDto>> Get(Guid id)
        {
            try
            {
                var joinList = await (from product in _context.Products
                                      join brand in _context.Brands on product.ProductBrandId equals brand.Id
                                      join city in _context.Cities on product.CityId equals city.Id
                                      select new
                                      {
                                          brand.BrandName,
                                          city.CityName,
                                          product.Id,
                                          product.ProductBrandId,
                                          product.CityId,
                                          product.ProductDetail,
                                          product.ProductFuelType,
                                          product.ProductGearType,
                                          product.ProductImageUrl,
                                          product.ProductIsActive,
                                          product.ActiveDateTime,
                                          product.ProductPrice,
                                          product.ProductYear,
                                          product.ProductName,
                                          product.ProductKm
                                      }).ToListAsync();

                var selectProduct = joinList.Where(x => x.Id == id).FirstOrDefault();

                //var mapProduct = _mapper.Map<ProductDto>(joinList);

                ProductDto mapProduct = new ProductDto();
                mapProduct = new ProductDto
                {
                    BrandName = selectProduct.BrandName,
                    CityName = selectProduct.CityName,
                    Id = selectProduct.Id,
                    ProductBrandId = selectProduct.ProductBrandId,
                    CityId = selectProduct.CityId,
                    ProductDetail = selectProduct.ProductDetail,
                    ProductFuelType = selectProduct.ProductFuelType,
                    ProductGearType = selectProduct.ProductGearType,
                    ProductImageUrl = selectProduct.ProductImageUrl,
                    ProductIsActive = selectProduct.ProductIsActive,
                    ActiveDateTime = selectProduct.ActiveDateTime,
                    ProductPrice = selectProduct.ProductPrice,
                    ProductYear = selectProduct.ProductYear,
                    ProductName = selectProduct.ProductName,
                    ProductKm = selectProduct.ProductKm

                };

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
                var joinList = await (from product in _context.Products
                                      join brand in _context.Brands on product.ProductBrandId equals brand.Id
                                      join city in _context.Cities on product.CityId equals city.Id
                                      select new
                                      {
                                          brand.BrandName,
                                          city.CityName,
                                          product.Id,
                                          product.ProductBrandId,
                                          product.CityId,
                                          product.ProductDetail,
                                          product.ProductFuelType,
                                          product.ProductGearType,
                                          product.ProductImageUrl,
                                          product.ProductIsActive,
                                          product.ActiveDateTime,
                                          product.ProductPrice,
                                          product.ProductYear,
                                          product.ProductName,
                                          product.ProductKm
                                      }).ToListAsync();

                List<ProductDto> productList = new List<ProductDto>();
                foreach (var item in joinList)
                {
                    ProductDto mapProduct = new ProductDto
                    {
                        BrandName = item.BrandName,
                        CityName = item.CityName,
                        Id = item.Id,
                        ProductBrandId = item.ProductBrandId,
                        CityId = item.CityId,
                        ProductDetail = item.ProductDetail,
                        ProductFuelType = item.ProductFuelType,
                        ProductGearType = item.ProductGearType,
                        ProductImageUrl = item.ProductImageUrl,
                        ProductIsActive = item.ProductIsActive,
                        ActiveDateTime = item.ActiveDateTime,
                        ProductPrice = item.ProductPrice,
                        ProductYear = item.ProductYear,
                        ProductName = item.ProductName,
                        ProductKm = item.ProductKm

                    };
                    productList.Add(mapProduct);
                }

                //List<ProductDto> productList = _mapper.Map<List<ProductDto>>(joinList);

                return new ApplicationResult<List<ProductDto>>
                {
                    Succeeded = true,
                    Result = productList
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
            throw new NotImplementedException();
        }
    }
}
