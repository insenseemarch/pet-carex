using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public partial class chitietmuasanpham
{
    public string mahd { get; set; } = null!;

    public string masp { get; set; } = null!;

    public int soluong { get; set; }

    public decimal thanhtien { get; set; }

    public virtual hoadon mahdNavigation { get; set; } = null!;

    public virtual sanpham maspNavigation { get; set; } = null!;
}
