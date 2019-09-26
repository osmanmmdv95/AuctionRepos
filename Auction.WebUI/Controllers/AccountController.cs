using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.Domain.Identity;
using Auction.WebUI.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auction.WebUI.Controllers
{
    public class AccountController : Controller
    {
        // Kullanici kaydetmek icin veya kullanici bilgilerinde degisiklik yapmak icin kullanilan servis.
        private readonly UserManager<ApplicationUser> _userManager;

        // Kullanicinin uygulamaya giris cikis islemlerini yonettigimiz servis.
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            // gelen modeli dogrula
            if (ModelState.IsValid)
            {
                // model dogruysa
                // kullaniyi kontrol et var mi?
                var existUser = await _userManager.FindByEmailAsync(model.Username);

                //LoginPartial'a Kullanıcının ad ve soyadını yazdırabilmek için ekledim!
                

                // yoksa hata don
                if (existUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Bu email ile kayitli bir kullanıcı bulunamadi!");
                    return View(model);
                }
                // kullanici adi ve sifre eslesiyor mu?
                var login = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                // eslesmiyorsa hata don
                if (!login.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Bu email ve sifre ile uyumlu bir kullanıcı bulunamadi! Sifrenizi kontrol edin!");
                    return View(model);
                }

                // ana sayfaya yonlendir (simdilik)

                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                //LoginPartial'a Kullanıcının ad ve soyadını yazdırabilmek için ekledim!
                var t = await _userManager.AddClaimAsync(existUser, new Claim("FullName", existUser.FirstName + " " + existUser.LastName));
                TempData["FullName"] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(existUser.FirstName + " " + existUser.LastName);

                return RedirectToAction("Index", "Home");
            }
            // basarili degilse hata don
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // gelen modeli valide et
            if (ModelState.IsValid)
            {

                //LoginPartial'a Kullanıcının ad ve soyadını yazdırabilmek için ekledim!
                TempData["FullName"] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.FirstName + " " + model.LastName);
                // validse kaydet

                // ApplicationUser olustur
                var newUser = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailConfirmed = true,
                    TwoFactorEnabled = false,
                    NationalIdNumber = model.NationalIdNumber
                };

                var registerUser = await _userManager.CreateAsync(newUser, model.Password);
                if (registerUser.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                // kaydetme basarisizsa hatalari modelstate e ekle
                AddErrors(registerUser);

            }
            // degilse hatalari don
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }

    }
}