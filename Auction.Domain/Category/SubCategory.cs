using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Auction.Domain.Shared;

namespace Auction.Domain.Category
{
   public class SubCategory :Entity
    {
        public string SubCategoryName { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual int? CategoryId { get; set; }
    }
}
