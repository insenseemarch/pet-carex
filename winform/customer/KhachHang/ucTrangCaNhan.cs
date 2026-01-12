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

namespace KhachHang
{
    public partial class ucTrangCaNhan : UserControl
    {
        public ucTrangCaNhan()
        {
            InitializeComponent();
        }

        private void ucTrangCaNhan_Load(object sender, EventArgs e)
        {
            //string connectionString = "Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;";
            //string maKH;

            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();

            //    // 1. Đổ dữ liệu vào các ô thông tin khách hàng
            //    string sqlKH = "SELECT * FROM KHACHHANG WHERE MaKH = @MaKH";
            //    SqlCommand cmdKH = new SqlCommand(sqlKH, conn);
            //    cmdKH.Parameters.AddWithValue("@MaKH", maKH);
            //    SqlDataReader reader = cmdKH.ExecuteReader();

            //    if (reader.Read())
            //    {
            //        txtHoTen.Text = reader["HoTen"].ToString();
            //        txtEmail.Text = reader["Email"].ToString();
            //        txtGioiTinh.Text = reader["GioiTinh"].ToString();
            //        txtSDT.Text = reader["SDT"].ToString();
            //        txtCCCD.Text = reader["CCCD"].ToString();
            //        dtpNgaySinh.Value = Convert.ToDateTime(reader["NgaySinh"]);
            //    }
            //    reader.Close();

            //    // 2. Đổ dữ liệu vào DataGridView (Thú cưng đang sở hữu)
            //    string sqlPet = "SELECT Mathucung, Tenthucung, Loai, GioiTinh, TinhTrangSucKhoe FROM THUCUNG WHERE MaKH = @MaKH";
            //    SqlDataAdapter da = new SqlDataAdapter(sqlPet, conn);
            //    da.SelectCommand.Parameters.AddWithValue("@MaKH", maKH);

            //    DataTable dtPet = new DataTable();
            //    da.Fill(dtPet);

            //    dgvThuCung.DataSource = dtPet; // dgvThuCung là cái bảng màu xám trong ảnh của bạn

            //    // Tùy chỉnh tiêu đề cột cho đẹp
            //    dgvThuCung.Columns[0].HeaderText = "Mã Thú Cưng";
            //    dgvThuCung.Columns[1].HeaderText = "Tên Thú Cưng";
            //    dgvThuCung.Columns[2].HeaderText = "Loài";
            //    dgvThuCung.Columns[3].HeaderText = "Giới Tính";
            //    dgvThuCung.Columns[4].HeaderText = "Tình Trạng Sức Khỏe";
            //}
        }
    }
}
