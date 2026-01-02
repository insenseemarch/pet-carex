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
    public partial class ucKeToa : UserControl
    {
        string strCon = @"Data Source=YOUR_SERVER;Initial Catalog=PetCareDB;Integrated Security=True";

        public ucKeToa()
        {
            InitializeComponent();
        }

        // 1. Hàm sinh mã toa tự động (VD: TOA-20231025-001)
        private string GenerateMaToa()
        {
            return "TOA" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void btnLuuToanBo_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(txtTenToa.Text) || string.IsNullOrWhiteSpace(txtMathucung.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên toa thuốc và Mã thú cưng!");
                return;
            }

            string maToaMoi = GenerateMaToa();

            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                // Sử dụng Transaction để đảm bảo: Lỗi một bước là hủy toàn bộ
                SqlTransaction trans = con.BeginTransaction();

                try
                {
                    // BƯỚC 1: Insert vào bảng TOATHUOC
                    string query1 = "INSERT INTO TOATHUOC (MaToa, TenToaThuoc) VALUES (@ma, @ten)";
                    SqlCommand cmd1 = new SqlCommand(query1, con, trans);
                    cmd1.Parameters.AddWithValue("@ma", maToaMoi);
                    cmd1.Parameters.AddWithValue("@ten", txtTenToa.Text.Trim());
                    cmd1.ExecuteNonQuery();

                    // BƯỚC 2: Chạy vòng lặp lưu danh sách thuốc từ DataGridView vào THUOCSUDUNG
                    foreach (DataGridViewRow row in dgvKeToa.Rows)
                    {
                        // Kiểm tra nếu dòng đó không phải dòng trống (có mã sản phẩm)
                        if (row.Cells["MaSP"].Value != null)
                        {
                            string query2 = @"INSERT INTO THUOCSUDUNG (Matoathuoc, MaSP, Soluong, Lieuluong, [Ghi chú]) 
                                             VALUES (@maToa, @maSP, @sl, @ll, @gc)";
                            SqlCommand cmd2 = new SqlCommand(query2, con, trans);
                            cmd2.Parameters.AddWithValue("@maToa", maToaMoi);
                            cmd2.Parameters.AddWithValue("@maSP", row.Cells["MaSP"].Value.ToString());
                            cmd2.Parameters.AddWithValue("@sl", row.Cells["Soluong"].Value ?? 0);
                            cmd2.Parameters.AddWithValue("@ll", row.Cells["Lieuluong"].Value ?? "");
                            cmd2.Parameters.AddWithValue("@gc", row.Cells["Ghichu"].Value ?? "");
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    // BƯỚC 3: Cập nhật mã toa vào bảng CHITIETKHAMBENH dựa trên MaDV và MaPet
                    // Đây là bước liên kết Toa thuốc với hồ sơ bệnh án bạn đã tạo trước đó
                    string query3 = @"UPDATE CHITIETKHAMBENH 
                                     SET MaToa = @maToa 
                                     WHERE MaDV = @maDV AND Mathucung = @maPet";
                    SqlCommand cmd3 = new SqlCommand(query3, con, trans);
                    cmd3.Parameters.AddWithValue("@maToa", maToaMoi);
                    cmd3.Parameters.AddWithValue("@maDV", txtMaDV.Text.Trim());
                    cmd3.Parameters.AddWithValue("@maPet", txtMathucung.Text.Trim());

                    int result = cmd3.ExecuteNonQuery();

                    if (result > 0)
                    {
                        trans.Commit(); // Xác nhận lưu vĩnh viễn vào DB
                        MessageBox.Show("Đã tạo toa thuốc và liên kết với bệnh án thành công!", "Thông báo");
                    }
                    else
                    {
                        trans.Rollback(); // Hủy bỏ nếu không tìm thấy bệnh án để liên kết
                        MessageBox.Show("Lỗi: Không tìm thấy hồ sơ bệnh án nào khớp với Mã Pet và Mã DV này để cập nhật!");
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback(); // Hủy bỏ nếu có bất kỳ lỗi code/SQL nào
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            }
        }
    }
}
