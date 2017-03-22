using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Clayton.Models;

namespace Clayton.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                        return RedirectToAction("Index", "Home");

                    return Redirect(loginViewModel.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Username/password not found");
            return View(loginViewModel);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = new IdentityUser() { UserName = loginViewModel.UserName };
            //    var result = await _userManager.CreateAsync(user, loginViewModel.Password);

            //    if (result.Succeeded)
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //    {
            //        // Add errors to model state
            //        AddErrors(result);
            //    }
            //}

            return View(loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }


        public IActionResult Manage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return PartialView("_ChangePassword");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return PartialView("_ChangePassword", model);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var response = _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if(response.Result.Succeeded)
            {
                var closeModal = new CloseViewModel();
                closeModal.ReloadPage = true;
                ///https://amlblog.net/programming/2016/05/02/forms-in-bootstrap-modal-partial-view.html
                //closeModal.ShouldClose = true;
                //closeModal.FetchData = true;
                //closeModal.Destination = Url.Action("Manage");
                //closeModal.Target = "list";
                //closeModal.OnSuccess = "success";
                return PartialView("_CloseModal", closeModal);
            }
            else
            {
                ModelState.AddModelError("", "Your current password is incorrect");
                return PartialView("_ChangePassword", model);
            }

            // Need to find current user
            // Then reset the password
            // Only close if successful
            //var closeModal = new CloseViewModel();
            //closeModal.ShouldClose = true;
            //closeModal.FetchData = false;
            //closeModal.Destination = Url.Action("Manage");
            //closeModal.Target = "list";
            //closeModal.OnSuccess = "success";
            //return PartialView("_CloseModal", closeModal);
        }

    }
}
