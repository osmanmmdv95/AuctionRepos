using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Application.BrandServices.Dtos
{
   public class CreateBrandViewModel
    {
        public string BrandName { get; set; }
        public string UrlName { get; set; } 
        public string CreatedById { get; set; }
        public int SubCategoryId { get; set; }
    }
}
