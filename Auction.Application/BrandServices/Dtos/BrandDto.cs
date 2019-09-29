using System;
using System.Collections.Generic;
using System.Text;
using Auction.Domain.Category;

namespace Auction.Application.BrandServices
{
    public class BrandDto : EntityDto
    {
        public string BrandName { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
