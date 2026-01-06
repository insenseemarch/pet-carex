using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public partial class tiemgoi
{
    public string madv { get; set; } = null!;

    public int sothang { get; set; }

    public decimal? phantramgiamgia { get; set; }

    public virtual tiemphong madvNavigation { get; set; } = null!;
}
