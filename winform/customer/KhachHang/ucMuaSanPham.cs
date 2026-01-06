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
using KhachHang.Common;
using KhachHang.Data;
using System.Data.SqlClient;

namespace KhachHang
{
    public partial class ucMuaSanPham : UserControl
    {
        private List<ProductModel> _view = new List<ProductModel>();     // kết quả sau lọc/tìm kiếm
        private int _pageIndex = 0;
        private const int PageSize = 40; // 30/40/60 tuỳ máy

        public ucMuaSanPham()
        {
            InitializeComponent();
            // Bật bộ đệm đôi để tránh hiện tượng nhấp nháy/nhảy hình
            //this.DoubleBuffered = true;
        }

        // Dùng 1 List duy nhất để quản lý tất cả sản phẩm
        List<ProductModel> AllProducts = new List<ProductModel>();
        private void LoadProductsFromDb()
        {
            AllProducts.Clear();

            var dt = Db.QueryToTable(@"
        select masp, tensp, loaisp, gia, soluongton, hsd
        from sanpham
    ");

            foreach (DataRow r in dt.Rows)
            {
                AllProducts.Add(new ProductModel
                {
                    MaSP = r["masp"].ToString(),
                    TenSP = r["tensp"].ToString(),
                    LoaiSP = r["loaisp"].ToString(),
                    GiaSP = r["gia"] == DBNull.Value ? 0 : Convert.ToDecimal(r["gia"]),
                    SoLuongTon = r["soluongton"] == DBNull.Value ? 0 : Convert.ToInt32(r["soluongton"]),
                    HSD = r["hsd"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["hsd"]),
                    AnhSP = null
                });
            }
        }


        private void ucMuaSanPham_Load(object sender, EventArgs e)
        {
            cboLoaiSP.SelectedIndex = 0;
            cboGia.SelectedIndex = 0;

            LoadProductsFromDb();   // đảm bảo _all được set từ DB
            _view = AllProducts;           // chưa lọc gì thì view = all
            _pageIndex = 0;
            RenderPage();
        }

        public void ApplySearch(string keyword)
        {
            ThucHienLoc(keyword);
        }

        private bool IsAll(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return true;
            s = s.Trim();
            return s.Equals("Tất cả", StringComparison.CurrentCultureIgnoreCase)
                || s.Equals("Tat ca", StringComparison.CurrentCultureIgnoreCase)
                || s.Equals("Tất Ca", StringComparison.CurrentCultureIgnoreCase);
        }

        private IEnumerable<ProductModel> FilterByGia(IEnumerable<ProductModel> q, string gia)
        {
            gia = (gia ?? "").Trim();

            if (gia == "Dưới 100.000") return q.Where(p => p.GiaSP < 100000m);
            if (gia == "100.000 - 500.000") return q.Where(p => p.GiaSP >= 100000m && p.GiaSP <= 500000m);
            if (gia == "Trên 500.000") return q.Where(p => p.GiaSP > 500000m);

            return q; // fallback
        }

        private void ThucHienLoc(string keyword = "")
        {
            if (AllProducts.Count == 0) LoadProductsFromDb(); // nhớ: Load chỉ fill AllProducts, KHÔNG overwrite

            string loaiSP = (cboLoaiSP.Text ?? "").Trim();  // <-- dùng Text
            string gia = (cboGia.Text ?? "").Trim();     // <-- dùng Text

            keyword = (keyword ?? "").Trim();
            if (keyword.Equals("Nhập từ khóa để tìm kiếm...", StringComparison.CurrentCultureIgnoreCase))
                keyword = "";

            IEnumerable<ProductModel> q = AllProducts;

            // lọc theo tên
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                q = q.Where(p =>
                    (!string.IsNullOrEmpty(p.TenSP) &&
                        p.TenSP.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    || (!string.IsNullOrEmpty(p.LoaiSP) &&
                        p.LoaiSP.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    || (!string.IsNullOrEmpty(p.MaSP) &&
                        p.MaSP.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) >= 0)
                );
            }

            // lọc theo loại SP
            if (!IsAll(loaiSP))
                q = q.Where(p => string.Equals((p.LoaiSP ?? "").Trim(), loaiSP, StringComparison.CurrentCultureIgnoreCase));

            // lọc theo giá
            if (!IsAll(gia))
                q = FilterByGia(q, gia);

            _view = q.ToList();     // <-- IMPORTANT: _view là list kết quả, đừng gán ngược vào AllProducts
            _pageIndex = 0;
            RenderPage();
        }


        private void RenderPage()
        {
            flpDanhSachSanPham.SuspendLayout();

            // Giải phóng handle cũ (đừng chỉ Clear)
            foreach (Control c in flpDanhSachSanPham.Controls) c.Dispose();
            flpDanhSachSanPham.Controls.Clear();

            var page = _view
                .Skip(_pageIndex * PageSize)
                .Take(PageSize)
                .ToList();

            foreach (var p in page)
            {
                var item = new ucItem();
                Console.WriteLine($"{p.MaSP} | {p.TenSP} | {p.GiaSP}");
                item.SetData(p);

                item.Dock = DockStyle.None;
                item.AutoSize = false;
                item.Width = 220;
                item.Height = 260;
                item.Margin = new Padding(12);

                flpDanhSachSanPham.Controls.Add(item);
            }

            flpDanhSachSanPham.ResumeLayout();

            // (tuỳ chọn) update label trang
            // lblPage.Text = $"{_pageIndex+1}/{Math.Max(1, (int)Math.Ceiling(_view.Count/(double)PageSize))}";
        }



        private void lbLoai_Click(object sender, EventArgs e)
        {

        }

        private void cboLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ThucHienLoc("");
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            int maxPage = (int)Math.Ceiling(_view.Count / (double)PageSize) - 1;
            if (_pageIndex < maxPage)
            {
                _pageIndex++;
                RenderPage();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (_pageIndex > 0)
            {
                _pageIndex--;
                RenderPage();
            }
        }

        private void btnThanhToan_Click_1(object sender, EventArgs e)
        {

        }
    }
}
