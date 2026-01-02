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
    public partial class ucDichVu : UserControl
    {
        public ucDichVu()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Chống lag và nhấp nháy ảnh nền
        }

        private void ucDichVu_Load(object sender, EventArgs e)
        {
            // 1. Chống giật màn hình khi nạp thẻ mới
            this.SuspendLayout();
            tLPDichVu.Controls.Clear();

            // 2. Tạo 2 đối tượng dịch vụ
            ucServiceItem dv1 = new ucServiceItem();
            dv1.SetData("TIÊM NGỪA", "Tiêm phòng đầy đủ các loại vaccine chuẩn quốc tế.", imageListDichVu.Images[0]);

            ucServiceItem dv2 = new ucServiceItem();
            dv2.SetData("KHÁM BỆNH", "Khám bách bệnh của thú cưng", imageListDichVu.Images[1]);

            // 3. Thiết lập cách thẻ nằm trong cột
            // Dùng Anchor = None để thẻ luôn nằm chính giữa ô 50% của nó
            dv1.Anchor = AnchorStyles.None;
            dv2.Anchor = AnchorStyles.None;

            // 4. Thêm vào bảng
            tLPDichVu.Controls.Add(dv1, 0, 0); // Cột 0, Hàng 0
            tLPDichVu.Controls.Add(dv2, 1, 0); // Cột 1, Hàng 0

            this.ResumeLayout();
        }
    }
}
