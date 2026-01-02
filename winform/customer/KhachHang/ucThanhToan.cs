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

        double tongTienChuaGiam = 0; // Số tiền lấy từ giỏ hàng

        private void ucThanhToan_Load(object sender, EventArgs e)
        {
            // Bước 1: Xóa sạch hàng cũ và cột cũ (nếu muốn tạo mới hoàn toàn bằng code)
            dgvGioHang.Rows.Clear();

            if (Form1.GioHang == null || Form1.GioHang.Count == 0)
            {
                MessageBox.Show("Cảnh báo: Danh sách Giỏ Hàng đang trống rỗng!");
                return;
            }

            double tong = 0;
            // Bước 2: Đổ dữ liệu từng dòng
            foreach (var sp in Form1.GioHang)
            {
                int n = dgvGioHang.Rows.Add();
                // Thay vì truyền cả cụm, hãy gán trực tiếp vào từng ô (Cells)
                dgvGioHang.Rows[n].Cells["colTen"].Value = sp.TenSP;
                dgvGioHang.Rows[n].Cells["colGia"].Value = sp.GiaSP;
                dgvGioHang.Rows[n].Cells["colSoLuong"].Value = 1;
                dgvGioHang.Rows[n].Cells["colThanhTien"].Value = sp.GiaSP;

                tong += sp.GiaSP;
            }

            // Bước 3: Hiển thị tiền
            lblTien.Text = tong.ToString("N0") + " VNĐ";
            lblThanhTien.Text = tong.ToString("N0") + " VNĐ";
            tongTienChuaGiam = tong;
        }

        // Lưu trữ các mã giảm giá: Tên mã và số tiền giảm (VNĐ)
        Dictionary<string, double> danhSachMaGiam = new Dictionary<string, double>()
        {
            { "PETCARE10", 10000 },
            { "GIAM50K", 50000 },
            { "NHANVIEN", 100000 },
            {"GIAM10", 10000 }
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
            string maNhap = txtMaGiamGia.Text.Trim().ToUpper(); // Lấy mã khách nhập
            double soTienGiam = 0;

            // Kiểm tra xem mã có trong danh sách không
            if (danhSachMaGiam.ContainsKey(maNhap))
            {
                soTienGiam = danhSachMaGiam[maNhap];
                MessageBox.Show($"Áp dụng mã thành công! Bạn được giảm {soTienGiam:N0}đ");
            }
            else if (!string.IsNullOrEmpty(maNhap))
            {
                MessageBox.Show("Mã giảm giá không hợp lệ hoặc đã hết hạn!");
            }

            // Tính toán số tiền cuối cùng
            double soTienPhaiTra = tongTienChuaGiam - soTienGiam;

            // Đảm bảo số tiền không bị âm
            if (soTienPhaiTra < 0) soTienPhaiTra = 0;

            // Hiển thị lên giao diện
            lblThanhTien.Text = soTienPhaiTra.ToString("N0") + " VNĐ";
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            Form1 f = (Form1)this.FindForm();
            f?.Navigation(new ucMuaSanPham());
        }
    }
}
