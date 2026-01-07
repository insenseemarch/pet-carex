using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class ManagerVisitViewModel
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public List<VisitStat> Items { get; set; } = new();
}
