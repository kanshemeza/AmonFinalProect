using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmonFinalProect.Models
{
    public class Order
    {
        public Order()
        {
            LineItems = new HashSet<LineItem>();
        }

        public int ID { get; set; }
        public DateTime SubmittedDate { get; set; }
        public Guid TrackingNumber { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal ShippingTotal { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<LineItem> LineItems { get; set; }
    }
}
