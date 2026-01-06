using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachHang
{
    public partial class ucThanhToan : UserControl
    {
        public ucThanhToan()
        {
            InitializeComponent();
        }

        decimal tongTienChuaGiam = 0m; // Số tiền lấy từ giỏ hàng

        private void ucThanhToan_Load(object sender, EventArgs e)
        {
            // Bước 1: Xóa sạch hàng cũ và cột cũ (nếu muốn tạo mới hoàn toàn bằng code)
            dgvGioHang.Rows.Clear();

            if (Form1.GioHang == null || Form1.GioHang.Count == 0)
            {
                MessageBox.Show("Cảnh báo: Danh sách Giỏ Hàng đang trống rỗng!");
                return;
            }

            decimal tong = 0m;

            foreach (var sp in Form1.GioHang)
            {
                int n = dgvGioHang.Rows.Add();

                dgvGioHang.Rows[n].Cells["colTen"].Value = sp.TenSP;
                dgvGioHang.Rows[n].Cells["colGia"].Value = sp.GiaSP;

                int sl = sp.SoLuong;                 // <-- lấy số lượng từ model
                decimal thanhTien = sp.GiaSP * sl;   // <-- tính thành tiền

                dgvGioHang.Rows[n].Cells["colSoLuong"].Value = sl;
                dgvGioHang.Rows[n].Cells["colThanhTien"].Value = thanhTien;

                tong += thanhTien;
            }

            lblTien.Text = tong.ToString("N0") + " VNĐ";
            lblThanhTien.Text = tong.ToString("N0") + " VNĐ";
            tongTienChuaGiam = tong;
        }

        // Lưu trữ các mã giảm giá: Tên mã và số tiền giảm (VNĐ)
        Dictionary<string, decimal> danhSachMaGiam = new Dictionary<string, decimal>()
        {
            { "PETCARE10", 10000m },
            { "GIAM50K", 50000m },
            { "NHANVIEN", 100000m },
            { "GIAM10", 10000m }
        };

        private void lblTienPhaiTra_Click(object sender, EventArgs e)
        {

        }

        private void tbMaGiamGia_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            Form1 f = (Form1)this.FindForm();
            f?.Navigation(new ucMuaSanPham());
        }

        private void x(object sender, MouseEventArgs e)
        {

        }

        private void btnApDung_Click(object sender, EventArgs e)
        {
            string maNhap = txtMaGiamGia.Text.Trim().ToUpper();
            decimal soTienGiam = 0m;

            if (danhSachMaGiam.ContainsKey(maNhap))
            {
                soTienGiam = danhSachMaGiam[maNhap];
                MessageBox.Show($"Áp dụng mã thành công! Bạn được giảm {soTienGiam:N0}đ");
            }
            else if (!string.IsNullOrEmpty(maNhap))
            {
                MessageBox.Show("Mã giảm giá không hợp lệ hoặc đã hết hạn!");
            }

            decimal soTienPhaiTra = tongTienChuaGiam - soTienGiam;
            if (soTienPhaiTra < 0m) soTienPhaiTra = 0m;

            lblThanhTien.Text = soTienPhaiTra.ToString("N0") + " VNĐ";
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            Form1 f = (Form1)this.FindForm();
            f?.Navigation(new ucMuaSanPham());
        }
    }
}
