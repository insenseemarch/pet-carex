using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public partial class chitiettiemphong
{
    public int stt { get; set; }

    public string madv { get; set; } = null!;

    public string mathucung { get; set; } = null!;

    public string mavacxin { get; set; } = null!;

    public string mabs { get; set; } = null!;

    public DateTime ngaytiem { get; set; }

    public string? trangthai { get; set; }

    public string? madanhgia { get; set; }

    public virtual nhanvien mabsNavigation { get; set; } = null!;

    public virtual danhgium? madanhgiaNavigation { get; set; }

    public virtual tiemphong madvNavigation { get; set; } = null!;

    public virtual thucung mathucungNavigation { get; set; } = null!;

    public virtual vacxin mavacxinNavigation { get; set; } = null!;
}
