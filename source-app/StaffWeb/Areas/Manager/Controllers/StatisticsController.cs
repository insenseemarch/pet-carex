using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using System.Data;
using System.Security.Claims;

namespace StaffWeb.Areas.Manager.Controllers;

[Area("Manager")]
[Authorize(Roles = "Manager")]
public class StatisticsController : Controller
{
    private readonly AppDbContext _db;

    public StatisticsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var manv = User.FindFirstValue("MaNhanVien");
        var macn = await _db.nhanviens.AsNoTracking()
            .Where(x => x.manv == manv)
            .Select(x => x.macn)
            .FirstOrDefaultAsync();

        if (string.IsNullOrWhiteSpace(macn))
        {
            return NotFound("Chi nhánh không tìm thấy");
        }

        var vm = new BranchStatisticsViewModel
        {
            BranchId = macn,
            BranchName = await _db.chinhanhs.AsNoTracking()
                .Where(x => x.macn == macn)
                .Select(x => x.tencn)
                .FirstOrDefaultAsync() ?? "",
            VaccinatedPets = await GetVaccinatedPets(macn, DateTime.Today.AddDays(-30), DateTime.Today),
            TopVaccines = await GetTopVaccines(macn),
            Inventory = await GetInventory(macn),
            StaffPerformance = await GetStaffPerformance(macn)
        };

        return View(vm);
    }

    public async Task<IActionResult> VaccinatedPets(DateTime? fromDate, DateTime? toDate)
    {
        var manv = User.FindFirstValue("MaNhanVien");
        var macn = await _db.nhanviens.AsNoTracking()
            .Where(x => x.manv == manv)
            .Select(x => x.macn)
            .FirstOrDefaultAsync();

        if (string.IsNullOrWhiteSpace(macn))
        {
            return NotFound("Chi nhánh không tìm thấy");
        }

        var from = fromDate ?? DateTime.Today.AddDays(-30);
        var to = toDate ?? DateTime.Today;

        var pets = await GetVaccinatedPets(macn, from, to);

        ViewBag.FromDate = from;
        ViewBag.ToDate = to;
        ViewBag.BranchId = macn;

        return View(pets);
    }

    private async Task<List<VaccinatedPetRow>> GetVaccinatedPets(string macn, DateTime from, DateTime to)
    {
        var results = new List<VaccinatedPetRow>();
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand("sp_ds_thucung_da_tiem", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.Add(new SqlParameter("@macn", macn));
        cmd.Parameters.Add(new SqlParameter("@tungay", from.Date));
        cmd.Parameters.Add(new SqlParameter("@denngay", to.Date));

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new VaccinatedPetRow
            {
                MaThucung = reader.GetString(0),
                TenThucung = reader.IsDBNull(1) ? "" : reader.GetString(1),
                Loai = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Giong = reader.IsDBNull(3) ? "" : reader.GetString(3),
                NgayTiem = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
            });
        }

        return results;
    }

    private async Task<List<VaccineStatRow>> GetTopVaccines(string macn)
    {
        var results = new List<VaccineStatRow>();
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand("sp_thongke_vacxin", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.Add(new SqlParameter("@macn", macn));

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new VaccineStatRow
            {
                MaVacxin = reader.GetString(0),
                TenVacxin = reader.IsDBNull(1) ? "" : reader.GetString(1),
                SoLanTiem = reader.GetInt32(2)
            });
        }

        return results;
    }

    private async Task<List<InventoryRow>> GetInventory(string macn)
    {
        var results = new List<InventoryRow>();
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand("sp_tonkho_chinhanh", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.Add(new SqlParameter("@macn", macn));

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new InventoryRow
            {
                MaSP = reader.GetString(0),
                TenSP = reader.IsDBNull(1) ? "" : reader.GetString(1),
                LoaiSP = reader.IsDBNull(2) ? "" : reader.GetString(2),
                SoLuongTon = reader.GetInt32(3)
            });
        }

        return results;
    }

    private async Task<List<StaffPerformanceRow>> GetStaffPerformance(string macn)
    {
        var results = new List<StaffPerformanceRow>();
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand("sp_hieusuat_nhanvien", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.Add(new SqlParameter("@macn", macn));

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new StaffPerformanceRow
            {
                MaNV = reader.GetString(0),
                HoTen = reader.IsDBNull(1) ? "" : reader.GetString(1),
                SoHoaDon = reader.GetInt32(2),
                DiemTrungBinh = reader.IsDBNull(3) ? (decimal?)null : reader.GetDecimal(3)
            });
        }

        return results;
    }
}

public class BranchStatisticsViewModel
{
    public string BranchId { get; set; } = "";
    public string BranchName { get; set; } = "";
    public List<VaccinatedPetRow> VaccinatedPets { get; set; } = new();
    public List<VaccineStatRow> TopVaccines { get; set; } = new();
    public List<InventoryRow> Inventory { get; set; } = new();
    public List<StaffPerformanceRow> StaffPerformance { get; set; } = new();
}

public class VaccinatedPetRow
{
    public string MaThucung { get; set; } = "";
    public string TenThucung { get; set; } = "";
    public string Loai { get; set; } = "";
    public string Giong { get; set; } = "";
    public DateTime? NgayTiem { get; set; }
}

public class VaccineStatRow
{
    public string MaVacxin { get; set; } = "";
    public string TenVacxin { get; set; } = "";
    public int SoLanTiem { get; set; }
}

public class InventoryRow
{
    public string MaSP { get; set; } = "";
    public string TenSP { get; set; } = "";
    public string LoaiSP { get; set; } = "";
    public int SoLuongTon { get; set; }
}

public class StaffPerformanceRow
{
    public string MaNV { get; set; } = "";
    public string HoTen { get; set; } = "";
    public int SoHoaDon { get; set; }
    public decimal? DiemTrungBinh { get; set; }
}
