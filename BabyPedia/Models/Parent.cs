using Microsoft.AspNetCore.Identity;

namespace BabyPedia.Models;

public class Parent : IdentityUser
{
    public Appointment? Appointment { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public List<Appointment> Appointments { get; set; } = new List<Appointment>();

    public DateTime DateTimeCreated { get; set; } = DateTime.Now;
}