using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachHang.Common
{
    public static class Session
    {
        public static bool IsLoggedIn { get; private set; }
        public static string TenDangNhap { get; private set; }
        public static string MaKH { get; private set; }
        public static string CapBac { get; private set; }
        public static int DiemTichLuy { get; private set; }

        public static void Login(string tenDangNhap, string maKH, string capBac, int diemTichLuy)
        {
            IsLoggedIn = true;
            TenDangNhap = tenDangNhap;
            MaKH = maKH;
            CapBac = capBac;
            DiemTichLuy = diemTichLuy;
        }

        public static void Logout()
        {
            IsLoggedIn = false;
            TenDangNhap = null;
            MaKH = null;
            CapBac = null;
            DiemTichLuy = 0;
        }
    }
}



