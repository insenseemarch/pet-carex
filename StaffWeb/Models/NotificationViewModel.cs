using System;
using System.Collections.Generic;

namespace StaffWeb.Models;

public class NotificationViewModel
{
    public List<CustomerBirthdayItem> CustomerBirthdays { get; set; } = new();
    public List<PetBirthdayItem> PetBirthdays { get; set; } = new();
    public List<ExamReminderItem> ExamReminders { get; set; } = new();
    public List<VaccinationReminderItem> VaccinationReminders { get; set; } = new();
    public List<InactiveCustomerItem> InactiveCustomers { get; set; } = new();
}

public class CustomerBirthdayItem
{
    public string MaKh { get; set; } = "";
    public string HoTen { get; set; } = "";
    public string Sdt { get; set; } = "";
    public string Email { get; set; } = "";
}

public class PetBirthdayItem
{
    public string MaThucung { get; set; } = "";
    public string TenThucung { get; set; } = "";
    public string Sdt { get; set; } = "";
    public string Email { get; set; } = "";
}

public class ExamReminderItem
{
    public string MaThucung { get; set; } = "";
    public string TenThucung { get; set; } = "";
    public string HoTen { get; set; } = "";
    public DateTime? NgayTaiKham { get; set; }
}

public class VaccinationReminderItem
{
    public string MaThucung { get; set; } = "";
    public string TenThucung { get; set; } = "";
    public string HoTen { get; set; } = "";
    public DateTime? NgayTiem { get; set; }
}

public class InactiveCustomerItem
{
    public string MaKh { get; set; } = "";
    public string HoTen { get; set; } = "";
    public DateTime? LanCuoi { get; set; }
}
