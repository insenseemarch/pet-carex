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

        var items = await (
            from c in _db.chitietkhambenhs.AsNoTracking()
            join dv in _db.dichvus.AsNoTracking() on c.madv equals dv.madv
            join tc in _db.thucungs.AsNoTracking() on c.mathucung equals tc.mathucung
            where c.mabs == manv && c.ngaysudung >= fromDate && c.ngaysudung <= toDate
            orderby c.ngaysudung
            select new DoctorAppointmentItem
            {
                PetId = c.mathucung,
                PetName = tc.tenthucung,
                ServiceId = c.madv,
                ServiceName = dv.tendv,
                VisitTime = c.ngaysudung,
                Notes = c.trieuchung,
                IsPending = string.IsNullOrWhiteSpace(c.chandoan)
            }).ToListAsync();

        var vm = new DoctorAppointmentViewModel
        {
            Date = day,
            Pending = items.Where(x => x.IsPending).ToList(),
            All = items
        };

        return View(vm);
    }
}
