using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Application.CategoryServices.Dtos
{
    public class CategoryDto : EntityDto
    {
        public string CategoryName { get; set; }
        public string CategoryUrlName { get; set; }
    }
}