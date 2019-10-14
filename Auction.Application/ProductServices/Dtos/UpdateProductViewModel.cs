using System;

namespace Auction.Application.ProductServices.Dtos
{
    public class UpdateProductViewModel : CreateProductViewModel
    {
        public Guid Id { get; set; }
        public string ModifiedById { get; set; }

    }
}
