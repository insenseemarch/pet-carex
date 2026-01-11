using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace PetCareX
{
    public partial class ucQuanLyNhanVien : UserControl
    {
        // 1. DỮ LIỆU GIẢ LẬP
        bool dangXuLyXoa = false;
        static List<NhanVien> dsNhanVien = new List<NhanVien>();
        static List<LichSuLamViec> dsLichSu = new List<LichSuLamViec>();
        bool dangThem = false;

        public ucQuanLyNhanVien()
        {
            InitializeComponent();
            KhoiTaoDuLieuMau();
            HienThiLenLuoi();

            // Khởi tạo trạng thái ban đầu
            panelDieuChuyen.Visible = false;
            EnableInput(false);

            // Đăng ký sự kiện nút chức năng chính
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLuu.Click += btnLuu_Click;
            txtSearch.TextChanged += txtSearch_TextChanged;
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;

            // Đăng ký sự kiện Điều chuyển
            btnDieuChuyen.Click += btnDieuChuyen_Click;
            btnHuy.Click += btnHuy_Click;
            btnXacNhan.Click += btnXacNhan_Click;
        }

        // --- LOGIC GIAO DIỆN: ĐIỀU KHIỂN ẨN HIỆN BẢNG ---

        private void btnDieuChuyen_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần điều chuyển!");
                return;
            }

            // Lấy thông tin nhân viên
            var nv = (NhanVien)dgvNhanVien.CurrentRow.DataBoundItem;
            label8.Text = $"ĐIỀU CHUYỂN CÔNG TÁC: {nv.HoTen.ToUpper()}";

            // Bước 1: Ẩn lưới và các tiêu đề liên quan để dọn chỗ sạch sẽ
            dgvNhanVien.Visible = false;
            label3.Visible = false;

            // Bước 2: Hiện bảng điều chuyển đè lên hoàn toàn
            panelDieuChuyen.Visible = true;
            panelDieuChuyen.BringToFront();
            panelDieuChuyen.Dock = DockStyle.Fill;

            // Reset dữ liệu nhập
            dtpNgayBatDau.Value = DateTime.Now.AddDays(1); // Gợi ý ngày mai
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // 1. Ẩn bảng điều chuyển
            panelDieuChuyen.Visible = false;

            // 2. Hiện lại danh sách và đưa lên trên cùng để thấy rõ Header cột
            dgvNhanVien.Visible = true;
            dgvNhanVien.BringToFront();
            label3.Visible = true;
        }

        // --- LOGIC NGHIỆP VỤ: XỬ LÝ ĐIỀU CHUYỂN (CN-19) ---

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            var nv = (NhanVien)dgvNhanVien.CurrentRow.DataBoundItem;
            string maCNMoi = comboBox1.Text;
            DateTime ngayBDMoi = dtpNgayBatDau.Value.Date;
            DateTime ngayHienTai = DateTime.Now.Date;

            // Kiểm tra ràng buộc chi nhánh
            if (maCNMoi == nv.ChiNhanh)
            {
                MessageBox.Show("Không thể điều chuyển sang cùng chi nhánh hiện tại!");
                return;
            }

            // Kiểm tra ràng buộc ngày tháng: Phải từ ngày mai trở đi
            if (ngayBDMoi <= ngayHienTai)
            {
                MessageBox.Show("Ngày bắt đầu phải từ ngày mai trở đi!", "Lỗi thời gian");
                return;
            }

            try
            {
                // Lưu lịch sử: Ngày kết thúc tại chi nhánh cũ = Ngày mới - 1
                dsLichSu.Add(new LichSuLamViec
                {
                    MaNV = nv.MaNV,
                    ChiNhanhCu = nv.ChiNhanh,
                    ChiNhanhMoi = maCNMoi,
                    NgayBDTaiCNM = ngayBDMoi,
                    NgayKetThucTaiCNC = ngayBDMoi.AddDays(-1)
                });

                // Cập nhật chi nhánh hiện tại của nhân viên
                nv.ChiNhanh = maCNMoi;

                MessageBox.Show("Điều chuyển công tác thành công!", "Thông báo");

                // Quay lại danh sách
                btnHuy_Click(null, null);
                HienThiLenLuoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // --- QUẢN LÝ DỮ LIỆU CƠ BẢN ---

        private void KhoiTaoDuLieuMau()
        {
            if (dsNhanVien.Count == 0)
            {
                dsNhanVien.Add(new NhanVien { MaNV = "NV001", HoTen = "Nguyễn Văn An", SDT = "0901234567", CCCD = "079090001234", GioiTinh = "Nam", NgaySinh = new DateTime(1990, 5, 12), ChucVu = "Quản lý", ChiNhanh = "Chi nhánh 1", LuongCoBan = 15000000 });
                dsNhanVien.Add(new NhanVien { MaNV = "NV002", HoTen = "Lê Thị Bình", SDT = "0987654321", CCCD = "079195005678", GioiTinh = "Nữ", NgaySinh = new DateTime(1995, 11, 20), ChucVu = "Bác sĩ", ChiNhanh = "Chi nhánh 2", LuongCoBan = 12000000 });
            }
        }

        private void HienThiLenLuoi(List<NhanVien> nguon = null)
        {
            dgvNhanVien.DataSource = null;
            dgvNhanVien.DataSource = nguon ?? dsNhanVien;

            if (dgvNhanVien.Columns.Count > 0)
            {
                dgvNhanVien.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvNhanVien.Columns["HoTen"].HeaderText = "Họ và Tên";
                dgvNhanVien.Columns["LuongCoBan"].DefaultCellStyle.Format = "N0";
                dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private string SinhMaNhanVien()
        {
            if (dsNhanVien.Count == 0) return "NV001";
            int maxId = dsNhanVien.Select(x => int.Parse(x.MaNV.Substring(2))).Max();
            return "NV" + (maxId + 1).ToString("D3");
        }

        private void EnableInput(bool status)
        {
            txtMaKH.Enabled = false;
            cboChiNhanh.Enabled = dangThem && status;
            txtHoTen.Enabled = status;
            txtSDT.Enabled = status;
            txtCCCD.Enabled = status;
            cboGioiTinh.Enabled = status;
            dtpNgaySinh.Enabled = status;
            cboChucVu.Enabled = status;
            txtLuongCoBan.Enabled = status;

            btnLuu.Enabled = status;
            btnThem.Enabled = !status;
            btnSua.Enabled = !status;
            btnXoa.Enabled = !status;
            btnDieuChuyen.Enabled = !status;
        }

        private void LamTrongO()
        {
            txtMaKH.Clear(); txtHoTen.Clear(); txtSDT.Clear(); txtCCCD.Clear(); txtLuongCoBan.Clear();
            if (cboChiNhanh.Items.Count > 0) cboChiNhanh.SelectedIndex = 0;
            if (cboChucVu.Items.Count > 0) cboChucVu.SelectedIndex = 0;
            if (cboGioiTinh.Items.Count > 0) cboGioiTinh.SelectedIndex = 0;
            dtpNgaySinh.Value = DateTime.Now;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            dangThem = true;
            LamTrongO();
            txtMaKH.Text = SinhMaNhanVien();
            EnableInput(true);
            txtHoTen.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;
            dangThem = false;
            EnableInput(true);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text)) { MessageBox.Show("Vui lòng nhập họ tên!"); return; }

            if (dangThem)
            {
                dsNhanVien.Add(new NhanVien
                {
                    MaNV = txtMaKH.Text,
                    HoTen = txtHoTen.Text,
                    SDT = txtSDT.Text,
                    CCCD = txtCCCD.Text,
                    GioiTinh = cboGioiTinh.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    ChucVu = cboChucVu.Text,
                    ChiNhanh = cboChiNhanh.Text,
                    LuongCoBan = double.TryParse(txtLuongCoBan.Text, out double l) ? l : 0
                });
            }
            else
            {
                var nv = dsNhanVien.FirstOrDefault(x => x.MaNV == txtMaKH.Text);
                if (nv != null)
                {
                    nv.HoTen = txtHoTen.Text; nv.SDT = txtSDT.Text; nv.CCCD = txtCCCD.Text;
                    nv.GioiTinh = cboGioiTinh.Text; nv.NgaySinh = dtpNgaySinh.Value;
                    nv.ChucVu = cboChucVu.Text; nv.LuongCoBan = double.TryParse(txtLuongCoBan.Text, out double l) ? l : 0;
                }
            }
            HienThiLenLuoi();
            EnableInput(false);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string k = txtSearch.Text.ToLower().Trim();
            var kq = dsNhanVien.Where(x => x.HoTen.ToLower().Contains(k) || x.MaNV.ToLower().Contains(k) || x.SDT.Contains(k)).ToList();
            HienThiLenLuoi(kq);
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var r = dgvNhanVien.Rows[e.RowIndex];
            txtMaKH.Text = r.Cells["MaNV"].Value?.ToString();
            txtHoTen.Text = r.Cells["HoTen"].Value?.ToString();
            txtSDT.Text = r.Cells["SDT"].Value?.ToString();
            txtCCCD.Text = r.Cells["CCCD"].Value?.ToString();
            cboGioiTinh.Text = r.Cells["GioiTinh"].Value?.ToString();
            cboChucVu.Text = r.Cells["ChucVu"].Value?.ToString();
            cboChiNhanh.Text = r.Cells["ChiNhanh"].Value?.ToString();
            txtLuongCoBan.Text = r.Cells["LuongCoBan"].Value?.ToString();
            if (r.Cells["NgaySinh"].Value != null) dtpNgaySinh.Value = (DateTime)r.Cells["NgaySinh"].Value;
            dangThem = false;
            EnableInput(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem có dòng nào đang được chọn không
            if (dgvNhanVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo");
                return;
            }

            // Lấy thông tin nhân viên từ dòng đang chọn
            var nv = (NhanVien)dgvNhanVien.CurrentRow.DataBoundItem;

            // 2. Xác nhận xóa ❓
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa nhân viên {nv.HoTen} (Mã: {nv.MaNV}) không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (dgvNhanVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!");
                return;
            }

            var nv1 = (NhanVien)dgvNhanVien.CurrentRow.DataBoundItem;

            if (MessageBox.Show($"Xóa nhân viên {nv.HoTen}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Bật cờ để chặn các sự kiện tự động 🚩
                dangXuLyXoa = true;

                dsNhanVien.Remove(nv);

                HienThiLenLuoi();
                
                // Xóa lựa chọn để không dòng nào bị chọn tự động
                dgvNhanVien.ClearSelection();
                dgvNhanVien.CurrentCell = null;

                LamTrongO();
                EnableInput(false);

                // Tắt cờ sau khi mọi thứ đã ổn định
                dangXuLyXoa = false;

                MessageBox.Show("Đã xóa nhân viên thành công! ✅");
            }
        }
    }

    public class LichSuLamViec
    {
        public string MaNV { get; set; }
        public string ChiNhanhCu { get; set; }
        public string ChiNhanhMoi { get; set; }
        public DateTime NgayBDTaiCNM { get; set; }
        public DateTime NgayKetThucTaiCNC { get; set; }
    }

    public class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string CCCD { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string ChucVu { get; set; }
        public string ChiNhanh { get; set; }
        public double LuongCoBan { get; set; }
    }
}