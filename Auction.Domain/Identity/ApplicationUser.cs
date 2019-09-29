using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public long? NationalIdNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool UserActive { get; set; }
        public string UserDetail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserAdress { get; set; }
        public string UserImageUrl { get; set; }
    }
}
