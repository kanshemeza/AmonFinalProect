using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmonFinalProect.Models
{
    public class LineItem
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public Products Product { get; set; }
        public Order Order { get; set; }
    }
}
