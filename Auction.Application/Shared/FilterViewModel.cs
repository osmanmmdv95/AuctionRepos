using System;
using System.ComponentModel;
using Auction.Domain.Category;

namespace Auction.Application.Shared
{
    public class FilterViewModel
    {
        [DisplayName("Kategori Adı")]
        public string CategoryName { get; set; }

        [DisplayName("Kategori Adı")]
        public Guid? CategoryId { get; set; }

        [DisplayName("Kategori")]
        public virtual Category Category { get; set; }


        [DisplayName("Alt Kategori Adı")]
        public string SubCategoryName { get; set; }

        [DisplayName("Alt Kategori Adı")]
        public Guid? SubCategoryId { get; set; }

        [DisplayName("Alt Kategori")]
        public virtual SubCategory SubCategory { get; set; }



        [DisplayName("Marka")]
        public string BrandName { get; set; }

        [DisplayName("Markası")]
        public virtual Guid? ProductBrandId { get; set; }
        [DisplayName("Markası")]
        public virtual Brand Brand { get; set; }


    }
}
