using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KhachHang.Common;

namespace KhachHang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void UpdateAuthUI()
        {
            if (Session.IsLoggedIn)
            {
                lblTKKH.Visible = true;
                lblTKKH.Text = "TK Khách hàng: " + Session.TenDangNhap; // hoặc MaKH
                btnLogIn_Out.Text = "Logout";
            }
            else
            {
                lblTKKH.Visible = false;
                lblTKKH.Text = "";
                btnLogIn_Out.Text = "Login/Out";
            }
        }


        private void labelSDT1_Click(object sender, EventArgs e)
        {

        }

        public static List<ProductModel> GioHang = new List<ProductModel>();

        private UserControl _currentUc;

        public void Navigation(UserControl uc)
        {
            panelContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContent.Controls.Add(uc);
            panelContent.Controls[0].Size = panelContent.ClientSize;

            _currentUc = uc; // <- lưu UC hiện tại để search
        }

        private string GetSearchKeyword()
        {
            var kw = (txtSearch.Text ?? "").Trim();
            if (kw == "Nhập từ khóa để tìm kiếm...") return "";
            return kw;
        }

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
            // nếu muốn bắt login trước khi mua:
            // if (!RequireLogin()) return;
            Navigation(new ucMuaSanPham());
        }

        private void btnDatLichHen_Click(object sender, EventArgs e)
        {
            Navigation(new ucDatLich());
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            if (!RequireLogin()) return;
            Navigation(new ucTraCuu());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var dt = KhachHang.Data.Db.QueryToTable("select @@servername as s, db_name() as d");
                MessageBox.Show($"Server={dt.Rows[0]["s"]}\nDB={dt.Rows[0]["d"]}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string cfgPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            var keys = string.Join(", ",
                ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>()
                    .Select(x => x.Name));

            MessageBox.Show("Config đang dùng:\n" + cfgPath + "\n\nKeys:\n" + keys);

            UpdateAuthUI();
            Navigation(new ucTrangChu());
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogIn_Out_Click(object sender, EventArgs e)
        {
            if (!Session.IsLoggedIn)
            {
                var ucLogin = new ucLogIn_Out();
                ucLogin.LoggedIn += () =>
                {
                    UpdateAuthUI();
                    Navigation(new ucTrangChu()); // hoặc chuyển thẳng qua Trang cá nhân
                };
                Navigation(ucLogin);
            }
            else
            {
                // Logout
                Session.Logout();
                UpdateAuthUI();

                // nếu muốn xoá giỏ hàng khi logout:
                GioHang.Clear();

                Navigation(new ucTrangChu());
            }
        }

        private bool RequireLogin()
        {
            if (Session.IsLoggedIn) return true;

            MessageBox.Show("Bạn cần đăng nhập để dùng chức năng này.");
            btnLogIn_Out_Click(null, null); // mở trang login
            return false;
        }

        private void btnTrangCaNhan_Click(object sender, EventArgs e)
        {
            if (!RequireLogin()) return;
            Navigation(new ucTrangCaNhan());
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            var kw = GetSearchKeyword();
            if (string.IsNullOrWhiteSpace(kw))
            {
                MessageBox.Show("Bạn chưa nhập từ khóa tìm kiếm.");
                return;
            }

            // chuyển sang tab mua SP
            var uc = new ucMuaSanPham();
            Navigation(uc);

            // đẩy keyword xuống UC mua SP
            uc.ApplySearch(kw);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTimKiem_Click(null, null);
            }
        }
    }

}
