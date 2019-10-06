using System;
using System.Collections.Generic;
using System.Text;
using Auction.Domain.Shared;

namespace Auction.Domain.Category
{
    public class Category : Entity<Guid>
    {
        public string CategoryName { get; set; }
        public string CategoryUrlName { get; set; }

    }
}
