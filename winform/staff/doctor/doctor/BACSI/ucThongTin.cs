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
    public partial class ucThongTin : UserControl
    {
        public ucThongTin()
        {
            InitializeComponent();
        }


        private void CanGiuaLayout2()
        {
            // Lấy số lượng control thực tế đang hiển thị (có thể là 4 hoặc 2, 3)
            int count = flpContainer.Controls.Count;
            if (count == 0) return;

            int doRongMotO = 300 + (20 * 2); // Kích thước ô + Margin

            // Tổng chiều rộng của toàn bộ cụm ô
            int tongWidth = doRongMotO * count;

            // Nếu tổng width vượt quá container thì tính theo hàng ngang (tối đa 4 ô)
            if (tongWidth > flpContainer.Width)
                tongWidth = doRongMotO * 4;

            int leTrai = (flpContainer.Width - tongWidth) / 2;
            int leTren = (flpContainer.Height - 280) / 2; // 280 là chiều cao ước tính của ô

            if (leTrai > 0 && leTren > 0)
            {
                flpContainer.Padding = new Padding(leTrai, leTren, 0, 0);
            }
        }

        private void Hien(object sender, EventArgs e)
        {
            // 1. Dọn dẹp các ô chức năng cũ
            flpContainer.Controls.Clear();

            // 2. Tạo dữ liệu cho 2 item mới
            string[] subTitles = { "KHÁCH HÀNG", "THÚ CƯNG" };
            string[] subDescs = { "Quản lý thông tin và lịch sử khách hàng", "Hồ sơ sức khỏe và giống thú nuôi" };

            for (int i = 0; i < 2; i++)
            {
                // Tận dụng lại ucItemService bạn đang có
                ucItemService itemSub = new ucItemService(subTitles[i], subDescs[i], null);
                itemSub.Margin = new Padding(20);

                string luaChon = subTitles[i];

                // Gán sự kiện khi click vào Khách hàng hoặc Thú cưng
                itemSub.OnSelect += (s, evt) =>
                {
                    Form1 f1 = (Form1)this.FindForm();
                    if (f1 == null) return;

                    if (luaChon == "KHÁCH HÀNG")
                    {
                        f1.hienThiTrang(new ucThongTinKhachHang()); // Bạn cần tạo UC này
                    }
                    else
                    {
                        f1.hienThiTrang(new ucThongTinThuCung());
                    }
                };

                flpContainer.Controls.Add(itemSub);

                // 4. Căn giữa lại các ô mới
                CanGiuaLayout2();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 f1 = (Form1)this.FindForm();
            f1.hienThiTrang(new ucTraCuuMain()); // Trở về menu 4 ô
        }
    }
}
