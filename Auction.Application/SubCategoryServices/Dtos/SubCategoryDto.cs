using System;
using System.Collections.Generic;
using System.Text;
using Auction.Application.CategoryServices.CategoryViewModels;

namespace Auction.Application.SubCategoryServices.Dtos
{
    public class SubCategoryDto : EntityDto
    {
        public string SubCategoryName { get; set; }
        public CategoryDto Category { get; set; }
    }
}
