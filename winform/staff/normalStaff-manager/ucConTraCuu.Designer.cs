namespace PetCareX
{
    partial class ucConTraCuu
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
            this.pnlProfile = new System.Windows.Forms.Panel();
            this.pnlKH = new System.Windows.Forms.Panel();
            this.pnlPet = new System.Windows.Forms.Panel();
            this.lblHangTV = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.lblMaKH = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMaPet = new System.Windows.Forms.Label();
            this.lblTenPet = new System.Windows.Forms.Label();
            this.lblLoai = new System.Windows.Forms.Label();
            this.lblNgaySinhPet = new System.Windows.Forms.Label();
            this.lblGiong = new System.Windows.Forms.Label();
            this.lblSucKhoe = new System.Windows.Forms.Label();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.lblGioiTinhKH = new System.Windows.Forms.Label();
            this.lblNgaySinhKH = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlHistory = new System.Windows.Forms.Panel();
            this.lblGioiTinhPet = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvKham = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvTiem = new System.Windows.Forms.DataGridView();
            this.pnlProfile.SuspendLayout();
            this.pnlKH.SuspendLayout();
            this.pnlPet.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiem)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlProfile
            // 
            this.pnlProfile.Controls.Add(this.pnlHistory);
            this.pnlProfile.Controls.Add(this.pnlPet);
            this.pnlProfile.Controls.Add(this.pnlKH);
            this.pnlProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProfile.Location = new System.Drawing.Point(0, 81);
            this.pnlProfile.Name = "pnlProfile";
            this.pnlProfile.Size = new System.Drawing.Size(1920, 999);
            this.pnlProfile.TabIndex = 2;
            this.pnlProfile.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // pnlKH
            // 
            this.pnlKH.BackColor = System.Drawing.Color.Silver;
            this.pnlKH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKH.Controls.Add(this.lblNgaySinhKH);
            this.pnlKH.Controls.Add(this.lblGioiTinhKH);
            this.pnlKH.Controls.Add(this.lblCCCD);
            this.pnlKH.Controls.Add(this.label5);
            this.pnlKH.Controls.Add(this.lblHangTV);
            this.pnlKH.Controls.Add(this.lblSDT);
            this.pnlKH.Controls.Add(this.lblHoTen);
            this.pnlKH.Controls.Add(this.lblMaKH);
            this.pnlKH.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlKH.Location = new System.Drawing.Point(0, 0);
            this.pnlKH.Name = "pnlKH";
            this.pnlKH.Size = new System.Drawing.Size(582, 999);
            this.pnlKH.TabIndex = 0;
            this.pnlKH.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlKH_Paint);
            // 
            // pnlPet
            // 
            this.pnlPet.BackColor = System.Drawing.Color.Silver;
            this.pnlPet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPet.Controls.Add(this.lblGioiTinhPet);
            this.pnlPet.Controls.Add(this.lblSucKhoe);
            this.pnlPet.Controls.Add(this.lblGiong);
            this.pnlPet.Controls.Add(this.lblNgaySinhPet);
            this.pnlPet.Controls.Add(this.lblLoai);
            this.pnlPet.Controls.Add(this.lblTenPet);
            this.pnlPet.Controls.Add(this.lblMaPet);
            this.pnlPet.Controls.Add(this.label6);
            this.pnlPet.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPet.Location = new System.Drawing.Point(582, 0);
            this.pnlPet.Name = "pnlPet";
            this.pnlPet.Size = new System.Drawing.Size(609, 999);
            this.pnlPet.TabIndex = 1;
            this.pnlPet.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint_1);
            // 
            // lblHangTV
            // 
            this.lblHangTV.AutoSize = true;
            this.lblHangTV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHangTV.Location = new System.Drawing.Point(15, 218);
            this.lblHangTV.Name = "lblHangTV";
            this.lblHangTV.Size = new System.Drawing.Size(84, 23);
            this.lblHangTV.TabIndex = 7;
            this.lblHangTV.Text = "Hạng TV:";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSDT.Location = new System.Drawing.Point(15, 178);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(126, 23);
            this.lblSDT.TabIndex = 6;
            this.lblSDT.Text = "Số Điện Thoại:";
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoTen.Location = new System.Drawing.Point(15, 141);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(75, 23);
            this.lblHoTen.TabIndex = 5;
            this.lblHoTen.Text = "Họ Tên: ";
            // 
            // lblMaKH
            // 
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaKH.Location = new System.Drawing.Point(15, 102);
            this.lblMaKH.Name = "lblMaKH";
            this.lblMaKH.Size = new System.Drawing.Size(141, 23);
            this.lblMaKH.TabIndex = 4;
            this.lblMaKH.Text = "Mã Khách Hàng:";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Cornsilk;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(580, 37);
            this.label5.TabIndex = 8;
            this.label5.Text = "Thông Tin Khách Hàng";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Cornsilk;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(607, 37);
            this.label6.TabIndex = 9;
            this.label6.Text = "Thông Tin Thú Cưng";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // lblMaPet
            // 
            this.lblMaPet.AutoSize = true;
            this.lblMaPet.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaPet.Location = new System.Drawing.Point(50, 102);
            this.lblMaPet.Name = "lblMaPet";
            this.lblMaPet.Size = new System.Drawing.Size(123, 23);
            this.lblMaPet.TabIndex = 9;
            this.lblMaPet.Text = "Mã Thú Cưng:";
            // 
            // lblTenPet
            // 
            this.lblTenPet.AutoSize = true;
            this.lblTenPet.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenPet.Location = new System.Drawing.Point(50, 141);
            this.lblTenPet.Name = "lblTenPet";
            this.lblTenPet.Size = new System.Drawing.Size(42, 23);
            this.lblTenPet.TabIndex = 10;
            this.lblTenPet.Text = "Tên:";
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoai.Location = new System.Drawing.Point(50, 178);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(48, 23);
            this.lblLoai.TabIndex = 11;
            this.lblLoai.Text = "Loài:";
            // 
            // lblNgaySinhPet
            // 
            this.lblNgaySinhPet.AutoSize = true;
            this.lblNgaySinhPet.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgaySinhPet.Location = new System.Drawing.Point(50, 218);
            this.lblNgaySinhPet.Name = "lblNgaySinhPet";
            this.lblNgaySinhPet.Size = new System.Drawing.Size(94, 23);
            this.lblNgaySinhPet.TabIndex = 12;
            this.lblNgaySinhPet.Text = "Ngày sinh:";
            // 
            // lblGiong
            // 
            this.lblGiong.AutoSize = true;
            this.lblGiong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiong.Location = new System.Drawing.Point(50, 257);
            this.lblGiong.Name = "lblGiong";
            this.lblGiong.Size = new System.Drawing.Size(63, 23);
            this.lblGiong.TabIndex = 13;
            this.lblGiong.Text = "Giống:";
            // 
            // lblSucKhoe
            // 
            this.lblSucKhoe.AutoSize = true;
            this.lblSucKhoe.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucKhoe.Location = new System.Drawing.Point(50, 333);
            this.lblSucKhoe.Name = "lblSucKhoe";
            this.lblSucKhoe.Size = new System.Drawing.Size(180, 23);
            this.lblSucKhoe.TabIndex = 14;
            this.lblSucKhoe.Text = "Tình Trạng Sức Khỏe:";
            // 
            // lblCCCD
            // 
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCCCD.Location = new System.Drawing.Point(15, 257);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(59, 23);
            this.lblCCCD.TabIndex = 9;
            this.lblCCCD.Text = "CCCD:";
            // 
            // lblGioiTinhKH
            // 
            this.lblGioiTinhKH.AutoSize = true;
            this.lblGioiTinhKH.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGioiTinhKH.Location = new System.Drawing.Point(15, 297);
            this.lblGioiTinhKH.Name = "lblGioiTinhKH";
            this.lblGioiTinhKH.Size = new System.Drawing.Size(88, 23);
            this.lblGioiTinhKH.TabIndex = 10;
            this.lblGioiTinhKH.Text = "Giới Tính:";
            // 
            // lblNgaySinhKH
            // 
            this.lblNgaySinhKH.AutoSize = true;
            this.lblNgaySinhKH.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgaySinhKH.Location = new System.Drawing.Point(15, 333);
            this.lblNgaySinhKH.Name = "lblNgaySinhKH";
            this.lblNgaySinhKH.Size = new System.Drawing.Size(97, 23);
            this.lblNgaySinhKH.TabIndex = 11;
            this.lblNgaySinhKH.Text = "Ngày Sinh:";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(6, 43);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(312, 23);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Nhập Mã Khách hàng/ Mã Thú Cưng: ";
            this.lblSearch.Click += new System.EventHandler(this.lblSearch_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(371, 33);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(280, 34);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Location = new System.Drawing.Point(682, 34);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(148, 32);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.btnTimKiem);
            this.groupBox1.Controls.Add(this.txtTimKiem);
            this.groupBox1.Controls.Add(this.lblSearch);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1920, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tra cứu thông tin thú cưng";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // pnlHistory
            // 
            this.pnlHistory.BackColor = System.Drawing.Color.Silver;
            this.pnlHistory.Controls.Add(this.dgvTiem);
            this.pnlHistory.Controls.Add(this.label2);
            this.pnlHistory.Controls.Add(this.dgvKham);
            this.pnlHistory.Controls.Add(this.label1);
            this.pnlHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistory.Location = new System.Drawing.Point(1191, 0);
            this.pnlHistory.Name = "pnlHistory";
            this.pnlHistory.Size = new System.Drawing.Size(729, 999);
            this.pnlHistory.TabIndex = 16;
            // 
            // lblGioiTinhPet
            // 
            this.lblGioiTinhPet.AutoSize = true;
            this.lblGioiTinhPet.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGioiTinhPet.Location = new System.Drawing.Point(50, 297);
            this.lblGioiTinhPet.Name = "lblGioiTinhPet";
            this.lblGioiTinhPet.Size = new System.Drawing.Size(85, 23);
            this.lblGioiTinhPet.TabIndex = 15;
            this.lblGioiTinhPet.Text = "Giới tính:";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Cornsilk;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(729, 38);
            this.label1.TabIndex = 17;
            this.label1.Text = "Lịch Sử Khám Bệnh";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvKham
            // 
            this.dgvKham.BackgroundColor = System.Drawing.Color.Silver;
            this.dgvKham.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKham.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvKham.GridColor = System.Drawing.Color.Silver;
            this.dgvKham.Location = new System.Drawing.Point(0, 38);
            this.dgvKham.Name = "dgvKham";
            this.dgvKham.RowHeadersWidth = 51;
            this.dgvKham.RowTemplate.Height = 24;
            this.dgvKham.Size = new System.Drawing.Size(729, 416);
            this.dgvKham.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Cornsilk;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(0, 454);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(729, 28);
            this.label2.TabIndex = 19;
            this.label2.Text = "Lịch Sử Tiêm Phòng";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvTiem
            // 
            this.dgvTiem.BackgroundColor = System.Drawing.Color.Silver;
            this.dgvTiem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTiem.GridColor = System.Drawing.Color.Silver;
            this.dgvTiem.Location = new System.Drawing.Point(0, 482);
            this.dgvTiem.Name = "dgvTiem";
            this.dgvTiem.RowHeadersWidth = 51;
            this.dgvTiem.RowTemplate.Height = 24;
            this.dgvTiem.Size = new System.Drawing.Size(729, 517);
            this.dgvTiem.TabIndex = 20;
            // 
            // ucConTraCuu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlProfile);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucConTraCuu";
            this.Size = new System.Drawing.Size(1920, 1080);
            this.Load += new System.EventHandler(this.ucConTraCuu_Load);
            this.pnlProfile.ResumeLayout(false);
            this.pnlKH.ResumeLayout(false);
            this.pnlKH.PerformLayout();
            this.pnlPet.ResumeLayout(false);
            this.pnlPet.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlProfile;
        private System.Windows.Forms.Panel pnlPet;
        private System.Windows.Forms.Panel pnlKH;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblHangTV;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.Label lblMaKH;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblNgaySinhPet;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.Label lblTenPet;
        private System.Windows.Forms.Label lblMaPet;
        private System.Windows.Forms.Label lblSucKhoe;
        private System.Windows.Forms.Label lblGiong;
        private System.Windows.Forms.Label lblNgaySinhKH;
        private System.Windows.Forms.Label lblGioiTinhKH;
        private System.Windows.Forms.Label lblCCCD;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnlHistory;
        private System.Windows.Forms.Label lblGioiTinhPet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTiem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvKham;
    }
}
