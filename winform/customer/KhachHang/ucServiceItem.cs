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
    public partial class ucServiceItem : UserControl
    {
        public ucServiceItem()
        {
            InitializeComponent();
        }

        public void SetData(string ten, string moTa, Image anh)
        {
            lblTieuDe.Text = ten;        // Gán tên dịch vụ vào Label tiêu đề
            lblMota.Text = moTa;         // Gán nội dung mô tả vào Label mô tả
            picDV.Image = anh;         // Gán ảnh vào PictureBox
        }

        private void btnTieuDe_Click(object sender, EventArgs e)
        {
            Form1 f = (Form1)this.FindForm();
            if (f != null)
            {
                // Lấy chữ trên chính cái nút bạn vừa ấn (Tiêm Ngừa hoặc Spa Beauty)
                string chuTrenNut = lblTieuDe.Text;

                // QUAN TRỌNG: Phải bỏ biến 'chuTrenNut' vào ngoặc của ucDatLich
                ucDatLich trangMoi = new ucDatLich(chuTrenNut);

                f.Navigation(trangMoi);
                f.SetMenuTitle("Đặt Lịch Hẹn");
            }
        }
    }
}
