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
                    Name = "Banana",
                   // Description = "This is one of the best desserts for breakfast",
                    Price = 2.99m,
                    ImageUrl = "/images/banana.jpg",
                    ItemNumber = 1
                    
                });
                

                context.Products.Add(new Products
                {
                    Name = "Hass Avocado",
                    //Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 1.99m,
                    ImageUrl = "/images/HassAvocado.jpg",
                    ItemNumber = 2
                });
               

                context.Products.Add(new Products
                {
                    Name = "Apple",
                   // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 1.99m,
                    ImageUrl = "/images/apple.jpg",
                    ItemNumber = 3
                });

                context.Products.Add(new Products
                {
                    Name = "Garlic",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 1.49m,
                    ImageUrl = "/images/garlic.jpg",
                    ItemNumber = 4
                });

                context.Products.Add(new Products
                {
                    Name = "Cucumber",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 1.99m,
                    ImageUrl = "/images/cucumber.jpg",
                    ItemNumber = 5
                });

                context.Products.Add(new Products
                {
                    Name = "Lemon",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 0.99m,
                    ImageUrl = "/images/lemon.jpg",
                    ItemNumber = 6
                });

                context.Products.Add(new Products
                {
                    Name = "Lime",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 0.75m,
                    ImageUrl = "/images/lime.jpg",
                    ItemNumber = 7
                });

                context.Products.Add(new Products
                {
                    Name = "Raspberries",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 1.69m,
                    ImageUrl = "/images/raspberries.jpg",
                    ItemNumber = 8
                });

                context.Products.Add(new Products
                {
                    Name = "Strawberries",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 1.49m,
                    ImageUrl = "/images/strawberries.jpg",
                    ItemNumber = 9
                });

                context.Products.Add(new Products
                {
                    Name = "Tomato",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 1.59m,
                    ImageUrl = "/images/tomatoes.jpg",
                    ItemNumber = 10
                });

                context.Products.Add(new Products
                {
                    Name = "Broccoli",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 1.65m,
                    ImageUrl = "/images/broccoli.png",
                    ItemNumber = 11
                });

                context.Products.Add(new Products
                {
                    Name = "Cilantro Bunch",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 0.39m,
                    ImageUrl = "/images/cilantro-bunch.png",
                    ItemNumber = 12
                });

                context.Products.Add(new Products
                {
                    Name = "Ginger Root",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 2.99m,
                    ImageUrl = "/images/ginger-root.png",
                    ItemNumber = 13
                });

                context.Products.Add(new Products
                {
                    Name = "Green Bell Pepper",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 0.49m,
                    ImageUrl = "/images/green-bell-pepper.jpg",
                    ItemNumber = 14
                });

                context.Products.Add(new Products
                {
                    Name = "Green Onion",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 1.89m,
                    ImageUrl = "/images/green-onions.jpg",
                    ItemNumber = 15
                });

                context.Products.Add(new Products
                {
                    Name = "Red Onion",
                    // Description = "Ingredients: Rice flower, added sugar, fried pan. I love it",
                    Price = 0.59m,
                    ImageUrl = "/images/red-onion.png",
                    ItemNumber = 16
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