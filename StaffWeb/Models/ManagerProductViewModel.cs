using System.Collections.Generic;

namespace StaffWeb.Models;

public class ManagerProductViewModel
{
    public int Top { get; set; } = 10;
    public List<ProductStat> Items { get; set; } = new();
}
