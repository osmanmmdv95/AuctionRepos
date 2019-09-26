using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.WebUI.ViewModels.Manage
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur!")]
        [DisplayName("Rol Adı")]
        public string Name { get; set; }
    }
}
