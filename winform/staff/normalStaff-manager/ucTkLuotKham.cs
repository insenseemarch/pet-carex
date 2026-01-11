using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PetCareX
{
    public partial class ucTkLuotKham : UserControl
    {
        // 1. Dữ liệu giả lập
        private List<dynamic> DataDichVu = new List<dynamic>()
        {
            new { Loai = "Khám bệnh", CN = "Chi Nhánh 1", Luot = 150, Ngay = new DateTime(2025, 12, 10) },
            new { Loai = "Tiêm chủng", CN = "Chi Nhánh 1", Luot = 85,  Ngay = new DateTime(2025, 12, 25) },
            new { Loai = "Mua hàng",  CN = "Chi Nhánh 1", Luot = 120, Ngay = new DateTime(2026, 01, 01) },
            new { Loai = "Khám bệnh", CN = "Chi Nhánh 2", Luot = 110, Ngay = new DateTime(2025, 12, 15) },
            new { Loai = "Tiêm chủng", CN = "Chi Nhánh 2", Luot = 130, Ngay = new DateTime(2026, 01, 02) },
            new { Loai = "Mua hàng",  CN = "Chi Nhánh 2", Luot = 250, Ngay = new DateTime(2026, 01, 05) },
            new { Loai = "Khám bệnh", CN = "Chi Nhánh 3", Luot = 200, Ngay = new DateTime(2025, 12, 05) },
            new { Loai = "Tiêm chủng", CN = "Chi Nhánh 3", Luot = 50,  Ngay = new DateTime(2025, 12, 20) },
            new { Loai = "Mua hàng",  CN = "Chi Nhánh 3", Luot = 180, Ngay = new DateTime(2026, 01, 10) }
        };

        public ucTkLuotKham()
        {
            InitializeComponent();
            this.Load += ucTkLuotKham_Load;
            this.btnThongKe.Click += btnThongKe_Click;
            this.cboLoaiThongKe.SelectedIndexChanged += cboLoaiThongKe_SelectedIndexChanged;
        }

        private void ucTkLuotKham_Load(object sender, EventArgs e)
        {
            // Cấu hình ComboBox loại thống kê
            cboLoaiThongKe.Items.Clear();
            cboLoaiThongKe.Items.AddRange(new string[] { "Ngày", "Tháng", "Năm" });
            cboLoaiThongKe.SelectedIndex = 0;

            if (cboChiNhanh.Items.Count > 0) cboChiNhanh.SelectedIndex = 0;

            dtpTuNgay.Value = new DateTime(2025, 12, 1);
            dtpDenNgay.Value = DateTime.Now;

            // Cấu hình giới hạn cho ô chọn Năm để tránh lỗi nhập liệu
            numTuNam.Minimum = numDenNam.Minimum = 2000;
            numTuNam.Maximum = numDenNam.Maximum = 2100;
            numTuNam.Value = 2025; numDenNam.Value = 2026;

            chartDoanhThu.Visible = false;
        }

        // --- LOGIC ẨN HIỆN CONTROL TRONG 1 PANEL ---
        private void cboLoaiThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loai = cboLoaiThongKe.Text;
            bool isNgay = (loai == "Ngày");
            bool isThang = (loai == "Tháng");

            dtpTuNgay.Visible = dtpDenNgay.Visible = (isNgay || isThang);
            numTuNam.Visible = numDenNam.Visible = (loai == "Năm");

            if (isNgay)
            {
                dtpTuNgay.Format = DateTimePickerFormat.Short;
                dtpDenNgay.Format = DateTimePickerFormat.Short;
                lblTu.Text = "Từ ngày:"; lblDen.Text = "Đến ngày:";
            }
            else if (isThang)
            {
                dtpTuNgay.Format = DateTimePickerFormat.Custom;
                dtpTuNgay.CustomFormat = "MM/yyyy";
                dtpDenNgay.Format = DateTimePickerFormat.Custom;
                dtpDenNgay.CustomFormat = "MM/yyyy";
                lblTu.Text = "Từ tháng:"; lblDen.Text = "Đến tháng:";
            }
            else
            {
                lblTu.Text = "Từ năm:"; lblDen.Text = "Đến năm:";
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            VeBieuDoTron();
        }

        private void VeBieuDoTron()
        {
            string cnChon = cboChiNhanh.Text.Trim();
            string loaiTK = cboLoaiThongKe.Text;

            // 1. XÁC ĐỊNH KHOẢNG THỜI GIAN LỌC
            DateTime tu, den;
            if (loaiTK == "Ngày" || loaiTK == "Tháng")
            {
                tu = dtpTuNgay.Value.Date;
                den = dtpDenNgay.Value.Date;
                if (loaiTK == "Tháng")
                {
                    tu = new DateTime(tu.Year, tu.Month, 1);
                    den = new DateTime(den.Year, den.Month, DateTime.DaysInMonth(den.Year, den.Month));
                }
            }
            else
            {
                tu = new DateTime((int)numTuNam.Value, 1, 1);
                den = new DateTime((int)numDenNam.Value, 12, 31);
            }

            // 2. LỌC VÀ GOM NHÓM DỮ LIỆU
            var dataThongKe = DataDichVu
                .Where(x => x.CN.ToString().Equals(cnChon, StringComparison.OrdinalIgnoreCase)
                            && x.Ngay >= tu && x.Ngay <= den)
                .GroupBy(x => x.Loai.ToString())
                .Select(g => new { Loai = g.Key, TongLuot = g.Sum(x => (int)x.Luot) })
                .ToList();

            if (dataThongKe.Count == 0)
            {
                chartDoanhThu.Visible = false;
                MessageBox.Show("Không có dữ liệu trong khoảng này!");
                return;
            }

            // 3. CẤU HÌNH BIỂU ĐỒ
            chartDoanhThu.Visible = true;
            chartDoanhThu.Series.Clear();
            chartDoanhThu.ChartAreas.Clear();
            chartDoanhThu.Titles.Clear();
            chartDoanhThu.Legends.Clear();

            // Tiêu đề (Đã chỉnh tọa độ để không bị Panel che)
            Title t = new Title($"PHÂN BỔ DỊCH VỤ - {cnChon.ToUpper()}");
            t.Font = new Font("Arial", 14, FontStyle.Bold);
            t.Position = new ElementPosition(0, 22, 100, 7);
            chartDoanhThu.Titles.Add(t);

            // Vùng vẽ 3D (Thu hẹp chiều rộng để nhường chỗ cho Chú thích)
            ChartArea ca = new ChartArea("MainArea") { Area3DStyle = { Enable3D = true } };
            ca.Position = new ElementPosition(2, 30, 65, 65);
            chartDoanhThu.ChartAreas.Add(ca);

            // --- CHÚ THÍCH (LEGEND) Ở BÊN NGOÀI ---
            Legend lg = new Legend("Default");
            lg.Font = new Font("Arial", 10);
            lg.BackColor = Color.Transparent;
            // X=70: Đặt bảng chú thích ở bên phải miếng bánh
            lg.Position = new ElementPosition(70, 35, 28, 50);
            chartDoanhThu.Legends.Add(lg);

            // Series Pie
            Series s = new Series("DichVuSeries")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true, // Bật nhãn hiển thị

                // --- ĐÚNG Ý ÔNG: TRONG HÌNH TRÒN GHI % ---
                Label = "#PERCENT{P1}",

                // --- ĐÚNG Ý ÔNG: NGOÀI CHÚ THÍCH HIỆN TÊN DỊCH VỤ ---
                LegendText = "#VALX",

                Font = new Font("Arial", 11, FontStyle.Bold),
                LabelForeColor = Color.White,
                ToolTip = "Dịch vụ: #VALX\nSố lượt: #VAL"
            };

            // Ép nhãn % nằm bên trong miếng bánh
            s["PieLabelStyle"] = "Inside";

            // 4. ĐỔ DỮ LIỆU
            foreach (var item in dataThongKe)
            {
                s.Points.AddXY(item.Loai, item.TongLuot);
            }

            chartDoanhThu.Series.Add(s);
            chartDoanhThu.Palette = ChartColorPalette.BrightPastel;
            chartDoanhThu.Invalidate();
        }

        // --- CÁC HÀM SỰ KIỆN TRỐNG ĐỂ TRÁNH LỖI CS1061 ---
        private void dtpDenNgay_ValueChanged(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void chartLuotKham_Click(object sender, EventArgs e) { }
        private void ucTkLuotKham_Click(object sender, EventArgs e) { }
    }
}
