using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server;
using BabyPedia.Models;
using System.IO;
using System.Web;

namespace BabyPedia.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Home()
    {
        return View();
    }

    public IActionResult Services()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
    public IActionResult ContactUs()
    {
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }
    public IActionResult Login()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

