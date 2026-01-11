using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BACSI
{
    public partial class ucTraCuuVaccine : UserControl
    {
        public ucTraCuuVaccine()
        {
            InitializeComponent();
            LamMoiKetQua(); // Đưa các label về trạng thái trống khi khởi tạo
        }

        // Hàm cập nhật dữ liệu lên giao diện
        private void HienThiKetQua(string ten, string loai, string ngaySX, string ngayHH)
        {
            lblTenVC.Text = "Tên Vaccine: " + ten;
            lblLoaiVC.Text = "Loại Vaccine: " + loai;
            lblNgaySanXuat.Text = "Ngày Sản Xuất: " + ngaySX;
            lblNgayHetHan.Text = "Ngày Hết Hạn: " + ngayHH;

            // Đổi màu để làm nổi bật thông tin tìm thấy
            lblTenVC.ForeColor = Color.Blue;
        }

        private void LamMoiKetQua()
        {
            lblTenVC.Text = "Tên Vaccine: ---";
            lblLoaiVC.Text = "Loại Vaccine: ---";
            lblNgaySanXuat.Text = "Ngày Sản Xuất: ---";
            lblNgayHetHan.Text = "Ngày Hết Hạn: ---";
            lblTenVC.ForeColor = Color.Black;
        }

        // Hỗ trợ nhấn Enter trong TextBox để tìm kiếm
        private void txtMaVaccine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 f1 = (Form1)this.FindForm();
            if (f1 != null)
            {
                f1.hienThiTrang(new ucTraCuuMain()); // Quay lại menu chính có 4 ô chức năng
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maVaccine = txtMaVC.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(maVaccine))
            {
                MessageBox.Show("Vui lòng nhập Mã Vaccine cần tìm!");
                return;
            }

            // GIẢ LẬP DỮ LIỆU (Sau này bạn có thể thay thế bằng truy vấn SQL)
            if (maVaccine == "VAC001")
            {
                HienThiKetQua("Rabisin", "Phòng dại", "01/10/2023", "01/10/2025");
            }
            else if (maVaccine == "VAC002")
            {
                HienThiKetQua("Leptospira", "Phòng xoắn khuẩn", "15/05/2024", "15/05/2026");
            }
            else
            {
                MessageBox.Show("Không tìm thấy Vaccine có mã: " + maVaccine);
                LamMoiKetQua();
            }
        }
    }
}
