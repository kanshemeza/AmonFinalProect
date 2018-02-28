using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmonFinalProect.Models
{
    public class ProfileModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Fname { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Lname { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Address { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string City { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string State { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Use a 5 or 9 digit zip-code")]
        public int? Zip { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; }
    }
}
