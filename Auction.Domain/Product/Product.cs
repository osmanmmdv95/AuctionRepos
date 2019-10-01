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
        public DateTime UploadDate { get; set; }
        public DateTime ActiveDateTime { get; set; }
        public bool IsItSold { get; set; }

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
        public virtual int ProductCityId { get; set; }


        [ForeignKey("HighestBidderUserId")]
        public virtual ApplicationUser HighestBidderUser { get; set; }
        public virtual string HighestBidderUserId { get; set; }



        [ForeignKey("AddProductUserId")]
        public virtual ApplicationUser AddProductUser { get; set; }
        public virtual string AddProductUserId { get; set; }

    }
}
