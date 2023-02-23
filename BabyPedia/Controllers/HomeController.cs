using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server;
using BabyPedia.Models;
using System.IO;
using System.Web;
using BabyPedia.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BabyPedia.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly BabyPediaContext _babyPediaContext;

    public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager, BabyPediaContext babyPediaContext)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
        _babyPediaContext = babyPediaContext;
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

    [HttpGet("/pedia/childinformation/{id}")]
    public async Task<IActionResult> ChildInformation(long id)
    {
        return View(await _babyPediaContext.Children.FirstOrDefaultAsync(x => x.Id == id));
    }


    [HttpGet("/pedia/immunizationrecord/{id}")]
    public async Task<IActionResult> immunizationrecord(long id)
    {
        return View(await _babyPediaContext.Children.Include(x => x.ImmunizationRecord)
            .FirstOrDefaultAsync(x => x.Id == id));
    }
    

    [HttpGet("/verify/{id}")]
    public async Task<IActionResult> Verification(string id)
    {
        return View(await _babyPediaContext.PartneredPedias.FirstOrDefaultAsync(x => x.Id == id));
    }

    [HttpGet("/user/delete/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        var identityResult = await _userManager.DeleteAsync(user);

        return Redirect($"/admin?message={user.UserName} has been deleted!");
    }

    [HttpGet("/confirm/{id}")]
    public async Task<IActionResult> ConfirmPedia(string id)
    {
        var foundPedia = await _babyPediaContext.PartneredPedias.FirstOrDefaultAsync(x => x.Id == id);
        foundPedia.IsVerified = !foundPedia.IsVerified;
        _babyPediaContext.Update(foundPedia);
        await _babyPediaContext.SaveChangesAsync();

        return Redirect(
            $"/admin?message={foundPedia.UserName} is {(foundPedia.IsVerified ? "Verified!" : "Unverified!")}!"
        );
    }

    [HttpGet("/vaccines/{id}")]
    public async Task<IActionResult> VaccineInformation(long id)
    {
        return View(await _babyPediaContext.Vaccines.FirstOrDefaultAsync(x => x.Id == id));
    }

    [HttpGet("/logout")]
    public async Task<IActionResult> LogoutUser()
    {
        await _signInManager.SignOutAsync();

        return Redirect("/");
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LoginUser([FromForm] string username, [FromForm] string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (await _userManager.IsInRoleAsync(user, "Parent"))
            {
                return Redirect("/parent");
            }

            if (await _userManager.IsInRoleAsync(user, "Pedia"))
            {
                return Redirect("/pedia");
            }

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return Redirect("/admin/");
            }
        }

        return Redirect(
            $"/home/login?error=Bad credentials!");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
