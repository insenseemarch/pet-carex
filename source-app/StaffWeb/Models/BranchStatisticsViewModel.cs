using System.Collections.Generic;

namespace StaffWeb.Models;

public class BranchStatisticsViewModel
{
    public string BranchName { get; set; } = "";
    public List<VaccineStatItem> TopVaccines { get; set; } = new();
    public List<StaffPerformanceItem> StaffPerformance { get; set; } = new();
    public List<VaccinatedPetItem> VaccinatedPets { get; set; } = new();
    public List<InventoryItem> Inventory { get; set; } = new();
}

public class VaccineStatItem
{
    public string MaVacxin { get; set; } = "";
    public string TenVacxin { get; set; } = "";
    public int SoLanTiem { get; set; }
}

public class StaffPerformanceItem
{
    public string MaNV { get; set; } = "";
    public string HoTen { get; set; } = "";
    public int SoHoaDon { get; set; }
    public decimal? DiemTrungBinh { get; set; }
}

public class VaccinatedPetItem
{
    public string MaThucung { get; set; } = "";
    public string TenThucung { get; set; } = "";
    public string Loai { get; set; } = "";
    public string Giong { get; set; } = "";
    public DateTime? NgayTiem { get; set; }
}

public class InventoryItem
{
    public string MaSP { get; set; } = "";
    public string TenSP { get; set; } = "";
    public string LoaiSP { get; set; } = "";
    public int SoLuongTon { get; set; }
}
