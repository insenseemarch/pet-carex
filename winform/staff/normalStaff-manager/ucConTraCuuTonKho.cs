using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PetCareX
{
    public partial class ucConTraCuuTonKho : UserControl
    {
        // Danh sách tổng hợp sản phẩm trên toàn hệ thống
        static List<SanPhamModel> dsKhoHeThong = new List<SanPhamModel>();

        public ucConTraCuuTonKho()
        {
            InitializeComponent();
            KhoiTaoDuLieuGiaLap();

            // 1. Ban đầu không gán DataSource để lưới trống
            dgvTonKho.DataSource = null;

            // 2. Đăng ký các sự kiện
            cboChiNhanh.SelectedIndexChanged += cboChiNhanh_SelectedIndexChanged;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            dgvTonKho.CellFormatting += dgvTonKho_CellFormatting;
        }

        private void KhoiTaoDuLieuGiaLap()
        {
            if (dsKhoHeThong.Count == 0)
            {
                // Chi nhánh 1
                dsKhoHeThong.Add(new SanPhamModel { MaSP = "SP001", TenSP = "Hạt Royal Canin Puppy", MaCN = "Chi nhánh 1", SoLuongTon = 0, HSD = new DateTime(2026, 6, 15) });
                dsKhoHeThong.Add(new SanPhamModel { MaSP = "SP002", TenSP = "Pate Ciao ức gà", MaCN = "Chi nhánh 1", SoLuongTon = 25, HSD = new DateTime(2024, 1, 1) });
                dsKhoHeThong.Add(new SanPhamModel { MaSP = "SP003", TenSP = "Cát vệ sinh đậu nành", MaCN = "Chi nhánh 1", SoLuongTon = 8, HSD = new DateTime(2026, 12, 20) });
                dsKhoHeThong.Add(new SanPhamModel { MaSP = "SP004", TenSP = "Xịt khử mùi thú cưng", MaCN = "Chi nhánh 1", SoLuongTon = 50, HSD = new DateTime(2027, 10, 10) });

                // Chi nhánh 2
                dsKhoHeThong.Add(new SanPhamModel { MaSP = "SP001", TenSP = "Hạt Royal Canin Puppy", MaCN = "Chi nhánh 2", SoLuongTon = 15, HSD = new DateTime(2026, 6, 15) });
                dsKhoHeThong.Add(new SanPhamModel { MaSP = "SP005", TenSP = "Sữa tắm bioline", MaCN = "Chi nhánh 2", SoLuongTon = 3, HSD = new DateTime(2026, 5, 5) });
                dsKhoHeThong.Add(new SanPhamModel { MaSP = "SP006", TenSP = "Thuốc tẩy giun Drontal", MaCN = "Chi nhánh 2", SoLuongTon = 20, HSD = new DateTime(2025, 12, 25) });

                // Chi nhánh 3
                dsKhoHeThong.Add(new SanPhamModel { MaSP = "SP007", TenSP = "Trụ cào móng mèo", MaCN = "Chi nhánh 3", SoLuongTon = 12, HSD = new DateTime(2099, 1, 1) });
                dsKhoHeThong.Add(new SanPhamModel { MaSP = "SP008", TenSP = "Bánh thưởng xương gặm", MaCN = "Chi nhánh 3", SoLuongTon = 5, HSD = new DateTime(2026, 1, 15) });
                dsKhoHeThong.Add(new SanPhamModel { MaSP = "SP009", TenSP = "Chuồng chó inox size L", MaCN = "Chi nhánh 3", SoLuongTon = 0, HSD = new DateTime(2030, 1, 1) });
            }
        }

        // --- XỬ LÝ SỰ KIỆN CHỌN CHI NHÁNH ---
        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocVaHienThi();
        }

        // --- XỬ LÝ TÌM KIẾM THỜI GIAN THỰC ---
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LocVaHienThi();
        }

        private void LocVaHienThi()
        {
            if (cboChiNhanh.SelectedItem == null) return;

            string chiNhanhChon = cboChiNhanh.SelectedItem.ToString();
            string tuKhoa = txtTimKiem.Text.ToLower().Trim();

            // Lọc theo chi nhánh TRƯỚC, sau đó lọc theo từ khóa
            var ketQua = dsKhoHeThong.Where(x =>
                x.MaCN == chiNhanhChon &&
                (x.TenSP.ToLower().Contains(tuKhoa) || x.MaSP.ToLower().Contains(tuKhoa))
            ).ToList();

            HienThiLenLuoi(ketQua);
        }

        private void HienThiLenLuoi(List<SanPhamModel> data)
        {
            // Cập nhật Trạng thái/Ghi chú cho từng dòng trước khi nạp
            foreach (var sp in data)
            {
                if (sp.HSD < DateTime.Now) { sp.TrangThai = "Hết hạn"; sp.GhiChu = "Không được bán"; }
                else if (sp.SoLuongTon == 0) { sp.TrangThai = "Hết hàng"; sp.GhiChu = "Cần nhập kho ngay"; }
                else if (sp.SoLuongTon < 10) { sp.TrangThai = "Tồn kho thấp"; sp.GhiChu = "Sắp hết hàng"; }
                else { sp.TrangThai = "Bình thường"; sp.GhiChu = "Ổn định"; }
            }

            dgvTonKho.DataSource = null;
            dgvTonKho.DataSource = data;

            if (dgvTonKho.Columns.Count > 0)
            {
                dgvTonKho.Columns["MaCN"].Visible = false; // Ẩn cột mã chi nhánh cho gọn
                dgvTonKho.Columns["HSD"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // --- TÔ MÀU THEO YÊU CẦU ---
        private void dgvTonKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTonKho.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string trangThai = e.Value.ToString();
                if (trangThai == "Hết hạn" || trangThai == "Hết hàng")
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.ForeColor = Color.White;
                }
                else if (trangThai == "Tồn kho thấp")
                {
                    e.CellStyle.BackColor = Color.Orange;
                }
                else if (trangThai == "Bình thường")
                {
                    e.CellStyle.BackColor = Color.LimeGreen;
                    e.CellStyle.ForeColor = Color.White;
                }
            }
        }
    }

    public class SanPhamModel
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string MaCN { get; set; } // Dùng để lọc theo ComboBox
        public int SoLuongTon { get; set; }
        public DateTime HSD { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
}