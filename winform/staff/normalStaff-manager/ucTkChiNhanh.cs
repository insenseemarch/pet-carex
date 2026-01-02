using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PetCareX
{
    public partial class ucTkChiNhanh : UserControl
    {
        private List<dynamic> DataChiNhanh = new List<dynamic>()
        {
            new { CN = "Chi Nhánh 1", Tien = 125000000, Ngay = new DateTime(2025, 12, 10) },
            new { CN = "Chi Nhánh 2", Tien = 98000000,  Ngay = new DateTime(2025, 12, 15) },
            new { CN = "Chi Nhánh 3", Tien = 150000000, Ngay = new DateTime(2025, 12, 20) },
            new { CN = "Chi Nhánh 1", Tien = 45000000,  Ngay = new DateTime(2026, 01, 05) },
            new { CN = "Chi Nhánh 2", Tien = 110000000, Ngay = new DateTime(2026, 01, 10) }
        };

        public ucTkChiNhanh()
        {
            InitializeComponent();
            this.Load += ucTkChiNhanh_Load;
            this.btnThongKe.Click += btnThongKe_Click;
            this.cboLoaiThongKe.SelectedIndexChanged += cboLoaiThongKe_SelectedIndexChanged;
        }

        private void ucTkChiNhanh_Load(object sender, EventArgs e)
        {
            cboLoaiThongKe.Items.Clear();
            cboLoaiThongKe.Items.AddRange(new string[] { "Ngày", "Tháng", "Năm" });
            cboLoaiThongKe.SelectedIndex = 0;

            numTuNam.Minimum = numDenNam.Minimum = 2000;
            numTuNam.Maximum = numDenNam.Maximum = 2100;
            numTuNam.Value = 2025; numDenNam.Value = 2026;

            dtpTuNgay.Value = new DateTime(2025, 12, 1);
            dtpDenNgay.Value = DateTime.Now;

            chartDoanhThu.Visible = false;
        }

        private void cboLoaiThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loai = cboLoaiThongKe.Text;
            bool isDtp = (loai == "Ngày" || loai == "Tháng");

            dtpTuNgay.Visible = dtpDenNgay.Visible = isDtp;
            numTuNam.Visible = numDenNam.Visible = !isDtp;

            if (loai == "Ngày")
            {
                dtpTuNgay.Format = dtpDenNgay.Format = DateTimePickerFormat.Short;
                lblTu.Text = "Từ ngày:"; lblDen.Text = "Đến ngày:";
            }
            else if (loai == "Tháng")
            {
                dtpTuNgay.Format = dtpDenNgay.Format = DateTimePickerFormat.Custom;
                dtpTuNgay.CustomFormat = dtpDenNgay.CustomFormat = "MM/yyyy";
                lblTu.Text = "Từ tháng:"; lblDen.Text = "Đến tháng:";
            }
            else
            {
                lblTu.Text = "Từ năm:"; lblDen.Text = "Đến năm:";
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e) { VeBieuDoChiNhanh(); }

        private void VeBieuDoChiNhanh()
        {
            string loaiTK = cboLoaiThongKe.Text;
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

            // --- KIỂM TRA TỔNG DOANH THU TOÀN HỆ THỐNG ---
            bool coBatKyDuLieuNao = DataChiNhanh.Any(x => x.Ngay >= tu && x.Ngay <= den);

            if (!coBatKyDuLieuNao)
            {
                chartDoanhThu.Visible = false;
                MessageBox.Show("Không có dữ liệu doanh thu cho tất cả chi nhánh trong khoảng này!");
                return;
            }

            var tatCaCN = DataChiNhanh.Select(x => x.CN.ToString()).Distinct().ToList();

            chartDoanhThu.Visible = true;
            chartDoanhThu.Series.Clear();
            chartDoanhThu.ChartAreas.Clear();
            chartDoanhThu.Titles.Clear();
            chartDoanhThu.Legends.Clear();

            Title title = new Title("SO SÁNH DOANH THU GIỮA CÁC CHI NHÁNH");
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.Position = new ElementPosition(0, 22, 100, 7);
            chartDoanhThu.Titles.Add(title);

            ChartArea ca = new ChartArea("MainArea");
            ca.Position = new ElementPosition(12, 30, 85, 60);
            chartDoanhThu.ChartAreas.Add(ca);

            ca.AxisX.Interval = 1;
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.LabelStyle.Format = "{0:N0}";
            ca.AxisY.Title = "Doanh thu (VNĐ)";

            Series s = new Series("Revenue")
            {
                ChartType = SeriesChartType.Bar,
                IsValueShownAsLabel = true,
                LabelFormat = "{0:N0}",
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Color[] colors = { Color.DodgerBlue, Color.Orange, Color.ForestGreen, Color.Crimson, Color.MediumPurple };
            int i = 0;

            foreach (string cnName in tatCaCN)
            {
                var locData = DataChiNhanh.Where(x => x.CN == cnName && x.Ngay >= tu && x.Ngay <= den);
                double tongTien = locData.Any() ? locData.Sum(x => (double)x.Tien) : 0;

                int pIdx = s.Points.AddXY(cnName, tongTien);
                s.Points[pIdx].Color = colors[i % colors.Length];
                s.Points[pIdx].AxisLabel = cnName;
                i++;
            }

            chartDoanhThu.Series.Add(s);
            chartDoanhThu.Invalidate();
        }
    }
}