using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class ManagerDoctorViewModel
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public List<DoctorStat> Items { get; set; } = new();
}
