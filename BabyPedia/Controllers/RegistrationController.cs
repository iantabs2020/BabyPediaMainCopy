using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BabyPedia.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult ParentRegistration()
        {
            return View();
        }

        public IActionResult PediaRegistration()
        {
            return View();
        }

        public IActionResult DataPrivacy()
        {
            return View();
        }

    }
}