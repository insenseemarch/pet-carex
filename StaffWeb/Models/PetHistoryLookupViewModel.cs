using System.Collections.Generic;

namespace StaffWeb.Models;

public class PetHistoryLookupViewModel
{
    public string? PetId { get; set; }
    public thucung? Pet { get; set; }
    public khachhang? Customer { get; set; }
    public List<chitietkhambenh> Exams { get; set; } = new();
    public List<chitiettiemphong> Vaccines { get; set; } = new();
}
