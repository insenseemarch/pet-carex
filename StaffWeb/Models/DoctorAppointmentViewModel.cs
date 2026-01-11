using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class DoctorAppointmentViewModel
{
    public DateTime Date { get; set; } = DateTime.Today;
    public List<DoctorAppointmentItem> Pending { get; set; } = new();
    public List<DoctorAppointmentItem> All { get; set; } = new();
}

public class DoctorAppointmentItem
{
    public string PetId { get; set; } = "";
    public string PetName { get; set; } = "";
    public string ServiceId { get; set; } = "";
    public string ServiceName { get; set; } = "";
    public string ServiceType { get; set; } = "";
    public DateTime VisitTime { get; set; }
    public string? Notes { get; set; }
    public bool IsPending { get; set; }
}
