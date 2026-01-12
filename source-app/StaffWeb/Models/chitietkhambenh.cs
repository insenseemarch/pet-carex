using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public partial class chitietkhambenh
{
    public string madv { get; set; } = null!;

    public string mathucung { get; set; } = null!;

    public DateTime ngaysudung { get; set; }

    public string mabs { get; set; } = null!;

    public string? trieuchung { get; set; }

    public string? chandoan { get; set; }

    public string? matoathuoc { get; set; }

    public DateOnly? ngaytaikham { get; set; }

    public string? madanhgia { get; set; }

    public string? ghichu { get; set; }

    public virtual nhanvien mabsNavigation { get; set; } = null!;

    public virtual danhgium? madanhgiaNavigation { get; set; }

    public virtual dichvu madvNavigation { get; set; } = null!;

    public virtual thucung mathucungNavigation { get; set; } = null!;

    public virtual toathuoc? matoathuocNavigation { get; set; }
}
