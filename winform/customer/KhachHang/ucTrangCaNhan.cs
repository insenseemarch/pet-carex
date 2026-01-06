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
using KhachHang.Common;
using KhachHang.Data;

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
            if (!Session.IsLoggedIn) return;

            // 1) Thông tin KH
            var dt = Db.QueryToTable(@"
                select  kh.makh, kh.hoten, kh.sdt, kh.email, kh.cccd, kh.gioitinh, kh.ngaysinh,
                tk.capbac, tk.diemtichluy
                from khachhang kh
                join taikhoankhachhang tk on tk.makh = kh.makh
                where kh.makh = @makh
               ", new SqlParameter("@makh", SqlDbType.VarChar, 10) { Value = Session.MaKH });


            if (dt.Rows.Count == 0) return;
            var r = dt.Rows[0];

            txtHoTen.Text = r["hoten"].ToString();
            txtSDT.Text = r["sdt"].ToString();
            txtEmail.Text = r["email"].ToString();
            txtCCCD.Text = r["cccd"].ToString();
            txtGioiTinh.Text = r["gioitinh"].ToString();

            if (r["ngaysinh"] != DBNull.Value)
                dtpNgaySinh.Value = Convert.ToDateTime(r["ngaysinh"]);

            // 2) Danh sách thú cưng
            ReloadThuCung();
        }

        private void ReloadThuCung()
        {
            dgvThuCung.DataSource = Db.QueryToTable(@"
                select  mathucung, tenthucung, loai, giong, ngaysinh, gioitinh,
                tinhtrangsuckhoe as tinhtrang
                from thucung
                where makh = @makh",
                new SqlParameter("@makh", SqlDbType.VarChar, 10) { Value = Session.MaKH }
            );
            dgvThuCung.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvThuCung.RowHeadersVisible = false;
            dgvThuCung.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThuCung.MultiSelect = false;
            dgvThuCung.AllowUserToAddRows = false;
            dgvThuCung.ReadOnly = true;
            dgvThuCung.Columns["mathucung"].HeaderText = "Mã thú cưng";
            dgvThuCung.Columns["tenthucung"].HeaderText = "Tên";
            dgvThuCung.Columns["loai"].HeaderText = "Loài";
            dgvThuCung.Columns["giong"].HeaderText = "Giống";
            dgvThuCung.Columns["ngaysinh"].HeaderText = "Ngày sinh";
            dgvThuCung.Columns["gioitinh"].HeaderText = "Giới tính";
            dgvThuCung.Columns["tinhtrang"].HeaderText = "Tình trạng";
            dgvThuCung.Columns["ngaysinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
    }
}
