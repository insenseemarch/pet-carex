using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public partial class vacxin
{
    public string mavacxin { get; set; } = null!;

    public string tenvacxin { get; set; } = null!;

    public string? loaivacxin { get; set; }

    public DateOnly ngaysanxuat { get; set; }

    public DateOnly ngayhethan { get; set; }

    public virtual ICollection<chitiettiemphong> chitiettiemphongs { get; set; } = new List<chitiettiemphong>();
}
