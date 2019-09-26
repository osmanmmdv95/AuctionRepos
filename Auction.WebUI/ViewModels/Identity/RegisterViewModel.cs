using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.WebUI.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email alanı zorunludur!")]
        [Display(Name = "Email")]
        [StringLength(30, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter olmalıdır.", MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre alanı zorunludur!")]
        [Display(Name = "Parola")]
        [StringLength(20, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Tekrar şifre alanı zorunludur!")]
        [Display(Name = "Parola Doğrulama")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler aynı değil!")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "İsim alanı zorunludur!")]
        [Display(Name = "Ad")]
        [StringLength(30, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter olmalıdır.", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyisim alanı zorunludur!")]
        [Display(Name = "Soyad")]
        [StringLength(30, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter olmalıdır.", MinimumLength = 2)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "TC kimlik numara alanı zorunludur!")]
        [Display(Name = "T.C. Kimlik Numarası")]
        public long NationalIdNumber { get; set; }
    }
}
