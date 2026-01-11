using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class DirectorProductViewModel
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? BranchId { get; set; }
    public int Top { get; set; } = 10;
    public List<BranchOption> Branches { get; set; } = new();
    public List<ProductStat> Items { get; set; } = new();
    public decimal? SystemRevenue { get; set; }
    public decimal? BranchRevenue { get; set; }
    public string? BranchName { get; set; }
    public List<ProductCategorySlice> Categories { get; set; } = new();
    public List<string> CompareBranchIds { get; set; } = new();
    public List<BranchProductRevenueRow> BranchTotals { get; set; } = new();
}

