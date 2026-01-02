using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PetCareX
{
    public partial class ucQuanLyDanhMuc : UserControl
    {
        // 1. DANH SÁCH DỮ LIỆU SẢN PHẨM
        static List<SanPham> dsSanPham = new List<SanPham>();
        bool dangThem = false;

        public ucQuanLyDanhMuc()
        {
            InitializeComponent();
            KhoiTaoDuLieuMau();
            HienThiLenLuoi();
            EnableInput(false);

            // Đăng ký các sự kiện chính 🖱️
            btnThem.Click += btnThem_Click;
            btnCapNhat.Click += btnCapNhat_Click;
            btnXoa.Click += btnXoa_Click;
            btnLuu.Click += btnLuu_Click;
            txtSearch.TextChanged += txtSearch_TextChanged;
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        // --- KHỞI TẠO VÀ HIỂN THỊ DỮ LIỆU ---

        private void KhoiTaoDuLieuMau()
        {
            if (dsSanPham.Count == 0)
            {
                dsSanPham.Add(new SanPham { MaSP = "SP001", TenSP = "Pate Mèo Royal Canin", LoaiSP = "Thức ăn", Gia = 45000, SoLuongTon = 100, HSD = new DateTime(2026, 05, 20) });
                dsSanPham.Add(new SanPham { MaSP = "SP002", TenSP = "Thuốc trị ve rận Frontline", LoaiSP = "Thuốc", Gia = 180000, SoLuongTon = 30, HSD = new DateTime(2027, 12, 15) });
            }
        }

        private void HienThiLenLuoi(List<SanPham> nguon = null)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = nguon ?? dsSanPham;

            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["MaSP"].HeaderText = "Mã SP";
                dataGridView1.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
                dataGridView1.Columns["LoaiSP"].HeaderText = "Loại";
                dataGridView1.Columns["Gia"].HeaderText = "Giá Bán";
                dataGridView1.Columns["Gia"].DefaultCellStyle.Format = "N0";
                dataGridView1.Columns["SoLuongTon"].HeaderText = "Tồn Kho";
                dataGridView1.Columns["HSD"].HeaderText = "Hạn Sử Dụng";
                dataGridView1.Columns["HSD"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // --- KIỂM TRA RÀNG BUỘC NGHIỆP VỤ ---

        private bool KiemTraHopLe()
        {
            // RB16.1: Mã sản phẩm duy nhất
            if (dangThem && dsSanPham.Any(x => x.MaSP.Trim().ToUpper() == txtMaSP.Text.Trim().ToUpper()))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại. Vui lòng nhập mã mới.", "Lỗi RB16.1 ⚠️");
                return false;
            }

            // RB16.2 & RB16.3: Giá và Số lượng không âm
            if (!double.TryParse(txtGia.Text, out double gia) || gia < 0 ||
                !int.TryParse(txtSoLuong.Text, out int sl) || sl < 0)
            {
                MessageBox.Show("Thông tin không hợp lệ. Giá và Số lượng tồn phải ≥ 0.", "Lỗi RB16.2/3 ⚠️");
                return false;
            }

            // RB16.5: Miền giá trị loại sản phẩm
            if (string.IsNullOrEmpty(cboLoaiSP.Text))
            {
                MessageBox.Show("Vui lòng chọn Loại sản phẩm hợp lệ.", "Lỗi RB16.5 ⚠️");
                return false;
            }

            return true;
        }

        // --- XỬ LÝ SỰ KIỆN NÚT BẤM ---

        private void btnThem_Click(object sender, EventArgs e)
        {
            dangThem = true;
            LamTrongO();
            EnableInput(true);
            txtMaSP.Focus();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSP.Text)) return;
            dangThem = false;
            EnableInput(true);
            txtMaSP.Enabled = false; // Không cho sửa khóa chính
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLe()) return;

            if (dangThem)
            {
                dsSanPham.Add(new SanPham
                {
                    MaSP = txtMaSP.Text.Trim(),
                    TenSP = txtTenSP.Text.Trim(),
                    LoaiSP = cboLoaiSP.Text,
                    Gia = double.Parse(txtGia.Text),
                    SoLuongTon = int.Parse(txtSoLuong.Text),
                    HSD = dtpHSD.Value
                });
            }
            else
            {
                var sp = dsSanPham.FirstOrDefault(x => x.MaSP == txtMaSP.Text);
                if (sp != null)
                {
                    sp.TenSP = txtTenSP.Text.Trim();
                    sp.LoaiSP = cboLoaiSP.Text;
                    sp.Gia = double.Parse(txtGia.Text);
                    sp.SoLuongTon = int.Parse(txtSoLuong.Text);
                    sp.HSD = dtpHSD.Value;
                }
            }

            HienThiLenLuoi();
            EnableInput(false);
            MessageBox.Show("Lưu thông tin sản phẩm thành công! ✅");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSP.Text)) return;

            // RB16.6: Không xoá vật lý nếu đã có giao dịch (Giả lập kiểm tra)
            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm {txtTenSP.Text}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                dsSanPham.RemoveAll(x => x.MaSP == txtMaSP.Text);
                HienThiLenLuoi();
                LamTrongO();
            }
        }

        // --- CÁC HÀM HỖ TRỢ ---

        private void EnableInput(bool status)
        {
            txtMaSP.Enabled = status;
            txtTenSP.Enabled = status;
            cboLoaiSP.Enabled = status;
            txtGia.Enabled = status;
            txtSoLuong.Enabled = status;
            dtpHSD.Enabled = status;
            btnLuu.Enabled = status;
            btnThem.Enabled = !status;
            btnCapNhat.Enabled = !status;
            btnXoa.Enabled = !status;
        }

        private void LamTrongO()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtGia.Clear();
            txtSoLuong.Clear();
            cboLoaiSP.SelectedIndex = -1;
            dtpHSD.Value = DateTime.Now;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var r = dataGridView1.Rows[e.RowIndex];
            txtMaSP.Text = r.Cells["MaSP"].Value.ToString();
            txtTenSP.Text = r.Cells["TenSP"].Value.ToString();
            cboLoaiSP.Text = r.Cells["LoaiSP"].Value.ToString();
            txtGia.Text = r.Cells["Gia"].Value.ToString();
            txtSoLuong.Text = r.Cells["SoLuongTon"].Value.ToString();
            dtpHSD.Value = (DateTime)r.Cells["HSD"].Value;
            EnableInput(false);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string k = txtSearch.Text.ToLower();
            var kq = dsSanPham.Where(x => x.TenSP.ToLower().Contains(k) || x.MaSP.ToLower().Contains(k)).ToList();
            HienThiLenLuoi(kq);
        }
    }

    // LỚP DỮ LIỆU SẢN PHẨM
    public class SanPham
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string LoaiSP { get; set; }
        public double Gia { get; set; }
        public int SoLuongTon { get; set; }
        public DateTime HSD { get; set; }
    }
}