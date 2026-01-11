using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class ManagerProductViewModel
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int Top { get; set; } = 10;
    public List<ProductStat> Items { get; set; } = new();
    public List<ProductCategorySlice> Categories { get; set; } = new();
}

public class ProductCategorySlice
{
    public string Name { get; set; } = "";
    public decimal Value { get; set; }
}
