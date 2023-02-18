using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyPedia.Data;
using BabyPedia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabyPedia.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly BabyPediaContext _dbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegistrationController(BabyPediaContext dbContext,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult ParentRegistration()
        {
            return View();
        }

        [HttpPost("register-parent")]
        public async Task<IActionResult> RegisterParent(Parent parent, [FromForm] string password)
        {
            var roleName = "Parent";

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var role = new IdentityRole(roleName);
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    return Redirect(
                        $"/Registration/ParentRegistration?error=Failed to create role for parent!");
                }
            }

            var signInResult = await _userManager.CreateAsync(parent, password);

            if (signInResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(parent, roleName);
                await _signInManager.SignInAsync(parent, true);

                return Redirect("/home/login");
            }
            else
            {
                return Redirect(
                    $"/Registration/ParentRegistration?error={signInResult.Errors.FirstOrDefault().Description}");
            }
        }

        [HttpPost("/register-appointment")]
        public async Task<IActionResult> RegisterAppointment(
            [FromForm] string date,
            [FromForm] string time,
            [FromForm] string description,
            [FromForm] string appointmentType,
            [FromForm] string pedia,
            [FromForm] double paid,
            [FromForm] string child
        )
        {
            var userId = _userManager.GetUserId(User);

            var selectedPedia =
                await _dbContext.PartneredPedias.FirstOrDefaultAsync(x => x.Id == pedia);

            var currentParent =
                await _dbContext.Parents.FirstOrDefaultAsync(x => x.Id == userId);

            var selectedChild =
                await _dbContext.Children.FirstOrDefaultAsync(x =>
                    x.Parent.Id == currentParent.Id && child == x.UserName);

            if (selectedChild is null)
            {
                selectedChild = new Child()
                {
                    UserName = child,
                    Parent = currentParent
                };

                await _dbContext.Children.AddAsync(selectedChild);
            }

            AppointmentType appointmentTypeObject;

            if (await _dbContext.AppointmentTypes.FirstOrDefaultAsync(x =>
                    x.Name == appointmentType) is AppointmentType appointmentTypeCheck)
            {
                appointmentTypeObject = appointmentTypeCheck;
            }
            else
            {
                appointmentTypeObject = (await _dbContext.AppointmentTypes.AddAsync(new AppointmentType()
                {
                    Name = appointmentType
                })).Entity;
            }

            var payment = new AppointmentPayment()
            {
                Amount = paid,
                Status = "Paid"
            };

            payment = (await _dbContext.AppointmentPayments.AddAsync(payment)).Entity;

            var appointment = new Appointment()
            {
                Name = new Guid().ToString(),
                AppointmentTime = time,
                AppointmentDate = date,
                Payment = payment,
                AppointmentType = appointmentTypeObject,
                Parent = currentParent,
                Pedia = selectedPedia,
                Child = selectedChild
            };

            appointment = (await _dbContext.Appointments.AddAsync(appointment)).Entity;

            currentParent.Appointments.Add(appointment);
            selectedPedia.Appointments.Add(appointment);
            selectedChild.Appointments.Add(appointment);

            _dbContext.Update(currentParent);
            _dbContext.Update(selectedPedia);

            await _dbContext.SaveChangesAsync();

            return Redirect("/parent/appointmentlist");
        }

        [HttpPost("/register-pedia")]
        public async Task<IActionResult> RegisterPedia(PartneredPedia parent, [FromForm] string password)
        {
            var roleName = "Pedia";

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var role = new IdentityRole(roleName);
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    return Redirect(
                        $"/Registration/ParentRegistration?error=Failed to create role for parent!");
                }
            }

            var signInResult = await _userManager.CreateAsync(parent, password);

            if (signInResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(parent, roleName);
                await _signInManager.SignInAsync(parent, true);

                return Redirect("/home/login");
            }
            else
            {
                return Redirect(
                    $"/Registration/PediaRegistration?error={signInResult.Errors.FirstOrDefault().Description}");
            }
        }

        public IActionResult PediaRegistration()
        {
            return View();
        }
    }
}