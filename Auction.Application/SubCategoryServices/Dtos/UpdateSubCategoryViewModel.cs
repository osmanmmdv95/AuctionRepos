using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Application.SubCategoryServices.Dtos
{
    public class UpdateSubCategoryViewModel : CreateSubCategoryViewModel
    {
        public string Id { get; set; }
        public string ModifiedById { get; set; }
    }
}
