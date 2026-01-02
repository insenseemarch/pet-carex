using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PetCareX
{
    public partial class ucConBaoCaoKhachHang : UserControl
    {
        // Placeholder text cho ô nội dung
        private const string PLACEHOLDER = "Viết tin nhắn...";

        // Giả lập danh sách khách hàng (Trong thực tế bạn sẽ lấy từ Database)
        private List<KhachHangModel> dsKhachHang;

        public ucConBaoCaoKhachHang()
        {
            InitializeComponent();
            SetupWatermark();
            KhoiTaoDuLieuGia();

            // Gán các sự kiện
            btnXemBaoCao.Click += btnXemBaoCao_Click;
            dgvBaoCao.CellClick += dgvBaoCao_CellClick;
            btnGuiEmail.Click += (s, e) => ThucHienGui("Email");
            btnGuiSMS.Click += (s, e) => ThucHienGui("SMS");
        }

        #region 1. Xử lý Watermark (Chữ in nghiêng mờ mờ) ✍️
        private void SetupWatermark()
        {
            rtxtNoiDung.Text = PLACEHOLDER;
            rtxtNoiDung.ForeColor = Color.Gray;
            rtxtNoiDung.Font = new Font(rtxtNoiDung.Font, FontStyle.Italic);

            rtxtNoiDung.Enter += RtxtNoiDung_Enter;
            rtxtNoiDung.Leave += RtxtNoiDung_Leave;
        }

        private void RtxtNoiDung_Enter(object sender, EventArgs e)
        {
            if (rtxtNoiDung.Text == PLACEHOLDER)
            {
                rtxtNoiDung.Text = "";
                rtxtNoiDung.ForeColor = Color.Black;
                rtxtNoiDung.Font = new Font(rtxtNoiDung.Font, FontStyle.Regular);
            }
        }

        private void RtxtNoiDung_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtxtNoiDung.Text))
            {
                rtxtNoiDung.Text = PLACEHOLDER;
                rtxtNoiDung.ForeColor = Color.Gray;
                rtxtNoiDung.Font = new Font(rtxtNoiDung.Font, FontStyle.Italic);
            }
        }
        #endregion

        #region 2. Xử lý Lọc Dữ liệu (Luồng Chính + Luồng Thay Thế) 🔍
        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (cboLoaiKH.SelectedItem == null) return;

            string loai = cboLoaiKH.SelectedItem.ToString();
            DateTime homNay = DateTime.Now;
            List<KhachHangModel> ketQua = new List<KhachHangModel>();

            if (loai == "Mới")
            {
                // Khách mới: Giao dịch đầu tiên trong vòng 14 ngày qua 📅
                ketQua = dsKhachHang.Where(kh => (homNay - kh.NgayDau).TotalDays <= 14).ToList();
                if (!ketQua.Any()) MessageBox.Show("Cửa hàng hiện chưa tiếp nhận khách hàng mới.");
            }
            else // Khách cũ
            {
                // Khách cũ: Không có giao dịch nào trong 6 tháng qua 🕰️
                ketQua = dsKhachHang.Where(kh => (homNay - kh.NgayCuoi).TotalDays >= 180).ToList();
                if (!ketQua.Any()) MessageBox.Show("Cửa hàng hiện chưa tồn tại khách hàng cũ.");
            }

            dgvBaoCao.DataSource = ketQua;
            DinhDangBang();
        }
        #endregion

        #region 3. Đổ dữ liệu & Gửi liên lạc ✉️
        private void dgvBaoCao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvBaoCao.Rows[e.RowIndex];
            txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
            txtSDT.Text = row.Cells["SDT"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
        }

        private void ThucHienGui(string phuongThuc)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng từ danh sách!");
                return;
            }

            if (rtxtNoiDung.Text == PLACEHOLDER || string.IsNullOrWhiteSpace(rtxtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung liên lạc!");
                return;
            }

            MessageBox.Show($"Đã gửi {phuongThuc} thành công đến khách hàng {txtHoTen.Text}!", "Thành công");
        }
        #endregion

        // --- Hỗ trợ định dạng & Dữ liệu mẫu ---
        private void DinhDangBang()
        {
            if (dgvBaoCao.Columns["HoTen"] != null) dgvBaoCao.Columns["HoTen"].HeaderText = "Họ Và Tên";
            if (dgvBaoCao.Columns["NgayCuoi"] != null) dgvBaoCao.Columns["NgayCuoi"].HeaderText = "Giao dịch cuối";
            dgvBaoCao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void KhoiTaoDuLieuGia()
        {
            dsKhachHang = new List<KhachHangModel>
            {
                new KhachHangModel("Nguyễn Văn A", "0901234567", "ana@gmail.com", DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-2)), // Mới
                new KhachHangModel("Trần Thị B", "0911223344", "bt@gmail.com", DateTime.Now.AddMonths(-8), DateTime.Now.AddMonths(-7)) // Cũ
            };
        }
    }

    public class KhachHangModel
    {
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public DateTime NgayDau { get; set; }
        public DateTime NgayCuoi { get; set; }

        public KhachHangModel(string ten, string sdt, string email, DateTime dau, DateTime cuoi)
        {
            HoTen = ten; SDT = sdt; Email = email; NgayDau = dau; NgayCuoi = cuoi;
        }
    }
}