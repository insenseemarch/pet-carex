using System.Collections.Generic;

namespace StaffWeb.Models;

public class DoctorHistoryViewModel
{
    public nhanvien? Doctor { get; set; }
    public List<chitietkhambenh> Exams { get; set; } = new();
    public List<chitiettiemphong> Vaccines { get; set; } = new();
    public DoctorSearchFilter Filter { get; set; } = new();
}
