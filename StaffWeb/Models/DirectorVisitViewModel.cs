using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class DirectorVisitViewModel
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? BranchId { get; set; }
    public List<BranchOption> Branches { get; set; } = new();
    public List<DirectorVisitRow> Items { get; set; } = new();
}
