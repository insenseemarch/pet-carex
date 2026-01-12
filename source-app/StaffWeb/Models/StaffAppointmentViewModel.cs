using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class StaffAppointmentViewModel
{
    public string? CustomerPhone { get; set; }
    public string? PetId { get; set; }
    public string? ServiceId { get; set; }
    public string? DoctorId { get; set; }
    public DateTime? VisitTime { get; set; }
    public string? Notes { get; set; }
    public string? SelectedPendingPackageId { get; set; }

    public List<thucung> Pets { get; set; } = new();
    public List<dichvu> Services { get; set; } = new();
    public List<nhanvien> Doctors { get; set; } = new();
    public CustomerInfo? Customer { get; set; }
    public List<PendingPackageInfo> PendingPackages { get; set; } = new();
}

public class CustomerInfo
{
    public string? MaKh { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
}

public class PendingPackageInfo
{
    public string ServiceId { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public int CompletedDoses { get; set; }
    public int PendingDoses { get; set; }
    public int NextDoseNumber { get; set; }
    public int CompletedCount { get; set; }
    public int TotalCount { get; set; }
}
