using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachHang
{
    public partial class ucLogIn_Out : UserControl
    {
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
            // Giả sử mã KH lấy được sau khi kiểm tra DB là "KH001"
            string maKH = "KH001";

            Form1 mainForm = (Form1)this.FindForm();

            // 1. Hiện mã KH lên cạnh nút Login/Out
            //mainForm.CapNhatTrangThaiDangNhap(true, maKH);

            // 2. Chuyển hướng màn hình về Trang Chủ
            mainForm.Navigation(new ucTrangChu());
        }
    }
}
