using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmonFinalProect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SendGrid;

namespace AmonFinalProect.Controllers
{
    public class ProfileController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private SendGridClient _sendGridClient;
        private Braintree.BraintreeGateway _braintreeGateway;

        public ProfileController(SignInManager<ApplicationUser> signInManager, 
            SendGrid.SendGridClient sendGridClient,
            Braintree.BraintreeGateway braintreeGateway)
        {
            this._signInManager = signInManager;
            this._sendGridClient = sendGridClient;
            _braintreeGateway = braintreeGateway;
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
        public async Task<IActionResult> Index(
            string fname, 
            string lname, 
            string address, 
            string state, 
            string city, 
            int zip, 
            string email, 
            string password,
            string creditcardnumber,
            string creditcardname,
            string creditcardverificationvalue,
            string expirationmonth,
            string expirationyear)
        {
            ViewData["States"] = new string[] { "Alabama", "Alaska",
                "Arkansas", "Illinois" };

            if (ModelState.IsValid)
            {
                Braintree.TransactionRequest saleRequest = new Braintree.TransactionRequest();
                saleRequest.Amount = 10;    //Hard-coded for now
                saleRequest.CreditCard = new Braintree.TransactionCreditCardRequest
                {
                    CardholderName = creditcardname,
                    CVV = creditcardverificationvalue,
                    ExpirationMonth = expirationmonth,
                    ExpirationYear = expirationyear,
                    Number = creditcardnumber
                };
                var result = await _braintreeGateway.Transaction.SaleAsync(saleRequest);
                if (result.IsSuccess())
                {
                    //If model state is valid, proceed to the next step.
                    return this.RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors.All())
                {
                    ModelState.AddModelError(error.Code.ToString(), error.Message);
                }

                //    ApplicationUser newUser = new ApplicationUser { Email = email, UserName = email };
                //    var userResult = _signInManager.UserManager.CreateAsync(newUser).Result;
                //    if (userResult.Succeeded)
                //    {
                //        var passwordResult = _signInManager.UserManager.AddPasswordAsync(newUser, password).Result;
                //        if (passwordResult.Succeeded)
                //        {
                //            SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                //            message.AddTo(email);
                //            message.Subject = "Welcome to AK-Restaurant";
                //            message.SetFrom("alpacaadmin@codingtemple.com");
                //            message.AddContent("text/plain", "Thanks for registering as " + email + " on AK-Restaurant!");
                //            await _sendGridClient.SendEmailAsync(message);

                //            _signInManager.SignInAsync(newUser, false).Wait();
                //            return this.RedirectToAction("Index", "Login");

                //        }
                //        else
                //        {
                //            foreach(var error in passwordResult.Errors)
                //            {
                //                ModelState.AddModelError(error.Code, error.Description);
                //            }
                //            _signInManager.UserManager.DeleteAsync(newUser).Wait();
                //        }
                //    }

                //    else
                //    {
                //        foreach(var error in userResult.Errors)
                //        {
                //            ModelState.AddModelError(error.Code, error.Description);
                //        }
                //    }
                //}

            }
            return View();
            
        }
    }
}