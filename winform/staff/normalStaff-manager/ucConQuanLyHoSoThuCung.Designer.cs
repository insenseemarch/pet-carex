namespace PetCareX
{
    partial class ucQuanLyHoSoThuCung
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
            this.label1 = new System.Windows.Forms.Label();
            this.grbChiTiet = new System.Windows.Forms.GroupBox();
            this.txtTinhTrang = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.txtGiong = new System.Windows.Forms.TextBox();
            this.txtTenThu = new System.Windows.Forms.TextBox();
            this.txtMaThuCung = new System.Windows.Forms.TextBox();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.lblNgaySinhKH = new System.Windows.Forms.Label();
            this.lblGioiTinhKH = new System.Windows.Forms.Label();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.lblHangTV = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.lblMaKH = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvThuCung = new System.Windows.Forms.DataGridView();
            this.txtLoai = new System.Windows.Forms.TextBox();
            this.grbChiTiet.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThuCung)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1920, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quản Lý Hồ Sơ Thú Cưng";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grbChiTiet
            // 
            this.grbChiTiet.Controls.Add(this.txtLoai);
            this.grbChiTiet.Controls.Add(this.txtTinhTrang);
            this.grbChiTiet.Controls.Add(this.label4);
            this.grbChiTiet.Controls.Add(this.dtpNgaySinh);
            this.grbChiTiet.Controls.Add(this.cboGioiTinh);
            this.grbChiTiet.Controls.Add(this.txtGiong);
            this.grbChiTiet.Controls.Add(this.txtTenThu);
            this.grbChiTiet.Controls.Add(this.txtMaThuCung);
            this.grbChiTiet.Controls.Add(this.txtMaKH);
            this.grbChiTiet.Controls.Add(this.lblNgaySinhKH);
            this.grbChiTiet.Controls.Add(this.lblGioiTinhKH);
            this.grbChiTiet.Controls.Add(this.lblCCCD);
            this.grbChiTiet.Controls.Add(this.lblHangTV);
            this.grbChiTiet.Controls.Add(this.lblSDT);
            this.grbChiTiet.Controls.Add(this.lblHoTen);
            this.grbChiTiet.Controls.Add(this.lblMaKH);
            this.grbChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbChiTiet.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbChiTiet.Location = new System.Drawing.Point(0, 86);
            this.grbChiTiet.Name = "grbChiTiet";
            this.grbChiTiet.Size = new System.Drawing.Size(1920, 388);
            this.grbChiTiet.TabIndex = 2;
            this.grbChiTiet.TabStop = false;
            this.grbChiTiet.Text = "Thông Tin Chi Tiết";
            // 
            // txtTinhTrang
            // 
            this.txtTinhTrang.Location = new System.Drawing.Point(942, 324);
            this.txtTinhTrang.Name = "txtTinhTrang";
            this.txtTinhTrang.Size = new System.Drawing.Size(244, 30);
            this.txtTinhTrang.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(628, 321);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 23);
            this.label4.TabIndex = 26;
            this.label4.Text = "Tình Trạng Sức Khỏe:";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.CalendarFont = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(942, 192);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(148, 26);
            this.dtpNgaySinh.TabIndex = 24;
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.FormattingEnabled = true;
            this.cboGioiTinh.Items.AddRange(new object[] {
            "Đực",
            "Cái"});
            this.cboGioiTinh.Location = new System.Drawing.Point(942, 284);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(148, 31);
            this.cboGioiTinh.TabIndex = 23;
            // 
            // txtGiong
            // 
            this.txtGiong.Location = new System.Drawing.Point(942, 242);
            this.txtGiong.Name = "txtGiong";
            this.txtGiong.Size = new System.Drawing.Size(244, 30);
            this.txtGiong.TabIndex = 22;
            // 
            // txtTenThu
            // 
            this.txtTenThu.Location = new System.Drawing.Point(942, 111);
            this.txtTenThu.Name = "txtTenThu";
            this.txtTenThu.Size = new System.Drawing.Size(244, 30);
            this.txtTenThu.TabIndex = 21;
            // 
            // txtMaThuCung
            // 
            this.txtMaThuCung.Location = new System.Drawing.Point(942, 68);
            this.txtMaThuCung.Name = "txtMaThuCung";
            this.txtMaThuCung.Size = new System.Drawing.Size(361, 30);
            this.txtMaThuCung.TabIndex = 20;
            // 
            // txtMaKH
            // 
            this.txtMaKH.Location = new System.Drawing.Point(942, 29);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(361, 30);
            this.txtMaKH.TabIndex = 19;
            // 
            // lblNgaySinhKH
            // 
            this.lblNgaySinhKH.AutoSize = true;
            this.lblNgaySinhKH.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgaySinhKH.Location = new System.Drawing.Point(628, 192);
            this.lblNgaySinhKH.Name = "lblNgaySinhKH";
            this.lblNgaySinhKH.Size = new System.Drawing.Size(97, 23);
            this.lblNgaySinhKH.TabIndex = 18;
            this.lblNgaySinhKH.Text = "Ngày Sinh:";
            // 
            // lblGioiTinhKH
            // 
            this.lblGioiTinhKH.AutoSize = true;
            this.lblGioiTinhKH.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGioiTinhKH.Location = new System.Drawing.Point(628, 280);
            this.lblGioiTinhKH.Name = "lblGioiTinhKH";
            this.lblGioiTinhKH.Size = new System.Drawing.Size(88, 23);
            this.lblGioiTinhKH.TabIndex = 17;
            this.lblGioiTinhKH.Text = "Giới Tính:";
            // 
            // lblCCCD
            // 
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCCCD.Location = new System.Drawing.Point(628, 239);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(58, 23);
            this.lblCCCD.TabIndex = 16;
            this.lblCCCD.Text = "Giống";
            // 
            // lblHangTV
            // 
            this.lblHangTV.AutoSize = true;
            this.lblHangTV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHangTV.Location = new System.Drawing.Point(628, 151);
            this.lblHangTV.Name = "lblHangTV";
            this.lblHangTV.Size = new System.Drawing.Size(48, 23);
            this.lblHangTV.TabIndex = 15;
            this.lblHangTV.Text = "Loài:";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSDT.Location = new System.Drawing.Point(628, 111);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(125, 23);
            this.lblSDT.TabIndex = 14;
            this.lblSDT.Text = "Tên Thú Cưng:";
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoTen.Location = new System.Drawing.Point(628, 68);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(123, 23);
            this.lblHoTen.TabIndex = 13;
            this.lblHoTen.Text = "Mã Thú Cưng:";
            // 
            // lblMaKH
            // 
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaKH.Location = new System.Drawing.Point(628, 29);
            this.lblMaKH.Name = "lblMaKH";
            this.lblMaKH.Size = new System.Drawing.Size(229, 23);
            this.lblMaKH.TabIndex = 12;
            this.lblMaKH.Text = "Mã Khách Hàng (Chủ nuôi):";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnLuu);
            this.panel2.Controls.Add(this.btnThem);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 474);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1920, 46);
            this.panel2.TabIndex = 4;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.LimeGreen;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(1280, 6);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(101, 28);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(468, 6);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(101, 28);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvThuCung);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 520);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1920, 560);
            this.panel4.TabIndex = 7;
            // 
            // dgvThuCung
            // 
            this.dgvThuCung.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvThuCung.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThuCung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThuCung.Location = new System.Drawing.Point(0, 0);
            this.dgvThuCung.Name = "dgvThuCung";
            this.dgvThuCung.ReadOnly = true;
            this.dgvThuCung.RowHeadersWidth = 51;
            this.dgvThuCung.RowTemplate.Height = 24;
            this.dgvThuCung.Size = new System.Drawing.Size(1920, 560);
            this.dgvThuCung.TabIndex = 7;
            // 
            // txtLoai
            // 
            this.txtLoai.Location = new System.Drawing.Point(942, 151);
            this.txtLoai.Name = "txtLoai";
            this.txtLoai.Size = new System.Drawing.Size(244, 30);
            this.txtLoai.TabIndex = 28;
            // 
            // ucQuanLyHoSoThuCung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.grbChiTiet);
            this.Controls.Add(this.label1);
            this.Name = "ucQuanLyHoSoThuCung";
            this.Padding = new System.Windows.Forms.Padding(0, 50, 0, 0);
            this.Size = new System.Drawing.Size(1920, 1080);
            this.grbChiTiet.ResumeLayout(false);
            this.grbChiTiet.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThuCung)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbChiTiet;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.TextBox txtGiong;
        private System.Windows.Forms.TextBox txtTenThu;
        private System.Windows.Forms.TextBox txtMaThuCung;
        private System.Windows.Forms.TextBox txtMaKH;
        private System.Windows.Forms.Label lblNgaySinhKH;
        private System.Windows.Forms.Label lblGioiTinhKH;
        private System.Windows.Forms.Label lblCCCD;
        private System.Windows.Forms.Label lblHangTV;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.Label lblMaKH;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgvThuCung;
        private System.Windows.Forms.TextBox txtTinhTrang;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLoai;
    }
}
