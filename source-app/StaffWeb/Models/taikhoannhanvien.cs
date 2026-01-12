using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public partial class taikhoannhanvien
{
    public string tendangnhap { get; set; } = null!;

    public string manv { get; set; } = null!;

    public string matkhau { get; set; } = null!;

    public string vaitro { get; set; } = null!;

    public string trangthai { get; set; } = null!;

    public virtual nhanvien manvNavigation { get; set; } = null!;
}
