using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class DirectorRevenueViewModel
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? BranchId { get; set; }
    public string Period { get; set; } = "day";
    public string? BranchName { get; set; }
    public decimal? BranchRevenue { get; set; }
    public List<BranchOption> Branches { get; set; } = new();
    public List<RevenueByBranch> Items { get; set; } = new();
    public List<ManagerRevenueTrendRow> Trend { get; set; } = new();
    public List<RevenueSourceSlice> Sources { get; set; } = new();
    public List<string> CompareBranchIds { get; set; } = new();
    public List<RevenueByBranch> Comparison { get; set; } = new();
}
