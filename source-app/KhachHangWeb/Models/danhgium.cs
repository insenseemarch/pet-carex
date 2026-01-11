using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public partial class danhgium
{
    public string madanhgia { get; set; } = null!;

    public string madv { get; set; } = null!;

    public string manv { get; set; } = null!;

    public string makh { get; set; } = null!;

    public DateOnly ngaydanhgia { get; set; }

    public int diemchatluongdv { get; set; }

    public int diemthaidonv { get; set; }

    public int mucdohailong { get; set; }

    public string? binhluan { get; set; }

    public virtual ICollection<chitietkhambenh> chitietkhambenhs { get; set; } = new List<chitietkhambenh>();

    public virtual ICollection<chitiettiemphong> chitiettiemphongs { get; set; } = new List<chitiettiemphong>();

    public virtual dichvu madvNavigation { get; set; } = null!;

    public virtual khachhang makhNavigation { get; set; } = null!;

    public virtual nhanvien manvNavigation { get; set; } = null!;
}
