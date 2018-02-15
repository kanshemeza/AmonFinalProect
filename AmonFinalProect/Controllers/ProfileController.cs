using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmonFinalProect.Models;
using Microsoft.AspNetCore.Mvc;


namespace AmonFinalProect.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            ViewData["States"] = new string[] { "Alabama", "Alaska",
                "Arkansas", "Illinois" };
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                //If model state is valid, proceed to the next step.
                return this.RedirectToAction("Index", "Home");
            }

            //Otherwise, redisplay the page!
            ViewData["States"] = new string[] { "Alabama", "Alaska",
                "Arkansas", "Illinois" };
            return View();
        }
    }
}