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
    public partial class ucDatLich : UserControl
    {
        public ucDatLich()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        // Trong ucDatLich.cs
        private string tenDichVuHienTai;

        // Sửa lại hàm khởi tạo để nhận tham số string
        public ucDatLich(string loaiDV)
        {
            InitializeComponent();
            this.tenDichVuHienTai = loaiDV; // Lưu tên dịch vụ vào biến
            //MessageBox.Show(loaiDV);
        }

        private void ucDatLich_Load(object sender, EventArgs e)
        {
            // Gán giá trị từ biến vào cái Label "TieuDe"
            lblTieuDe.Text = "ĐẶT LỊCH " + tenDichVuHienTai.ToUpper();
            // Chỉ cho phép chọn từ ngày hôm nay
            dtpNgayHen.MinDate = DateTime.Now;

            // Giới hạn trong 7 ngày tới (1 tuần)
            dtpNgayHen.MaxDate = DateTime.Now.AddDays(7);
        }

        private void lblMTC_Click(object sender, EventArgs e)
        {

        }

        // Hàm này nằm trong ucDatLich.cs
        private string LayTenBacSiTruc(string maCN, DateTime ngay, string khungGio)
        {
            string tenBS = "Chưa có bác sĩ trực";
            try
            {
                // Câu lệnh SQL giả định: Kết hợp bảng LICHTRUC và BACSI
                // SELECT B.TenBacSi FROM LICHTRUC L JOIN BACSI B ON L.MaBS = B.MaBS 
                // WHERE L.MaCN = @maCN AND L.NgayTruc = @ngay AND L.KhungGio = @gio

                // Code kết nối SQL của bạn ở đây...
                // tenBS = kết quả truy vấn;
            }
            catch
            {
                tenBS = "Lỗi kết nối";
            }
            return "tenBS";
        }

        // 1. Kiểm tra mã thú cưng có tồn tại trong hệ thống không
        private bool KiemTraThuCungTonTai(string maTC)
        {
            // Giả sử bạn dùng SQL để check: SELECT COUNT(*) FROM THUCUNG WHERE MaThucung = maTC
            // Trả về true nếu có, false nếu không thấy mã này
            return true; // Thay bằng code xử lý SQL của bạn
        }

        // 2. Kiểm tra khung giờ đó tại chi nhánh đó đã có ai đặt chưa
        private bool KiemTraLichTrong(string maCN, DateTime ngay, string khungGio)
        {
            // SELECT COUNT(*) FROM CHITIETKHAMBENH WHERE MaCN = maCN AND NgayHen = ngay AND KhungGio = khungGio
            // Nếu kết quả = 0 thì trả về true (lịch trống)
            return true; // Thay bằng code xử lý SQL của bạn
        }

        private void ThucHienLuuLichHen(string maTC, string maCN, DateTime ngay, string gio)
        {
            try
            {
                // Kiểm tra biến loaiDichVu đã nhận từ ucServiceItem
                if (tenDichVuHienTai == "TIÊM NGỪA")
                {
                    // INSERT INTO CHITIETTIEMPHONG ...
                    MessageBox.Show("Đã lưu lịch TIÊM PHÒNG thành công!");
                }
                else
                {
                    // INSERT INTO CHITIETKHAMBENH ...
                    MessageBox.Show("Đã lưu lịch KHÁM BỆNH thành công!");
                }

                // Sau khi lưu xong có thể quay về trang Dịch vụ
                Form1 f = (Form1)this.FindForm();
                f.Navigation(new ucDichVu());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string maTC = txtMaThuCung.Text.Trim();
            string maCN = cboChiNhanh.SelectedValue?.ToString();
            string khungGio = cboKhungGio.SelectedItem?.ToString();
            DateTime ngayHen = dtpNgayHen.Value.Date;

            // Bước 1: Kiểm tra nhập liệu cơ bản
            if (string.IsNullOrEmpty(maTC) || string.IsNullOrEmpty(khungGio))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã thú cưng và Khung giờ!");
                return;
            }

            // Bước 2: Kiểm tra mã thú cưng (Bước 2 trong quy trình)
            if (!KiemTraThuCungTonTai(maTC))
            {
                MessageBox.Show("Mã thú cưng không tồn tại! Vui lòng kiểm tra lại.");
                return;
            }

            // Bước 3: Kiểm tra lịch trống (Bước 4 trong quy trình)
            if (!KiemTraLichTrong(maCN, ngayHen, khungGio))
            {
                MessageBox.Show("Khung giờ này tại chi nhánh đã có người đặt. Vui lòng chọn giờ khác!");
                return;
            }

            // Bước 4: Thực hiện lưu (Bước 5 trong quy trình)
            ThucHienLuuLichHen(maTC, maCN, ngayHen, khungGio);
        }

        private void cboKhungGio_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu đã chọn đủ thông tin cần thiết
            if (cboChiNhanh.SelectedValue != null && cboKhungGio.SelectedIndex != -1)
            {
                string maCN = cboChiNhanh.SelectedValue.ToString();
                DateTime ngayHen = dtpNgayHen.Value.Date;
                string gioHen = cboKhungGio.Text;

                // Gọi hàm lấy tên bác sĩ
                string bacSi = LayTenBacSiTruc(maCN, ngayHen, gioHen);

                // Hiển thị lên giao diện
                txtTenBacSi.Text = bacSi;
            }
        }
    }
}
