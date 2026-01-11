using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BACSI
{
    public partial class ucLichSuKham : UserControl
    {
        public ucLichSuKham()
        {
            InitializeComponent();
        }

        private void btnTimKiemLS_Click(object sender, EventArgs e)
        {
            string maPet = txtMaPetLS.Text.Trim();
            if (string.IsNullOrEmpty(maPet))
            {
                MessageBox.Show("Vui lòng nhập mã thú cưng!");
                return;
            }

            LoadDataFromSQL(maPet);
        }

        private void LoadDataFromSQL(string maPet)
        {
            // 1. Xóa dữ liệu cũ trên bảng trước khi tìm kiếm mới
            dgvKhamBenh.DataSource = null;
            dgvTiemPhong.DataSource = null;

            /* --- CODE KẾT NỐI SQL THỰC TẾ (Bỏ comment khi bạn đã có Database) ---
            try 
            {
                using (SqlConnection con = new SqlConnection(strCon)) 
                {
                    con.Open();

                    // Lấy lịch sử khám bệnh với đầy đủ các cột như hình ảnh yêu cầu
                    string sqlKham = "SELECT NgayKham AS [Ngày Khám], ChuanDoan AS [Chẩn Đoán], DieuTri AS [Điều Trị], BacSi AS [Bác Sĩ] " +
                                     "FROM LichSuBenh WHERE MaPet = @ma ORDER BY NgayKham DESC";
                    SqlDataAdapter daKham = new SqlDataAdapter(sqlKham, con);
                    daKham.SelectCommand.Parameters.AddWithValue("@ma", maPet);
                    DataTable dtKham = new DataTable();
                    daKham.Fill(dtKham);

                    // Lấy lịch sử tiêm phòng
                    string sqlTiem = "SELECT NgayTiem AS [Ngày Tiêm], TenVaccine AS [Tên Vaccine], LoaiVaccine AS [Loại], NgayNhacLai AS [Hẹn Nhắc Lại] " +
                                     "FROM LichSuTiem WHERE MaPet = @ma ORDER BY NgayTiem DESC";
                    SqlDataAdapter daTiem = new SqlDataAdapter(sqlTiem, con);
                    daTiem.SelectCommand.Parameters.AddWithValue("@ma", maPet);
                    DataTable dtTiem = new DataTable();
                    daTiem.Fill(dtTiem);

                    if (dtKham.Rows.Count == 0 && dtTiem.Rows.Count == 0) 
                    {
                        MessageBox.Show("Thú cưng mã " + maPet + " chưa từng khám bệnh hoặc tiêm ngừa tại đây.", "Thông báo");
                    } 
                    else 
                    {
                        dgvKhamBenh.DataSource = dtKham;
                        dgvTiemPhong.DataSource = dtTiem;
                    }
                }
                return; // Thoát hàm nếu đã chạy SQL thành công
            } 
            catch (Exception ex) 
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
            }
            */

            // 2. DỮ LIỆU GIẢ LẬP (MOCK DATA) ĐỂ TEST GIAO DIỆN THEO HÌNH ẢNH
            if (maPet.ToUpper() == "PET01")
            {
                // Giả lập bảng Khám bệnh (Đầy đủ 4 cột như hình)
                DataTable dtKhamMock = new DataTable();
                dtKhamMock.Columns.Add("Ngày Khám");
                dtKhamMock.Columns.Add("Chẩn Đoán");
                dtKhamMock.Columns.Add("Điều Trị");
                dtKhamMock.Columns.Add("Bác Sĩ");

                dtKhamMock.Rows.Add("20/12/2025", "Viêm da cơ địa", "Bôi thuốc mỡ, uống kháng sinh", "Bsi. Nam");
                dtKhamMock.Rows.Add("15/10/2025", "Rối loạn tiêu hóa", "Truyền dịch, uống men vi sinh", "Bsi. Tiến");
                dgvKhamBenh.DataSource = dtKhamMock;

                // Giả lập bảng Tiêm phòng
                DataTable dtTiemMock = new DataTable();
                dtTiemMock.Columns.Add("Ngày Tiêm");
                dtTiemMock.Columns.Add("Tên Vaccine");
                dtTiemMock.Columns.Add("Loại");
                dtTiemMock.Columns.Add("Hẹn Nhắc Lại");

                dtTiemMock.Rows.Add("01/12/2025", "Rabisin", "Phòng dại", "01/12/2026");
                dtTiemMock.Rows.Add("20/05/2025", "Vanguard Plus 5", "Phòng 5 bệnh", "20/05/2026");
                dgvTiemPhong.DataSource = dtTiemMock;
            }
            else
            {
                MessageBox.Show("Không tìm thấy dữ liệu cho mã thú cưng: " + maPet, "Thông báo");
            }
        }
    }
}
