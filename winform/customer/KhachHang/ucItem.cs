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
    public partial class ucItem : UserControl
    {
        public ucItem()
        {
            InitializeComponent();
        }

        public Form1.Product ThongTinHienTai; // Biến này giữ dữ liệu của món hàng

        private void lblTenSP_Click(object sender, EventArgs e)
        {

        }

        private void ucItem_Load(object sender, EventArgs e)
        {

        }

        public void SetData(string ten, double gia, Image anh)
        {
            lblTenSP.Text = ten;
            lblGiaSP.Text = gia.ToString("N0") + "đ";
            picAnhSP.Image = anh;

            // Lưu vào biến để sau này "Thêm vào giỏ" thì lấy ra dùng
            ThongTinHienTai = new Form1.Product { TenSP = ten, GiaSP = gia, AnhSP = anh };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Thêm món hàng hiện tại của thẻ này vào danh sách tĩnh ở Form1
            if (ThongTinHienTai != null)
            {
                Form1.GioHang.Add(ThongTinHienTai);
                // Kiểm tra xem danh sách có tăng lên không
                MessageBox.Show($"Đã thêm! Trong giỏ hiện có: {Form1.GioHang.Count} sản phẩm.");
            }
        }
    }
}
