namespace StaffWeb.Models;

public class BranchProductRevenueRow
{
    public string BranchId { get; set; } = "";
    public string BranchName { get; set; } = "";
    public decimal TotalRevenue { get; set; }
    public int TotalQuantity { get; set; }
}
