using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace PetCareX
{
    public partial class ucPhanTichDVSP : UserControl
    {
        // 1. Khai báo danh sách dữ liệu giả lập 📂
        private List<DV_PhanTich> dsDichVu;
        private List<HD_PhanTich> dsHoaDon;
        private List<TC_PhanTich> dsThuCung;

        public ucPhanTichDVSP()
        {
            InitializeComponent();
            KhoiTaoDuLieuMau();

            // Thiết lập mặc định cho các Control
            cboLoaiBaoCao.SelectedIndex = 0;
            numTopN.Value = 5;

            // Gán sự kiện Click cho nút Xem
            btnXem.Click += btnXem_Click;
        }

        private void KhoiTaoDuLieuMau()
        {
            // Danh sách 11 dịch vụ để test Top 10 🛠️
            dsDichVu = new List<DV_PhanTich> {
                new DV_PhanTich("DV01", "Khám tổng quát", 200000),
                new DV_PhanTich("DV02", "Tiêm Vaccine 5 bệnh", 350000),
                new DV_PhanTich("DV03", "Spa - Tắm sấy", 150000),
                new DV_PhanTich("DV04", "Cắt tỉa lông nghệ thuật", 400000),
                new DV_PhanTich("DV05", "Trị ve rận", 100000),
                new DV_PhanTich("DV06", "Lấy cao răng", 300000),
                new DV_PhanTich("DV07", "Triệt sản mèo", 800000),
                new DV_PhanTich("DV08", "Xét nghiệm máu", 500000),
                new DV_PhanTich("DV09", "Lưu chuồng VIP", 250000),
                new DV_PhanTich("DV10", "Tẩy giun", 50000),
                new DV_PhanTich("DV11", "Siêu âm", 300000)
            };

            // Danh sách thú cưng mẫu 🐾
            dsThuCung = new List<TC_PhanTich> {
                new TC_PhanTich("TC01", "Chó", "Poodle"),
                new TC_PhanTich("TC02", "Mèo", "Anh lông ngắn"),
                new TC_PhanTich("TC03", "Chó", "Corgi"),
                new TC_PhanTich("TC04", "Mèo", "Mướp"),
                new TC_PhanTich("TC05", "Chó", "Pug")
            };

            // Giả lập hóa đơn trong 6 tháng qua (RB27.2) 💰
            dsHoaDon = new List<HD_PhanTich>();
            Random rand = new Random();

            // Tạo ngẫu nhiên 50 hóa đơn để có số liệu thống kê
            for (int i = 0; i < 50; i++)
            {
                string maDV = "DV" + rand.Next(1, 12).ToString("D2");
                string maTC = "TC" + rand.Next(1, 6).ToString("D2");
                double gia = dsDichVu.First(d => d.MaDV == maDV).Gia;
                DateTime ngay = DateTime.Now.AddMonths(-rand.Next(0, 6)).AddDays(-rand.Next(0, 28));

                dsHoaDon.Add(new HD_PhanTich(maDV, maTC, gia, ngay, "Đã thanh toán"));
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            int n = (int)numTopN.Value; // Lấy giá trị N (RB27.5)
            DateTime ngayBatDau = DateTime.Now.AddMonths(-6); // Mốc 6 tháng (RB27.2)

            if (cboLoaiBaoCao.SelectedIndex == 0) // Top dịch vụ doanh thu cao nhất 📈
            {
                var topDV = dsHoaDon
                    .Where(hd => hd.NgayLap >= ngayBatDau && hd.TrangThai == "Đã thanh toán")
                    .GroupBy(hd => hd.MaDV)
                    .Select(group => new {
                        MaDV = group.Key,
                        TenDV = dsDichVu.FirstOrDefault(d => d.MaDV == group.Key)?.TenDV,
                        DoanhThu = group.Sum(hd => hd.TongTien)
                    })
                    .OrderByDescending(x => x.DoanhThu)
                    .Take(n).ToList();

                dgvKetQua.DataSource = topDV;
                FormatBieuDoDichVu();
            }
            else // Top loài/giống sử dụng dịch vụ nhiều nhất 🐕
            {
                var topTC = dsHoaDon
                    .Where(hd => hd.NgayLap >= ngayBatDau)
                    .Join(dsThuCung, hd => hd.MaThucung, tc => tc.MaThucung, (hd, tc) => tc)
                    .GroupBy(tc => new { tc.Loai, tc.Giong })
                    .Select(group => new {
                        Loai = group.Key.Loai,
                        Giong = group.Key.Giong,
                        SoLuotSuDung = group.Count()
                    })
                    .OrderByDescending(x => x.SoLuotSuDung)
                    .Take(n).ToList();

                dgvKetQua.DataSource = topTC;
                FormatBieuDoThuCung();
            }
        }

        // --- CÁC HÀM ĐỊNH DẠNG DATA GRID VIEW ---

        private void FormatBieuDoDichVu()
        {
            dgvKetQua.Columns["MaDV"].HeaderText = "Mã Dịch Vụ";
            dgvKetQua.Columns["TenDV"].HeaderText = "Tên Dịch Vụ";
            dgvKetQua.Columns["DoanhThu"].HeaderText = "Tổng Doanh Thu (VNĐ)";
            dgvKetQua.Columns["DoanhThu"].DefaultCellStyle.Format = "N0"; // Định dạng tiền tệ
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FormatBieuDoThuCung()
        {
            dgvKetQua.Columns["Loai"].HeaderText = "Loài";
            dgvKetQua.Columns["Giong"].HeaderText = "Giống";
            dgvKetQua.Columns["SoLuotSuDung"].HeaderText = "Số Lượt Sử Dụng (Tần Suất)";
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }

    // --- CÁC LỚP DỮ LIỆU NỘI BỘ ĐỂ TRÁNH TRÙNG TÊN (AMBIGUITY) 🛡️ ---

    public class DV_PhanTich
    {
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public double Gia { get; set; }
        public DV_PhanTich(string m, string t, double g) { MaDV = m; TenDV = t; Gia = g; }
    }

    public class HD_PhanTich
    {
        public string MaDV { get; set; }
        public string MaThucung { get; set; }
        public double TongTien { get; set; }
        public DateTime NgayLap { get; set; }
        public string TrangThai { get; set; }
        public HD_PhanTich(string mdv, string mtc, double tt, DateTime nl, string st)
        {
            MaDV = mdv; MaThucung = mtc; TongTien = tt; NgayLap = nl; TrangThai = st;
        }
    }

    public class TC_PhanTich
    {
        public string MaThucung { get; set; }
        public string Loai { get; set; }
        public string Giong { get; set; }
        public TC_PhanTich(string m, string l, string g) { MaThucung = m; Loai = l; Giong = g; }
    }
}