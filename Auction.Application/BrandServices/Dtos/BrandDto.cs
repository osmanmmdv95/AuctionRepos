using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Auction.Domain.Category;

namespace Auction.Application.BrandServices
{
    public class BrandDto : EntityDto
    {
        [DisplayName("Marka")]
        public string BrandName { get; set; }

        [DisplayName("Marka Url")]
        public string BrandUrlName { get; set; }


        [DisplayName("Alt Kategori Adı")]
        public string SubCategoryName { get; set; }

        [DisplayName("Alt Kategori Adı")]
        public int? SubCategoryId { get; set; }

        [DisplayName("Alt Kategori")]
        public virtual SubCategory SubCategory { get; set; }
    }
}
