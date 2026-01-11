using System.Data;
using KhachHangWeb.Data;
using KhachHangWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KhachHangWeb.Controllers;

[Authorize]
public class HistoryController : Controller
{
    private readonly AppDbContext _db;
    public HistoryController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Index(string? orderId, string? petId)
    {
        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return RedirectToAction("Login", "Account");

        _db.Database.SetCommandTimeout(60);

        var vm = new PetHistoryViewModel
        {
            CustomerId = makh,
            PetId = petId,
            OrderId = orderId
        };

        var customerName = await LoadCustomerNameAsync(makh);
        if (customerName == null)
        {
            vm.Message = "Khong tim thay khach hang.";
            return View(vm);
        }
        vm.CustomerName = string.IsNullOrWhiteSpace(customerName) ? "Khach hang" : customerName;

        vm.Pets = await _db.thucungs.AsNoTracking()
            .Where(t => t.makh == makh)
            .OrderBy(t => t.tenthucung)
            .Select(t => new SelectOption { Id = t.mathucung, Name = t.tenthucung })
            .ToListAsync();

        if (string.IsNullOrWhiteSpace(petId))
        {
            vm.PetId = "";
        }
        else
        {
            var pet = await _db.thucungs.AsNoTracking()
                .FirstOrDefaultAsync(t => t.mathucung == petId);
            vm.PetName = pet?.tenthucung;
        }

        var purchasesQuery =
            from h in _db.hoadons.AsNoTracking()
            join c in _db.chinhanhs.AsNoTracking() on h.macn equals c.macn into cb
            from c in cb.DefaultIfEmpty()
            join ct in _db.chitietmuasanphams.AsNoTracking() on h.mahd equals ct.mahd
            join sp in _db.sanphams.AsNoTracking() on ct.masp equals sp.masp
            where h.makh == makh
            select new PurchaseHistoryItem
            {
                OrderId = h.mahd,
                OrderDate = h.ngaylap,
                BranchName = c != null ? c.tencn : "",
                ProductName = sp.tensp,
                Quantity = ct.soluong,
                Amount = ct.thanhtien,
                Status = h.trangthai,
                PaymentMethod = h.hinhthucthanhtoan
            };

        if (!string.IsNullOrWhiteSpace(orderId))
            purchasesQuery = purchasesQuery.Where(p => p.OrderId == orderId);

        vm.Purchases = await purchasesQuery
            .OrderByDescending(x => x.OrderDate)
            .ToListAsync();

        var petIds = vm.Pets.Select(p => p.Id).ToList();
        var examQuery = _db.chitietkhambenhs.AsNoTracking()
            .Where(k => petIds.Contains(k.mathucung));

        if (!string.IsNullOrWhiteSpace(vm.PetId))
            examQuery = examQuery.Where(k => k.mathucung == vm.PetId);

        vm.Exams = await examQuery
            .Include(k => k.mabsNavigation)
            .Include(k => k.madvNavigation)
            .OrderByDescending(k => k.ngaysudung)
            .Select(k => new ExamHistoryItem
            {
                ServiceId = k.madv,
                ExamDate = k.ngaysudung,
                DoctorId = k.mabs,
                DoctorName = k.mabsNavigation.hoten,
                Symptom = k.trieuchung,
                Diagnosis = k.chandoan,
                RevisitDate = k.ngaytaikham
            })
            .ToListAsync();

        var vaccineBase = _db.chitiettiemphongs.AsNoTracking()
            .Where(t => petIds.Contains(t.mathucung));

        if (!string.IsNullOrWhiteSpace(vm.PetId))
            vaccineBase = vaccineBase.Where(t => t.mathucung == vm.PetId);

        vm.Vaccines = await (
            from c in vaccineBase
            join v in _db.vacxins.AsNoTracking() on c.mavacxin equals v.mavacxin
            join n in _db.nhanviens.AsNoTracking() on c.mabs equals n.manv
            orderby c.ngaytiem descending
            select new VaccineHistoryItem
            {
                Stt = c.stt,
                VaccineId = c.mavacxin,
                VaccineName = v.tenvacxin,
                DoctorId = c.mabs,
                DoctorName = n.hoten,
                Date = c.ngaytiem,
                Status = c.trangthai
            })
            .ToListAsync();

        return View(vm);
    }

    private async Task<string?> LoadCustomerNameAsync(string makh)
    {
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand(
            "SELECT kh.makh, tt.hoten FROM khachhang kh LEFT JOIN thongtin tt ON kh.cccd = tt.cccd WHERE kh.makh = @makh",
            conn
        );
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqlParameter("@makh", makh));

        await using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;
        return reader.IsDBNull(1) ? "" : reader.GetString(1);
    }
}
