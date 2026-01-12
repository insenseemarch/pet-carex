using System.Collections.Generic;

namespace StaffWeb.Models;

public class VaccineLookupViewModel
{
    public string? Query { get; set; }
    public string? PetId { get; set; }
    public List<vacxin> Vaccines { get; set; } = new();
    public List<chitiettiemphong> PetVaccines { get; set; } = new();
}
