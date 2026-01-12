using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;

namespace KhachHang
{
    public partial class ucMuaSanPham : UserControl
    {
        public ucMuaSanPham()
        {
            InitializeComponent();
            // Bật bộ đệm đôi để tránh hiện tượng nhấp nháy/nhảy hình
            //this.DoubleBuffered = true;
        }

        public class Product
        {
            public string Name { get; set; }
            public string Species { get; set; } // Chó, Mèo, Thỏ, Chim, Hamster, Sóc
            public string Category { get; set; } // Thức ăn, Nước, Thuốc, Đồ chơi
            public double Price { get; set; }
            public Image Photo { get; set; }
        }

        // Dùng 1 List duy nhất để quản lý tất cả sản phẩm
        List<Product> AllProducts = new List<Product>();

        private void ucMuaSanPham_Load(object sender, EventArgs e)
        {
            // 1. Set mặc định
            cboLoaiTC.SelectedIndex = 0; // Loài thú cưng: Tất cả
            cboLoaiSP.SelectedIndex = 0; // Loại SP: Tất cả
            cboGia.SelectedIndex = 0;    // Giá: Tất cả

            // 2. Nạp dữ liệu mẫu vào danh sách AllProducts
            LoadDataSample();

            // 3. Hiển thị tất cả lần đầu
            HienThiSanPham(AllProducts);
        }

        private void LoadDataSample()
        {
            AllProducts.Clear();

            // Ví dụ nạp sản phẩm Chó (Thức ăn)
            for (int i = 0; i < imageListCho.Images.Count; i++)
            {
                AllProducts.Add(new Product { Name = "Thức ăn Chó " + (i + 1), Species = "Chó", Category = "Thức ăn", Price = 120000, Photo = imageListCho.Images[i] });
            }

            // Ví dụ nạp sản phẩm Mèo (Nước uống)
            for (int i = 0; i < imageListMeo.Images.Count; i++)
            {
                AllProducts.Add(new Product { Name = "Nước uống Mèo " + (i + 1), Species = "Mèo", Category = "Nước", Price = 45000, Photo = imageListMeo.Images[i] });
            }

            // Ví dụ nạp Đồ chơi chung
            for (int i = 0; i < imageListDoChoi.Images.Count; i++)
            {
                AllProducts.Add(new Product { Name = "Đồ chơi Pet " + (i + 1), Species = "Tất cả", Category = "Đồ chơi", Price = 600000, Photo = imageListDoChoi.Images[i] });
            }

            // Thêm Hamster, Thỏ, Chim... tương tự vào AllProducts
        }

        private void HienThiSanPham(List<Product> danhSach)
        {
            flpDanhSachSanPham.Controls.Clear();
            foreach (var p in danhSach)
            {
                ucItem item = new ucItem();
                item.SetData(p.Name, p.Price, p.Photo);
                flpDanhSachSanPham.Controls.Add(item);
            }
        }

        private void ThucHienLoc()
        {
            // 1. Dừng vẽ giao diện để tăng tốc và tránh nhấp nháy
            this.SuspendLayout();
            flpDanhSachSanPham.Controls.Clear();

            // 2. Lấy giá trị và chuẩn hóa chuỗi (bỏ khoảng trắng thừa)
            string loaiTC = cboLoaiTC.SelectedItem?.ToString().Trim() ?? "Tất cả";
            string loaiSP = cboLoaiSP.SelectedItem?.ToString().Trim() ?? "Tất cả";
            string gia = cboGia.SelectedItem?.ToString().Trim() ?? "Tất cả";

            // 3. Bắt đầu lọc từ danh sách TỔNG (AllProducts)
            // Lưu ý: Không dùng các list riêng lẻ như ChoProducts nữa
            var query = AllProducts.AsEnumerable();

            // Lọc theo Loài thú cưng
            if (loaiTC != "Tất cả")
            {
                query = query.Where(p => p.Species == loaiTC);
            }

            // Lọc theo Loại sản phẩm
            if (loaiSP != "Tất cả")
            {
                query = query.Where(p => p.Category == loaiSP);
            }

            // Lọc theo Giá
            if (gia != "Tất cả")
            {
                if (gia == "Dưới 100.000")
                    query = query.Where(p => p.Price < 100000);
                else if (gia == "100.000 - 500.000")
                    query = query.Where(p => p.Price >= 100000 && p.Price <= 500000);
                else if (gia == "Trên 500.000")
                    query = query.Where(p => p.Price > 500000);
            }

            // 4. Chuyển kết quả về List và hiển thị
            List<Product> ketQua = query.ToList();

            foreach (var p in ketQua)
            {
                ucItem item = new ucItem();
                item.SetData(p.Name, p.Price, p.Photo);
                flpDanhSachSanPham.Controls.Add(item);
            }

            this.ResumeLayout();
        }

        private void lbLoai_Click(object sender, EventArgs e)
        {

        }

        private void cboLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ThucHienLoc();
        }

        private void pnlMuaSP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra nếu giỏ hàng trống thì không cho chuyển trang
            if (Form1.GioHang.Count == 0)
            {
                MessageBox.Show("Giỏ hàng của bạn đang trống. Vui lòng thêm sản phẩm!");
                return;
            }

            // 2. Tìm Form chính (Form1) và gọi hàm Navigation để chuyển trang
            Form1 f = (Form1)this.FindForm();
            if (f != null)
            {
                f.Navigation(new ucThanhToan());
            }
        }

        private void panelSearch_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
