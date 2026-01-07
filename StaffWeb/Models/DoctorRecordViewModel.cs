using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class DoctorRecordViewModel
{
    public string? PetId { get; set; }
    public string? ServiceId { get; set; }
    public DateTime? VisitTime { get; set; }
    public string? Symptoms { get; set; }
    public string? Diagnosis { get; set; }
    public DateTime? RecheckDate { get; set; }
    public string? Notes { get; set; }

    public string? PrescriptionName { get; set; }
    public List<PrescriptionItemViewModel> Items { get; set; } = new();

    public List<dichvu> Services { get; set; } = new();
    public List<sanpham> Medicines { get; set; } = new();
}
