using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public partial class sanpham
{
    public string masp { get; set; } = null!;

    public string tensp { get; set; } = null!;

    public string loaisp { get; set; } = null!;

    public decimal gia { get; set; }

    public int soluongton { get; set; }

    public DateOnly? hsd { get; set; }

    public virtual ICollection<chitietmuasanpham> chitietmuasanphams { get; set; } = new List<chitietmuasanpham>();

    public virtual ICollection<thuocsudung> thuocsudungs { get; set; } = new List<thuocsudung>();

    public virtual ICollection<chinhanh> macns { get; set; } = new List<chinhanh>();
}
