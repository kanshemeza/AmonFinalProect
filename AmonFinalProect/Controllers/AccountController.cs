﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmonFinalProect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Extensions;
using SendGrid;

namespace AmonFinalProect.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private SendGridClient _sendGridClient;


        public AccountController(SignInManager<ApplicationUser> signInManager, SendGridClient sendGridClient)
        {
            this._signInManager = signInManager;
            _sendGridClient = sendGridClient;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string password, string email)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser { Email = username, UserName = username };
                var userResult = _signInManager.UserManager.CreateAsync(newUser).Result;
                if (userResult.Succeeded)
                {
                    var passwordResult = _signInManager.UserManager.AddPasswordAsync(newUser, password).Result;
                    if (passwordResult.Succeeded)
                    {
                        SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                        message.AddTo(username);
                        message.Subject = "Welcome to AK-Restaurant";
                        message.SetFrom("alpacaadmin@codingtemple.com");
                        message.AddContent("text/plain", "Thanks for registering as " + username + " on AK-Restaurant!");
                        await _sendGridClient.SendEmailAsync(message);

                        _signInManager.SignInAsync(newUser, false).Wait();
                        return RedirectToAction("Index", "Account");

                    }
                    else
                    {
                        foreach (var error in passwordResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                        _signInManager.UserManager.DeleteAsync(newUser).Wait();
                    }
                }

                else
                {
                    foreach (var error in userResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(
            string username, 
            string password
            )
        {
            if (ModelState.IsValid)
            {
               
                ApplicationUser existingUser = _signInManager.UserManager.FindByNameAsync(username).Result;
                if (existingUser != null)
                {
                    //I found a user - try validating their password
                    if (_signInManager.UserManager.CheckPasswordAsync(existingUser, password).Result)
                    {
                        //I got the right password for the user - log them in!
                        _signInManager.SignInAsync(existingUser, false).Wait();
                        return RedirectToAction("Index", "Products");
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

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(email);
            if(user != null)
            {
                string token = await _signInManager.UserManager.GeneratePasswordResetTokenAsync(user);
                string currentUrl = Request.GetDisplayUrl();
                System.Uri uri = new Uri(currentUrl);   //This will wrap it in a "URI" object so I can split it into parts
                string resetUrl = uri.GetLeftPart(UriPartial.Authority); //This gives me just the scheme + authority of the URI
                resetUrl += "/account/resetpassword/" + System.Net.WebUtility.UrlEncode(token);

                SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                message.AddTo(email);
                message.Subject = "Your password reset token";
                message.SetFrom("alpacaadmin@codingtemple.com");
                message.AddContent("text/plain", resetUrl);
                message.AddContent("text/html", string.Format("<a href=\"{0}\">{0}</a>", resetUrl));
                await _sendGridClient.SendEmailAsync(message);
            }

            return RedirectToAction("Index", "Account");
        }

        
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string id, string email, string password)
        {
            string originalToken = id;
            var user = await _signInManager.UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.UserManager.ResetPasswordAsync(user, originalToken, password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", new { resetSuccessful = true });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }

            }
            return View();
        }
    }
}