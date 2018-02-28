using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmonFinalProect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace AmonFinalProect.Controllers
{
    public class LoginController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;

        public LoginController(SignInManager<IdentityUser> signInManager)
        {
            this._signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string username, string password)
        {
            if (ModelState.IsValid)
            {
                IdentityUser existingUser = _signInManager.UserManager.FindByNameAsync(username).Result;
                if (existingUser != null)
                {
                    //I found a user - try validating their password
                    if (_signInManager.UserManager.CheckPasswordAsync(existingUser, password).Result)
                    {
                        //I got the right password for the user - log them in!
                        _signInManager.SignInAsync(existingUser, false).Wait();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("username", "Username or password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError("username", "Username or password is incorrect");
                }

            }
            return View();
        }
    }
}