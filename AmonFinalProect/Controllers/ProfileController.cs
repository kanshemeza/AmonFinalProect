using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmonFinalProect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;


namespace AmonFinalProect.Controllers
{
    public class ProfileController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;

        public ProfileController(SignInManager<IdentityUser> signInManager)
        {
            this._signInManager = signInManager;
        }


        public IActionResult Index()
        {
            ViewData["States"] = new string[] { "Alabama", "Alaska",
                "Arkansas", "Illinois" };
            return View();
        }


        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string fname, string lname, string address, 
            string state, string city, int zip, string email, string password)
        {
            ViewData["States"] = new string[] { "Alabama", "Alaska",
                "Arkansas", "Illinois" };

            if (ModelState.IsValid)
            {
                IdentityUser newUser = new IdentityUser(email);
                var userResult = _signInManager.UserManager.CreateAsync(newUser).Result;
                if (userResult.Succeeded)
                {
                    var passwordResult = _signInManager.UserManager.AddPasswordAsync(newUser, password).Result;
                    if (passwordResult.Succeeded)
                    {
                        _signInManager.SignInAsync(newUser, false).Wait();
                        return this.RedirectToAction("Index", "Login");

                    }
                    else
                    {
                        foreach(var error in passwordResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                        _signInManager.UserManager.DeleteAsync(newUser).Wait();
                    }
                }

                else
                {
                    foreach(var error in userResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            
            return View();
        }
    }
}