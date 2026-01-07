using System.Collections.Generic;

namespace StaffWeb.Models;

public class PetLookupViewModel
{
    public string? PetId { get; set; }
    public string? Phone { get; set; }
    public thucung? Pet { get; set; }
    public khachhang? Customer { get; set; }
    public List<thucung> PetsByCustomer { get; set; } = new();
}
