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
    public partial class ucConTaoLich : UserControl
    {
        public ucConTaoLich()
        {
            InitializeComponent();
        }

        // 1. Danh sách bác sĩ trực (Phân công theo khung giờ)
        private List<dynamic> LichTrucBS = new List<dynamic>()
        {
            new { BS = "BS. Cường", CN = "Chi nhánh 1", Ngay = "28/12/2025", Tu = 8.5, Den = 10.0 },
            new { BS = "BS. An", CN = "Chi nhánh 1", Ngay = "28/12/2025", Tu = 8.5, Den = 10.0 },
            new { BS = "BS. Bình", CN = "Chi nhánh 1", Ngay = "28/12/2025", Tu = 8.5, Den = 16.5 },
            new { BS = "BS. Khôi", CN = "Chi nhánh 2", Ngay = "28/12/2025", Tu = 8.5, Den = 16.5 },
            new { BS = "BS. Linh", CN = "Chi nhánh 2", Ngay = "28/12/2025", Tu = 14.0, Den = 16.5 }
        };

        // 2. Danh sách lịch ĐÃ ĐẶT (Nhiều dữ liệu để test trùng)
        private List<dynamic> DanhSachLichDaCo = new List<dynamic>()
        {
            new { CN = "Chi nhánh 1", Ngay = "28/12/2025", Gio = "08:30 - 09:00", BS = "BS. Cường" },
            new { CN = "Chi nhánh 1", Ngay = "28/12/2025", Gio = "08:30 - 09:00", BS = "BS. An" },
            new { CN = "Chi nhánh 1", Ngay = "28/12/2025", Gio = "08:30 - 09:00", BS = "BS. Bình" },
            new { CN = "Chi nhánh 1", Ngay = "28/12/2025", Gio = "09:00 - 09:30", BS = "BS. Cường" },
            new { CN = "Chi nhánh 2", Ngay = "28/12/2025", Gio = "14:30 - 15:00", BS = "BS. Khôi" }
        };

        private void btnTaoLich_Click(object sender, EventArgs e)
        {
            // BƯỚC 4: Kiểm tra bác sĩ rảnh
            string bsRanh = CheckSlotVaTimBacSi(cboChiNhanh.Text, dtpNgayHen.Value, cboKhungGio.Text);

            if (bsRanh != null)
            {
                // BƯỚC 5: Lưu lịch và thông báo
                DanhSachLichDaCo.Add(new
                {
                    CN = cboChiNhanh.Text,
                    Ngay = dtpNgayHen.Value.ToString("dd/MM/yyyy"),
                    Gio = cboKhungGio.Text,
                    BS = bsRanh
                });

                MessageBox.Show($"ĐẶT LỊCH THÀNH CÔNG!\nBác sĩ phụ trách: {bsRanh}", "Xác nhận");
                ResetForm();
            }
            else
            {
                MessageBox.Show("Khung giờ này tất cả bác sĩ đã kín lịch. Vui lòng chọn giờ khác!", "Hết chỗ");
            }
        }

        // --- HÀM KIỂM TRA KHUNG GIỜ TRỐNG (BƯỚC 4) ---
        private string CheckSlotVaTimBacSi(string chiNhanh, DateTime ngay, string khungGio)
        {
            string ngayHenStr = ngay.ToString("dd/MM/yyyy");

            // 1. Chuyển khung giờ chọn thành số để so sánh (Ví dụ: "08:30 - 09:00" -> lấy 8.5)
            double gioBatDau = double.Parse(khungGio.Split(':')[0]) + (double.Parse(khungGio.Split(':')[1].Substring(0, 2)) / 60.0);

            // 2. Lấy danh sách bác sĩ ĐANG TRỰC
            var dsBacSiDangTruc = LichTrucBS.Where(x => x.CN == chiNhanh && x.Ngay == ngayHenStr &&
                                                       gioBatDau >= x.Tu && gioBatDau < x.Den)
                                           .Select(x => (string)x.BS).ToList();

            // 3. Tìm ông nào rảnh (chưa có trong DanhSachLichDaCo)
            foreach (var tenBS in dsBacSiDangTruc)
            {
                bool daBiDat = DanhSachLichDaCo.Any(l => l.CN == chiNhanh && l.Ngay == ngayHenStr &&
                                                         l.Gio == khungGio && l.BS == tenBS);
                if (!daBiDat) return tenBS;
            }

            return null;
        }

        private bool SaveToKhamBenh() => true;

        private bool SaveToTiemPhong() => true;

        private void ResetForm()
        {
            txtMaThucung.Clear();
            cboDichVu.SelectedIndex = -1;
            cboChiNhanh.SelectedIndex = -1;
            cboKhungGio.SelectedIndex = -1;
            dtpNgayHen.Value = DateTime.Now;
        }
    } 
} 