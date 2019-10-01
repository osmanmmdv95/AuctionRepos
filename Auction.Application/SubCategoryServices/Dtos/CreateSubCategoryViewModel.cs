using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Application.SubCategoryServices.Dtos
{
    public class CreateSubCategoryViewModel
    {
        public string SubCategoryName { get; set; }
        public string SubCategoryUrlName { get; set; }
        public string CreatedById { get; set; }
        public int CategoryId { get; set; }
    }
}
