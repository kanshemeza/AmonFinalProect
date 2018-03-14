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
        public IActionResult Index(int id, int quantity, string instructions)
        {
            //Rather than saving data in plain-text on the cookie, I'll use GUIDs to save data
            //that isn't easy for your end-user to edit.  In this case, Globally Unique IDs
            //are helpful for tracking "carts" across user sessions.
            string cartId;
            Guid cartCode;
            Carts cart;
            if (Request.Cookies.TryGetValue("cartId", out cartId) && Guid.TryParse(cartId, out cartCode) && _context.Carts.Any(x => x.CartCode == cartCode))
            {

                cart = _context.Carts.Include(x => x.CartsProducts).ThenInclude(y => y.Product).Single(x => x.CartCode == cartCode);
                cart.DateLastModified = DateTime.UtcNow;
            }
            else
            {
                cart = new Carts();
                cart.DateCreated = DateTime.UtcNow;
                cart.DateLastModified = DateTime.UtcNow;
                cartCode = Guid.NewGuid();
                cart.CartCode = cartCode;
                cartId = cart.CartCode.ToString();
                Response.Cookies.Append("cartId", cartId,
                    new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddYears(1)
                    });

                _context.Carts.Add(cart);

            }
            CartsProducts item = cart.CartsProducts.FirstOrDefault(x => x.ProductId == id);
            if(item == null)
            {
                item = new CartsProducts();
                item.ProductId = id;
                item.Quantity = 0;
                item.SpecialInstructions = instructions;
                cart.CartsProducts.Add(item);
            }
            item.Quantity += quantity;
            _context.SaveChanges();
            
            Console.WriteLine("Added {0} to cart {1}", id, cartId);


            return Ok("Added to cart");
        }

       
    }
}