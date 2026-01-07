using System.Collections.Generic;

namespace StaffWeb.Models;

public class PrescriptionViewModel
{
    public string? PetId { get; set; }
    public string? PrescriptionName { get; set; }
    public List<PrescriptionItemViewModel> Items { get; set; } = new();
    public List<sanpham> Medicines { get; set; } = new();
}
