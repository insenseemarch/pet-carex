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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Cố định vị trí hiện Form ở giữa màn hình máy tính
            this.StartPosition = FormStartPosition.CenterScreen;

            // Mặc định khi mở app sẽ hiện trang 4 nút Tra cứu
            hienThiTrang(new ucTraCuuMain());
        }

        // HÀM CHUYỂN MÀN HÌNH CHUẨN
        public void hienThiTrang(UserControl uc)
        {
            pnlContent.Controls.Clear();      // Xóa trang cũ
            uc.Dock = DockStyle.Fill;         // Tràn đầy Panel
            pnlContent.Controls.Add(uc);      // Thêm trang mới
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            // 1. Khởi tạo trang Tra Cứu
            ucTraCuuMain trangTraCuu = new ucTraCuuMain();

            // 2. Thiết lập cho nó tràn đầy Panel của bạn
            trangTraCuu.Dock = DockStyle.Fill;


            // 4. Xóa cái cũ, nạp cái mới vào pnlContent
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(trangTraCuu);
        }

        private void pnlInTro_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnXemLichSuKham_Click_1(object sender, EventArgs e)
        {
            hienThiTrang(new ucLichSuKham());
        }

        private void btnBenhAn_Click(object sender, EventArgs e)
        {
            hienThiTrang(new ucBenhAn());
        }

        private void btnKeToa_Click(object sender, EventArgs e)
        {
            hienThiTrang(new ucKeToa());
        }
    }
}
