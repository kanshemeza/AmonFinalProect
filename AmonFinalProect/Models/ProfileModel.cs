using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmonFinalProect.Models
{
    public class ProfileModel
    {
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string fname { get; set; }
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string lname { get; set; }
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string address { get; set; }
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string city { get; set; }
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string state { get; set; }
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public int? zip { get; set; }
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string email { get; set; }
    }
}
