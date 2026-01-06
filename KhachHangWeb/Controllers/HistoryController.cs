using KhachHangWeb.Data;
using KhachHangWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KhachHangWeb.Controllers;

[Authorize]
public class HistoryController : Controller
{
    private readonly AppDbContext _db;
    public HistoryController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Index(string? makh, string? petId)
    {
        var claimMakh = User.FindFirst("MaKhachHang")?.Value;
        if (!string.IsNullOrWhiteSpace(claimMakh))
        {
            if (!string.IsNullOrWhiteSpace(makh) && makh != claimMakh)
            {
                return Forbid();
            }
            makh = claimMakh;
        }

        var vm = new PetHistoryViewModel { CustomerId = makh, PetId = petId };

        if (string.IsNullOrWhiteSpace(makh))
        {
            vm.Message = "Vui lòng nhập mã khách hàng.";
            return View(vm);
        }

        var kh = await _db.khachhangs.AsNoTracking()
            .FirstOrDefaultAsync(k => k.makh == makh);
        if (kh == null)
        {
            vm.Message = "Không tìm thấy khách hàng.";
            return View(vm);
        }
        vm.CustomerName = kh.hoten;

        vm.Pets = await _db.thucungs.AsNoTracking()
            .Where(t => t.makh == makh)
            .OrderBy(t => t.tenthucung)
            .Select(t => new SelectOption { Id = t.mathucung, Name = t.tenthucung })
            .ToListAsync();

        if (string.IsNullOrWhiteSpace(petId))
            vm.PetId = vm.Pets.FirstOrDefault()?.Id;

        if (string.IsNullOrWhiteSpace(vm.PetId))
        {
            vm.Message = "Khách hàng chưa có thú cưng.";
            return View(vm);
        }

        var pet = await _db.thucungs.AsNoTracking()
            .FirstOrDefaultAsync(t => t.mathucung == vm.PetId);
        vm.PetName = pet?.tenthucung;

        vm.Purchases = await _db.hoadons.AsNoTracking()
            .Where(h => h.makh == makh && h.mathucung == vm.PetId)
            .Include(h => h.chitietmuasanphams)
            .ThenInclude(ct => ct.maspNavigation)
            .Include(h => h.macnNavigation)
            .SelectMany(h => h.chitietmuasanphams.Select(ct => new PurchaseHistoryItem
            {
                OrderId = h.mahd,
                OrderDate = h.ngaylap,
                BranchName = h.macnNavigation.tencn,
                ProductName = ct.maspNavigation.tensp,
                Quantity = ct.soluong,
                Amount = ct.thanhtien,
                Status = h.trangthai,
                PaymentMethod = h.hinhthucthanhtoan
            }))
            .OrderByDescending(x => x.OrderDate)
            .ToListAsync();

        vm.Exams = await _db.chitietkhambenhs.AsNoTracking()
            .Where(k => k.mathucung == vm.PetId)
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

        vm.Vaccines = await _db.chitiettiemphongs.AsNoTracking()
            .Where(t => t.mathucung == vm.PetId)
            .Include(t => t.mavacxinNavigation)
            .Include(t => t.mabsNavigation)
            .OrderByDescending(t => t.ngaytiem)
            .Select(t => new VaccineHistoryItem
            {
                Stt = t.stt,
                VaccineId = t.mavacxin,
                VaccineName = t.mavacxinNavigation.tenvacxin,
                DoctorId = t.mabs,
                DoctorName = t.mabsNavigation.hoten,
                Date = t.ngaytiem,
                Status = t.trangthai
            })
            .ToListAsync();

        return View(vm);
    }
}
