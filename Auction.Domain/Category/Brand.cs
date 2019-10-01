using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Auction.Domain.Shared;

namespace Auction.Domain.Category
{
    public class Brand : Entity
    {
        public string BrandName { get; set; }
        public string BrandUrlName { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }
        public virtual int? SubCategoryId { get; set; }
    }
}
