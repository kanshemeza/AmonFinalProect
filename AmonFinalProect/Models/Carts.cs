using System;
using System.Collections.Generic;

namespace AmonFinalProect.Models
{
    public partial class Carts
    {
        public Carts()
        {
            CartsProducts = new HashSet<CartsProducts>();
        }

        public int CartId { get; set; }
        public Guid CartCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }

        public ICollection<CartsProducts> CartsProducts { get; set; }
    }
}
