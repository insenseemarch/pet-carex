using System;
using System.Drawing;
using System.Windows.Forms;

namespace PetCareX
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            UpdateUI();
        }

        // HÀM CẬP NHẬT GIAO DIỆN VÀ TỰ ĐỘNG CHUYỂN TRANG
        public void UpdateUI()
        {
            if (Session.IsLoggedIn)
            {
                lblLoginLogout.Text = "ĐĂNG XUẤT";
                lblLoginLogout.ForeColor = Color.Red;
                lblUserStatus.Text = $"Chào {Session.VaiTro}: {Session.HoTen}";


                // Tự động nhảy vào trang theo quyền
                if (Session.VaiTro == "Quản lý")
                    showChucNang(new ucQuanLy(), "Trang Dành Cho Quản Lý");
                else if (Session.VaiTro == "Nhân viên" || Session.VaiTro == "Lễ tân")
                    showChucNang(new ucNhanVien(), "Trang Dành Cho Nhân Viên");
                else if (Session.VaiTro == "Bác sĩ")
                    showChucNang(new ucBacSi(), "Trang Dành Cho Bác Sĩ");
            }
            else
            {
                lblLoginLogout.Text = "ĐĂNG NHẬP";
                lblLoginLogout.ForeColor = Color.Blue;
                lblUserStatus.Text = "Vui lòng đăng nhập hệ thống";

                pnlTieuDe.Text = "HỆ THỐNG QUẢN LÝ PETCAREX";

                pnlHienThi.Controls.Clear();
                if (!pnlHienThi.Controls.Contains(pictureBox2))
                    pnlHienThi.Controls.Add(pictureBox2);
            }
        }

        // HÀM SHOW CÁC USERCONTROL (GIỮ NGUYÊN CHỮ ÔNG GIAO)
        public void showChucNang(UserControl uc, string tieuDe)
        {
            pnlTieuDe.Text = tieuDe; // Không ToUpper, để nguyên chữ ông viết

            pnlHienThi.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlHienThi.Controls.Add(uc);

        }

        private void lblLoginLogout_Click(object sender, EventArgs e)
        {
            if (!Session.IsLoggedIn)
            {
                pnlHienThi.Controls.Clear();
                ucLogin loginControl = new ucLogin(this); // Truyền Form1 vào ucLogin
                loginControl.Dock = DockStyle.Fill;
                pnlHienThi.Controls.Add(loginControl);
                pnlTieuDe.Text = "ĐĂNG NHẬP HỆ THỐNG";
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Session.IsLoggedIn = false;
                    UpdateUI();
                }
            }
        }

        private void lblBack_Click_1(object sender, EventArgs e)
        {
            pnlHienThi.Controls.Clear();
            pnlHienThi.Controls.Add(pictureBox2);
            pnlTieuDe.Text = "HỆ THỐNG QUẢN LÝ PETCAREX";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}