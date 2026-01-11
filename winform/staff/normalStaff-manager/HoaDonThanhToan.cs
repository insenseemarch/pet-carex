using System.Collections.Generic;
using System;
using PetCareX;
public class HoaDonThanhToan
{
    public string MaKH { get; set; }
    public string TenNV { get; set; }
    public DateTime NgayLap { get; set; }
    public string TongTien { get; set; }
    public string ThanhTien { get; set; }
    public string HangKH { get; set; }
    public string DiemTichLuy { get; set; } 
    public List<ChiTietMon> DanhSachMon { get; set; } = new List<ChiTietMon>();
}

public class ChiTietMon
{
    public string Ma { get; set; }
    public string Ten { get; set; }
    public int SoLuong { get; set; }
    public double DonGia { get; set; }
}