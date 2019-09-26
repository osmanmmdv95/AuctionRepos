using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Auction.WebUI.ViewModels.Manage
{
    public class AssignRoleViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [DisplayName("İşlem yapmak istediğiniz rolü seçin")]
        public string RoleId { get; set; }
        public List<SelectListItem> RoleList { get; set; }
    }
}
