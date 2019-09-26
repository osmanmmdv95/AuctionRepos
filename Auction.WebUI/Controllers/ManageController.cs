using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Auction.Domain.Identity;
using Auction.Domain.RoleClaims;
using Auction.WebUI.ViewModels.Manage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.IdentityModel.Xml;
using Org.BouncyCastle.Bcpg;
using Renci.SshNet;

namespace Auction.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ManageController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        //USER TASKS
        public IActionResult Index()
        {
            List<UserViewModel> users = _userManager.Users
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.FirstName),
                    LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.LastName),
                    FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.FirstName + " " + x.LastName)

                }).ToList();

            return View(users);

        }

        [Route("Users/Detail/{id}")]
        public async Task<IActionResult> UserDetail(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            UserViewModel model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName),
                LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.LastName),
                FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName + " " + user.LastName)
            };
            ViewBag.UserRoles = "Role sahip degil";
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Any())
            {
                ViewBag.UserRoles = string.Join(",", userRoles);

            }

            return View(model);
        }

        [Route("Users/Delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!User.IsInRole("Admin"))

            {
                ViewBag.Message = "Sadece Admin yetkisi olan kişiler kullanıcı silebilir!";
            }

            var user = await _userManager.FindByIdAsync(id);
            UserViewModel model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName),
                LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.LastName),
                FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName + " " + user.LastName)
            };
            ViewBag.UserRoles = "Role sahip degil";
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Any())
            {
                ViewBag.UserRoles = string.Join(",", userRoles);

            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Users/Delete/{id}")]
        public async Task<IActionResult> DeleteUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (!User.IsInRole("Admin"))
                {
                    ViewBag.Message = "Sadece Admin yetkisi olan kişiler kullanıcı silebilir!";

                    UserViewModel errorModel = new UserViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName),
                        LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.LastName),
                        FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName + " " + user.LastName)
                    };
                    return View(errorModel);
                }
                else
                {
                    var deleteUser = await _userManager.DeleteAsync(user);
                    if (deleteUser.Succeeded)
                    {
                        return RedirectToAction("Index", "Manage");
                    }
                    ModelState.AddModelError(string.Empty, "Bir hata oluştu, lütfen tekrar deneyiniz!");
                }
            }

            return View(model);
        }


        [Route("Users/Edit/{id}")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            UserViewModel model = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Users/Edit/{id}")]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user.Email != model.Email)
                {
                    return RedirectToAction("UnknownError", "Error");

                }

                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                var updateUser = await _userManager.UpdateAsync(user);

                if (updateUser.Succeeded)
                {
                    return RedirectToAction("Index", "Manage");
                }
                ModelState.AddModelError(string.Empty, "Bir hata oluştu, tekrar deneyin!");

            }

            return View(model);
        }

        // ROLE CRUD TASKS

        [Route("Roles")]
        public IActionResult Roles()
        {
            List<RoleViewModel> roles = _roleManager.Roles
                  .Select(x => new RoleViewModel
                  {
                      Id = x.Id,
                      Name = x.Name
                  }).ToList();
            return View(roles);
        }

        [Route("Roles/Create")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Roles/Create")]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roles = await _roleManager.FindByNameAsync(model.Name);
                if (roles != null)
                {
                    ModelState.AddModelError(string.Empty, "Bu isimde bir rol mevcut!");
                    return View(model);
                }

                var result = await _roleManager.CreateAsync(new IdentityRole { Name = model.Name });
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Bir hata oluştu!");
                }
            }

            return View(model);
        }

        [Route("Roles/Edit/{id}")]
        public async Task<IActionResult> EditRole(string id)
        {
            ViewBag.IsRoleEditable = await CheckRoleIsEditable(id);
            var role = await _roleManager.FindByIdAsync(id);
            RoleViewModel model = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(model);
        }

        [Route("/Roles/Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isRoleEditable = await CheckRoleIsEditable(model.Id);
                if (!isRoleEditable)
                {
                    ModelState.AddModelError(string.Empty, "Düzenlenemeyecek bir rol üzerinde işlem yapamazsınız!");
                    ViewBag.IsRoleEditable = isRoleEditable;
                    return View(model);
                }

                var updateRole = await _roleManager.FindByIdAsync(model.Id);
                updateRole.Name = model.Name;
                updateRole.NormalizedName = model.Name.ToUpperInvariant();
                var update = await _roleManager.UpdateAsync(updateRole);
                if (update.Succeeded)
                {
                    return RedirectToAction("Roles", "Manage");
                }

            }
            ViewBag.IsRoleEditable = true;
            ModelState.AddModelError(string.Empty, "Bir hata oluştu!");
            return View(model);
        }

        [Route("/Roles/Delete/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var deleteRole = await _roleManager.FindByIdAsync(id);
            ViewBag.ErrorMessage = string.Empty;
            bool isRoleEditable = await CheckRoleIsEditable(id);

            if (!isRoleEditable)
            {
                ViewBag.ErrorMessage = "* Bu bir sistem rolüdür!";
            }
            else
            {
                var usersInThisRole = await _userManager.GetUsersInRoleAsync(deleteRole.Name);
                if (usersInThisRole.Any())
                {
                    string[] userHas = usersInThisRole.Select(x => x.UserName).ToArray();
                    var usersInThisRoleCommaSeperated = string.Join(",", userHas);
                    ViewBag.ErrorMessage = $"* Bu role sahip kullanicilar var: {usersInThisRoleCommaSeperated}";
                }
            }

            var model = new RoleViewModel
            {
                Id = deleteRole.Id,
                Name = deleteRole.Name
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Roles/Delete/{id}")]
        public async Task<IActionResult> DeleteRole(string id, RoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            model.Name = role.Name;
            ViewBag.ErrorMessage = string.Empty;
            bool hasError = false;
            bool isEditableRole = await CheckRoleIsEditable(id);
            if (!isEditableRole)
            {
                ViewBag.ErrorMessage = "Bu bir sistem rolüdür!";
                hasError = true;
            }
            else
            {

                var usersInThisRole = await _userManager.GetUsersInRoleAsync(role.Name);
                if (usersInThisRole.Any())
                {
                    string[] usersHas = usersInThisRole.Select(x => x.UserName).ToArray();
                    var usersInThisRoleCommaSeperated = string.Join(", ", usersHas);
                    ViewBag.ErrorMessage = $"Bu role sahip kullanicilar var: {usersInThisRoleCommaSeperated}";
                    hasError = true;
                }
            }

            if (!hasError)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                ModelState.AddModelError(string.Empty, "Bir hata oluştu tekrar deneyin");
            }
            return View(model);
        }

        [Route("/Roles/{id}")]
        public async Task<IActionResult> RoleDetail(string id)
        {
            var detailRole = await _roleManager.FindByIdAsync(id);
            ViewBag.ErrorMessage = string.Empty;

            if (detailRole == null)
            {
                return RedirectToAction("UnknownError", "Error");
            }

            RoleViewModel model = new RoleViewModel
            {
                Id = detailRole.Id,
                Name = detailRole.Name
            };

            var usersInRole = await _userManager.GetUsersInRoleAsync(detailRole.Name);
            ViewBag.UsersInRole = "Bu role sahip herhangi bir kullanici bulunamadi!";

            if (usersInRole.Any())
            {
                string[] usersArr = usersInRole.Select(x => x.UserName).ToArray();
                string usersMsg = string.Join(", ", usersArr);
                ViewBag.UsersInRole = "Bu role sahip kullanicilar: " + usersMsg;
            }
            return View(model);
        }

        // ROLE ASSIGN TASKS
        [Route("/Roles/Assign/{id}")]
        public async Task<IActionResult> AssignRole(string id)
        {
            List<SelectListItem> roleList = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id,
                Selected = false
            }).ToList();

            AssignRoleViewModel model = new AssignRoleViewModel
            {
                UserId = id,
                RoleList = roleList
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Roles/Assign/{id}")]
        public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                var assignRole = await _userManager.AddToRoleAsync(user, role.Name);
                if (assignRole.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Bir hata oluştu, tekrar deneyiniz!");
                    var roleAssignationErrors = assignRole.Errors.Select(x => x.Description);
                    ModelState.AddModelError(string.Empty,
                        string.Join(", ", roleAssignationErrors));
                }

            }

            return View(model);
        }

        [Route("Roles/Revoke/{id}")]
        public async Task<IActionResult> RevokeRole(string id)
        {
            AssignRoleViewModel model = new AssignRoleViewModel();
            model.UserId = id;
            var user = await _userManager.FindByIdAsync(id);
            var userRolesList = await _userManager.GetRolesAsync(user);
            if (userRolesList.Any())
            {
                var userRoles = _roleManager.Roles.Where(x => userRolesList.Contains(x.Name)).ToList();
                model.RoleList = userRoles.Select(x => new SelectListItem
                {
                    Selected = false,
                    Value = x.Id,
                    Text = x.Name

                }).ToList();
            }
            else
            {
                model.RoleList = new List<SelectListItem>();
            }
            return View(model);
        }

        [HttpPost]
        [Route("Roles/Revoke/{id}")]
        public async Task<IActionResult> RevokeRole(AssignRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", new { id = model.UserId });
                }
                ModelState.AddModelError(String.Empty, "Bir hata oluştu, tekrar deneyiniz!");
            }

            return View(model);
        }

        //cHECK, IS IT A SYSTEM ROLE OR NOT

        private async Task<bool> CheckRoleIsEditable(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            return !Enum.GetNames(typeof(SystemRoles)).Contains(role.Name);
        }
    }
}