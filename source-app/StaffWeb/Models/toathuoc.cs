using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public partial class toathuoc
{
    public string matoathuoc { get; set; } = null!;

    public string tentoathuoc { get; set; } = null!;

    public virtual ICollection<chitietkhambenh> chitietkhambenhs { get; set; } = new List<chitietkhambenh>();

    public virtual ICollection<thuocsudung> thuocsudungs { get; set; } = new List<thuocsudung>();
}
