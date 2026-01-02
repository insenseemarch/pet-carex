using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PetCareX
{
    public partial class ucQuanLy : UserControl
    {
        public ucQuanLy()
        {
            InitializeComponent();
        }

        // Hàm hỗ trợ nạp các UserControl thống kê vào vùng nội dung chính
        private void showNghiepVu(UserControl uc)
        {
            pnlNoiDungQuanLy.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlNoiDungQuanLy.Controls.Add(uc);
            uc.BringToFront();
        }


        // 1. Sự kiện Click cho Doanh Thu Bác Sĩ
        private void btnTkBacSi_Click(object sender, EventArgs e)
        {
            showNghiepVu(new ucTkBacSi());

            ResetTabColors();
            btnTkBacSi.ForeColor = Color.Red; // Đánh dấu tab đang chọn
        }

        // 2. Sự kiện Click cho Số Lượt Khám
        private void btnTkLuotKham_Click(object sender, EventArgs e)
        {
            showNghiepVu(new ucTkLuotKham());

            ResetTabColors();
            btnTkLuotKham.ForeColor = Color.Red;
        }

        // 3. Sự kiện Click cho Doanh Thu Sản Phẩm
        private void btnTkSanPham_Click(object sender, EventArgs e)
        {
            // Lưu ý: Kiểm tra class của file này là tkSanPham hay ucTkSanPham
            showNghiepVu(new ucTkSanPham());

            ResetTabColors();
            btnTkSanPham.ForeColor = Color.Red;
        }

        // 4. Sự kiện Click cho Doanh Thu Chi Nhánh
        private void btnTkChiNhanh_Click(object sender, EventArgs e)
        {
            showNghiepVu(new ucTkChiNhanh());

            ResetTabColors();
            btnTkChiNhanh.ForeColor = Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            showNghiepVu(new ucQuanLyNhanVien());

            // 2. Reset màu tất cả các nút về đen
            ResetTabColors();

            // 3. Đổi màu nút hiện tại sang đỏ để đánh dấu
            btnQuanLyNhanVien.ForeColor = Color.Red;
        }
        private void ResetTabColors()
        {
            btnTkLuotKham.ForeColor = Color.Black;
            btnTkBacSi.ForeColor = Color.Black;
            btnTkSanPham.ForeColor = Color.Black;
            btnTkChiNhanh.ForeColor = Color.Black;
            btnQuanLyNhanVien.ForeColor = Color.Black;
            button1.ForeColor = Color.Black;
            btnQuanLyTiem.ForeColor = Color.Black;
            btnPTDVSP.ForeColor = Color.Black;
            btnPTBCNSVL.ForeColor = Color.Black;
            btnCauHinhTD.ForeColor = Color.Black;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            showNghiepVu(new ucQuanLyDanhMuc());

            // 2. Reset màu tất cả các nút về đen
            ResetTabColors();

            // 3. Đổi màu nút hiện tại sang đỏ để đánh dấu
            button1.ForeColor = Color.Red;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void btnPTDVSP_Click(object sender, EventArgs e)
        {
            // Gọi UserControl phân tích mà chúng ta đã viết code LINQ
            showNghiepVu(new ucPhanTichDVSP());

            ResetTabColors();
            btnPTDVSP.ForeColor = Color.Red;
        }
        private void btnQuanLyTiem_Click(object sender, EventArgs e)
        {
            showNghiepVu(new ucQuanLyTiem());

            // 2. Reset màu tất cả các nút về đen
            ResetTabColors();

            // 3. Đổi màu nút hiện tại sang đỏ để đánh dấu
            btnQuanLyTiem.ForeColor = Color.Red;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
        private void btnPTBCNSVL_Click(object sender, EventArgs e)
        {
            // Gọi UserControl phân tích mà chúng ta đã viết code LINQ
            showNghiepVu(new ucPhanTichBaoCaoNhanSuVaLuong());

            ResetTabColors();
            btnPTBCNSVL.ForeColor = Color.Red;
        }

        private void btnCauHinhTD_Click(object sender, EventArgs e)
        {
            showNghiepVu(new ucCauHinhTichDiemVaHanMuc());

            // 2. Reset màu tất cả các nút về đen
            ResetTabColors();

            btnCauHinhTD.ForeColor = Color.Red;
        }
    }
}