using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Auction.Domain.Category;
using Auction.Domain.Identity;
using Auction.Domain.Shared;

namespace Auction.Domain.Product
{
    public class Product : Entity<Guid>
    {
        public string ProductName { get; set; }
        public bool ProductIsActive { get; set; }
        public DateTime ActiveDateTime { get; set; }
        public bool IsItSold { get; set; }

        public decimal ProductPrice { get; set; }
        public DateTime ProductYear { get; set; }
        public decimal ProductKm { get; set; }
        public decimal ProductMinPrice { get; set; }
        public string ProductGearType { get; set; }
        public string ProductFuelType { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDetail { get; set; }

        [ForeignKey("ProductBrandId")]
        public virtual Brand Brand { get; set; }
        public virtual Guid? ProductBrandId { get; set; }


        [ForeignKey("ProductCityId")]
        public virtual City City { get; set; }
        public virtual int? CityId { get; set; }


    }
}
