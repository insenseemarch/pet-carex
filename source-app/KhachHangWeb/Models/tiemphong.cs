using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public partial class tiemphong
{
    public string madv { get; set; } = null!;

    public string? lieuluong { get; set; }

    public virtual ICollection<chitiettiemphong> chitiettiemphongs { get; set; } = new List<chitiettiemphong>();

    public virtual dichvu madvNavigation { get; set; } = null!;

    public virtual tiemgoi? tiemgoi { get; set; }
}
