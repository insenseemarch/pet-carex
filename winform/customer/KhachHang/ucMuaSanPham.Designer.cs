using System;

namespace KhachHang
{
    partial class ucMuaSanPham
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMuaSanPham));
            this.pnlMuaSP = new System.Windows.Forms.Panel();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.cboGia = new System.Windows.Forms.ComboBox();
            this.lbGia = new System.Windows.Forms.Label();
            this.lbLoai = new System.Windows.Forms.Label();
            this.cboLoaiSP = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.imageListCho = new System.Windows.Forms.ImageList(this.components);
            this.imageListMeo = new System.Windows.Forms.ImageList(this.components);
            this.imageListDoChoi = new System.Windows.Forms.ImageList(this.components);
            this.imageListTho = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnPrev = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.pnlPaging = new System.Windows.Forms.Panel();
            this.flpDanhSachSanPham = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlMuaSP.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.pnlPaging.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMuaSP
            // 
            this.pnlMuaSP.BackColor = System.Drawing.Color.Silver;
            this.pnlMuaSP.Controls.Add(this.panelSearch);
            this.pnlMuaSP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMuaSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMuaSP.Location = new System.Drawing.Point(0, 0);
            this.pnlMuaSP.MaximumSize = new System.Drawing.Size(0, 80);
            this.pnlMuaSP.Name = "pnlMuaSP";
            this.pnlMuaSP.Size = new System.Drawing.Size(1218, 80);
            this.pnlMuaSP.TabIndex = 0;
            this.pnlMuaSP.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMuaSP_Paint);
            // 
            // panelSearch
            // 
            this.panelSearch.AutoScroll = true;
            this.panelSearch.BackColor = System.Drawing.Color.Silver;
            this.panelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearch.Controls.Add(this.cboGia);
            this.panelSearch.Controls.Add(this.lbGia);
            this.panelSearch.Controls.Add(this.lbLoai);
            this.panelSearch.Controls.Add(this.cboLoaiSP);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1218, 77);
            this.panelSearch.TabIndex = 1;
            this.panelSearch.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSearch_Paint);
            // 
            // cboGia
            // 
            this.cboGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.cboGia.FormattingEnabled = true;
            this.cboGia.Items.AddRange(new object[] {
            "Tất cả",
            "Dưới 100.000",
            "100.000 - 500.000",
            "Trên 500.000"});
            this.cboGia.Location = new System.Drawing.Point(429, 2);
            this.cboGia.Name = "cboGia";
            this.cboGia.Size = new System.Drawing.Size(174, 28);
            this.cboGia.TabIndex = 7;
            // 
            // lbGia
            // 
            this.lbGia.AutoSize = true;
            this.lbGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.lbGia.Location = new System.Drawing.Point(378, 6);
            this.lbGia.Name = "lbGia";
            this.lbGia.Size = new System.Drawing.Size(45, 20);
            this.lbGia.TabIndex = 6;
            this.lbGia.Text = "Giá: ";
            // 
            // lbLoai
            // 
            this.lbLoai.AutoSize = true;
            this.lbLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.lbLoai.Location = new System.Drawing.Point(2, 6);
            this.lbLoai.Name = "lbLoai";
            this.lbLoai.Size = new System.Drawing.Size(133, 20);
            this.lbLoai.TabIndex = 3;
            this.lbLoai.Text = "Loại Sản Phẩm: ";
            this.lbLoai.Click += new System.EventHandler(this.lbLoai_Click);
            // 
            // cboLoaiSP
            // 
            this.cboLoaiSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.cboLoaiSP.FormattingEnabled = true;
            this.cboLoaiSP.Items.AddRange(new object[] {
            "Tất Cả",
            "Thức Ăn",
            "Nước",
            "Thuốc",
            "Đồ Chơi cho thú cưng"});
            this.cboLoaiSP.Location = new System.Drawing.Point(141, 2);
            this.cboLoaiSP.Name = "cboLoaiSP";
            this.cboLoaiSP.Size = new System.Drawing.Size(204, 28);
            this.cboLoaiSP.TabIndex = 2;
            this.cboLoaiSP.SelectedIndexChanged += new System.EventHandler(this.cboLoaiSP_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(677, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 31);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Lọc";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // imageListCho
            // 
            this.imageListCho.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCho.ImageStream")));
            this.imageListCho.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListCho.Images.SetKeyName(0, "10-loai-thuc-an-cho-cho-con-dinh-duong-nhat-hien-nay-202104141607304502.jpg");
            this.imageListCho.Images.SetKeyName(1, "top-thuc-an-cho-cho-ngon-bo-duong-ma-chu-cho-nao-cung-me-202201101631345340.jpg");
            this.imageListCho.Images.SetKeyName(2, "DItekSiFzogDbmMtSmDS.jpg");
            this.imageListCho.Images.SetKeyName(3, "Hat-Zoi-Dog-thuc-an-cho-cho-20kg.jpg");
            // 
            // imageListMeo
            // 
            this.imageListMeo.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMeo.ImageStream")));
            this.imageListMeo.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMeo.Images.SetKeyName(0, "sg-11134201-7rfib-m367oy9keuavf8.jpg");
            this.imageListMeo.Images.SetKeyName(1, "Haisenpet-Premium-Cat-Food-with-Tuna-Salmon-Mackerel-3kg-Pack-Cat-Food.jpg");
            this.imageListMeo.Images.SetKeyName(2, "6035a77789080.jpg");
            this.imageListMeo.Images.SetKeyName(3, "8564a8cc2a560d1ff3bf598f5b4cc3c4.jpg");
            this.imageListMeo.Images.SetKeyName(4, "Petland-Jelly-Pouch.jpg");
            // 
            // imageListDoChoi
            // 
            this.imageListDoChoi.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDoChoi.ImageStream")));
            this.imageListDoChoi.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDoChoi.Images.SetKeyName(0, "5736017403_1734590950-768x768.jpg");
            this.imageListDoChoi.Images.SetKeyName(1, "2676804811_1847764777.jpg");
            // 
            // imageListTho
            // 
            this.imageListTho.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTho.ImageStream")));
            this.imageListTho.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTho.Images.SetKeyName(0, "sg-11134201-7rd3n-lvg29fb40cz5d1.jpg");
            this.imageListTho.Images.SetKeyName(1, "4f8826300157bf5a880a670287205f50.jpg_720x720q80.jpg");
            this.imageListTho.Images.SetKeyName(2, "d329a7003a2c75ece5f06d691e1163c7.jpg");
            this.imageListTho.Images.SetKeyName(3, "vn-11134207-7r98o-lp3bihq9topaae.jpg");
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPrev.Location = new System.Drawing.Point(7, 13);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(97, 40);
            this.btnPrev.TabIndex = 1;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button2.Location = new System.Drawing.Point(120, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 40);
            this.button2.TabIndex = 2;
            this.button2.Text = "Next";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BackColor = System.Drawing.Color.Green;
            this.btnThanhToan.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhToan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(1055, 0);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(163, 61);
            this.btnThanhToan.TabIndex = 3;
            this.btnThanhToan.Text = "Thanh Toán";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click_1);
            // 
            // pnlPaging
            // 
            this.pnlPaging.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pnlPaging.Controls.Add(this.btnThanhToan);
            this.pnlPaging.Controls.Add(this.button2);
            this.pnlPaging.Controls.Add(this.btnPrev);
            this.pnlPaging.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPaging.Location = new System.Drawing.Point(0, 605);
            this.pnlPaging.Name = "pnlPaging";
            this.pnlPaging.Size = new System.Drawing.Size(1218, 61);
            this.pnlPaging.TabIndex = 4;
            // 
            // flpDanhSachSanPham
            // 
            this.flpDanhSachSanPham.AutoScroll = true;
            this.flpDanhSachSanPham.BackColor = System.Drawing.Color.SteelBlue;
            this.flpDanhSachSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDanhSachSanPham.Location = new System.Drawing.Point(0, 0);
            this.flpDanhSachSanPham.Name = "flpDanhSachSanPham";
            this.flpDanhSachSanPham.Size = new System.Drawing.Size(1215, 516);
            this.flpDanhSachSanPham.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flpDanhSachSanPham);
            this.panel1.Location = new System.Drawing.Point(0, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1215, 516);
            this.panel1.TabIndex = 5;
            // 
            // ucMuaSanPham
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnlMuaSP);
            this.Controls.Add(this.pnlPaging);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.Name = "ucMuaSanPham";
            this.Size = new System.Drawing.Size(1218, 666);
            this.Load += new System.EventHandler(this.ucMuaSanPham_Load);
            this.pnlMuaSP.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.pnlPaging.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMuaSP;
        private System.Windows.Forms.ImageList imageListCho;
        private System.Windows.Forms.ImageList imageListMeo;
        private System.Windows.Forms.ImageList imageListDoChoi;
        private System.Windows.Forms.ImageList imageListTho;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Panel pnlPaging;
        private System.Windows.Forms.FlowLayoutPanel flpDanhSachSanPham;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.ComboBox cboGia;
        private System.Windows.Forms.Label lbGia;
        private System.Windows.Forms.Label lbLoai;
        private System.Windows.Forms.ComboBox cboLoaiSP;
        private System.Windows.Forms.Button btnSearch;
    }
}
