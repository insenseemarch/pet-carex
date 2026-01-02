using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PetCareX
{
    public partial class ucTkSanPham : UserControl
    {
        // 1. Bộ dữ liệu giả lập doanh thu chi tiết cho 3 chi nhánh (18/12/2025 - 01/01/2026)
        private List<dynamic> DataSanPham = new List<dynamic>()
        {
            // CHI NHÁNH 1 - Tăng trưởng mạnh cuối năm
            new { CN = "Chi Nhánh 1", Tien = 1500000, Ngay = new DateTime(2025, 12, 18) },
            new { CN = "Chi Nhánh 1", Tien = 2100000, Ngay = new DateTime(2025, 12, 19) },
            new { CN = "Chi Nhánh 1", Tien = 1800000, Ngay = new DateTime(2025, 12, 20) },
            new { CN = "Chi Nhánh 1", Tien = 3500000, Ngay = new DateTime(2025, 12, 22) },
            new { CN = "Chi Nhánh 1", Tien = 4200000, Ngay = new DateTime(2025, 12, 24) },
            new { CN = "Chi Nhánh 1", Tien = 6500000, Ngay = new DateTime(2025, 12, 26) },
            new { CN = "Chi Nhánh 1", Tien = 5800000, Ngay = new DateTime(2025, 12, 28) },
            new { CN = "Chi Nhánh 1", Tien = 8200000, Ngay = new DateTime(2025, 12, 30) },
            new { CN = "Chi Nhánh 1", Tien = 9500000, Ngay = new DateTime(2025, 12, 31) },
            new { CN = "Chi Nhánh 1", Tien = 7100000, Ngay = new DateTime(2026, 01, 01) },

            // CHI NHÁNH 2 - Tăng trưởng đột biến ngày lễ
            new { CN = "Chi Nhánh 2", Tien = 2000000, Ngay = new DateTime(2025, 12, 18) },
            new { CN = "Chi Nhánh 2", Tien = 2500000, Ngay = new DateTime(2025, 12, 21) },
            new { CN = "Chi Nhánh 2", Tien = 3800000, Ngay = new DateTime(2025, 12, 24) },
            new { CN = "Chi Nhánh 2", Tien = 7200000, Ngay = new DateTime(2025, 12, 25) },
            new { CN = "Chi Nhánh 2", Tien = 5500000, Ngay = new DateTime(2025, 12, 27) },
            new { CN = "Chi Nhánh 2", Tien = 9100000, Ngay = new DateTime(2025, 12, 30) },
            new { CN = "Chi Nhánh 2", Tien = 12500000, Ngay = new DateTime(2025, 12, 31) },
            new { CN = "Chi Nhánh 2", Tien = 10500000, Ngay = new DateTime(2026, 01, 01) },

            // CHI NHÁNH 3 - Doanh thu ổn định
            new { CN = "Chi Nhánh 3", Tien = 3000000, Ngay = new DateTime(2025, 12, 18) },
            new { CN = "Chi Nhánh 3", Tien = 3200000, Ngay = new DateTime(2025, 12, 20) },
            new { CN = "Chi Nhánh 3", Tien = 2900000, Ngay = new DateTime(2025, 12, 23) },
            new { CN = "Chi Nhánh 3", Tien = 4100000, Ngay = new DateTime(2025, 12, 26) },
            new { CN = "Chi Nhánh 3", Tien = 3800000, Ngay = new DateTime(2025, 12, 28) },
            new { CN = "Chi Nhánh 3", Tien = 5500000, Ngay = new DateTime(2025, 12, 31) },
            new { CN = "Chi Nhánh 3", Tien = 4200000, Ngay = new DateTime(2026, 01, 01) }
        };

        public ucTkSanPham()
        {
            InitializeComponent();
            this.Load += ucTkSanPham_Load;
            this.btnThongKe.Click += btnThongKe_Click;
            this.cboLoaiThongKe.SelectedIndexChanged += cboLoaiThongKe_SelectedIndexChanged;
        }

        private void ucTkSanPham_Load(object sender, EventArgs e)
        {
            // Thiết lập ComboBox loại thống kê
            cboLoaiThongKe.Items.Clear();
            cboLoaiThongKe.Items.AddRange(new string[] { "Ngày", "Tháng", "Năm" });
            cboLoaiThongKe.SelectedIndex = 0;

            if (cboChiNhanh.Items.Count > 0) cboChiNhanh.SelectedIndex = 0;

            dtpTuNgay.Value = new DateTime(2025, 12, 18);
            dtpDenNgay.Value = DateTime.Now;

            chartDoanhThu.Visible = false;
        }

        // --- ĐIỀU KHIỂN ẨN/HIỆN CONTROL TRONG PANEL ---
        private void cboLoaiThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loai = cboLoaiThongKe.Text;

            if (loai == "Ngày")
            {
                dtpTuNgay.Visible = dtpDenNgay.Visible = true;
                numTuNam.Visible = numDenNam.Visible = false; // Giả sử ông đặt tên NumericUpDown như vậy
                dtpTuNgay.Format = DateTimePickerFormat.Short;
                dtpDenNgay.Format = DateTimePickerFormat.Short;
                lblTu.Text = "Từ ngày:"; lblDen.Text = "Đến ngày:";
            }
            else if (loai == "Tháng")
            {
                dtpTuNgay.Visible = dtpDenNgay.Visible = true;
                numTuNam.Visible = numDenNam.Visible = false;
                // Ép hiện mỗi Tháng/Năm trên bảng chọn
                dtpTuNgay.Format = DateTimePickerFormat.Custom;
                dtpTuNgay.CustomFormat = "MM/yyyy";
                dtpDenNgay.Format = DateTimePickerFormat.Custom;
                dtpDenNgay.CustomFormat = "MM/yyyy";
                lblTu.Text = "Từ tháng:"; lblDen.Text = "Đến tháng:";
            }
            else // Theo Năm
            {
                dtpTuNgay.Visible = dtpDenNgay.Visible = false;
                numTuNam.Visible = numDenNam.Visible = true;
                lblTu.Text = "Từ năm:"; lblDen.Text = "Đến năm:";
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            VeBieuDoTangTruong();
        }

        private void VeBieuDoTangTruong()
        {
            string cnChon = cboChiNhanh.Text.Trim();
            string loaiTK = cboLoaiThongKe.Text;
            var dataDayDu = new List<dynamic>();

            string formatX = "dd";
            DateTimeIntervalType intervalType = DateTimeIntervalType.Days;
            string titleX = "Ngày";

            // --- 1. XỬ LÝ DỮ LIỆU ĐA NĂNG ---
            if (loaiTK == "Ngày")
            {
                DateTime tu = dtpTuNgay.Value.Date;
                DateTime den = dtpDenNgay.Value.Date;
                var data = DataSanPham.Where(x => x.CN == cnChon && x.Ngay >= tu && x.Ngay <= den)
                             .GroupBy(x => x.Ngay.Date)
                             .ToDictionary(g => g.Key, g => g.Sum(x => (double)x.Tien));

                for (DateTime d = tu; d <= den; d = d.AddDays(1))
                    dataDayDu.Add(new { Time = d, Tien = data.ContainsKey(d) ? data[d] : 0 });
            }
            else if (loaiTK == "Tháng")
            {
                // Lấy mốc đầu tháng
                DateTime tuThang = new DateTime(dtpTuNgay.Value.Year, dtpTuNgay.Value.Month, 1);
                DateTime denThang = new DateTime(dtpDenNgay.Value.Year, dtpDenNgay.Value.Month, 1);

                var data = DataSanPham.Where(x => x.CN == cnChon)
                             .Select(x => new { x.Tien, ThangNam = new DateTime(x.Ngay.Year, x.Ngay.Month, 1) })
                             .Where(x => x.ThangNam >= tuThang && x.ThangNam <= denThang)
                             .GroupBy(x => x.ThangNam)
                             .ToDictionary(g => g.Key, g => g.Sum(x => (double)x.Tien));

                for (DateTime d = tuThang; d <= denThang; d = d.AddMonths(1))
                    dataDayDu.Add(new { Time = d, Tien = data.ContainsKey(d) ? data[d] : 0 });

                formatX = "MM/yy"; titleX = "Tháng"; intervalType = DateTimeIntervalType.Months;
            }
            else // Theo Năm
            {
                int tuN = (int)numTuNam.Value;
                int denN = (int)numDenNam.Value;
                var data = DataSanPham.Where(x => x.CN == cnChon && x.Ngay.Year >= tuN && x.Ngay.Year <= denN)
                             .GroupBy(x => x.Ngay.Year)
                             .ToDictionary(g => g.Key, g => g.Sum(x => (double)x.Tien));

                for (int y = tuN; y <= denN; y++)
                    dataDayDu.Add(new { Time = new DateTime(y, 1, 1), Tien = data.ContainsKey(y) ? data[y] : 0 });

                formatX = "yyyy"; titleX = "Năm"; intervalType = DateTimeIntervalType.Years;
            }

            if (dataDayDu.Count == 0) return;

            // --- 2. CẤU HÌNH BIỂU ĐỒ TĂNG TRƯỞNG ---
            chartDoanhThu.Visible = true;
            chartDoanhThu.Series.Clear();
            chartDoanhThu.ChartAreas.Clear();
            chartDoanhThu.Titles.Clear();

            ChartArea ca = new ChartArea("MainArea");
            // Fix khuất chữ trục X
            ca.Position = new ElementPosition(0, 0, 100, 90);
            chartDoanhThu.ChartAreas.Add(ca);

            ca.AxisX.Title = titleX;
            ca.AxisY.Title = "Doanh thu (VNĐ)";
            ca.AxisX.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            ca.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            ca.AxisX.Interval = 1;
            ca.AxisX.IntervalType = intervalType;
            ca.AxisX.LabelStyle.Format = formatX;
            ca.AxisY.LabelStyle.Format = "{0:N0}";
            ca.AxisX.MajorGrid.LineColor = Color.LightGray;
            ca.AxisY.MajorGrid.LineColor = Color.LightGray;

            // --- 3. VẼ NÉT CONG (SPLINE) & CHẤM ĐỎ ---
            Series s = new Series("Doanh thu")
            {
                ChartType = SeriesChartType.Spline, // Đường cong mượt mà
                XValueType = ChartValueType.DateTime,
                BorderWidth = 3,
                Color = Color.DodgerBlue,

                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 10, // Chấm đỏ to rõ
                MarkerColor = Color.Red,
                MarkerBorderColor = Color.White,
                MarkerBorderWidth = 1
            };

            foreach (var item in dataDayDu)
                s.Points.AddXY(item.Time, item.Tien);

            chartDoanhThu.Series.Add(s);
        }
    }
}