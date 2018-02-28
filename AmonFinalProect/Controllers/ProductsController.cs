using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmonFinalProect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AmonFinalProect.Controllers
{
    public class ProductsController : Controller
    {
        private AmonTestContext _context;

        public ProductsController(AmonTestContext context)
        {
            _context = context;
        }

        //private ConnectionStrings _connectionStrings;

        //public ProductsController
        //    (IOptions<ConnectionStrings> connectionStrings)
        //{
        //    _connectionStrings = connectionStrings.Value;
        //}


        public IActionResult Index(int? ItemNumber)
        {
            
            if (ItemNumber.HasValue)
            {
                return View(_context.Products.Include(x => x.Reviews).Single(x => x.ItemNumber == ItemNumber));
            }
            else
            {
                return View(_context.Products.Include(x => x.Reviews));
            }
         
        }

        [HttpPost]
        public IActionResult Index(int id, bool extraParam = true)
        {
            //Rather than saving data in plain-text on the cookie, I'll use GUIDs to save data
            //that isn't easy for your end-user to edit.  In this case, Globally Unique IDs
            //are helpful for tracking "carts" across user sessions.
            string cartId;
            var product = _context.Products.Find(id);
            Carts cart = new Carts();
            CartsProducts item = new CartsProducts();
            item.Quantity = 1;
            item.Product = product;
            cart.CartsProducts.Add(item);
            cart.DateCreated = DateTime.UtcNow;
            cart.DateLastModified = DateTime.UtcNow;
            
            cart.CartCode = Guid.NewGuid();

            _context.Carts.Add(cart);
            _context.SaveChanges();
            cartId = cart.CartCode.ToString();
            Response.Cookies.Append("cartId", cartId,
                new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1)
                });

            Console.WriteLine("Added {0} to cart {1}", id, cartId);


            return RedirectToAction("Index", "Delivery");
        }
    }
}