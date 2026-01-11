using System.Collections.Generic;

namespace KhachHangWeb.Models;

public class CheckoutViewModel
{
    public List<CartItem> Items { get; set; } = new();
    public List<BranchOption> Branches { get; set; } = new();
    public string SelectedBranch { get; set; } = "all";
    public string PaymentMethod { get; set; } = "Tien mat";
    public string PromoCode { get; set; } = "";
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public decimal Payable { get; set; }
    public string? DefaultPetId { get; set; }
    public string? DefaultPetName { get; set; }
    public string MemberTier { get; set; } = "Cơ bản";
    public decimal MemberDiscountPercent { get; set; }
    public decimal MemberDiscountAmount { get; set; }
    public int PointsAvailable { get; set; }
    public int PointsUsed { get; set; }
    public decimal PointsDiscountAmount { get; set; }
    public bool UsePoints { get; set; }
}

public class BranchOption
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
}
