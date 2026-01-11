namespace StaffWeb.Models;

public class DirectorBranchCompareViewModel
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public List<BranchOption> Branches { get; set; } = new();
    public List<string> SelectedBranchIds { get; set; } = new();
    public List<RevenueByBranch> Revenue { get; set; } = new();
    public List<BranchProductRevenueRow> ProductRevenue { get; set; } = new();
}
