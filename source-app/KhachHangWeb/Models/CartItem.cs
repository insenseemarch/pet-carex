namespace KhachHangWeb.Models;

// Model lưu trong session giỏ hàng, không map DB
public class CartItem
{
    public string MaSP { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
}
