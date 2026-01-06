using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public class BookingViewModel
{
    public string? BranchId { get; set; }
    public string? PetId { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
    public string? Slot { get; set; }
    public string? DoctorId { get; set; }
    public string? DoctorName { get; set; }
    public string? Note { get; set; }

    public List<SelectOption> Branches { get; set; } = new();
    public List<SelectOption> Pets { get; set; } = new();
    public List<SelectOption> Doctors { get; set; } = new();
    public List<SelectOption> Slots { get; set; } = new();
}

public class DoctorScheduleViewModel
{
    public string? DoctorId { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
    public List<SelectOption> Doctors { get; set; } = new();
    public List<DoctorScheduleItem> Items { get; set; } = new();
}

public class DoctorScheduleItem
{
    public DateTime Time { get; set; }
    public string? PetId { get; set; }
    public string? PetName { get; set; }
    public string? BranchName { get; set; }
    public string? ServiceName { get; set; }
}

public class SelectOption
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
}
