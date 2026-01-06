using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public partial class chinhanh
{
    public string macn { get; set; } = null!;

    public string tencn { get; set; } = null!;

    public string diachi { get; set; } = null!;

    public string sdt { get; set; } = null!;

    public TimeOnly giomocua { get; set; }

    public TimeOnly giodongcua { get; set; }

    public virtual ICollection<hoadon> hoadons { get; set; } = new List<hoadon>();

    public virtual ICollection<lichsulamviec> lichsulamviecmacncuNavigations { get; set; } = new List<lichsulamviec>();

    public virtual ICollection<lichsulamviec> lichsulamviecmacnmoiNavigations { get; set; } = new List<lichsulamviec>();

    public virtual ICollection<nhanvien> nhanviens { get; set; } = new List<nhanvien>();

    public virtual ICollection<dichvu> madvs { get; set; } = new List<dichvu>();

    public virtual ICollection<sanpham> masps { get; set; } = new List<sanpham>();
}
