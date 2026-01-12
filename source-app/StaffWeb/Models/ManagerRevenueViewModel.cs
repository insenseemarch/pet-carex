using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class ManagerRevenueViewModel
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string Period { get; set; } = "day";
    public List<RevenueByBranch> Items { get; set; } = new();
    public string? BranchId { get; set; }
    public string? BranchName { get; set; }
    public decimal? BranchRevenue { get; set; }
    public List<ManagerRevenueTrendRow> Trend { get; set; } = new();
    public List<RevenueSourceSlice> Sources { get; set; } = new();
}
