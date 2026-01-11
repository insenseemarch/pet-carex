using System;
using System.Drawing;
using System.Windows.Forms;

namespace PetCareX
{
    public partial class ucNhanVien : UserControl
    {
        public ucNhanVien()
        {
            InitializeComponent();
        }

        // --- HÀM DÙNG CHUNG ĐỂ HIỂN THỊ CÁC TAB CON ---
        private void showNghiepVu(UserControl uc)
        {
            pnlNoiDungNhanVien.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlNoiDungNhanVien.Controls.Add(uc);
            uc.BringToFront();
        }

        // --- 1. TAB TRA CỨU (CN-08) ---
        private void btnTabTraCuu_Click_1(object sender, EventArgs e)
        {
            showNghiepVu(new ucConTraCuu());
            ResetTabColors();
            btnTabTraCuu.ForeColor = Color.Red;
        }

        // --- 2. TAB TẠO LỊCH (CN-07) ---
        private void btnTabTaoLich_Click_1(object sender, EventArgs e)
        {
            showNghiepVu(new ucConTaoLich());
            ResetTabColors();
            btnTabTaoLich.ForeColor = Color.Red;
        }

        // --- 3. TAB LẬP HÓA ĐƠN (CN-14) ---
        private void btnTabLapHoaDon_Click(object sender, EventArgs e)
        {
            showNghiepVu(new ucConLapHoaDon());
            ResetTabColors();
            btnTabLapHoaDon.ForeColor = Color.Red;
        }

        // --- 4. TAB QUẢN LÝ HỒ SƠ KHÁCH HÀNG (CN-05) - MỚI THÊM ---
        private void btnTabQuanLyHoSo_Click(object sender, EventArgs e)
        {
            // Ông cần tạo file UserControl tên là ucConQuanLyHoSo
            showNghiepVu(new ucConQuanLyHoSo());
            ResetTabColors();
            btnTabQuanLyHoSo.ForeColor = Color.Red;
        }
        // --- 5. TAB QUẢN LÝ HỒ SƠ THÚ CƯNG (CN-06) - MỚI THÊM ---
        private void btnTabQuanLyHoSoThuCung_Click(object sender, EventArgs e)
        {
            // Gọi UserControl quản lý thú cưng mà chúng ta vừa code xong
            showNghiepVu(new ucQuanLyHoSoThuCung());
            ResetTabColors();
            btnTabQuanLyHoSoThuCung.ForeColor = Color.Red;
        }

        // Cập nhật lại hàm Reset màu để bao gồm cả nút mới
        private void ResetTabColors()
        {
            btnTabTraCuu.ForeColor = Color.Black;
            btnTabTaoLich.ForeColor = Color.Black;
            btnTabLapHoaDon.ForeColor = Color.Black;
            btnTabQuanLyHoSo.ForeColor = Color.Black;
            btnTabQuanLyHoSoThuCung.ForeColor = Color.Black; 
            btnTabTraCuuTonKho.ForeColor = Color.Black;
            btnTabBaoCaoKhachHang.ForeColor = Color.Black;
        }

        // Các hàm Paint/Click trống giữ nguyên
        private void label1_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }

        private void btnTabTraCuuTonKho_Click(object sender, EventArgs e)
        {
            showNghiepVu(new ucConTraCuuTonKho());

            // 2. Reset màu sắc của tất cả các nút về mặc định
            ResetTabColors();

           
            btnTabTraCuuTonKho.ForeColor = Color.Red;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnTabBaoCaoKhachHang_Click(object sender, EventArgs e)
        {
            showNghiepVu(new ucConBaoCaoKhachHang());
            ResetTabColors();
            btnTabBaoCaoKhachHang.ForeColor = Color.Red;
        }
    }
}