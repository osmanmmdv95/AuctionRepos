using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Application.ProductServices
{
    public class UpdateProductViewModel : CreateProductViewModel
    {
        public Guid Id { get; set; }
        public string ModifiedById { get; set; }

    }
}
