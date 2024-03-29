﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Auction.Application.CategoryServices.Dtos;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Auction.Application.SubCategoryServices.Dtos
{
    public class SubCategoryDto : EntityDto<Guid>
    {
        [DisplayName("Alt Kategori Adı")]
        public string SubCategoryName { get; set; }

        [DisplayName("Alt Kategori Url")]
        public string SubCategoryUrlName { get; set; }
        [DisplayName("Kategori Adı")]
        public string CategoryName { get; set; }

        [DisplayName("Kategori Adı")]
        public Guid? CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
