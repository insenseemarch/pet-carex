namespace KhachHang
{
    partial class ucThanhToan
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
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.colTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTomTat = new System.Windows.Forms.Panel();
            this.btnApDung = new System.Windows.Forms.Button();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.lblHinhThucTT = new System.Windows.Forms.Label();
            this.grBHinhThucTT = new System.Windows.Forms.GroupBox();
            this.rdoChuyenKhoan = new System.Windows.Forms.RadioButton();
            this.rdoTienMat = new System.Windows.Forms.RadioButton();
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.lblTienPhaiTra = new System.Windows.Forms.Label();
            this.txtMaGiamGia = new System.Windows.Forms.TextBox();
            this.lblMaGiamGia = new System.Windows.Forms.Label();
            this.lblTien = new System.Windows.Forms.Label();
            this.lbTongTien = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.pnlTomTat.SuspendLayout();
            this.grBHinhThucTT.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGioHang.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvGioHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGioHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTen,
            this.colGia,
            this.colSoLuong,
            this.colThanhTien});
            this.dgvGioHang.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvGioHang.Location = new System.Drawing.Point(0, 0);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.RowHeadersWidth = 51;
            this.dgvGioHang.RowTemplate.Height = 24;
            this.dgvGioHang.Size = new System.Drawing.Size(553, 666);
            this.dgvGioHang.TabIndex = 0;
            this.dgvGioHang.MouseClick += new System.Windows.Forms.MouseEventHandler(this.x);
            // 
            // colTen
            // 
            this.colTen.HeaderText = "Tên Sản Phẩm";
            this.colTen.MinimumWidth = 6;
            this.colTen.Name = "colTen";
            // 
            // colGia
            // 
            this.colGia.HeaderText = "Đơn Giá";
            this.colGia.MinimumWidth = 6;
            this.colGia.Name = "colGia";
            // 
            // colSoLuong
            // 
            this.colSoLuong.HeaderText = "Số Lượng";
            this.colSoLuong.MinimumWidth = 6;
            this.colSoLuong.Name = "colSoLuong";
            // 
            // colThanhTien
            // 
            this.colThanhTien.HeaderText = "Thành Tiền";
            this.colThanhTien.MinimumWidth = 6;
            this.colThanhTien.Name = "colThanhTien";
            // 
            // pnlTomTat
            // 
            this.pnlTomTat.BackColor = System.Drawing.Color.Wheat;
            this.pnlTomTat.Controls.Add(this.btnApDung);
            this.pnlTomTat.Controls.Add(this.btnQuayLai);
            this.pnlTomTat.Controls.Add(this.btnXacNhan);
            this.pnlTomTat.Controls.Add(this.lblHinhThucTT);
            this.pnlTomTat.Controls.Add(this.grBHinhThucTT);
            this.pnlTomTat.Controls.Add(this.lblThanhTien);
            this.pnlTomTat.Controls.Add(this.lblTienPhaiTra);
            this.pnlTomTat.Controls.Add(this.txtMaGiamGia);
            this.pnlTomTat.Controls.Add(this.lblMaGiamGia);
            this.pnlTomTat.Controls.Add(this.lblTien);
            this.pnlTomTat.Controls.Add(this.lbTongTien);
            this.pnlTomTat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTomTat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTomTat.Location = new System.Drawing.Point(553, 0);
            this.pnlTomTat.Name = "pnlTomTat";
            this.pnlTomTat.Size = new System.Drawing.Size(662, 666);
            this.pnlTomTat.TabIndex = 1;
            // 
            // btnApDung
            // 
            this.btnApDung.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnApDung.Location = new System.Drawing.Point(538, 130);
            this.btnApDung.Name = "btnApDung";
            this.btnApDung.Size = new System.Drawing.Size(121, 38);
            this.btnApDung.TabIndex = 10;
            this.btnApDung.Text = "Áp Dụng";
            this.btnApDung.UseVisualStyleBackColor = false;
            this.btnApDung.Click += new System.EventHandler(this.btnApDung_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnQuayLai.Location = new System.Drawing.Point(121, 553);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(169, 48);
            this.btnQuayLai.TabIndex = 9;
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.UseVisualStyleBackColor = false;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnXacNhan.Location = new System.Drawing.Point(371, 553);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(169, 48);
            this.btnXacNhan.TabIndex = 8;
            this.btnXacNhan.Text = "Xác Nhận";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // lblHinhThucTT
            // 
            this.lblHinhThucTT.AutoSize = true;
            this.lblHinhThucTT.Location = new System.Drawing.Point(6, 348);
            this.lblHinhThucTT.Name = "lblHinhThucTT";
            this.lblHinhThucTT.Size = new System.Drawing.Size(218, 25);
            this.lblHinhThucTT.TabIndex = 7;
            this.lblHinhThucTT.Text = "Hình thức thanh toán:";
            // 
            // grBHinhThucTT
            // 
            this.grBHinhThucTT.Controls.Add(this.rdoChuyenKhoan);
            this.grBHinhThucTT.Controls.Add(this.rdoTienMat);
            this.grBHinhThucTT.Location = new System.Drawing.Point(247, 348);
            this.grBHinhThucTT.Name = "grBHinhThucTT";
            this.grBHinhThucTT.Size = new System.Drawing.Size(200, 71);
            this.grBHinhThucTT.TabIndex = 6;
            this.grBHinhThucTT.TabStop = false;
            this.grBHinhThucTT.Text = "groupBox1";
            // 
            // rdoChuyenKhoan
            // 
            this.rdoChuyenKhoan.AutoSize = true;
            this.rdoChuyenKhoan.Location = new System.Drawing.Point(6, 35);
            this.rdoChuyenKhoan.Name = "rdoChuyenKhoan";
            this.rdoChuyenKhoan.Size = new System.Drawing.Size(177, 29);
            this.rdoChuyenKhoan.TabIndex = 1;
            this.rdoChuyenKhoan.Text = "Chuyển Khoản";
            this.rdoChuyenKhoan.UseVisualStyleBackColor = true;
            // 
            // rdoTienMat
            // 
            this.rdoTienMat.AutoSize = true;
            this.rdoTienMat.Checked = true;
            this.rdoTienMat.Location = new System.Drawing.Point(6, 0);
            this.rdoTienMat.Name = "rdoTienMat";
            this.rdoTienMat.Size = new System.Drawing.Size(118, 29);
            this.rdoTienMat.TabIndex = 0;
            this.rdoTienMat.TabStop = true;
            this.rdoTienMat.Text = "Tiền Mặt";
            this.rdoTienMat.UseVisualStyleBackColor = true;
            // 
            // lblThanhTien
            // 
            this.lblThanhTien.AutoSize = true;
            this.lblThanhTien.Location = new System.Drawing.Point(210, 235);
            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.Size = new System.Drawing.Size(70, 25);
            this.lblThanhTien.TabIndex = 5;
            this.lblThanhTien.Text = "label1";
            // 
            // lblTienPhaiTra
            // 
            this.lblTienPhaiTra.AutoSize = true;
            this.lblTienPhaiTra.Location = new System.Drawing.Point(6, 235);
            this.lblTienPhaiTra.Name = "lblTienPhaiTra";
            this.lblTienPhaiTra.Size = new System.Drawing.Size(171, 25);
            this.lblTienPhaiTra.TabIndex = 4;
            this.lblTienPhaiTra.Text = "Số tiền phải trả: ";
            this.lblTienPhaiTra.Click += new System.EventHandler(this.lblTienPhaiTra_Click);
            // 
            // txtMaGiamGia
            // 
            this.txtMaGiamGia.Location = new System.Drawing.Point(215, 134);
            this.txtMaGiamGia.Name = "txtMaGiamGia";
            this.txtMaGiamGia.Size = new System.Drawing.Size(304, 30);
            this.txtMaGiamGia.TabIndex = 3;
            this.txtMaGiamGia.TextChanged += new System.EventHandler(this.tbMaGiamGia_TextChanged);
            // 
            // lblMaGiamGia
            // 
            this.lblMaGiamGia.AutoSize = true;
            this.lblMaGiamGia.Location = new System.Drawing.Point(6, 137);
            this.lblMaGiamGia.Name = "lblMaGiamGia";
            this.lblMaGiamGia.Size = new System.Drawing.Size(192, 25);
            this.lblMaGiamGia.TabIndex = 2;
            this.lblMaGiamGia.Text = "Nhập mã giảm giá:";
            // 
            // lblTien
            // 
            this.lblTien.AutoSize = true;
            this.lblTien.Location = new System.Drawing.Point(210, 34);
            this.lblTien.Name = "lblTien";
            this.lblTien.Size = new System.Drawing.Size(70, 25);
            this.lblTien.TabIndex = 1;
            this.lblTien.Text = "label1";
            // 
            // lbTongTien
            // 
            this.lbTongTien.AutoSize = true;
            this.lbTongTien.Location = new System.Drawing.Point(6, 34);
            this.lbTongTien.Name = "lbTongTien";
            this.lbTongTien.Size = new System.Drawing.Size(124, 25);
            this.lbTongTien.TabIndex = 0;
            this.lbTongTien.Text = "Tổng Tiền: ";
            // 
            // ucThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTomTat);
            this.Controls.Add(this.dgvGioHang);
            this.Name = "ucThanhToan";
            this.Size = new System.Drawing.Size(1215, 666);
            this.Load += new System.EventHandler(this.ucThanhToan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.pnlTomTat.ResumeLayout(false);
            this.pnlTomTat.PerformLayout();
            this.grBHinhThucTT.ResumeLayout(false);
            this.grBHinhThucTT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Panel pnlTomTat;
        private System.Windows.Forms.Label lbTongTien;
        private System.Windows.Forms.Label lblThanhTien;
        private System.Windows.Forms.Label lblTienPhaiTra;
        private System.Windows.Forms.TextBox txtMaGiamGia;
        private System.Windows.Forms.Label lblMaGiamGia;
        private System.Windows.Forms.Label lblTien;
        private System.Windows.Forms.Label lblHinhThucTT;
        private System.Windows.Forms.GroupBox grBHinhThucTT;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RadioButton rdoChuyenKhoan;
        private System.Windows.Forms.RadioButton rdoTienMat;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.Button btnApDung;
    }
}
