using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BACSI
{
    public partial class ucBenhAn : UserControl
    {
        // Chuỗi kết nối SQL Server
        string strCon = @"Data Source=TEN_MAY;Initial Catalog=PetCareDB;Integrated Security=True";

        public ucBenhAn()
        {
            InitializeComponent();
            this.Size = new Size(1400, 600);
            SetupControls();
        }

        private void SetupControls()
        {
            // Thiết lập font chữ chung cho các ô nhập liệu 12pt để dễ đọc
            foreach (Control c in this.Controls)
            {
                if (c is TextBox || c is RichTextBox)
                    c.Font = new Font("Segoe UI", 12);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường bắt buộc theo sơ đồ dữ liệu
            if (string.IsNullOrWhiteSpace(txtMathucung.Text) || string.IsNullOrWhiteSpace(txtMaBS.Text))
            {
                MessageBox.Show("Mã thú cưng và Mã bác sĩ không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    // Query Insert đúng theo các cột trong hình ảnh CHITIETKHAMBENH của bạn
                    string sql = @"INSERT INTO CHITIETKHAMBENH 
                        (MaDV, Mathucung, Ngaysudung, MaBS, Trieuchung, MaToa, Chandoan, NgayTaiKham, Ghichu) 
                        VALUES (@MaDV, @Mathucung, @Ngaysudung, @MaBS, @Trieuchung, @MaToa, @Chandoan, @NgayTaiKham, @Ghichu)";

                    SqlCommand cmd = new SqlCommand(sql, con);

                    // Gán giá trị từ giao diện
                    cmd.Parameters.AddWithValue("@MaDV", txtMaDV.Text.Trim());
                    cmd.Parameters.AddWithValue("@Mathucung", txtMathucung.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ngaysudung", DateTime.Now); // Ngày lập hồ sơ
                    cmd.Parameters.AddWithValue("@MaBS", txtMaBS.Text.Trim());
                    cmd.Parameters.AddWithValue("@Trieuchung", rtbTrieuchung.Text);
                    //cmd.Parameters.AddWithValue("@MaToa", string.IsNullOrEmpty(txtMaToa.Text) ? (object)DBNull.Value : txtMaToa.Text.Trim());
                    cmd.Parameters.AddWithValue("@Chandoan", rtbChandoan.Text);
                    cmd.Parameters.AddWithValue("@NgayTaiKham", dtpNgayTaiKham.Value);
                    cmd.Parameters.AddWithValue("@Ghichu", rtbGhichu.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Lưu bệnh án thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox) c.Text = "";
                if (c is RichTextBox) c.Text = "";
            }
            dtpNgayTaiKham.Value = DateTime.Now;
        }
    }
}
