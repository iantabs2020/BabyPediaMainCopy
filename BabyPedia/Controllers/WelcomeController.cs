using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BabyPedia.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult ParentWelcome()
        {
            return View();
        }

        public IActionResult PediaWelcome()
        {
            return View();
        }
    }
}