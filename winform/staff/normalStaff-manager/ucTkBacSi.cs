using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PetCareX
{
    public partial class ucTkBacSi : UserControl
    {
        private List<dynamic> DataHethong = new List<dynamic>()
        {
            new { Ten = "BS. Cường", CN = "Chi nhánh 1", DoanhThu = 52000000, Ngay = new DateTime(2025, 12, 10) },
            new { Ten = "BS. An", CN = "Chi nhánh 1", DoanhThu = 35600000, Ngay = new DateTime(2025, 12, 15) },
            new { Ten = "BS. Bình", CN = "Chi nhánh 1", DoanhThu = 28900000, Ngay = new DateTime(2025, 12, 20) },
            new { Ten = "BS. Khôi", CN = "Chi nhánh 2", DoanhThu = 45200000, Ngay = new DateTime(2025, 12, 25) },
            new { Ten = "BS. Linh", CN = "Chi nhánh 2", DoanhThu = 31000000, Ngay = new DateTime(2026, 01, 05) },
            new { Ten = "BS. Nam", CN = "Chi nhánh 3", DoanhThu = 38000000, Ngay = new DateTime(2025, 11, 20) },
            new { Ten = "BS. Lan", CN = "Chi nhánh 3", DoanhThu = 22000000, Ngay = new DateTime(2025, 12, 01) },
            new { Ten = "BS. Tuấn", CN = "Chi nhánh 3", DoanhThu = 25000000, Ngay = new DateTime(2026, 01, 10) }
        };

        public ucTkBacSi()
        {
            InitializeComponent();
            // CHỈ đăng ký những sự kiện mà trong file Designer chưa có
            this.cboLoaiThongKe.SelectedIndexChanged += cboLoaiThongKe_SelectedIndexChanged;
        }

        private void ucTkBacSi_Load(object sender, EventArgs e)
        {
            cboLoaiThongKe.Items.Clear();
            cboLoaiThongKe.Items.AddRange(new string[] { "Ngày", "Tháng", "Năm" });
            cboLoaiThongKe.SelectedIndex = 0;

            numTuNam.Minimum = numDenNam.Minimum = 2000;
            numTuNam.Maximum = numDenNam.Maximum = 2100;
            numTuNam.Value = 2025; numDenNam.Value = 2026;

            cboChiNhanh.Items.Clear();
            cboChiNhanh.Items.AddRange(new string[] { "Chi nhánh 1", "Chi nhánh 2", "Chi nhánh 3" });
            if (cboChiNhanh.Items.Count > 0) cboChiNhanh.SelectedIndex = 0;

            chartBacSi.Visible = false;
        }

        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cnChon = cboChiNhanh.Text;
            clbBacSi.Items.Clear();
            var locBS = DataHethong.Where(x => x.CN == cnChon).Select(x => x.Ten).Distinct().ToList();
            foreach (var ten in locBS) { clbBacSi.Items.Add(ten, false); }
        }

        // --- SỬA LỖI ẨN HIỆN TẠI ĐÂY ---
        private void cboLoaiThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loai = cboLoaiThongKe.Text;

            // 1. Điều khiển Visibility
            bool hienDtp = (loai == "Ngày" || loai == "Tháng");
            bool hienNum = (loai == "Năm");

            dtpTuNgay.Visible = dtpDenNgay.Visible = hienDtp;
            numTuNam.Visible = numDenNam.Visible = hienNum;

            // 2. Đổi nhãn và Format
            if (loai == "Ngày")
            {
                lblTu.Text = "Từ ngày:"; lblDen.Text = "Đến ngày:";
                dtpTuNgay.Format = dtpDenNgay.Format = DateTimePickerFormat.Short;
            }
            else if (loai == "Tháng")
            {
                lblTu.Text = "Từ tháng:"; lblDen.Text = "Đến tháng:";
                dtpTuNgay.Format = dtpDenNgay.Format = DateTimePickerFormat.Custom;
                dtpTuNgay.CustomFormat = dtpDenNgay.CustomFormat = "MM/yyyy";
            }
            else
            { // Năm
                lblTu.Text = "Từ năm:"; lblDen.Text = "Đến năm:";
            }
        }

        private void btnLoc_Click(object sender, EventArgs e) { VeBieuDoDoanhThu(); }

        private void VeBieuDoDoanhThu()
        {
            string cnChon = cboChiNhanh.Text;
            string loaiTK = cboLoaiThongKe.Text;
            var dsBacSiChon = clbBacSi.CheckedItems.Cast<string>().ToList();

            if (dsBacSiChon.Count == 0) { MessageBox.Show("Vui lòng chọn bác sĩ!"); return; }

            DateTime tu, den;
            if (loaiTK == "Ngày" || loaiTK == "Tháng")
            {
                tu = dtpTuNgay.Value.Date; den = dtpDenNgay.Value.Date;
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

            // Lọc dữ liệu theo thời gian thực tế
            var dataLoc = DataHethong
                .Where(x => x.CN == cnChon && dsBacSiChon.Contains(x.Ten.ToString()) && x.Ngay >= tu && x.Ngay <= den)
                .GroupBy(x => x.Ten.ToString())
                .Select(g => new { TenBS = g.Key, DoanhThu = g.Sum(x => (double)x.DoanhThu) })
                .ToList();

            if (dataLoc.Count == 0)
            {
                chartBacSi.Visible = false;
                MessageBox.Show("Không có dữ liệu trong khoảng thời gian này!");
                return;
            }

            chartBacSi.Visible = true;
            chartBacSi.Series.Clear();
            chartBacSi.ChartAreas.Clear();
            chartBacSi.Titles.Clear();
            chartBacSi.Legends.Clear();

            Title title = new Title($"SO SÁNH DOANH THU BÁC SĨ - {cnChon.ToUpper()}");
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.Position = new ElementPosition(0, 22, 100, 7);
            chartBacSi.Titles.Add(title);

            ChartArea ca = new ChartArea("MainArea");
            ca.Position = new ElementPosition(3, 30, 92, 55);
            chartBacSi.ChartAreas.Add(ca);

            ca.AxisX.Title = "Bác sĩ được chọn";
            ca.AxisY.Title = "Doanh thu (VNĐ)";
            ca.AxisX.TitleFont = ca.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            ca.AxisX.Interval = 1;
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.LabelStyle.Format = "{0:N0}";

            Series s = new Series("Revenue") { ChartType = SeriesChartType.Column, IsValueShownAsLabel = true, LabelFormat = "{0:N0}" };
            Color[] mauSac = { Color.DodgerBlue, Color.Orange, Color.Red, Color.Green, Color.Purple, Color.Brown };
            int i = 0;

            foreach (string tenBS in dsBacSiChon)
            {
                var d = dataLoc.FirstOrDefault(x => x.TenBS == tenBS);
                double tien = (d != null) ? d.DoanhThu : 0;

                int pIdx = s.Points.AddXY(tenBS, tien);
                s.Points[pIdx].Color = mauSac[i % mauSac.Length];
                s.Points[pIdx].AxisLabel = tenBS;
                i++;
            }
            chartBacSi.Series.Add(s);
            chartBacSi.Invalidate();
        }

        private void clbBacSi_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}