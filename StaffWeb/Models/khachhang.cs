using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public partial class khachhang
{
    public string makh { get; set; } = null!;

    public string hoten { get; set; } = null!;

    public string sdt { get; set; } = null!;

    public string? email { get; set; }

    public string? cccd { get; set; }

    public string? gioitinh { get; set; }

    public DateOnly? ngaysinh { get; set; }

    public virtual ICollection<danhgium> danhgia { get; set; } = new List<danhgium>();

    public virtual ICollection<hoadon> hoadons { get; set; } = new List<hoadon>();

    public virtual ICollection<taikhoankhachhang> taikhoankhachhangs { get; set; } = new List<taikhoankhachhang>();

    public virtual ICollection<thucung> thucungs { get; set; } = new List<thucung>();
}
