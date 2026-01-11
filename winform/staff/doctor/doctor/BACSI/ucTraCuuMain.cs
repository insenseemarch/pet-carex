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
    public partial class ucTraCuuMain : UserControl
    {
        public ucTraCuuMain()
        {
            InitializeComponent();
            LoadMenuChucNang();
        }

        private void LoadMenuChucNang()
        {
            flpContainer.Controls.Clear();

            // 1. Dữ liệu cho các ô chức năng
            string[] titles = { "THÔNG TIN", "TRA CỨU THUỐC", "TRA CỨU VACCINE", "LỊCH LÀM VIỆC" };
            string[] descs = { "Tra cứu hồ sơ của thú cưng hoặc khách hàng", "Danh mục thuốc và liều lượng", "Lịch tiêm và các loại Vaccine", "Xem lịch trực bác sĩ" };

            for (int i = 0; i < 4; i++)
            {
                // Khởi tạo item
                ucItemService item = new ucItemService(titles[i], descs[i], null);
                item.Margin = new Padding(20);

                // Gán biến tạm để tránh lỗi closure trong vòng lặp
                string chucNang = titles[i];

                // Xử lý sự kiện chuyển màn hình
                item.OnSelect += (s, e) =>
                {
                    Form1 f1 = (Form1)this.FindForm();
                    if (f1 == null) return;

                    // Kiểm tra tên chức năng để hiển thị trang tương ứng
                    switch (chucNang)
                    {
                        case "THÔNG TIN":
                            f1.hienThiTrang(new ucThongTin());
                            break;

                        case "TRA CỨU THUỐC":
                            f1.hienThiTrang(new ucTraCuuThuoc());
                            break;

                        case "TRA CỨU VACCINE":
                            // Sau này bạn tạo ucTraCuuVaccine thì thay vào đây
                            f1.hienThiTrang(new ucTraCuuVaccine());
                            break;

                        case "LỊCH LÀM VIỆC":
                            // Sau này bạn tạo ucLichLamViec thì thay vào đây
                            f1.hienThiTrang(new ucLichLamViec());
                            break;

                        default:
                            MessageBox.Show("Chức năng đang được cập nhật!");
                            break;
                    }
                };

                flpContainer.Controls.Add(item);
            }
            CanGiuaLayout();
        }

        // CODE CĂN GIỮA TUYỆT ĐỐI CHO 4 Ô (MÀN HÌNH 1482x683)
        private void CanGiuaLayout()
        {
            // Chiều rộng 1 ô (300) + lề 2 bên (20*2) = 340px
            int doRongMotO = 300 + (20 * 2);

            // Tổng chiều rộng của cụm 4 ô trên 1 hàng
            int tongWidthChucNang = doRongMotO * 4;

            // Tính toán lề trái dư ra để cụm 4 ô nằm chính giữa
            int leTrai = (flpContainer.Width - tongWidthChucNang) / 2;

            // Tính toán lề trên để cụm nằm giữa chiều cao (683px)
            int leTren = (flpContainer.Height - 280) / 2;

            if (leTrai > 0 && leTren > 0)
            {
                flpContainer.Padding = new Padding(leTrai, leTren, 0, 0);
            }
        }

        private void ucTraCuuMain_Resize(object sender, EventArgs e) => CanGiuaLayout();
    }
}
