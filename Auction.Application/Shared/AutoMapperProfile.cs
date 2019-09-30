using System;
using System.Collections.Generic;
using System.Text;
using Auction.Application.CategoryServices.Dtos;
using Auction.Domain.Category;
using AutoMapper;

namespace Auction.Application.Shared
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
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


        }
    }
}
