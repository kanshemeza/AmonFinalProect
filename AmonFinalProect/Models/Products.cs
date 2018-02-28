using System;
using System.Collections.Generic;

namespace AmonFinalProect.Models
{
    public partial class Products
    {
        public Products()
        {
            CartsProducts = new HashSet<CartsProducts>();
            Reviews = new HashSet<Review>();
            LineItems = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string ImageUrl { get; set; }
        public int ItemNumber { get; set; }

        public ICollection<CartsProducts> CartsProducts { get; set; }
        //I added Product as a relational property on Review, I should add a collection of Reviews on the product to create 
        //a one-to-many relationship between Products and Reviews (e.g. a product has many reviews)
        public ICollection<Review> Reviews { get; set; }
        public ICollection<LineItem> LineItems { get; set; }

    }
}
