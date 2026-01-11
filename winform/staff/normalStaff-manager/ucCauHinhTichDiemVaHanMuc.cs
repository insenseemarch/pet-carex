using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PetCareX
{
    public partial class ucCauHinhTichDiemVaHanMuc : UserControl
    {
        // Các biến lưu trữ giá trị cấu hình trong bộ nhớ
        private long soTienDoiDiem;
        private long mocThanThiet;
        private long mocVip;

        public ucCauHinhTichDiemVaHanMuc()
        {
            InitializeComponent();
            SetupEvents();
            LoadDefaultValues();
        }

        private void SetupEvents()
        {
            // Gán sự kiện chặn nhập chữ cho cả 3 TextBox 🚫
            txtSoTienDoiDiem.KeyPress += OnlyNumber_KeyPress;
            txtMocThanThiet.KeyPress += OnlyNumber_KeyPress;
            txtMocVip.KeyPress += OnlyNumber_KeyPress;

            // Gán sự kiện cho nút Lưu
            btnLuu.Click += btnLuu_Click;
        }

        private void LoadDefaultValues()
        {
            // Thiết lập các giá trị mặc định ban đầu ⚙️
            txtSoTienDoiDiem.Text = "50000";
            txtMocThanThiet.Text = "5000000";
            txtMocVip.Text = "12000000";
        }

        // Hàm hỗ trợ: Chỉ cho phép nhập số và phím xóa Backspace 🔢
        private void OnlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Chặn ký tự không hợp lệ
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra không được bỏ trống dữ liệu 📝
            if (string.IsNullOrWhiteSpace(txtSoTienDoiDiem.Text) ||
                string.IsNullOrWhiteSpace(txtMocThanThiet.Text) ||
                string.IsNullOrWhiteSpace(txtMocVip.Text))
            {
                MessageBox.Show("Vui lòng không để trống các giá trị cấu hình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Chuyển đổi dữ liệu từ chuỗi sang số 🔄
            soTienDoiDiem = long.Parse(txtSoTienDoiDiem.Text);
            mocThanThiet = long.Parse(txtMocThanThiet.Text);
            mocVip = long.Parse(txtMocVip.Text);

            // 3. Kiểm tra các ràng buộc nghiệp vụ (RB4.1, RB4.2) 🛡️
            if (soTienDoiDiem <= 0)
            {
                MessageBox.Show("Số tiền đổi điểm phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (mocVip < mocThanThiet)
            {
                MessageBox.Show("Mốc VIP phải lớn hơn hoặc bằng mốc Thân thiết!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Thông báo thành công (Ở đây bạn có thể thêm code lưu vào Database) 💾
            MessageBox.Show("Cập nhật quy tắc tích điểm và hạng mức thành viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}