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
    public partial class ucThongTinThuCung : UserControl
    {
        public ucThongTinThuCung()
        {
            InitializeComponent();
            LamMoiKetQua(); // Đưa các label về trạng thái trống khi vừa mở trang
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maPet = txtMaPet.Text.Trim();

            if (string.IsNullOrEmpty(maPet))
            {
                MessageBox.Show("Vui lòng nhập Mã Thú Cưng!");
                return;
            }

            // GIẢ LẬP DỮ LIỆU (Sau này bạn thay đoạn này bằng câu lệnh SQL)
            if (maPet == "PET001")
            {
                HienThiKetQua("Milo", "Chó Golden", "12/10/2022", "Khỏe mạnh", "Đực");
            }
            else if (maPet == "PET002")
            {
                HienThiKetQua("Luna", "Mèo Anh lông ngắn", "05/02/2023", "Đang theo dõi", "Cái");
            }
            else
            {
                MessageBox.Show("Không tìm thấy thú cưng có mã: " + maPet);
                LamMoiKetQua();
            }
        }

        // Hàm gán dữ liệu vào các Label bên phải
        private void HienThiKetQua(string ten, string loai, string ngaySinh, string tinhTrang, string gioiTinh)
        {
            lblTenTC.Text = "Tên Thú Cưng: " + ten;
            lblLoaiTC.Text = "Loài Thú Cưng: " + loai;
            lblNgaySinh.Text = "Ngày Sinh: " + ngaySinh;
            lblTinhTrang.Text = "Tình Trạng Sức Khỏe: " + tinhTrang;
            lblGioiTinh.Text = "Giới Tính: " + gioiTinh;

            // Chỉnh màu sắc để nổi bật kết quả
            lblTenTC.ForeColor = Color.Blue;
        }

        private void LamMoiKetQua()
        {
            lblTenTC.Text = "Tên Thú Cưng: ---";
            lblLoaiTC.Text = "Loài Thú Cưng: ---";
            lblNgaySinh.Text = "Ngày Sinh: ---";
            lblTinhTrang.Text = "Tình Trạng Sức Khỏe: ---";
            lblGioiTinh.Text = "Giới Tính: ---";
        }

        private void grBOutput_Enter(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 f1 = (Form1)this.FindForm();
            f1.hienThiTrang(new ucThongTin()); // Trở về menu 2 ô
        }
    }
}
