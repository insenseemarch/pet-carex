using System;

namespace StaffWeb.Models;

public class DoctorSearchFilter
{
    public string? PetId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}
