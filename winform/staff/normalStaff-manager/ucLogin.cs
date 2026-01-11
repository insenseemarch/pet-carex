using System;
using System.Windows.Forms;

namespace PetCareX
{
    public partial class ucLogin : UserControl
    {
        Form1 parent;

        public ucLogin(Form1 f)
        {
            InitializeComponent();
            this.parent = f;

            // Đảm bảo mật khẩu luôn ẩn khi vừa load lên
            txtMatKhau.UseSystemPasswordChar = true;
        }

        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            string user = txtTenDangNhap.Text.Trim();
            string pass = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo");
                return;
            }

            // GIẢ LẬP TÀI KHOẢN ĐỂ TEST LUỒNG BAY THẲNG
            if (user == "admin" && pass == "123")
            {
                Session.IsLoggedIn = true;
                Session.HoTen = "Nguyễn Quản Lý";
                Session.VaiTro = "Quản lý";
                parent.UpdateUI();
            }
            else if (user == "nv01" && pass == "123")
            {
                Session.IsLoggedIn = true;
                Session.HoTen = "Trần Lễ Tân";
                Session.VaiTro = "Nhân viên";
                parent.UpdateUI();
            }
            else if (user == "bs01" && pass == "123")
            {
                Session.IsLoggedIn = true;
                Session.HoTen = "Lê Bác Sĩ";
                Session.VaiTro = "Bác sĩ";
                parent.UpdateUI();
            }
            else
            {

                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Clear();
                txtTenDangNhap.Focus();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ucLogin_Load(object sender, EventArgs e)
        {

        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void txtTenDangNhap_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtMatKhau_TextChanged_1(object sender, EventArgs e)
        {

        }


    }
}