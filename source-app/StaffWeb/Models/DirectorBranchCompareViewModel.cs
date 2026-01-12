namespace StaffWeb.Models;

public class DirectorBranchCompareViewModel
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public List<BranchOption> Branches { get; set; } = new();
    public List<string> SelectedBranchIds { get; set; } = new();
    public List<RevenueByBranch> Revenue { get; set; } = new();
    public List<BranchServiceRevenueRow> ServiceRevenue { get; set; } = new();
    public List<BranchProductRevenueRow> ProductRevenue { get; set; } = new();
}

public class BranchServiceRevenueRow
{
    public string BranchId { get; set; } = "";
    public string BranchName { get; set; } = "";
    public decimal ExamRevenue { get; set; }
    public decimal VaccinationRevenue { get; set; }
    public decimal TotalRevenue => ExamRevenue + VaccinationRevenue;
}
