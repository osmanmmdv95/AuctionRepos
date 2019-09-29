using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Application.CategoryServices.Dtos
{
   public class CreateCategoryViewModel
    {
        public string CategoryName { get; set; }
        public string UrlName { get; set; }
        public string CreatedById { get; set; }

       
    }
}
