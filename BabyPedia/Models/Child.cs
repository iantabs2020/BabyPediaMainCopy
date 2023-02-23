using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BabyPedia.Models;

public class Child
{
    public long Id { get; set; }

    public string? Username => $"{FirstName} {LastName}";
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PlaceOfBirth { get; set; }

    public double? BirthHeight { get; set; }

    public double? BirthWeight { get; set; }

    public string? Sex { get; set; }

    [DataType(DataType.Date)] public DateTime DateTimeCreated { get; set; } = DateTime.Now;

    public List<ImmunizationRecord> ImmunizationRecord { get; set; } =
        new List<ImmunizationRecord>();

    public Parent Parent { get; set; }

    public List<Appointment> Appointments { get; set; } =
        new List<Appointment>();
}
