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
    public partial class ucTraCuu : UserControl
    {
        public ucTraCuu()
        {
            InitializeComponent();
        }

        // Hàm trả về một DataTable chứa thông tin khách hàng
        public DataTable LayThongTinKH(string maKH)
        {
            // Chuỗi truy vấn SQL để lấy thông tin từ bảng KHACHHANG
            // Lưu ý: Tên cột (MaKH, TenKH...) phải khớp với CSDL của bạn
            string query = "SELECT MaKH, TenKH, SDT, HangTV, TongChiTieu FROM KHACHHANG WHERE MaKH = '" + maKH + "'";

            // Gọi class kết nối của bạn để thực thi (Giả sử bạn có class KetNoi)
            // Nếu bạn dùng thư viện dùng chung thì gọi nó ở đây
            //KetNoi kn = new KetNoi();
            //return kn.ExecuteQuery(query);
            DataTable table = new DataTable();
            // Giả lập dữ liệu trả về
            table.Columns.Add("MaKH");
            table.Columns.Add("TenKH");
            table.Columns.Add("SDT");
            table.Columns.Add("HangTV");
            table.Columns.Add("TongChiTieu", typeof(decimal));
            if (maKH == "KH001")
            {
                table.Rows.Add("KH001", "Nguyen Van A", "0123456789", "Vang", 1500000);
            }
            return table;
        }

        public DataTable LayLichSuMuaHang(string maKH)
        {
            // Câu lệnh SQL JOIN để lấy cả tên sản phẩm và thông tin hóa đơn
            string query = @"SELECT HD.MaHD, SP.TenSP, CTHD.SoLuong, CTHD.ThanhTien, HD.NgayLap
                     FROM HOADON HD
                     JOIN CHITIETHOADON CTHD ON HD.MaHD = CTHD.MaHD
                     JOIN SANPHAM SP ON CTHD.MaSP = SP.MaSP
                     WHERE HD.MaKH = '" + maKH + "'";

            // kn = new KetNoi();
            //return kn.ExecuteQuery(query);
            DataTable table = new DataTable();
            table.Columns.Add("MaHD");
            table.Columns.Add("TenSP");
            table.Columns.Add("SoLuong", typeof(int));
            table.Columns.Add("ThanhTien", typeof(decimal));
            table.Columns.Add("NgayLap", typeof(DateTime));
            table.Columns.Add("TongTien", typeof(decimal));
            // Giả lập dữ liệu trả về
            if (maKH == "KH001")
            {
                table.Rows.Add("HD001", "San Pham A", 2, 500000, new DateTime(2023, 1, 15), 500000);
                table.Rows.Add("HD002", "San Pham B", 1, 1000000, new DateTime(2023, 3, 10), 1000000);
            }
            return table;
        }

        private void TuDongCoGianBang(DataGridView dgv)
        {
            // 1. Tính chiều cao của phần tiêu đề (Header)
            int tongChieuCao = dgv.ColumnHeadersHeight;

            // 2. Cộng dồn chiều cao của tất cả các dòng hiện có
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Visible)
                {
                    tongChieuCao += row.Height;
                }
            }

            // 3. Thêm một khoảng nhỏ (ví dụ 2-5px) để tránh hiện thanh cuộn dọc không cần thiết
            dgv.Height = tongChieuCao + 5;

            // 4. (Tùy chọn) Điều chỉnh GroupBox chứa nó co lại theo
            // groupLichSu.Height = dgv.Height + 30; // 30 là khoảng cách cho tiêu đề GroupBox
        }

        private void btnTimKH_Click(object sender, EventArgs e)
        {
            string ma = txtMaKhachHang.Text.Trim(); // Ô bạn nhập mã

            // Gọi hàm 1: Lấy thông tin cá nhân
            DataTable dtKH = LayThongTinKH(ma);

            if (dtKH.Rows.Count > 0)
            {
                // Đổ vào các ô TextBox hiển thị
                txtMaKH.Text = dtKH.Rows[0]["MaKH"].ToString();
                txtHoTen.Text = dtKH.Rows[0]["TenKH"].ToString();
                txtSDT.Text = dtKH.Rows[0]["SDT"].ToString();
                txtHangThanhVien.Text = dtKH.Rows[0]["HangTV"].ToString();

                // Gọi hàm 2: Đổ vào bảng lịch sử
                dgvLichSuMuaHang.DataSource = LayLichSuMuaHang(ma);

                // Tự động co giãn DataGridView cho vừa
                TuDongCoGianBang(dgvLichSuMuaHang);

                // Sau khi có dữ liệu thì hiện Panel kết quả lên
                pnlKetQuaTC.Visible = false;
                pnlKetQuaKH.Visible = true;
                //pnlKetQuaTC.Visible = false;
                pnlKetQuaKH.BringToFront();
            }
            else
            {
                MessageBox.Show("Mã khách hàng này không tồn tại!");
            }
        }

        public DataTable LayThongTinKhamBenhThuCung(string maTC)
        {
            // Chuỗi truy vấn SQL để lấy thông tin từ bảng THUCUNG
            // string query = "SELECT MaTC, TenTC, LoaiTC, Tuoi, MaKH FROM THUCUNG WHERE MaTC = '" + maTC + "'";
            // Gọi class kết nối của bạn để thực thi (Giả sử bạn có class KetNoi)
            //KetNoi kn = new KetNoi();
            //return kn.ExecuteQuery(query);
            DataTable table = new DataTable();
            table.Columns.Add("MaDV");
            table.Columns.Add("MaTC");
            table.Columns.Add("NgaySD", typeof(DateTime));
            table.Columns.Add("MaBS");
            table.Columns.Add("TrieuChung");
            table.Columns.Add("ChuanDoan");
            table.Columns.Add("NgayTaiKham", typeof(DateTime));
            // Giả lập dữ liệu trả về
            if (maTC == "TC001")
            {
                table.Rows.Add("DV001", "TC001", new DateTime(2023, 2, 20), "BS001", "Sot, Ho", "Viêm phổi", new DateTime(2023, 3, 5));
                table.Rows.Add("DV002", "TC001", new DateTime(2023, 4, 15), "BS002", "Tiêu chảy", "Rối loạn tiêu hóa", DBNull.Value);
            }

            return table;
        }

        public DataTable LayThongTinTiemNguaThuCung(string maTC)
        {
                       // Chuỗi truy vấn SQL để lấy thông tin tiêm ngừa thú cưng
            // string query = "SELECT MaTC, TenTC, LoaiTC, Tuoi, MaKH FROM THUCUNG WHERE MaTC = '" + maTC + "'";
            // Gọi class kết nối của bạn để thực thi (Giả sử bạn có class KetNoi)
            //KetNoi kn = new KetNoi();
            //return kn.ExecuteQuery(query);
            DataTable table = new DataTable();
            table.Columns.Add("STT");
            table.Columns.Add("MaTC");
            table.Columns.Add("MaVC");
            table.Columns.Add("MaBS");
            table.Columns.Add("NgayTiem", typeof(DateTime));
            table.Columns.Add("TrangThai");
            // Giả lập dữ liệu trả về
            if (maTC == "TC001") {
                table.Rows.Add(1, "TC001", "VC001", "BS001", new DateTime(2023, 1, 10), "Đã tiêm");
                table.Rows.Add(2, "TC001", "VC002", "BS002", new DateTime(2023, 3, 12), "Đã tiêm");

            }
            return table;
        }

        private void btnTimTC_Click(object sender, EventArgs e)
        {
            string ma = txtMaThuCung.Text.Trim(); // Ô bạn nhập mã

            // Xử lý tương tự như btnTimKH_Click nhưng cho thú cưng
            DataTable dtKhamBenh = LayThongTinKhamBenhThuCung(ma);
            DataTable dtTiemNgua = LayThongTinTiemNguaThuCung(ma);
            if (dtKhamBenh.Rows.Count > 0 || dtTiemNgua.Rows.Count > 0)
            {
                // 1. Gán dữ liệu vào các DataGridView đã thiết kế
                dgvKhamBenh.DataSource = dtKhamBenh;
                dgvTiemNgua.DataSource = dtTiemNgua; // Đừng quên gán cho bảng tiêm ngừa

                // 2. Điều khiển ẩn hiện Panel
                pnlKetQuaKH.Visible = false;
                pnlKetQuaTC.Visible = true;
                pnlKetQuaTC.BringToFront(); // Đưa lên trên cùng

                // 3. Gọi hàm co giãn chiều cao cho cả 2 bảng
                TuDongCoGianBang(dgvTiemNgua);
                TuDongCoGianBang(dgvKhamBenh);
            }
            else
            {
                pnlKetQuaTC.Visible = false;
                MessageBox.Show("Mã thú cưng này không tồn tại hoặc chưa có lịch sử khám bệnh!");
            }
        }
    }
}
