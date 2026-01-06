using System.Collections.Generic;

namespace KhachHangWeb.Models;

public class ProfileViewModel
{
    public khachhang Customer { get; set; } = null!;
    public List<thucung> Pets { get; set; } = new();
}
