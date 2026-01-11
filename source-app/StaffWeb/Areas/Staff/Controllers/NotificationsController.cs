using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using System.Data;

namespace StaffWeb.Areas.Staff.Controllers;

[Area("Staff")]
[Authorize(Roles = "Staff,Manager,Director")]
public class NotificationsController : Controller
{
    private readonly AppDbContext _db;

    public NotificationsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var vm = new NotificationViewModel
        {
            CustomerBirthdays = await GetCustomerBirthdays(),
            PetBirthdays = await GetPetBirthdays(),
            ExamReminders = await GetExamReminders(),
            VaccinationReminders = await GetVaccinationReminders(),
            InactiveCustomers = await GetInactiveCustomers(180)
        };
        return View(vm);
    }

    private async Task<List<CustomerBirthdayNotification>> GetCustomerBirthdays()
    {
        var results = new List<CustomerBirthdayNotification>();
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand("sp_nhac_sinh_nhat_khach_hang", conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new CustomerBirthdayNotification
            {
                MaKh = reader.GetString(0),
                HoTen = reader.IsDBNull(1) ? "" : reader.GetString(1),
                Sdt = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Email = reader.IsDBNull(3) ? "" : reader.GetString(3)
            });
        }

        return results;
    }

    private async Task<List<PetBirthdayNotification>> GetPetBirthdays()
    {
        var results = new List<PetBirthdayNotification>();
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand("sp_nhac_sinh_nhat_thu_cung", conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new PetBirthdayNotification
            {
                MaThucung = reader.GetString(0),
                TenThucung = reader.IsDBNull(1) ? "" : reader.GetString(1),
                NgaySinh = reader.GetDateTime(2),
                Sdt = reader.IsDBNull(3) ? "" : reader.GetString(3),
                Email = reader.IsDBNull(4) ? "" : reader.GetString(4)
            });
        }

        return results;
    }

    private async Task<List<ExamReminderNotification>> GetExamReminders()
    {
        var results = new List<ExamReminderNotification>();
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand("sp_nhac_tai_kham", conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new ExamReminderNotification
            {
                MaThucung = reader.GetString(0),
                TenThucung = reader.IsDBNull(1) ? "" : reader.GetString(1),
                HoTen = reader.IsDBNull(2) ? "" : reader.GetString(2),
                NgayTaiKham = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)
            });
        }

        return results;
    }

    private async Task<List<VaccinationReminderNotification>> GetVaccinationReminders()
    {
        var results = new List<VaccinationReminderNotification>();
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand("sp_nhac_tiem_phong", conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new VaccinationReminderNotification
            {
                MaThucung = reader.GetString(0),
                TenThucung = reader.IsDBNull(1) ? "" : reader.GetString(1),
                HoTen = reader.IsDBNull(2) ? "" : reader.GetString(2),
                NgayTiem = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)
            });
        }

        return results;
    }

    private async Task<List<InactiveCustomerNotification>> GetInactiveCustomers(int days)
    {
        var results = new List<InactiveCustomerNotification>();
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand("sp_nhac_khach_lau_khong_quay_lai", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.Add(new SqlParameter("@songay", days));

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new InactiveCustomerNotification
            {
                MaKh = reader.GetString(0),
                HoTen = reader.IsDBNull(1) ? "" : reader.GetString(1),
                LanCuoi = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2)
            });
        }

        return results;
    }
}

public class NotificationViewModel
{
    public List<CustomerBirthdayNotification> CustomerBirthdays { get; set; } = new();
    public List<PetBirthdayNotification> PetBirthdays { get; set; } = new();
    public List<ExamReminderNotification> ExamReminders { get; set; } = new();
    public List<VaccinationReminderNotification> VaccinationReminders { get; set; } = new();
    public List<InactiveCustomerNotification> InactiveCustomers { get; set; } = new();
}

public class CustomerBirthdayNotification
{
    public string MaKh { get; set; } = "";
    public string HoTen { get; set; } = "";
    public string Sdt { get; set; } = "";
    public string Email { get; set; } = "";
}

public class PetBirthdayNotification
{
    public string MaThucung { get; set; } = "";
    public string TenThucung { get; set; } = "";
    public DateTime NgaySinh { get; set; }
    public string Sdt { get; set; } = "";
    public string Email { get; set; } = "";
}

public class ExamReminderNotification
{
    public string MaThucung { get; set; } = "";
    public string TenThucung { get; set; } = "";
    public string HoTen { get; set; } = "";
    public DateTime? NgayTaiKham { get; set; }
}

public class VaccinationReminderNotification
{
    public string MaThucung { get; set; } = "";
    public string TenThucung { get; set; } = "";
    public string HoTen { get; set; } = "";
    public DateTime? NgayTiem { get; set; }
}

public class InactiveCustomerNotification
{
    public string MaKh { get; set; } = "";
    public string HoTen { get; set; } = "";
    public DateTime? LanCuoi { get; set; }
}
