using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace PetCareX
{
    public partial class ucQuanLyTiem : UserControl
    {
        // 1. Khai báo danh sách dữ liệu giả lập 📂
        static List<Vacxin> dsVacxin = new List<Vacxin>();
        static List<ChiTietTiemPhong> dsChiTietTiem = new List<ChiTietTiemPhong>();

        // DataGridView cho danh sách thú cưng (nằm trong panel dgvChiTietThuCung)
        private DataGridView dgvPets = new DataGridView();

        public ucQuanLyTiem()
        {
            InitializeComponent();
            SetupGiaoDien();
            LoadDuLieuMau();
            HienThiThongKeVacxin();
        }

        // 2. Cấu hình giao diện và sự kiện 🛠️
        private void SetupGiaoDien()
        {
            // Thiết lập bảng thú cưng chi tiết
            dgvPets.Dock = DockStyle.Fill;
            dgvPets.BackgroundColor = SystemColors.Control;
            dgvPets.ReadOnly = true;
            dgvPets.RowHeadersVisible = false;
            // Tự động giãn cột full màn hình ↔️
            dgvPets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTietThuCung.Controls.Add(dgvPets);

            // Tự động giãn cột cho bảng Vaccine
            dgvVacxin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVacxin.RowHeadersVisible = false;

            // Đăng ký sự kiện
            dgvVacxin.CellClick += dgvVacxin_CellClick;
            dgvVacxin.CellFormatting += dgvVacxin_CellFormatting;
        }

        private void LoadDuLieuMau()
        {
            if (dsVacxin.Count == 0)
            {
                // Vaccine mẫu: Có cái hết hạn, có cái sắp hết hạn (<30 ngày)
                dsVacxin.Add(new Vacxin { MaVacxin = "VX01", TenVX = "Dại (Rabies)", NgayHetHan = DateTime.Now.AddDays(15) });
                dsVacxin.Add(new Vacxin { MaVacxin = "VX02", TenVX = "5 Bệnh (Penta)", NgayHetHan = DateTime.Now.AddDays(-5) });
                dsVacxin.Add(new Vacxin { MaVacxin = "VX03", TenVX = "Viêm gan", NgayHetHan = DateTime.Now.AddMonths(6) });

                // Dữ liệu tiêm chủng mẫu
                dsChiTietTiem.Add(new ChiTietTiemPhong { MaThucung = "TC01", TenThucung = "LuLu", MaVacxin = "VX01", NgayTiem = DateTime.Now.AddDays(-10), TrangThai = "Thành công" });
                dsChiTietTiem.Add(new ChiTietTiemPhong { MaThucung = "TC02", TenThucung = "MiuMiu", MaVacxin = "VX01", NgayTiem = DateTime.Now.AddDays(-2), TrangThai = "Thành công" });
            }
        }

        // 3. Hiển thị thống kê Vaccine 📊
        private void HienThiThongKeVacxin()
        {
            var thongKe = dsVacxin.Select(vx => {
                // Tính số mũi tiêm thành công (RB24.5)
                int soLanTiem = dsChiTietTiem.Count(tp => tp.MaVacxin == vx.MaVacxin && tp.TrangThai == "Thành công");

                // Tính toán cảnh báo HSD (RB24.6)
                TimeSpan gap = vx.NgayHetHan.Date - DateTime.Now.Date;
                string tinhTrang = "Bình thường";
                if (gap.TotalDays < 0) tinhTrang = "Đã hết hạn";
                else if (gap.TotalDays <= 30) tinhTrang = "Gần hết hạn";

                return new
                {
                    vx.MaVacxin,
                    vx.TenVX,
                    vx.NgayHetHan,
                    SoLanDaDung = soLanTiem,
                    CanhBao = tinhTrang
                };
            }).ToList();

            dgvVacxin.DataSource = thongKe;

            // Định dạng tiêu đề và ngày tháng (Bỏ giờ) 📅
            dgvVacxin.Columns["MaVacxin"].HeaderText = "Mã Vaccine";
            dgvVacxin.Columns["TenVX"].HeaderText = "Tên Vaccine";
            dgvVacxin.Columns["NgayHetHan"].HeaderText = "Hạn Sử Dụng";
            dgvVacxin.Columns["NgayHetHan"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvVacxin.Columns["SoLanDaDung"].HeaderText = "Số Mũi Đã Tiêm";
            dgvVacxin.Columns["CanhBao"].HeaderText = "Cảnh Báo HSD";
        }

        // 4. Tô màu cảnh báo trực tiếp trên ô (RB24.6) 🎨
        private void dgvVacxin_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvVacxin.Columns[e.ColumnIndex].Name == "CanhBao" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Đã hết hạn")
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.ForeColor = Color.White;
                }
                else if (status == "Gần hết hạn")
                {
                    e.CellStyle.BackColor = Color.Yellow;
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        // 5. Hiển thị danh sách thú cưng khi chọn Vaccine (RB24.8) 🐾
        private void dgvVacxin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string maVX = dgvVacxin.Rows[e.RowIndex].Cells["MaVacxin"].Value.ToString();

            var listPets = dsChiTietTiem
                .Where(tp => tp.MaVacxin == maVX && tp.TrangThai == "Thành công")
                .Select(tp => new {
                    tp.MaThucung,
                    tp.TenThucung,
                    tp.NgayTiem,
                    TrangThai = "Hoàn thành"
                }).ToList();

            dgvPets.DataSource = listPets;

            if (dgvPets.Columns.Count > 0)
            {
                dgvPets.Columns["MaThucung"].HeaderText = "Mã Thú Cưng";
                dgvPets.Columns["TenThucung"].HeaderText = "Tên Thú Cưng";
                dgvPets.Columns["NgayTiem"].HeaderText = "Ngày Tiêm";
                dgvPets.Columns["NgayTiem"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (listPets.Count == 0)
                MessageBox.Show("Chi nhánh hiện chưa có thông tin tiêm chủng cho loại vắc-xin này.");
        }
    }

    // Các lớp dữ liệu (Models) 🦴
    public class Vacxin
    {
        public string MaVacxin { get; set; }
        public string TenVX { get; set; }
        public DateTime NgayHetHan { get; set; }
    }

    public class ChiTietTiemPhong
    {
        public string MaThucung { get; set; }
        public string TenThucung { get; set; }
        public string MaVacxin { get; set; }
        public DateTime NgayTiem { get; set; }
        public string TrangThai { get; set; }
    }
}