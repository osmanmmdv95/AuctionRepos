﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Auction.Application.BrandServices;
using Auction.Application.BrandServices.Dtos;
using Auction.Application.CategoryServices.Dtos;
using Auction.Application.ProductServices;
using Auction.Application.ProductServices.Dtos;
using Auction.Application.SubCategoryServices.Dtos;
using Auction.Domain.Category;
using Auction.Domain.Product;
using AutoMapper;

namespace Auction.Application.Shared
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            /****************************CATEGORY MAPPER***********************************************/
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryViewModel, Category>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());


            CreateMap<UpdateCategoryViewModel, Category>()
                .ForMember(x => x.Id, opt => opt.UseDestinationValue())
                .ForMember(x => x.CreatedBy, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedBy, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedById, opt => opt.UseDestinationValue())
                .ForMember(x => x.CreatedDate, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(s => DateTime.UtcNow));




            /****************************SUBCATEGORY MAPPER***********************************************/
            CreateMap<SubCategoryDto, SubCategory>();
            CreateMap<SubCategory, SubCategoryDto>()
                /*.ForMember(x => x.CategoryName, opt => opt.Ignore())*/;

            CreateMap<CreateSubCategoryViewModel, SubCategory>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());


            CreateMap<UpdateSubCategoryViewModel, SubCategory>()
                .ForMember(x => x.Id, opt => opt.UseDestinationValue())
                .ForMember(x => x.CreatedBy, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedBy, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedById, opt => opt.UseDestinationValue())
                .ForMember(x => x.CreatedDate, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(s => DateTime.UtcNow));


            /****************************BRAND MAPPER***********************************************/
            CreateMap<BrandDto, Brand>();
            CreateMap<Brand, BrandDto>();
            CreateMap<CreateBrandViewModel, Brand>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());


            CreateMap<UpdateBrandViewModel, Brand>()
                .ForMember(x => x.Id, opt => opt.UseDestinationValue())
                .ForMember(x => x.CreatedBy, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedBy, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedById, opt => opt.UseDestinationValue())
                .ForMember(x => x.CreatedDate, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(s => DateTime.UtcNow));

            CreateMap<List<SubCategory>, BrandDto>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());




            /****************************Product MAPPER***********************************************/
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            //.ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //.ForMember(x => x.ModifiedBy, opt => opt.Ignore())
            //.ForMember(x => x.ModifiedById, opt => opt.Ignore())
            //.ForMember(x => x.CreatedDate, opt => opt.Ignore())
            //.ForMember(x => x.ModifiedDate, opt => opt.Ignore())
            //.ForMember(x => x.ProductIsActive, opt => opt.Ignore())
            //.ForMember(x => x.ActiveDateTime, opt => opt.Ignore())
            //.ForMember(x => x.IsItSold, opt => opt.Ignore());

            CreateMap<CreateProductViewModel, Product>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.ProductIsActive, opt => opt.MapFrom(s => false))
                .ForMember(x => x.ActiveDateTime, opt => opt.Ignore())
                .ForMember(x => x.IsItSold, opt => opt.MapFrom(s => false));



            CreateMap<UpdateProductViewModel, Product>()
                .ForMember(x => x.Id, opt => opt.UseDestinationValue())
                .ForMember(x => x.CreatedBy, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedBy, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedById, opt => opt.UseDestinationValue())
                .ForMember(x => x.CreatedDate, opt => opt.UseDestinationValue())
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(x => x.ProductIsActive, opt => opt.Ignore())
                .ForMember(x => x.ActiveDateTime, opt => opt.Ignore())
                .ForMember(x => x.IsItSold, opt => opt.Ignore());

            CreateMap<List<Product>, ProductDto>()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.ProductIsActive, opt => opt.Ignore())
                .ForMember(x => x.ActiveDateTime, opt => opt.Ignore())
                .ForMember(x => x.IsItSold, opt => opt.Ignore());



        }

    }

}
