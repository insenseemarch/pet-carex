using System.Collections.Generic;

namespace KhachHangWeb.Models;

public class CheckoutViewModel
{
    public List<CartItem> Items { get; set; } = new();
    public List<BranchOption> Branches { get; set; } = new();
    public string SelectedBranch { get; set; } = "all";
    public string PaymentMethod { get; set; } = "Tiền mặt";
    public string PromoCode { get; set; } = "";
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public decimal Payable { get; set; }
    public string? DefaultPetId { get; set; }
    public string? DefaultPetName { get; set; }
}

public class BranchOption
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
}
