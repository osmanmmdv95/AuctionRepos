using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Application.CategoryServices.Dtos
{
    public class UpdateCategoryViewModel : CreateCategoryViewModel
    {
        public Guid? Id { get; set; }
        public string ModifiedById { get; set; }

    }
}
