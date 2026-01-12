using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachHang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void labelSDT1_Click(object sender, EventArgs e)
        {

        }

        public class Product
        {
            public string TenSP { get; set; }
            public double GiaSP { get; set; }
            public Image AnhSP { get; set; }
        }

        public static List<Product> GioHang = new List<Product>();

        private void linkFB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Đường dẫn bạn muốn mở
            string url = "https://www.facebook.com/thuytien.traan";

            try
            {
                // Lệnh mở trình duyệt mặc định trên máy tính
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });

                // Đổi màu link sang màu "đã thăm" sau khi click
                linkFB.LinkVisited = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở liên kết: " + ex.Message);
            }
        }

        private void txtSearch_MouseEnter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập từ khóa để tìm kiếm...")
            {
                txtSearch.Text = "";           // Xóa chữ xám mờ đi
                txtSearch.ForeColor = Color.Black; // Chuyển màu chữ sang đen để người dùng nhập
            }
        }

        private void txtSearch_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Nhập từ khóa để tìm kiếm..."; // Hiện lại thông báo
                txtSearch.ForeColor = Color.Gray;               // Chuyển lại màu xám mờ
            }
        }

        // Trong Form1.cs
        public void Navigation(UserControl uc)
        {
            panelContent.Controls.Clear(); // pnlContent là cái Panel chính của bạn
            uc.Dock = DockStyle.Fill;
            panelContent.Controls.Add(uc);
            panelContent.Controls[0].Size = panelContent.ClientSize;
        }

        // Trong Form1.cs
        public void SetMenuTitle(string title)
        {
            // Giả sử cái nút "Dịch Vụ" hoặc "Trang Chủ" của bạn là một Button
            // Đoạn code này sẽ đổi chữ trên cái nút đang được chọn hoặc một Label tiêu đề
            btnDichVu.Text = title;

            // Nếu bạn có một cái Label riêng để hiện tên trang đang mở:
            // lblTenTrangHienTai.Text = title;
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            Navigation(new ucTrangChu());
        }

        private void btnMuaHang_Click(object sender, EventArgs e)
        {
            Navigation(new ucMuaSanPham());
        }

        private void btnDatLichHen_Click(object sender, EventArgs e)
        {
            Navigation(new ucDichVu());
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            Navigation(new ucTraCuu());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Navigation(new ucTrangChu());
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogIn_Out_Click(object sender, EventArgs e)
        {
            // Nếu chưa đăng nhập (lblTKKH đang ẩn)
            if (lblTKKH.Visible == false)
            {
                Navigation(new ucLogIn_Out()); // Mở trang login vừa tạo
            }
            else // Nếu đã đăng nhập thì nhấn vào là Logout
            {
                lblTKKH.Visible = false;
                Navigation(new ucTrangChu());
            }
        }

        private void btnTrangCaNhan_Click(object sender, EventArgs e)
        {
            Navigation(new ucTrangCaNhan());
        }
    }

}
