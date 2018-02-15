using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmonFinalProect.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmonFinalProect.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            ProductsViewModel model = new ProductsViewModel();
            model.Products = new Product[]
            {
                new Product
                {
                    Name = "Pilau",
                    Description = "lorem ipsum lorem ipsum lorem ipsum",
                    Price = 14.99m,
                    ImageUrl = "/images/mandazi.jpg"

                },
                new Product
                {
                    Name = "Vitumbua",
                    Description = "lorem ipsum lorem ipsum lorem ipsum",
                    Price = 10.99m,
                    ImageUrl = "/images/vitumbua.jpg"
                }
            };
            return View(model);
        }
    }
}