using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using StaffWeb.Models;

namespace StaffWeb.Areas.Staff.Controllers;

[Area("Staff")]
[Authorize(Roles = "Staff")]
public class PetsController : Controller
{
    private readonly AppDbContext _db;

    public PetsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? petId, string? phone)
    {
        var vm = new PetLookupViewModel
        {
            PetId = petId,
            Phone = phone
        };

        if (!string.IsNullOrWhiteSpace(petId))
        {
            vm.Pet = await _db.thucungs
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.mathucung == petId);
            if (vm.Pet != null)
            {
                var info = await LoadCustomerInfoByMaKhAsync(vm.Pet.makh);
                vm.CustomerId = info?.MaKh;
                vm.CustomerName = info?.Name;
                vm.CustomerPhone = info?.Phone;
            }
        }

        if (!string.IsNullOrWhiteSpace(phone))
        {
            var info = await LoadCustomerInfoByPhoneAsync(phone);
            if (info == null)
            {
                vm.IsNewCustomer = true;
            }
            else
            {
                vm.CustomerId = info.MaKh;
                vm.CustomerName = info.Name;
                vm.CustomerPhone = info.Phone;
            }

            if (!string.IsNullOrWhiteSpace(vm.CustomerId))
            {
                vm.PetsByCustomer = await _db.thucungs
                    .AsNoTracking()
                    .Where(x => x.makh == vm.CustomerId)
                    .OrderBy(x => x.tenthucung)
                    .ToListAsync();
            }
        }

        return View(vm);
    }

    private sealed class CustomerInfo
    {
        public string? MaKh { get; init; }
        public string? Name { get; init; }
        public string? Phone { get; init; }
    }

    private async Task<CustomerInfo?> LoadCustomerInfoByMaKhAsync(string makh)
    {
        var connectionString = _db.Database.GetConnectionString();
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = _db.Database.GetDbConnection().ConnectionString;
        }

        await using var conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString);
        await conn.OpenAsync();

        await using var cmd = conn.CreateCommand();
        cmd.CommandText =
            "SELECT kh.makh, kh.sdt, tt.hoten " +
            "FROM khachhang kh " +
            "LEFT JOIN thongtin tt ON kh.cccd = tt.cccd " +
            "WHERE kh.makh = @makh";

        var param = cmd.CreateParameter();
        param.ParameterName = "@makh";
        param.Value = makh;
        cmd.Parameters.Add(param);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new CustomerInfo
        {
            MaKh = reader.IsDBNull(0) ? null : reader.GetString(0),
            Phone = reader.IsDBNull(1) ? null : reader.GetString(1),
            Name = reader.IsDBNull(2) ? null : reader.GetString(2)
        };
    }

    private async Task<CustomerInfo?> LoadCustomerInfoByPhoneAsync(string phone)
    {
        var connectionString = _db.Database.GetConnectionString();
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = _db.Database.GetDbConnection().ConnectionString;
        }

        await using var conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString);
        await conn.OpenAsync();

        await using var cmd = conn.CreateCommand();
        cmd.CommandText =
            "SELECT kh.makh, kh.sdt, tt.hoten " +
            "FROM khachhang kh " +
            "LEFT JOIN thongtin tt ON kh.cccd = tt.cccd " +
            "WHERE kh.sdt = @phone";

        var param = cmd.CreateParameter();
        param.ParameterName = "@phone";
        param.Value = phone;
        cmd.Parameters.Add(param);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new CustomerInfo
        {
            MaKh = reader.IsDBNull(0) ? null : reader.GetString(0),
            Phone = reader.IsDBNull(1) ? null : reader.GetString(1),
            Name = reader.IsDBNull(2) ? null : reader.GetString(2)
        };
    }
}
