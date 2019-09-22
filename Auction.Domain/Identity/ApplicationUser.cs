using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public int? NationalIdNumber { get; set; }
    }
}
