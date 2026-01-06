using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KhachHang.Common;

namespace KhachHang
{
    public partial class ucItem : UserControl
    {
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutItem();
        }

        private void LayoutItem()
        {
            int pad = 10;

            // button nằm dưới
            button1.Left = pad;
            button1.Width = this.Width - pad * 2;
            button1.Top = this.Height - button1.Height - pad;

            // ảnh nằm trên, chừa chỗ cho label + button
            int infoH = 55; // vùng cho 2 label
            picAnhSP.Left = 0;
            picAnhSP.Top = 0;
            picAnhSP.Width = this.Width;
            picAnhSP.Height = button1.Top - infoH - 5;

            // label nằm giữa ảnh và button
            lblTenSP.Left = pad;
            lblTenSP.Top = picAnhSP.Bottom + 5;

            lblGiaSP.Left = pad;
            lblGiaSP.Top = lblTenSP.Bottom + 3;

            // đảm bảo label nổi trên ảnh
            picAnhSP.SendToBack();
            lblTenSP.BringToFront();
            lblGiaSP.BringToFront();
            button1.BringToFront();
        }

        public ucItem()
        {
            InitializeComponent();
            LayoutItem();
        }


        public ProductModel ThongTinHienTai; // Biến này giữ dữ liệu của món hàng

        private void lblTenSP_Click(object sender, EventArgs e)
        {
            
        }

        private void ucItem_Load(object sender, EventArgs e)
        {

        }

        public void SetData(ProductModel p)
        {
            ThongTinHienTai = p;

            lblTenSP.Text = p.TenSP ?? "";
            lblGiaSP.Text = p.GiaSP.ToString("N0") + "đ";
            picAnhSP.Image = p.AnhSP;
            lblTenSP.Visible = true;
            lblGiaSP.Visible = true;
            //lblTenSP.BackColor = Color.Yellow;
            //lblGiaSP.BackColor = Color.Yellow;

            // ✅ tránh bị che chữ bởi picture
            lblTenSP.BringToFront();
            lblGiaSP.BringToFront();
            button1.BringToFront();

            if (p.SoLuongTon <= 0)
            {
                button1.Enabled = false;
                button1.Text = "Hết hàng";
            }
            else
            {
                button1.Enabled = true;
                button1.Text = "Thêm vào giỏ";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (ThongTinHienTai == null) return;

            // add giỏ: nếu trùng MaSP thì tăng số lượng
            var exist = Form1.GioHang.Find(x => x.MaSP == ThongTinHienTai.MaSP);
            if (exist != null) exist.SoLuong++;
            else Form1.GioHang.Add(new ProductModel
            {
                MaSP = ThongTinHienTai.MaSP,
                TenSP = ThongTinHienTai.TenSP,
                GiaSP = ThongTinHienTai.GiaSP,
                AnhSP = ThongTinHienTai.AnhSP,
                LoaiSP = ThongTinHienTai.LoaiSP,
                SoLuong = 1
            });

            MessageBox.Show($"Đã thêm! Trong giỏ hiện có: {Form1.GioHang.Sum(x => x.SoLuong)} món.");
        }
    }
}
