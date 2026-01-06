using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public partial class nhanvien
{
    public string manv { get; set; } = null!;

    public string macn { get; set; } = null!;

    public string hoten { get; set; } = null!;

    public DateOnly ngaysinh { get; set; }

    public string? gioitinh { get; set; }

    public DateOnly ngayvaolam { get; set; }

    public string chucvu { get; set; } = null!;

    public decimal luongcoban { get; set; }

    public string loainv { get; set; } = null!;

    public virtual ICollection<chitietkhambenh> chitietkhambenhs { get; set; } = new List<chitietkhambenh>();

    public virtual ICollection<chitiettiemphong> chitiettiemphongs { get; set; } = new List<chitiettiemphong>();

    public virtual ICollection<danhgium> danhgia { get; set; } = new List<danhgium>();

    public virtual ICollection<hoadon> hoadons { get; set; } = new List<hoadon>();

    public virtual ICollection<lichsulamviec> lichsulamviecs { get; set; } = new List<lichsulamviec>();

    public virtual chinhanh macnNavigation { get; set; } = null!;

    public virtual taikhoannhanvien? taikhoannhanvien { get; set; }
}
