using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Auction.Domain.Category;
using Auction.Domain.Identity;
using Auction.Domain.Product;

namespace Auction.Application.ProductServices
{
    public class ProductDto : EntityDto<Guid>
    {
        [DisplayName("Ürün Adı")]
        public string ProductName { get; set; }

        [DisplayName("Ürün Durumu")]
        public bool ProductIsActive { get; set; }

        [DisplayName("Ürün Onaylan Zamanı")]
        public DateTime ActiveDateTime { get; set; }

        [DisplayName("Ürün Satış durumu")]
        public bool IsItSold { get; set; }

        [DisplayName("Ürün Fiyatı")]
        public decimal ProductPrice { get; set; }

        [DisplayName("Ürün Yılı")]
        public int? ProductYear { get; set; }

        [DisplayName("Km")]
        public decimal ProductKm { get; set; }

        [DisplayName("Satılacak Minumum Fiyat")]
        public decimal ProductMinPrice { get; set; }

        [DisplayName("Vites Türü")]
        public string ProductGearType { get; set; }

        [DisplayName("Yakıt Tipi")]
        public string ProductFuelType { get; set; }

        [DisplayName("Ürün Fotoğrafı")]
        public string ProductImageUrl { get; set; }

        [DisplayName("Ürün Detayları")]
        public string ProductDetail { get; set; }

        [DisplayName("Markası")]
        public virtual Brand Brand { get; set; }


        [DisplayName("Marka")]
        public string BrandName { get; set; }

        [DisplayName("Markası")]
        public virtual Guid? ProductBrandId { get; set; }

        [DisplayName("Şehir")]
        public virtual City City { get; set; }

        [DisplayName("Şehir")]
        public string CityName { get; set; }

        [DisplayName("Şehir")]
        public virtual int? ProductCityId { get; set; }


    }
}
