using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public partial class taikhoankhachhang
{
    public string tendangnhap { get; set; } = null!;

    public string makh { get; set; } = null!;

    public string matkhau { get; set; } = null!;

    public int diemtichluy { get; set; }

    public string capbac { get; set; } = null!;

    public string trangthai { get; set; } = null!;

    public virtual khachhang makhNavigation { get; set; } = null!;
}
