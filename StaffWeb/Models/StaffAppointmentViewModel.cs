using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class StaffAppointmentViewModel
{
    public string? PetId { get; set; }
    public string? ServiceId { get; set; }
    public string? DoctorId { get; set; }
    public DateTime? VisitTime { get; set; }
    public string? Notes { get; set; }

    public List<dichvu> Services { get; set; } = new();
    public List<nhanvien> Doctors { get; set; } = new();
}
