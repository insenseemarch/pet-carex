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
    public partial class ucItemService : UserControl
    {
        // Sự kiện tùy chỉnh để báo cho trang cha biết ô này được bấm
        public event EventHandler OnSelect;

        public ucItemService(string title, string desc, Image icon)
        {
            InitializeComponent();

            // Gán dữ liệu vào các Label/PictureBox
            lblTitle.Text = title;
            lblDesc.Text = desc;
            picIcon.Image = icon;

            // ĐĂNG KÝ SỰ KIỆN CLICK (Bấm vào đâu cũng ăn)
            this.Click += Item_Click;
            foreach (Control c in this.Controls)
            {
                c.Click += Item_Click;
            }

            // Đăng ký hiệu ứng Hover cho chính UserControl
            this.MouseEnter += Item_MouseEnter;
            this.MouseLeave += Item_MouseLeave;

            // Đăng ký hiệu ứng Hover cho tất cả các con (Chữ, Hình)
            foreach (Control c in this.Controls)
            {
                c.MouseEnter += Item_MouseEnter;
                c.MouseLeave += Item_MouseLeave;
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            // Khi người dùng click, kích hoạt sự kiện OnSelect
            OnSelect?.Invoke(this, e);
        }

        // Khi chuột đi vào vùng của ô (hoặc các con của ô)
        private void Item_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.AliceBlue; // Đổi màu nền sang xanh nhạt
        }

        // Khi chuột đi ra khỏi vùng của ô
        private void Item_MouseLeave(object sender, EventArgs e)
        {
            // Kiểm tra xem chuột có thực sự rời khỏi toàn bộ ô không
            // (Tránh trường hợp chuột di chuyển giữa Label và PictureBox làm màu bị nháy)
            if (!this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
            {
                this.BackColor = Color.White; // Trở lại màu trắng
            }
        }
    }
}
