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
    public partial class ucTraCuuThuoc : UserControl
    {

        public ucTraCuuThuoc()
        {
            InitializeComponent();
            this.Size = new Size(1400, 600);
            LamMoiGiaoDien();
        }

        // SỰ KIỆN NÚT QUAY LẠI

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            Form1 f1 = (Form1)this.FindForm();
            f1.hienThiTrang(new ucTraCuuMain()); // Nạp lại trang 4 ô ban đầu
        }

        private void lblMainTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string ma = txtMaThuoc.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng nhập mã thuốc để kiểm tra!");
                return;
            }

            // Giả lập dữ liệu (Bạn sẽ thay bằng SQL sau)
            if (ma == "P123")
            {
                HienThiKetQua("Paracetamol 500mg", "Hạ sốt, giảm đau", "120 vỉ", "15.000 VNĐ");
            }
            else if (ma == "A456")
            {
                HienThiKetQua("Amoxicillin", "Kháng sinh", "45 hộp", "85.000 VNĐ");
            }
            else
            {
                MessageBox.Show("Không tìm thấy mã thuốc: " + ma);
                LamMoiGiaoDien();
            }
        }

        private void HienThiKetQua(string ten, string loai, string ton, string gia)
        {
            lblTenThuoc.Text = "Tên thuốc: " + ten;
            lblLoaiThuoc.Text = "Loại thuốc: " + loai;
            lblSoLuongTon.Text = "Số lượng tồn: " + ton;
            lblGia.Text = "Giá bán: " + gia;

            // Hiện màu sắc nhấn mạnh
            lblTenThuoc.ForeColor = Color.FromArgb(0, 102, 204);
            lblGia.ForeColor = Color.Red;
        }

        private void LamMoiGiaoDien()
        {
            lblTenThuoc.Text = "Tên thuốc: ---";
            lblLoaiThuoc.Text = "Loại thuốc: ---";
            lblSoLuongTon.Text = "Số lượng tồn: ---";
            lblGia.Text = "Giá bán: ---";
            txtMaThuoc.Clear();
            txtMaThuoc.Focus();
        }
    }
}
