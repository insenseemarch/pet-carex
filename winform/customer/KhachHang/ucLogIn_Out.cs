using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KhachHang.Common;
using KhachHang.Data;

namespace KhachHang
{
    public partial class ucLogIn_Out : UserControl
    {
        public event Action LoggedIn;

        public ucLogIn_Out()
        {
            InitializeComponent();
        }

        private void ucLogIn_Out_Load(object sender, EventArgs e)
        {

        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // (Đoạn này sẽ thêm code SQL lưu vào CSDL sau)
            MessageBox.Show("Đăng ký thành công! Mời bạn đăng nhập.");

            pnlRegister.Visible = false;
            pnlLogin.Visible = true;
            pnlLogin.BringToFront();
        }

        private void lnkGoRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Ẩn panel cũ
            pnlLogin.Visible = false;

            // 2. Cấu hình panel mới trước khi hiện
            pnlRegister.Dock = DockStyle.Fill; // Ép nó tràn đầy màn hình
            pnlRegister.Location = new Point(0, 0); // Đảm bảo ở góc trên bên trái

            // 3. Hiển thị
            pnlRegister.Visible = true;
            pnlRegister.BringToFront();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = Db.ExecProcToTable("sp_dangnhap",
                    new SqlParameter("@tendangnhap", txtUser.Text.Trim()),
                    new SqlParameter("@matkhau", txtPass.Text.Trim())
                );

                if (dt.Rows.Count == 0) { MessageBox.Show("Sai tài khoản/mật khẩu"); return; }

                var r = dt.Rows[0];

                Session.Login(
                    txtUser.Text.Trim(),                 // TenDangNhap
                    r["makh"].ToString(),                // MaKH
                    r["capbac"].ToString(),              // CapBac
                    Convert.ToInt32(r["diemtichluy"])    // DiemTichLuy
                );

                LoggedIn?.Invoke();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, $"Lỗi đăng nhập ({ex.Number})");
            }
        }
    }
}
