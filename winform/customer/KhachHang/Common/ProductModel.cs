using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace KhachHang.Common
{
    public class ProductModel
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string LoaiSP { get; set; }      // loaisp trong DB
        public decimal GiaSP { get; set; }

        public int SoLuongTon { get; set; }     // NEW
        public DateTime? HSD { get; set; }      // NEW

        public Image AnhSP { get; set; }        // nếu chưa có ảnh thì null
        public int SoLuong { get; set; } = 1;   // dùng cho giỏ hàng
    }
}
