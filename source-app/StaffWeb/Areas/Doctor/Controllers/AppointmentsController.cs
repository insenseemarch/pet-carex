using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using StaffWeb.Models;

namespace StaffWeb.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = "Doctor")]
public class AppointmentsController : Controller
{
    private readonly AppDbContext _db;

    public AppointmentsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(DateTime? date)
    {
        var manv = User.FindFirstValue("MaNhanVien");
        if (string.IsNullOrWhiteSpace(manv))
        {
            return Forbid();
        }

        var day = (date ?? DateTime.Today).Date;
        var fromDate = day;
        var toDate = day.AddDays(1).AddTicks(-1);

        // Lấy lịch khám bệnh
        var examItems = await (
            from c in _db.chitietkhambenhs.AsNoTracking()
            join dv in _db.dichvus.AsNoTracking() on c.madv equals dv.madv
            join tc in _db.thucungs.AsNoTracking() on c.mathucung equals tc.mathucung
            where c.mabs == manv && c.ngaysudung >= fromDate && c.ngaysudung <= toDate
            select new DoctorAppointmentItem
            {
                PetId = c.mathucung,
                PetName = tc.tenthucung,
                ServiceId = c.madv,
                ServiceName = dv.tendv,
                ServiceType = "Khám bệnh",
                VisitTime = c.ngaysudung,
                Notes = c.trieuchung,
                IsPending = string.IsNullOrWhiteSpace(c.chandoan)
            }).ToListAsync();

        // Lấy lịch tiêm
        var vaccineItems = await (
            from ctp in _db.chitiettiemphongs.AsNoTracking()
            join dv in _db.dichvus.AsNoTracking() on ctp.madv equals dv.madv
            join tc in _db.thucungs.AsNoTracking() on ctp.mathucung equals tc.mathucung
            join vx in _db.vacxins.AsNoTracking() on ctp.mavacxin equals vx.mavacxin
            where ctp.mabs == manv && ctp.ngaytiem >= fromDate && ctp.ngaytiem <= toDate
            select new DoctorAppointmentItem
            {
                PetId = ctp.mathucung,
                PetName = tc.tenthucung,
                ServiceId = ctp.madv,
                // Only display the service name for vaccination items
                ServiceName = dv.tendv,
                ServiceType = "Tiêm phòng",
                VisitTime = ctp.ngaytiem,
                Notes = "",
                IsPending = ctp.trangthai == null || ctp.trangthai == "Chưa tiêm" || ctp.trangthai == "Cho tiem"
            }).ToListAsync();

        // Gộp cả 2 danh sách và sort theo thời gian
        var items = examItems.Concat(vaccineItems).OrderBy(x => x.VisitTime).ToList();

        var vm = new DoctorAppointmentViewModel
        {
            Date = day,
            Pending = items.Where(x => x.IsPending).ToList(),
            All = items
        };

        return View(vm);
    }
}
