using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public partial class hoadon
{
    public string mahd { get; set; } = null!;

    public string mathucung { get; set; } = null!;

    public string manvlap { get; set; } = null!;

    public string macn { get; set; } = null!;

    public string makh { get; set; } = null!;

    public string? makham { get; set; }

    public string? matiem { get; set; }

    public DateTime ngaylap { get; set; }

    public decimal tongtien { get; set; }

    public decimal khuyenmai { get; set; }

    public decimal? thanhtien { get; set; }

    public string? hinhthucthanhtoan { get; set; }

    public string trangthai { get; set; } = null!;

    public virtual ICollection<chitietmuasanpham> chitietmuasanphams { get; set; } = new List<chitietmuasanpham>();

    public virtual chinhanh macnNavigation { get; set; } = null!;

    public virtual khachhang makhNavigation { get; set; } = null!;

    public virtual dichvu? makhamNavigation { get; set; }

    public virtual nhanvien manvlapNavigation { get; set; } = null!;

    public virtual thucung mathucungNavigation { get; set; } = null!;

    public virtual dichvu? matiemNavigation { get; set; }
}
