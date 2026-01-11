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
    public partial class ucLichLamViec : UserControl
    {
        // CHUỖI KẾT NỐI SQL (Thay đổi Data Source và Initial Catalog theo máy của bạn)
        // string strCon = @"Data Source=TEN_MAY\SQLEXPRESS;Initial Catalog=PetCareDB;Integrated Security=True";

        public ucLichLamViec()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadLichTruc();
        }

        private void SetupDataGridView()
        {
            dgvLichTruc.ColumnCount = 3;
            dgvLichTruc.Columns[0].Name = "Thứ / Ngày";
            dgvLichTruc.Columns[1].Name = "Ca Sáng (07:00 - 10:00)";
            dgvLichTruc.Columns[2].Name = "Ca Chiều (13:00 - 16:00)";

            // Tùy chỉnh giao diện bảng
            dgvLichTruc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLichTruc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgvLichTruc.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dgvLichTruc.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Xuống dòng khi tên bác sĩ dài
            dgvLichTruc.RowTemplate.Height = 80; // Tăng chiều cao dòng cho dễ nhìn
            dgvLichTruc.AllowUserToAddRows = false;
        }

        private void LoadLichTruc()
        {
            dgvLichTruc.Rows.Clear();

            DateTime today = DateTime.Now;
            int dayOfWeek = (int)today.DayOfWeek;
            if (dayOfWeek == 0) dayOfWeek = 7; // Quy đổi Chủ Nhật thành 7

            for (int i = dayOfWeek; i <= 7; i++)
            {
                DateTime targetDate = today.AddDays(i - dayOfWeek);
                string thuStr = (i == 7) ? "Chủ Nhật" : "Thứ " + (i + 1);
                string ngayThang = targetDate.ToString("dd/MM/yyyy");

                // --- PHẦN LẤY DỮ LIỆU ---
                // Mặc định hiện tại dùng hàm giả lập
                string bsiSang = LayBsiTuDatabase(targetDate, "Sáng");
                string bsiChieu = LayBsiTuDatabase(targetDate, "Chiều");

                dgvLichTruc.Rows.Add($"{thuStr}\n({ngayThang})", bsiSang, bsiChieu);
            }
        }

        // HÀM MẪU LẤY DỮ LIỆU (Kết hợp giả lập và hướng dẫn SQL)
        private string LayBsiTuDatabase(DateTime ngay, string ca)
        {
            /* // CODE MẪU LẤY TỪ SQL SERVER:
            try 
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    // Giả sử bảng LichTruc có các cột: NgayTruc, CaTruc, TenBacSi
                    string sql = "SELECT TenBacSi FROM LichTruc WHERE NgayTruc = @ngay AND CaTruc = @ca";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ngay", ngay.Date);
                    cmd.Parameters.AddWithValue("@ca", ca);
                    
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : "Chưa phân công";
                }
            }
            catch (Exception ex)
            {
                // return "Lỗi kết nối";
            }
            */

            // TRẢ VỀ GIẢ LẬP (Nếu chưa mở SQL)
            int thu = (int)ngay.DayOfWeek == 0 ? 7 : (int)ngay.DayOfWeek;
            if (ca == "Sáng")
                return (thu % 2 == 0) ? "Bsi Nam, Bsi Tiến" : "Bsi Lan, Bsi Hương";
            else
                return (thu % 2 == 0) ? "Bsi Phương, Bsi Tuấn" : "Bsi Minh, Bsi Ngọc";
        }

        private void btnQuayLai_Click_1(object sender, EventArgs e)
        {
            Form1 f1 = (Form1)this.FindForm();
            f1.hienThiTrang(new ucTraCuuMain());
        }
    }
}
