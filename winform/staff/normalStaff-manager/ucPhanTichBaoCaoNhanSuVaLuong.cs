using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace PetCareX
{
    public partial class ucPhanTichBaoCaoNhanSuVaLuong : UserControl
    {
        // Danh sách dữ liệu giả lập
        private List<NV_BaoCao> dsNhanVien;
        private List<HD_BaoCao> dsHoaDon;
        private List<DG_BaoCao> dsDanhGia;
        private List<KhamTiem_BaoCao> dsKhamTiem;

        public ucPhanTichBaoCaoNhanSuVaLuong()
        {
            InitializeComponent();
            SetupControls();
            KhoiTaoDuLieu();
        }

        private void SetupControls()
        {
            // Cấu hình DateTimePicker chỉ hiện Tháng/Năm 📅
            dtpThangNam.Format = DateTimePickerFormat.Custom;
            dtpThangNam.CustomFormat = "MM/yyyy";
            dtpThangNam.ShowUpDown = true;

            // Gán sự kiện
            btnXemBaoCao.Click += btnXemBaoCao_Click;
            dataGridView1.CellClick += dataGridView1_CellClick;
            btnCapNhat.Click += btnCapNhat_Click;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void KhoiTaoDuLieu()
        {
            dsNhanVien = new List<NV_BaoCao> {
                new NV_BaoCao("NV01", "Nguyễn Văn A", "Bác sĩ", 15000000),
                new NV_BaoCao("NV02", "Trần Thị B", "Lễ tân", 8000000),
                new NV_BaoCao("NV03", "Lê Văn C", "Bác sĩ", 14500000)
            };

            // Dữ liệu đánh giá (RB28.4) ⭐
            dsDanhGia = new List<DG_BaoCao> {
                new DG_BaoCao("NV01", 5, new DateTime(2026, 1, 1)),
                new DG_BaoCao("NV01", 4, new DateTime(2026, 1, 2)),
                new DG_BaoCao("NV02", 5, new DateTime(2026, 1, 5))
            };

            // Dữ liệu hiệu suất (RB28.3) 👥
            dsHoaDon = new List<HD_BaoCao> {
                new HD_BaoCao("NV02", new DateTime(2026, 1, 2)),
                new HD_BaoCao("NV02", new DateTime(2026, 1, 3))
            };

            dsKhamTiem = new List<KhamTiem_BaoCao> {
                new KhamTiem_BaoCao("NV01", new DateTime(2026, 1, 1)),
                new KhamTiem_BaoCao("NV03", new DateTime(2026, 1, 1))
            };
        }
        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            int thang = dtpThangNam.Value.Month;
            int nam = dtpThangNam.Value.Year;

            var baoCao = dsNhanVien.Select(nv => {
                // 1. Tính số khách phục vụ (RB28.3)
                int soKhach = 0;
                if (nv.ChucVu == "Bác sĩ")
                {
                    soKhach = dsKhamTiem.Count(kt => kt.MaBS == nv.MaNV && kt.Ngay.Month == thang && kt.Ngay.Year == nam);
                }
                else
                {
                    soKhach = dsHoaDon.Count(hd => hd.MaNVLap == nv.MaNV && hd.NgayLap.Month == thang && hd.NgayLap.Year == nam);
                }

                // 2. Tính điểm trung bình (RB28.4) ⭐
                var listDanhGia = dsDanhGia.Where(dg => dg.MaNV == nv.MaNV && dg.Ngay.Month == thang && dg.Ngay.Year == nam).ToList();
                double dtb = listDanhGia.Any() ? listDanhGia.Average(dg => dg.DiemThaiDo) : 0;

                return new
                {
                    nv.MaNV,
                    nv.HoTen,
                    nv.ChucVu,
                    SoKhachPhucVu = soKhach,
                    DiemTrungBinh = Math.Round(dtb, 1),
                    nv.LuongCoBan
                };
            }).ToList();

            dataGridView1.DataSource = baoCao;
            FormatGrid();
        }

        private void FormatGrid()
        {
            dataGridView1.Columns["MaNV"].HeaderText = "Mã NV";
            dataGridView1.Columns["HoTen"].HeaderText = "Họ Tên";
            dataGridView1.Columns["SoKhachPhucVu"].HeaderText = "Khách Phục Vụ";
            dataGridView1.Columns["DiemTrungBinh"].HeaderText = "Điểm Đánh Giá";
            dataGridView1.Columns["LuongCoBan"].HeaderText = "Lương Cơ Bản";
            dataGridView1.Columns["LuongCoBan"].DefaultCellStyle.Format = "N0";
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Lấy thông tin từ dòng được chọn
            string ten = dataGridView1.Rows[e.RowIndex].Cells["HoTen"].Value.ToString();
            string luong = dataGridView1.Rows[e.RowIndex].Cells["LuongCoBan"].Value.ToString();

            lblTenNhanVienDangChon.Text = "Nhân viên: " + ten;
            txtLuongMoi.Text = luong;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần cập nhật!");
                return;
            }

            // Kiểm tra tính hợp lệ của lương (RB28.5)
            if (double.TryParse(txtLuongMoi.Text, out double luongMoi) && luongMoi >= 0)
            {
                string maNV = dataGridView1.CurrentRow.Cells["MaNV"].Value.ToString();
                var nv = dsNhanVien.FirstOrDefault(x => x.MaNV == maNV);

                if (nv != null)
                {
                    nv.LuongCoBan = luongMoi;
                    MessageBox.Show("Cập nhật lương thành công!");
                    btnXemBaoCao_Click(null, null); // Refresh lại bảng
                }
            }
            else
            {
                MessageBox.Show("Mức lương mới không hợp lệ (phải là số và >= 0)!");
            }
        }
    }

    // --- CÁC LỚP DỮ LIỆU BỔ TRỢ ---
    public class NV_BaoCao
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public double LuongCoBan { get; set; }
        public NV_BaoCao(string m, string t, string cv, double l) { MaNV = m; HoTen = t; ChucVu = cv; LuongCoBan = l; }
    }

    public class HD_BaoCao
    {
        public string MaNVLap { get; set; }
        public DateTime NgayLap { get; set; }
        public HD_BaoCao(string m, DateTime n) { MaNVLap = m; NgayLap = n; }
    }

    public class DG_BaoCao
    {
        public string MaNV { get; set; }
        public int DiemThaiDo { get; set; }
        public DateTime Ngay { get; set; }
        public DG_BaoCao(string m, int d, DateTime n) { MaNV = m; DiemThaiDo = d; Ngay = n; }
    }

    public class KhamTiem_BaoCao
    {
        public string MaBS { get; set; }
        public DateTime Ngay { get; set; }
        public KhamTiem_BaoCao(string m, DateTime n) { MaBS = m; Ngay = n; }
    }
}