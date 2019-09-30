using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Application.CategoryServices.Dtos
{
    public class UpdateCategoryViewModel : CreateCategoryViewModel
    {
        public int Id { get; set; }
        public string ModifiedById { get; set; }

    }
}
