using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public partial class dichvu
{
    public string madv { get; set; } = null!;

    public string tendv { get; set; } = null!;

    public string loai { get; set; } = null!;

    public decimal gia { get; set; }

    public virtual ICollection<chitietkhambenh> chitietkhambenhs { get; set; } = new List<chitietkhambenh>();

    public virtual ICollection<danhgium> danhgia { get; set; } = new List<danhgium>();

    public virtual ICollection<hoadon> hoadonmakhamNavigations { get; set; } = new List<hoadon>();

    public virtual ICollection<hoadon> hoadonmatiemNavigations { get; set; } = new List<hoadon>();

    public virtual tiemphong? tiemphong { get; set; }

    public virtual ICollection<chinhanh> macns { get; set; } = new List<chinhanh>();
}
