using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public partial class lichsulamviec
{
    public string manv { get; set; } = null!;

    public DateOnly ngaybdtaicnmoi { get; set; }

    public string? macncu { get; set; }

    public string macnmoi { get; set; } = null!;

    public DateOnly? ngayketthuccncu { get; set; }

    public virtual chinhanh? macncuNavigation { get; set; }

    public virtual chinhanh macnmoiNavigation { get; set; } = null!;

    public virtual nhanvien manvNavigation { get; set; } = null!;
}
