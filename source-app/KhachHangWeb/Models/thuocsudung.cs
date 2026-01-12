using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public partial class thuocsudung
{
    public string matoathuoc { get; set; } = null!;

    public string masp { get; set; } = null!;

    public int soluong { get; set; }

    public string? lieuluong { get; set; }

    public string? ghichu { get; set; }

    public virtual sanpham maspNavigation { get; set; } = null!;

    public virtual toathuoc matoathuocNavigation { get; set; } = null!;
}
