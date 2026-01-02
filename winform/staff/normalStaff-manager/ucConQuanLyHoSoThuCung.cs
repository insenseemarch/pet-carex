using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PetCareX
{
    public partial class ucQuanLyHoSoThuCung : UserControl
    {
        // 1. DỮ LIỆU GIẢ LẬP THEO LUỒNG NGHIỆP VỤ 15 (1).pdf
        static List<ThuCung> dsThuCung = new List<ThuCung>();

        // Danh sách mã khách hàng có sẵn để kiểm tra ràng buộc chủ nuôi
        static List<string> dsMaKHHopLe = new List<string> { "KH001", "KH002", "KH003", "KH004", "KH005" };

        public ucQuanLyHoSoThuCung()
        {
            InitializeComponent();
            KhoiTaoDuLieuMau();
            HienThiLenLuoi();
            EnableInput(false); // Khóa các ô nhập liệu khi vừa vào

            // Đăng ký sự kiện nút bấm
            btnThem.Click += btnThem_Click;
            btnLuu.Click += btnLuu_Click;
            dgvThuCung.CellClick += dgvThuCung_CellClick;
        }

        // HÀM TỰ SINH MÃ THÚ CƯNG (TCxxx) 🤖
        private string SinhMaThuCung()
        {
            if (dsThuCung.Count == 0) return "TC001";
            int maxId = dsThuCung.Select(x => int.Parse(x.MaThuCung.Substring(2))).Max();
            return "TC" + (maxId + 1).ToString("D3");
        }

        private void KhoiTaoDuLieuMau()
        {
            if (dsThuCung.Count == 0)
            {
                dsThuCung.Add(new ThuCung { MaThuCung = "TC001", MaKH = "KH001", TenThu = "Lu Lu", Loai = "Chó", Giong = "Poodle", NgaySinh = new DateTime(2022, 5, 10), GioiTinh = "Đực", TinhTrang = "Khỏe mạnh" });
                dsThuCung.Add(new ThuCung { MaThuCung = "TC002", MaKH = "KH002", TenThu = "Miu Miu", Loai = "Mèo", Giong = "Anh lông ngắn", NgaySinh = new DateTime(2023, 1, 15), GioiTinh = "Cái", TinhTrang = "Bình thường" });
            }
        }

        private void HienThiLenLuoi()
        {
            dgvThuCung.DataSource = null;
            dgvThuCung.DataSource = dsThuCung;
            if (dgvThuCung.Columns.Count > 0)
            {
                dgvThuCung.Columns["MaThuCung"].HeaderText = "Mã Pet";
                dgvThuCung.Columns["MaKH"].HeaderText = "Mã Chủ";
                dgvThuCung.Columns["TenThu"].HeaderText = "Tên Pet";
                dgvThuCung.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvThuCung.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void EnableInput(bool status)
        {
            txtMaThuCung.Enabled = false; // Mã thú cưng luôn khóa vì hệ thống tự sinh 🔒
            txtMaKH.Enabled = status;
            txtTenThu.Enabled = status;
            txtLoai.Enabled = status;
            txtGiong.Enabled = status;
            dtpNgaySinh.Enabled = status;
            cboGioiTinh.Enabled = status;
            txtTinhTrang.Enabled = status;

            btnLuu.Enabled = status;
            btnThem.Enabled = !status;
        }

        private void LamTrongO()
        {
            txtMaKH.Clear();
            txtTenThu.Clear();
            txtLoai.Clear();
            txtGiong.Clear();
            txtTinhTrang.Clear();
            if (cboGioiTinh.Items.Count > 0) cboGioiTinh.SelectedIndex = 0;
            dtpNgaySinh.Value = DateTime.Now;
        }

        // SỰ KIỆN NÚT THÊM ➕
        private void btnThem_Click(object sender, EventArgs e)
        {
            LamTrongO();
            txtMaThuCung.Text = SinhMaThuCung(); // Hiển thị mã mới
            EnableInput(true);
            txtMaKH.Focus();
        }

        // SỰ KIỆN NÚT LƯU 💾
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim().ToUpper();

            // RÀNG BUỘC: Kiểm tra mã khách hàng có tồn tại không
            if (!dsMaKHHopLe.Contains(maKH))
            {
                MessageBox.Show($"Lỗi: Mã khách hàng '{maKH}' không tồn tại!\nVui lòng nhập từ KH001 đến KH005.",
                                "Sai dữ liệu chủ nuôi ❌", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaKH.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtTenThu.Text) || string.IsNullOrEmpty(txtLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên và Loài thú cưng!", "Thông báo");
                return;
            }

            // Lưu vào danh sách giả lập
            dsThuCung.Add(new ThuCung
            {
                MaThuCung = txtMaThuCung.Text,
                MaKH = maKH,
                TenThu = txtTenThu.Text,
                Loai = txtLoai.Text,
                Giong = txtGiong.Text,
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = cboGioiTinh.Text,
                TinhTrang = txtTinhTrang.Text
            });

            HienThiLenLuoi();
            EnableInput(false);
            MessageBox.Show("Đã đăng ký hồ sơ thú cưng thành công!", "Thành công ✅");
        }

        private void dgvThuCung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThuCung.Rows[e.RowIndex];
                txtMaThuCung.Text = row.Cells["MaThuCung"].Value?.ToString();
                txtMaKH.Text = row.Cells["MaKH"].Value?.ToString();
                txtTenThu.Text = row.Cells["TenThu"].Value?.ToString();
                txtLoai.Text = row.Cells["Loai"].Value?.ToString();
                txtGiong.Text = row.Cells["Giong"].Value?.ToString();
                txtTinhTrang.Text = row.Cells["TinhTrang"].Value?.ToString();
                cboGioiTinh.Text = row.Cells["GioiTinh"].Value?.ToString();
                if (row.Cells["NgaySinh"].Value != null)
                    dtpNgaySinh.Value = (DateTime)row.Cells["NgaySinh"].Value;

                EnableInput(false);
            }
        }
    }

    // LỚP ĐỐI TƯỢNG THÚ CƯNG (THUCUNG)
    public class ThuCung
    {
        public string MaThuCung { get; set; }
        public string MaKH { get; set; }
        public string TenThu { get; set; }
        public string Loai { get; set; }
        public string Giong { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string TinhTrang { get; set; }
    }
}