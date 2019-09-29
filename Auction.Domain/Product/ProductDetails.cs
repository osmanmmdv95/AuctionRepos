using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Auction.Domain.Product
{
    public class ProductDetails
    {

        public decimal ProductPrice { get; set; }
        public DateTime ProductYear { get; set; }
        public decimal ProductKm { get; set; }
        public decimal ProductMinPrice { get; set; }
        public decimal ProductMaxPrice { get; set; }
        public string ProductGearType { get; set; }
        public string ProductFuelType { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDetail { get; set; }




        [ForeignKey("ProductCityId")]
        public virtual ProductCity ProductCity { get; set; }
        public virtual int? ProductCityId { get; set; }


        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public virtual string ProductId {get; set; }
    }
}
