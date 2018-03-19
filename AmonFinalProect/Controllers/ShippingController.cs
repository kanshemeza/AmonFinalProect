using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmonFinalProect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SendGrid;
using Microsoft.EntityFrameworkCore;

namespace AmonFinalProect.Controllers
{
    public class ShippingController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private SendGridClient _sendGridClient;
        private Braintree.BraintreeGateway _braintreeGateway;
        private AmonTestContext _context;


        public ShippingController(SignInManager<ApplicationUser> signInManager, 
            SendGrid.SendGridClient sendGridClient,
            Braintree.BraintreeGateway braintreeGateway,
            AmonTestContext context)
        {
            this._signInManager = signInManager;
            this._sendGridClient = sendGridClient;
            _braintreeGateway = braintreeGateway;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["States"] = new string[] { "Alabama", "Alaska",
                "Arkansas", "Illinois" };
            string cartId;
            Guid cartCode;
            decimal total = 0m;
            if (Request.Cookies.TryGetValue("cartId", out cartId) && Guid.TryParse(cartId, out cartCode) && _context.Carts.Any(x => x.CartCode == cartCode))
            {
               var cart = _context.Carts.Include(x => x.CartsProducts).ThenInclude(y => y.Product).Single(x => x.CartCode == cartCode);
                total = cart.CartsProducts.Sum(x => x.Quantity * (x.Product.Price??0));
            }

            ViewData["total"] = total.ToString("c");

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
            string creditcardnumber,
            string creditcardname,
            string creditcardverificationvalue,
            string expirationmonth,
            string expirationyear, 
            ShippingModel model )
        {
            ViewData["States"] = new string[] { "Alabama", "Alaska",
                "Arkansas", "Illinois" };

            if (ModelState.IsValid)
            {
                string cartId;
                Guid cartCode;
                if (Request.Cookies.TryGetValue("cartId", out cartId) && Guid.TryParse(cartId, out cartCode) && _context.Carts.Any(x => x.CartCode == cartCode))
                {

                    var cart = _context.Carts.Include(x => x.CartsProducts).ThenInclude(y => y.Product).Single(x => x.CartCode == cartCode);
                    //return View(cart);
               
                
                //var cart = _context.Carts.Include(x => x.CartsProducts).ThenInclude(y => y.Product).Single(x => x.CartCode == cartCode);
                Braintree.TransactionRequest saleRequest = new Braintree.TransactionRequest();
                saleRequest.Amount = cart.CartsProducts.Sum(x => x.Quantity * (x.Product.Price ?? 0));
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

                }

                
            }
            return View();
            
        }
    }
}