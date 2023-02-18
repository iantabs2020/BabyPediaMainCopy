using Microsoft.AspNetCore.Identity;

namespace BabyPedia.Models;

public class Child : IdentityUser
{
    public string? PlaceOfBirth { get; set; }

    public double? BirthHeight { get; set; }

    public double? BirthWeight { get; set; }

    public string? Sex { get; set; }

    public DateTime DateTimeCreated { get; set; } = DateTime.Now;

    public ImmunizationRecord? ImmunizationRecord { get; set; }

    public Parent Parent { get; set; }

    public List<Appointment> Appointments { get; set; } = new List<Appointment>();
}