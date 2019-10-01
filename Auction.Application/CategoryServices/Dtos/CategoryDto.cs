using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Auction.Application.CategoryServices.Dtos
{
    public class CategoryDto : EntityDto
    {
        [DisplayName("Kategori Adı")]
        public string CategoryName { get; set; }

        [DisplayName("Kategori Url")]
        public string CategoryUrlName { get; set; }
    }
}