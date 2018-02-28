using System;
using System.Collections.Generic;

namespace AmonFinalProect.Models
{
    public partial class CartsProducts
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string SpecialInstructions { get; set; }

        public Carts Cart { get; set; }
        public Products Product { get; set; }
    }
}
