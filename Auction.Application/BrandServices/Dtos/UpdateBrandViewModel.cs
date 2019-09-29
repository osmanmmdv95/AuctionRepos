using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Application.BrandServices.Dtos
{
    public class UpdateBrandViewModel : CreateBrandViewModel
    {
        public string Id { get; set; }
        public string ModifiedById { get; set; }

    }
}
