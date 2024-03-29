﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Auction.Application.BrandServices.Dtos
{
   public class CreateBrandViewModel
    {
        [DisplayName("Marka")]
        [Required(ErrorMessage = "Marka alanı zorunludur!")]
        public string BrandName { get; set; }


        [DisplayName("Marka Url")]
        [Required(ErrorMessage = "Marka alanı zorunludur!")]
        public string BrandUrlName { get; set; }

        public string CreatedById { get; set; }

        [DisplayName("Alt Kategori Adı")]
        public Guid? SubCategoryId { get; set; }
    }
}
