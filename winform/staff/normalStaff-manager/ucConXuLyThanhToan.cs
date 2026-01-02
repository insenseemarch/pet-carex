using System.Windows.Forms;
using System;

namespace PetCareX
{
    public partial class ucConXuLyThanhToan : UserControl
    {
        private HoaDonThanhToan _hd;

        public ucConXuLyThanhToan(HoaDonThanhToan data)
        {
            InitializeComponent();
            this._hd = data;

            // Đăng ký các sự kiện
            this.Load += ucConXuLyThanhToan_Load;
            this.cboPhuongThuc.SelectedIndexChanged += cboPhuongThuc_SelectedIndexChanged;
            this.btnThanhToan.Click += btnThanhToan_Click;
            this.btnHuyThanhToan.Click += btnHuyThanhToan_Click;
        }

        private void ucConXuLyThanhToan_Load(object sender, EventArgs e)
        {
            if (_hd != null)
            {
                // 1. Hiển thị thông tin cơ bản với tiêu đề đầy đủ
                lblMaKH_HienThi.Text = $"Mã KH: {_hd.MaKH}";
                lblNgayLap_HienThi.Text = $"Ngày lập: {_hd.NgayLap:dd/MM/yyyy HH:mm}";
                lblTenNV_HienThi.Text = $"NV lập: {_hd.TenNV}";

                // 2. Cột thông tin hạng và điểm
                lblHangThanhVien.Text = $"Hạng thành viên: {_hd.HangKH}";
                lblDiemHienTai.Text = $"Điểm tích lũy: {_hd.DiemTichLuy}";
                lblUuDai.Text = "Ưu đãi theo hạng: (Giảm giá trực tiếp)";

                // 3. Xử lý logic tiền tệ để hiển thị số tiền giảm cụ thể
                try
                {
                    // Làm sạch chuỗi tiền từ trang trước (bỏ VNĐ, dấu phẩy, chữ "Tổng tạm tính",...)
                    double tongGoc = ParseMoney(_hd.TongTien);
                    double thanhTien = ParseMoney(_hd.ThanhTien);
                    double giamGia = tongGoc - thanhTien;

                    lblTongTienGoc.Text = $"Tổng tạm tính: {tongGoc:N0} VNĐ";
                    lblSoTienGiam.Text = $"Số tiền được giảm: {giamGia:N0} VNĐ";
                    lblTongThanhToan.Text = $"THÀNH TIỀN: {thanhTien:N0} VNĐ";
                }
                catch
                {
                    // Trường hợp lỗi Parse thì hiện nguyên văn dữ liệu cũ
                    lblTongTienGoc.Text = _hd.TongTien;
                    lblSoTienGiam.Text = "Số tiền được giảm: (Đã khấu trừ)";
                    lblTongThanhToan.Text = _hd.ThanhTien;
                }

                // 4. Hiển thị Panel phương thức thanh toán
                pnlHienThiChiTiet.Visible = true;
                pnlHienThiChiTiet.BringToFront();

                // 5. Cấu hình mặc định
                cboPhuongThuc.SelectedIndex = 0; // Mặc định: Tiền mặt
                picQR.Visible = false;           // Ẩn QR
            }
        }

        // Hàm phụ bổ trợ để ép kiểu tiền tệ chính xác
        private double ParseMoney(string moneyString)
        {
            if (string.IsNullOrEmpty(moneyString)) return 0;
            // Loại bỏ tất cả ký tự không phải số (giữ lại dấu chấm nếu có số thập phân)
            string cleanString = System.Text.RegularExpressions.Regex.Replace(moneyString, @"[^0-9]", "");
            return double.TryParse(cleanString, out double result) ? result : 0;
        }

        // Xử lý khi thay đổi phương thức thanh toán
        private void cboPhuongThuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nếu chọn 'Chuyển khoản' (Index 1) thì hiện ảnh QR
            if (cboPhuongThuc.SelectedIndex == 1)
            {
                picQR.Visible = true;
            }
            else
            {
                picQR.Visible = false;
            }
        }

        // Xử lý nút Thanh Toán
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xác nhận thanh toán thành công!", "PetCareX", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Xóa User Control này để quay về Menu chính hoặc trang Lập hóa đơn
            if (this.Parent != null)
            {
                this.Parent.Controls.Remove(this);
            }
        }

        // Xử lý nút Hủy
        private void btnHuyThanhToan_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Xác nhận hủy hóa đơn và quay lại?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                if (this.Parent != null)
                {
                    // Duyệt tìm lại trang Lập hóa đơn trong Panel cha
                    foreach (Control ctr in this.Parent.Controls)
                    {
                        if (ctr is ucConLapHoaDon)
                        {
                            ucConLapHoaDon trangLap = (ucConLapHoaDon)ctr;

                            // Gọi hàm public mình vừa sửa ở Bước 1 để hồi lại kho
                            trangLap.NapDuLieuHeThong();
                        }
                    }
                    // Xóa trang thanh toán hiện tại
                    this.Parent.Controls.Remove(this);
                }
            }
        }
    }
}