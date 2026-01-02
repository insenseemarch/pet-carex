using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PetCareX
{

    public partial class ucConLapHoaDon : UserControl
    {
        DataTable dtKho = new DataTable();
        Dictionary<string, (string Ten, string Hang)> dsKhachHang = new Dictionary<string, (string, string)>
        {
            { "KH001", ("Nguyễn Văn A", "VIP") },
            { "KH002", ("Trần Thị B", "Thân thiết") }
        };

        public ucConLapHoaDon()
        {
            InitializeComponent();
            // Đăng ký sự kiện (giữ nguyên của ông)
            this.Load += ucConLapHoaDon_Load;
            this.txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            this.txtMaKH.TextChanged += TxtMaKH_TextChanged;
            this.dgvDanhMuc.CellContentClick += dgvDanhMuc_CellContentClick;
            this.dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            this.btnLapHoaDon.Click += btnLapHoaDon_Click;
        }

        private void ucConLapHoaDon_Load(object sender, EventArgs e)
        {
            this.Padding = new Padding(0, 50, 0, 0);

            // Khởi tạo cấu trúc DataTable (giữ nguyên logic của ông)
            if (dtKho.Columns.Count == 0)
            {
                dtKho.Columns.Add("Ma", typeof(string));
                dtKho.Columns.Add("Ten", typeof(string));
                dtKho.Columns.Add("Loai", typeof(string));
                dtKho.Columns.Add("Gia", typeof(double));
                dtKho.Columns.Add("SLTonKho", typeof(string));
            }

            NapDuLieuHeThong();

            // Cấu hình ánh xạ cột cho dgvDanhMuc
            dgvDanhMuc.AutoGenerateColumns = false;
            if (dgvDanhMuc.Columns.Contains("Ma")) dgvDanhMuc.Columns["Ma"].DataPropertyName = "Ma";
            if (dgvDanhMuc.Columns.Contains("Ten")) dgvDanhMuc.Columns["Ten"].DataPropertyName = "Ten";
            if (dgvDanhMuc.Columns.Contains("Loai")) dgvDanhMuc.Columns["Loai"].DataPropertyName = "Loai";
            if (dgvDanhMuc.Columns.Contains("Gia")) dgvDanhMuc.Columns["Gia"].DataPropertyName = "Gia";
            if (dgvDanhMuc.Columns.Contains("SLTonKho")) dgvDanhMuc.Columns["SLTonKho"].DataPropertyName = "SLTonKho";

            dgvDanhMuc.DataSource = dtKho;

            // --- THAY ĐỔI TẠI ĐÂY: Lấy tên từ Session thay vì ghi cứng ---
            txtTenNV.Text = Session.HoTen;

            dtpNgayLap.Value = DateTime.Now;
            numericUpDown1.Value = 1;

            AnHienFooter(false);

            dgvDanhMuc.Visible = true;
            dgvDanhMuc.BringToFront();
        }

        public void NapDuLieuHeThong()
        {
            dtKho.Rows.Clear();
            dtKho.Rows.Add("DV01", "Khám bệnh tổng quát", "Dịch vụ", 200000, "-");
            dtKho.Rows.Add("SP01", "Hạt Royal Canin 1kg", "Sản phẩm", 350000, "25");
            dtKho.Rows.Add("SP02", "Cát vệ sinh Maneki", "Sản phẩm", 95000, "60");
            dtKho.Rows.Add("T01", "Thuốc trị nấm Fungikur", "Thuốc", 75000, "15");
            dgvDanhMuc.Refresh();
        }

        // LOGIC XÓA MÓN (Giữ nguyên logic của ông)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "btnXoa")
            {
                string maMonXoa = dataGridView1.Rows[e.RowIndex].Cells["Ma_HD"].Value.ToString().Trim();
                int slTraLai = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SoLuong_HD"].Value);

                foreach (DataRow r in dtKho.Rows)
                {
                    if (r["Ma"].ToString().Trim() == maMonXoa && r["SLTonKho"].ToString() != "-")
                    {
                        int slHienTai = int.Parse(r["SLTonKho"].ToString());
                        r["SLTonKho"] = (slHienTai + slTraLai).ToString();
                        break;
                    }
                }

                dataGridView1.Rows.RemoveAt(e.RowIndex);
                dgvDanhMuc.Refresh();
                KiemTraDieuKienHienThi();
            }
        }

        // LOGIC THÊM MÓN (Giữ nguyên logic của ông)
        private void dgvDanhMuc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDanhMuc.Columns[e.ColumnIndex].Name == "btnThemCol")
            {
                // Sử dụng CurrencyManager để lấy đúng hàng dù đang lọc
                DataRowView drv = (DataRowView)dgvDanhMuc.Rows[e.RowIndex].DataBoundItem;
                DataRow rowKho = drv.Row;

                int slMua = (int)numericUpDown1.Value;

                if (rowKho["SLTonKho"].ToString() != "-")
                {
                    int slHienTai = int.Parse(rowKho["SLTonKho"].ToString());
                    if (slMua > slHienTai)
                    {
                        MessageBox.Show($"Kho chỉ còn {slHienTai}!", "Cảnh báo");
                        return;
                    }
                    rowKho["SLTonKho"] = (slHienTai - slMua).ToString();
                }

                string ma = rowKho["Ma"].ToString();
                string ten = rowKho["Ten"].ToString();
                double gia = Convert.ToDouble(rowKho["Gia"]);

                bool daCo = false;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (r.Cells["Ma_HD"].Value?.ToString() == ma)
                    {
                        int slMoi = Convert.ToInt32(r.Cells["SoLuong_HD"].Value) + slMua;
                        r.Cells["SoLuong_HD"].Value = slMoi;
                        r.Cells["ThanhTien_HD"].Value = slMoi * gia;
                        daCo = true;
                        break;
                    }
                }

                if (!daCo)
                {
                    dataGridView1.Rows.Add(ma, ten, slMua, gia, slMua * gia);
                }

                dgvDanhMuc.Refresh();
                KiemTraDieuKienHienThi();
            }
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng và chọn món!", "Thông báo");
                return;
            }

            // 1. Gom dữ liệu: Dùng Session.HoTen để gán cho TenNV
            HoaDonThanhToan duLieuGuiDi = new HoaDonThanhToan
            {
                MaKH = txtMaKH.Text,
                TenNV = Session.HoTen, // Lấy tên nhân viên đang đăng nhập
                NgayLap = dtpNgayLap.Value,
                TongTien = lblTongTien.Text,
                ThanhTien = lblThanhTien.Text,
                HangKH = lblHangKH.Text,
                DiemTichLuy = lblDiemTichLuy.Text
            };

            // 2. Hiện thông báo
            MessageBox.Show($"Đã lập hóa đơn thành công cho khách hàng: {txtMaKH.Text}\n{lblThanhTien.Text}",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 3. Chuyển sang trang thanh toán
            ucConXuLyThanhToan trangThanhToan = new ucConXuLyThanhToan(duLieuGuiDi);
            trangThanhToan.Dock = DockStyle.Fill;

            Control pnlChinh = this.Parent;
            if (pnlChinh != null)
            {
                pnlChinh.Controls.Add(trangThanhToan);
                trangThanhToan.BringToFront();
            }

            // 4. Reset Form (BỎ NapDuLieuHeThong() để giữ số tồn kho đã trừ)
            dataGridView1.Rows.Clear();
            txtMaKH.Clear();
            KiemTraDieuKienHienThi();
        }

        private void KiemTraDieuKienHienThi()
        {
            bool hopLe = !string.IsNullOrWhiteSpace(txtMaKH.Text) && dataGridView1.Rows.Count > 0;
            AnHienFooter(hopLe);
            if (hopLe) TinhToanHoaDon();
        }

        private void AnHienFooter(bool hien)
        {
            lblTongTien.Visible = hien;
            lblHangKH.Visible = hien;
            lblThanhTien.Visible = hien;
            lblDiemTichLuy.Visible = hien;
            btnLapHoaDon.Visible = hien;
        }

        private void TinhToanHoaDon()
        {
            double tong = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
                if (r.Cells["ThanhTien_HD"].Value != null)
                    tong += Convert.ToDouble(r.Cells["ThanhTien_HD"].Value);

            double giam = 0;
            string hang = "Vãng lai";
            string maKH = txtMaKH.Text.Trim();

            if (dsKhachHang.ContainsKey(maKH))
            {
                hang = dsKhachHang[maKH].Hang;
                if (hang == "VIP") giam = 0.1;
                else if (hang == "Thân thiết") giam = 0.05;
            }

            double thanhTien = tong * (1 - giam);
            int diem = (int)(thanhTien / 50000);

            lblTongTien.Text = $"Tổng tạm tính: {tong:N0} VNĐ";
            lblHangKH.Text = $"Hạng: {hang} (Giảm {giam * 100}%)";
            lblThanhTien.Text = $"THÀNH TIỀN: {thanhTien:N0} VNĐ";
            lblDiemTichLuy.Text = $"+{diem} điểm loyalty";
        }

        private void TxtMaKH_TextChanged(object sender, EventArgs e) => KiemTraDieuKienHienThi();

        // FIX: Hàm lọc tìm kiếm chuẩn không mất dữ liệu
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim().Replace("'", "''"); // Chống lỗi SQL injection đơn giản
            DataView dv = dtKho.DefaultView;
            dv.RowFilter = $"Ten LIKE '%{key}%' OR Ma LIKE '%{key}%'";
            // Không gán lại DataSource bằng ToTable() để giữ liên kết với dtKho ban đầu
        }

        private void panelTop_Paint(object sender, PaintEventArgs e) { }
    }
}
