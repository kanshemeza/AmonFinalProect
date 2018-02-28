using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmonFinalProect.Models
{
    public class ApplicationUser: Microsoft.AspNetCore.Identity.IdentityUser
    {
        public ApplicationUser()
        {
            Reviews = new HashSet<Review>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        //A user can author many reviews
        public ICollection<Review> Reviews { get; set; }
    }
}
