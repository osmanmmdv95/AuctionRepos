using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Application.ProductServices
{
    public class CreateProductViewModel
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime ProductYear { get; set; }
        public decimal ProductKm { get; set; }
        public decimal ProductMinPrice { get; set; }
        public decimal ProductMaxPrice { get; set; }
        public string ProductGearType { get; set; }
        public string ProductFuelType { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDetail { get; set; }

        public virtual int? ProductCityId { get; set; }

    }
}
