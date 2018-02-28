using System;
using AmonFinalProect.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AmonFinalProect
{
    internal class DbInitializer
    {
        internal static void Initialize(AmonTestContext context)
        {
            context.Database.Migrate();

            if(!context.Products.Any())
            {
                context.Products.Add(new Products
                {
                    Name = "Mandazi",
                    Description = "This is one of the best desserts for breakfast",
                    Price = 6.99m,
                    ImageUrl = "/images/mandazi.jpg",
                    ItemNumber = 1
                    
                });

                context.Products.Add(new Products
                {
                    Name = "Vitumbua",
                    Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 10.99m,
                    ImageUrl = "/images/vitumbua.jpg",
                    ItemNumber = 2
                });
                context.SaveChanges();
            }

            if (!context.Reviews.Any())
            {
                context.Reviews.Add(new Review
                {
                    Rating = 5,
                    Body = "This is a great dessert",
                    IsApproved = true,
                    Product = context.Products.First()
                });
                context.SaveChanges();
            }
        }
    }
}