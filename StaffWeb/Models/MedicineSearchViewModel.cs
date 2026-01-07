using System.Collections.Generic;

namespace StaffWeb.Models;

public class MedicineSearchViewModel
{
    public string? Query { get; set; }
    public List<sanpham> Medicines { get; set; } = new();
}
