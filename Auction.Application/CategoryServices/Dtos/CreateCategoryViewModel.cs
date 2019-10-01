using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Auction.Application.CategoryServices.Dtos
{
    public class CreateCategoryViewModel
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "Kategori ad alanı zorunludur!")]
        public string CategoryName { get; set; }

        [DisplayName("Kategori Url")]
        [Required(ErrorMessage = "Kategori url alanı zorunludur!")]
        public string CategoryUrlName { get; set; }
        public string CreatedById { get; set; }


    }
}
