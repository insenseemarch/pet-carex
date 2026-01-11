    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using System.Drawing;

    namespace PetCareX
    {
        public partial class ucConQuanLyHoSo : UserControl
        {
            // 1. DỮ LIỆU GIẢ LẬP
            static List<KhachHang> dsKhachHang = new List<KhachHang>();
            bool dangThem = false;

            public ucConQuanLyHoSo()
            {
                InitializeComponent();
                KhoiTaoDuLieuMau();
                HienThiLenLuoi();
                EnableInput(false); // Mặc định khóa hết khi vừa vào
            }
        private string SinhMaKhachHang()
        {
            if (dsKhachHang.Count == 0) return "KH001";

            // Lấy mã lớn nhất hiện có, cắt bỏ chữ "KH" và chuyển sang số
            int maxId = dsKhachHang
                .Select(x => int.Parse(x.MaKH.Substring(2)))
                .Max();

            // Trả về mã mới tăng thêm 1 đơn vị, định dạng 3 chữ số (VD: KH011)
            return "KH" + (maxId + 1).ToString("D3");
        }
        private void KhoiTaoDuLieuMau()
            {
                if (dsKhachHang.Count == 0)
                {
                    dsKhachHang.Add(new KhachHang { MaKH = "KH001", HoTen = "Nguyễn Văn An", SDT = "0901234567", CCCD = "079090001234", GioiTinh = "Nam", NgaySinh = new DateTime(1990, 5, 12), HangTV = "Cơ bản" });
                    dsKhachHang.Add(new KhachHang { MaKH = "KH002", HoTen = "Trần Thị Bình", SDT = "0987654321", CCCD = "079195005678", GioiTinh = "Nữ", NgaySinh = new DateTime(1995, 11, 20), HangTV = "Thân thiết" });
                    dsKhachHang.Add(new KhachHang { MaKH = "KH003", HoTen = "Lê Hoàng Nam", SDT = "0912345678", CCCD = "079088009999", GioiTinh = "Nam", NgaySinh = new DateTime(1988, 2, 15), HangTV = "VIP" });
                    dsKhachHang.Add(new KhachHang { MaKH = "KH004", HoTen = "Phạm Minh Thư", SDT = "0977888999", CCCD = "079200001111", GioiTinh = "Nữ", NgaySinh = new DateTime(2000, 8, 30), HangTV = "Cơ bản" });
                    dsKhachHang.Add(new KhachHang { MaKH = "KH005", HoTen = "Võ Văn Thành", SDT = "0355666777", CCCD = "079192002222", GioiTinh = "Nam", NgaySinh = new DateTime(1992, 4, 10), HangTV = "Thân thiết" });
                    dsKhachHang.Add(new KhachHang { MaKH = "KH006", HoTen = "Đặng Thu Thảo", SDT = "0866555444", CCCD = "079197003333", GioiTinh = "Nữ", NgaySinh = new DateTime(1997, 12, 05), HangTV = "VIP" });
                    dsKhachHang.Add(new KhachHang { MaKH = "KH007", HoTen = "Bùi Anh Tuấn", SDT = "0707123123", CCCD = "079085004444", GioiTinh = "Nam", NgaySinh = new DateTime(1985, 1, 25), HangTV = "Cơ bản" });
                    dsKhachHang.Add(new KhachHang { MaKH = "KH008", HoTen = "Ngô Thanh Vân", SDT = "0944333222", CCCD = "079193005555", GioiTinh = "Nữ", NgaySinh = new DateTime(1993, 7, 18), HangTV = "Thân thiết" });
                    dsKhachHang.Add(new KhachHang { MaKH = "KH009", HoTen = "Lý Minh Triết", SDT = "0933222111", CCCD = "079099006666", GioiTinh = "Nam", NgaySinh = new DateTime(1999, 3, 14), HangTV = "VIP" });
                    dsKhachHang.Add(new KhachHang { MaKH = "KH010", HoTen = "Đỗ Mỹ Linh", SDT = "0922111000", CCCD = "079196007777", GioiTinh = "Nữ", NgaySinh = new DateTime(1996, 10, 02), HangTV = "Cơ bản" });
                }
            }

            private void HienThiLenLuoi()
            {
                dgvKhachHang.DataSource = null;
                dgvKhachHang.DataSource = dsKhachHang;

                if (dgvKhachHang.Columns.Count > 0)
                {
                    dgvKhachHang.Columns["MaKH"].HeaderText = "Mã Khách Hàng";
                    dgvKhachHang.Columns["HoTen"].HeaderText = "Họ và Tên";
                    dgvKhachHang.Columns["SDT"].HeaderText = "Số Điện Thoại";
                    dgvKhachHang.Columns["CCCD"].HeaderText = "Số CCCD";
                    dgvKhachHang.Columns["GioiTinh"].HeaderText = "Giới Tính";
                    dgvKhachHang.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                    dgvKhachHang.Columns["HangTV"].HeaderText = "Hạng Hội Viên";

                    // ĐỊNH DẠNG NGÀY SINH dd/MM/yyyy (Mất cái giờ đi)
                    dgvKhachHang.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }

        // HÀM MỞ/KHÓA CÁC Ô NHẬP LIỆU
        private void EnableInput(bool status)
        {
            // Mã KH luôn luôn khóa vì hệ thống tự sinh 🔒
            txtMaKH.Enabled = false;

            // Các thông tin cơ bản mở theo trạng thái status
            txtHoTen.Enabled = status;
            txtSDT.Enabled = status;
            txtCCCD.Enabled = status;
            cboGioiTinh.Enabled = status;
            dtpNgaySinh.Enabled = status;

            // LOGIC MỚI: Nếu đang THÊM thì khóa Hạng TV, nếu đang SỬA thì mở Hạng TV 🔑
            if (dangThem)
            {
                cboHangTV.Enabled = false;
            }
            else
            {
                cboHangTV.Enabled = status;
            }

            // Các nút điều hướng
            btnLuu.Enabled = status;
            btnThem.Enabled = !status;
            btnSua.Enabled = !status;
            btnXoa.Enabled = !status;
        }
        private void LamTrongO()
        {
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            txtCCCD.Clear();
            cboGioiTinh.SelectedIndex = 0; // Đưa giới tính về lựa chọn đầu tiên
            dtpNgaySinh.Value = DateTime.Now; // Đưa ngày sinh về ngày hiện tại
            cboHangTV.Text = "Cơ bản"; // Đặt lại hạng mặc định
        }
        private void btnThem_Click(object sender, EventArgs e)
        {

            txtMaKH.Text = SinhMaKhachHang();
            cboHangTV.Text = "Cơ bản";

            dangThem = true;
            LamTrongO();
            EnableInput(true);

            // Tự động điền mã mới vào ô TextBox nhưng vẫn để Enabled = false ✨
            txtMaKH.Text = SinhMaKhachHang();

            txtHoTen.Focus(); // Nhảy thẳng xuống ô Họ tên để nhập
        }

        // --- NÚT SỬA ---
        private void btnSua_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(txtMaKH.Text))
                {
                    MessageBox.Show("Vui lòng chọn một khách hàng từ danh sách để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                dangThem = false;
                EnableInput(true);
                txtMaKH.Enabled = false; // Mã không được sửa
                txtHoTen.Focus();
            }

            // --- NÚT XÓA ---
            private void btnXoa_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(txtMaKH.Text))
                {
                    MessageBox.Show("Vui lòng chọn hồ sơ cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Bạn có chắc muốn xóa khách hàng {txtHoTen.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dsKhachHang.RemoveAll(x => x.MaKH == txtMaKH.Text);
                    HienThiLenLuoi();
                    LamTrongO();
                    EnableInput(false);
                    MessageBox.Show("Đã xóa hồ sơ thành công.", "Thông báo");
                }
            }

        // --- NÚT LƯU ---
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Chỉ kiểm tra họ tên và SĐT vì mã đã có sẵn
            if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Họ tên và Số điện thoại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dangThem)
            {
                // Thêm mới với mã đã sinh sẵn
                dsKhachHang.Add(new KhachHang
                {
                    MaKH = txtMaKH.Text,
                    HoTen = txtHoTen.Text,
                    SDT = txtSDT.Text,
                    CCCD = txtCCCD.Text,
                    GioiTinh = cboGioiTinh.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    HangTV = cboHangTV.Text
                });
            }
            else
            {
                // Cập nhật khách hàng cũ
                var kh = dsKhachHang.FirstOrDefault(x => x.MaKH == txtMaKH.Text);
                if (kh != null)
                {
                    kh.HoTen = txtHoTen.Text;
                    kh.SDT = txtSDT.Text;
                    kh.CCCD = txtCCCD.Text;
                    kh.GioiTinh = cboGioiTinh.Text;
                    kh.NgaySinh = dtpNgaySinh.Value;
                    kh.HangTV = cboHangTV.Text;
                }
            }

            HienThiLenLuoi();
            EnableInput(false);
            MessageBox.Show("Dữ liệu đã được cập nhật thành công.", "Thành công ✅");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
            {
                string keyword = txtSearch.Text.ToLower().Trim();
                var ketQua = dsKhachHang.Where(x =>
                    x.HoTen.ToLower().Contains(keyword) || x.SDT.Contains(keyword) || x.CCCD.Contains(keyword)
                ).ToList();

                dgvKhachHang.DataSource = null;
                dgvKhachHang.DataSource = ketQua;

                if (dgvKhachHang.Columns.Count > 0)
                    dgvKhachHang.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                    txtMaKH.Text = row.Cells["MaKH"].Value?.ToString();
                    txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
                    txtSDT.Text = row.Cells["SDT"].Value?.ToString();
                    txtCCCD.Text = row.Cells["CCCD"].Value?.ToString();
                    cboGioiTinh.Text = row.Cells["GioiTinh"].Value?.ToString();

                    if (row.Cells["NgaySinh"].Value != null)
                        dtpNgaySinh.Value = (DateTime)row.Cells["NgaySinh"].Value;

                    cboHangTV.Text = row.Cells["HangTV"].Value?.ToString();

                    EnableInput(false); // Vừa chọn xong thì chỉ cho xem
                }
            }

            private void txtSDT_TextChanged(object sender, EventArgs e)
            {

            }

            private void lblHangTV_Click(object sender, EventArgs e)
            {

            }

            private void lblSDT_Click(object sender, EventArgs e)
            {

            }

            private void cboHangTV_SelectedIndexChanged(object sender, EventArgs e)
            {

            }
        }

        public class KhachHang
        {
            public string MaKH { get; set; }
            public string HoTen { get; set; }
            public string SDT { get; set; }
            public string CCCD { get; set; }
            public string GioiTinh { get; set; }
            public DateTime NgaySinh { get; set; }
            public string HangTV { get; set; }
        }
    }