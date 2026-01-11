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
    public partial class ucThongTinKhachHang : UserControl
    {
        public ucThongTinKhachHang()
        {
            InitializeComponent();
            XoaTrangThongTin();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 f1 = (Form1)this.FindForm();
            f1.hienThiTrang(new ucThongTin()); // Trở về menu 2 ô
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maTim = txtMaKH.Text.Trim();
            //if (string.IsNullOrEmpty(maTim))
            //{
            //    MessageBox.Show("Vui lòng nhập mã khách hàng!");
            //    return;
            //}

            //try
            //{
            //    if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            //    if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

            //    string query = "SELECT * FROM KHACHHANG WHERE MaKH = @MaKH";
            //    SqlCommand cmd = new SqlCommand(query, sqlCon);
            //    cmd.Parameters.AddWithValue("@MaKH", maTim);

            //    SqlDataReader reader = cmd.ExecuteReader();

            //    if (reader.Read())
            //    {
            //        // Đổ dữ liệu vào các Label/Lable bên phải của bạn
            //        lblTenKH.Text = "Tên Khách hàng: " + reader["TenKH"].ToString();
            //        lblGioiTinh.Text = "Giới Tính: " + reader["GioiTinh"].ToString();
            //        lblSDT.Text = "Số Điện Thoại: " + reader["SDT"].ToString();
            //        lblNgaySinh.Text = "Ngày Sinh: " + Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy");
            //        lblEmail.Text = "Email: " + reader["Email"].ToString();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Không tìm thấy khách hàng này!");
            //        XoaTrangThongTin();
            //    }
            //    reader.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi: " + ex.Message);
            //}
            if (maTim == "KH001")
            {
                lblTenKH.Text = "Tên Khách hàng: Nguyễn Văn A";
                lblGioiTinh.Text = "Giới Tính: Nam";
                lblSDT.Text = "Số Điện Thoại: 0123456789";
                lblNgaySinh.Text = "Ngày Sinh: 15/08/1985";
                lblEmail.Text = "Email: abc@gmail.com";
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng này!");
                XoaTrangThongTin();
            }
        }

        private void XoaTrangThongTin()
        {
            lblTenKH.Text = "Tên Khách hàng:--";
            lblGioiTinh.Text = "Giới Tính:--";
            lblSDT.Text = "Số Điện Thoại:--";
            lblNgaySinh.Text = "Ngày Sinh:--";
            lblEmail.Text = "Email:--";

        }
    }
}
