using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmonFinalProect.Models
{
    public class Review
    {
        public Review()
        {

        }

        public int ID { get; set; }
        public int Rating { get; set; }
        public string Body { get; set; }
        public bool IsApproved { get; set; }

        public Products Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
