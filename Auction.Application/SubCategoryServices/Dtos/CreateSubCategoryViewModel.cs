using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Auction.Application.SubCategoryServices.Dtos
{
    public class CreateSubCategoryViewModel
    {
        [DisplayName("Alt Kategori Adı")]
        [Required(ErrorMessage = "Alt kategori ad alanı zorunludur!")]
        public string SubCategoryName { get; set; }

        [DisplayName("Alt Kategori Url")]
        [Required(ErrorMessage = "Alt kategori url alanı zorunludur!")]
        public string SubCategoryUrlName { get; set; }
        public string CreatedById { get; set; }
        public int? CategoryId { get; set; }
    }
}
