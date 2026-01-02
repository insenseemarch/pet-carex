using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCareX
{
    public partial class ucConTraCuu : UserControl
    {
        public ucConTraCuu()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void dgvLichSu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string ma = txtTimKiem.Text.Trim().ToUpper();

            // Tra cứu theo Mã KH hoặc Mã Thú Cưng
            if (ma == "KH001" || ma == "TC01")
            {
                // Hiện 3 cột thông tin
                pnlKH.Visible = pnlPet.Visible = pnlHistory.Visible = true;

                // --- CỘT 1: KHÁCH HÀNG ---
                lblMaKH.Text = "Mã Khách Hàng: KH001";
                lblHoTen.Text = "Họ Tên: Trần Thị Thủy Tiên";
                lblSDT.Text = "Số Điện Thoại: 0365123987";
                lblHangTV.Text = "Hạng TV: VIP";
                lblCCCD.Text = "CCCD: 123456789012";
                lblGioiTinhKH.Text = "Giới Tính: Nữ";
                lblNgaySinhKH.Text = "Ngày Sinh: 01/01/1995";

                // --- CỘT 2: THÚ CƯNG ---
                lblMaPet.Text = "Mã Thú Cưng: TC01";
                lblTenPet.Text = "Tên: Miu Miu";
                lblLoai.Text = "Loài: Mèo";
                lblNgaySinhPet.Text = "Ngày sinh: 15/05/2023";
                lblGiong.Text = "Giống: Anh lông ngắn";
                lblGioiTinhPet.Text = "Giới tính: Cái";
                lblSucKhoe.Text = "Tình Trạng Sức Khỏe: Khỏe mạnh";

                // --- CỘT 3: TÁCH 2 BẢNG LỊCH SỬ ---

                // 1. Nạp bảng Khám bệnh (dgvKham - bảng nằm trên)
                dgvKham.DataSource = new[] {
            new {
                Ngay = "28/12/2025", // NgaySuDung
                TrieuChung = "Sốt, bỏ ăn", // Trieuchung
                ChanDoan = "Viêm phổi", // Chandoan
                ToaThuoc = "T102", // MaToaThuoc
                Hen = "05/01/2026", // NgayTaiKham
                BacSi = "BS. Khôi" // MaBS
            }
        }.OrderByDescending(x => x.Ngay).ToList();

                // Đặt tên cột trực tiếp cho dgvKham
                dgvKham.Columns["Ngay"].HeaderText = "Ngày khám";
                dgvKham.Columns["TrieuChung"].HeaderText = "Triệu chứng";
                dgvKham.Columns["ChanDoan"].HeaderText = "Chẩn đoán";
                dgvKham.Columns["ToaThuoc"].HeaderText = "Mã toa thuốc";
                dgvKham.Columns["Hen"].HeaderText = "Hẹn tái khám";
                dgvKham.Columns["BacSi"].HeaderText = "Bác sĩ";

                // 2. Nạp bảng Tiêm phòng (dgvTiem - bảng nằm dưới)
                dgvTiem.DataSource = new[] {
            new {
                NgayTiem = "15/12/2025", // NgayTiem
                Vacxin = "VX-05", // MaVacxin
                Mui = "02", // STT
                TrangThai = "Hoàn thành", // TrangThai
                DanhGia = "Tốt", // MaDanhGia
                BacSi = "BS. Cường" // MaBS
            }
        }.OrderByDescending(x => x.NgayTiem).ToList();

                // Đặt tên cột trực tiếp cho dgvTiem
                dgvTiem.Columns["NgayTiem"].HeaderText = "Ngày tiêm";
                dgvTiem.Columns["Vacxin"].HeaderText = "Mã Vaccine";
                dgvTiem.Columns["Mui"].HeaderText = "STT mũi";
                dgvTiem.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvTiem.Columns["DanhGia"].HeaderText = "Đánh giá";
                dgvTiem.Columns["BacSi"].HeaderText = "Bác sĩ";
            }
            else
            {
                // Thông báo lỗi duy nhất theo quy tắc RB8.1
                MessageBox.Show("Không tìm thấy thông tin phù hợp");
                pnlKH.Visible = pnlPet.Visible = pnlHistory.Visible = false;
            }
        }

        private void pnlKH_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucConTraCuu_Load(object sender, EventArgs e)
        {
            pnlKH.Visible = false;
            pnlPet.Visible = false;
            pnlHistory.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
