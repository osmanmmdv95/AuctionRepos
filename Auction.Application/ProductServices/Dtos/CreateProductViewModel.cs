using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Auction.Domain.Category;

namespace Auction.Application.ProductServices
{
    public class CreateProductViewModel
    {
        [DisplayName("Ürün Adı")]
        [Required(ErrorMessage = "Ürün adı zorunludur!")]
        public string ProductName { get; set; }

        [DisplayName("Ürün Fiyatı")]
        [Required(ErrorMessage = "Ürün fiyatı zorunludur!")]
        public decimal ProductPrice { get; set; }

        [DisplayName("Ürün Yılı")]
        [Required(ErrorMessage = "Ürün yılı zorunludur!")]
        [StringLength(4, ErrorMessage ="{0} {1} haneli rakam olabilir!",MinimumLength =4)]

        public int? ProductYear { get; set; }

        [DisplayName("Km")]
        [Required(ErrorMessage = "Ürün km zorunludur!")]
        public decimal ProductKm { get; set; }

        [DisplayName("Satılacak Minumum Fiyat")]
        [Required(ErrorMessage = "Ürün minumum fiyatı zorunludur!")]
        public decimal ProductMinPrice { get; set; }

        [DisplayName("Vites Türü")]
        [Required(ErrorMessage = "Ürün vites türü zorunludur!")]
        public string ProductGearType { get; set; }

        [DisplayName("Yakıt Türü")]
        [Required(ErrorMessage = "Ürün yakıt türü zorunludur!")]
        public string ProductFuelType { get; set; }

        [DisplayName("Ürün Fotoğrafı")]
        [Required(ErrorMessage = "Lütfen ürün fotoğrafı ekleyiniz.")]
        public string ProductImageUrl { get; set; }

        [DisplayName("Ürün Detayları")]
        [Required(ErrorMessage = "Lütfen ürün açıklaması giriniz.")]
        public string ProductDetail { get; set; }

        //[DisplayName("Markası")]
        //public virtual Brand Brand { get; set; }

        [DisplayName("Markası")]
        [Required]
        public virtual Guid? ProductBrandId { get; set; }

        //[Required]
        //public int? BrandId { get; set; }
        public string CreatedById { get; set; }

        [DisplayName("Şehir")]
        public virtual int? ProductCityId { get; set; }

    }
}
