using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmonFinalProect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmonFinalProect.Controllers
{
    public class CartController : Controller
    {

        private AmonTestContext _context;

        public CartController(AmonTestContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            string cartId;
            Guid cartCode;
            if (Request.Cookies.TryGetValue("cartId", out cartId) && Guid.TryParse(cartId, out cartCode) && _context.Carts.Any(x => x.CartCode == cartCode))
            {

                var cart = _context.Carts.Include(x => x.CartsProducts).ThenInclude(y => y.Product).Single(x => x.CartCode == cartCode);
                return View(cart);
            }
            return RedirectToAction("Index", "Products");
        }
        //[HttpPost]
        //public IActionResult Checkout()
        //{
        //    return RedirectToAction("Index", "Profile");
        //}

        [HttpPost]
        public IActionResult Update(int quantity, int id)
        {
            string cartId;
            Guid cartCode;
            if (Request.Cookies.TryGetValue("cartId", out cartId) && Guid.TryParse(cartId, out cartCode) && _context.Carts.Any(x => x.CartCode == cartCode))
            {

                var cart = _context.Carts.Include(x => x.CartsProducts).ThenInclude(y => y.Product).Single(x => x.CartCode == cartCode);
                var cartItem = cart.CartsProducts.Single(x => x.Product.Id == id);
                cartItem.Quantity = quantity;

                if (cartItem.Quantity == 0)
                {
                    _context.CartsProducts.Remove(cartItem);
                }
                _context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Remove(int id, int quantity)
        {
            string cartId;
            Guid cartCode;
            if (Request.Cookies.TryGetValue("cartId", out cartId) && Guid.TryParse(cartId, out cartCode) && _context.Carts.Any(x => x.CartCode == cartCode))
            {

                var cart = _context.Carts.Include(x => x.CartsProducts).ThenInclude(y => y.Product).Single(x => x.CartCode == cartCode);
                var cartItem = cart.CartsProducts.Single(x => x.Product.Id == id);
                cartItem.Quantity = quantity;

                if (cartItem.Quantity != 0)
                {
                    _context.CartsProducts.Remove(cartItem);
                }
              
                _context.SaveChanges();
            }
            return Ok();
        }
    }
}