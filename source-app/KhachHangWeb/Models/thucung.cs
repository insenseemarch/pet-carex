using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public partial class thucung
{
    public string mathucung { get; set; } = null!;

    public string makh { get; set; } = null!;

    public string tenthucung { get; set; } = null!;

    public string loai { get; set; } = null!;

    public string giong { get; set; } = null!;

    public DateOnly? ngaysinh { get; set; }

    public string? gioitinh { get; set; }

    public string? tinhtrangsuckhoe { get; set; }

    public virtual ICollection<chitietkhambenh> chitietkhambenhs { get; set; } = new List<chitietkhambenh>();

    public virtual ICollection<chitiettiemphong> chitiettiemphongs { get; set; } = new List<chitiettiemphong>();

    public virtual ICollection<hoadon> hoadons { get; set; } = new List<hoadon>();

    public virtual khachhang makhNavigation { get; set; } = null!;
}
