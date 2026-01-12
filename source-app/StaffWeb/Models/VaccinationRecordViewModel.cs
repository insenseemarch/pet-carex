using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class VaccinationRecordViewModel
{
    public string Mode { get; set; } = "Le";
    public string? PetId { get; set; }
    public string? VaccineId { get; set; }
    public DateTime? Date { get; set; } = DateTime.Now;
    public string? ServiceId { get; set; }

    public int? SelectedPendingStt { get; set; }
    public string? SelectedPendingServiceId { get; set; }
    public string? NewPackageServiceId { get; set; }
    public DateTime? PackageStartDate { get; set; } = DateTime.Now;

    public List<tiemphong> Services { get; set; } = new();
    public List<VaccinationPackageOption> PackageOptions { get; set; } = new();
    public List<VaccinationPendingItem> PendingPackages { get; set; } = new();
    public List<vacxin> Vaccines { get; set; } = new();
}

public class VaccinationPackageOption
{
    public string ServiceId { get; set; } = "";
    public string Name { get; set; } = "";
    public int Months { get; set; }
}

public class VaccinationPendingItem
{
    public int Stt { get; set; }
    public string ServiceId { get; set; } = "";
    public string ServiceName { get; set; } = "";
    public string VaccineId { get; set; } = "";
    public string VaccineName { get; set; } = "";
    public DateTime? PlannedDate { get; set; }
    public string? Status { get; set; }
}
