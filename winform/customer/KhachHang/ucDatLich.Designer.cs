namespace KhachHang
{
    partial class ucDatLich
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblCN = new System.Windows.Forms.Label();
            this.lblNgay = new System.Windows.Forms.Label();
            this.lblMTC = new System.Windows.Forms.Label();
            this.cboChiNhanh = new System.Windows.Forms.ComboBox();
            this.txtMaThuCung = new System.Windows.Forms.TextBox();
            this.dtpNgayHen = new System.Windows.Forms.DateTimePicker();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.lblKhungGio = new System.Windows.Forms.Label();
            this.cboKhungGio = new System.Windows.Forms.ComboBox();
            this.lblBacSi = new System.Windows.Forms.Label();
            this.txtTenBacSi = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblTieuDe.Location = new System.Drawing.Point(346, 26);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(128, 39);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "TieuDe";
            // 
            // lblCN
            // 
            this.lblCN.AutoSize = true;
            this.lblCN.Location = new System.Drawing.Point(79, 108);
            this.lblCN.Name = "lblCN";
            this.lblCN.Size = new System.Drawing.Size(128, 25);
            this.lblCN.TabIndex = 1;
            this.lblCN.Text = "- Chi Nhánh: ";
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location = new System.Drawing.Point(79, 244);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(122, 25);
            this.lblNgay.TabIndex = 2;
            this.lblNgay.Text = "- Ngày Hẹn: ";
            this.lblNgay.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblMTC
            // 
            this.lblMTC.AutoSize = true;
            this.lblMTC.Location = new System.Drawing.Point(79, 177);
            this.lblMTC.Name = "lblMTC";
            this.lblMTC.Size = new System.Drawing.Size(156, 25);
            this.lblMTC.TabIndex = 3;
            this.lblMTC.Text = "- Mã Thú Cưng: ";
            this.lblMTC.Click += new System.EventHandler(this.lblMTC_Click);
            // 
            // cboChiNhanh
            // 
            this.cboChiNhanh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChiNhanh.FormattingEnabled = true;
            this.cboChiNhanh.Items.AddRange(new object[] {
            "Chi Nhánh 1",
            "Chi Nhánh 2",
            "Chi Nhánh 3",
            "Chi Nhánh 4",
            "Chi Nhánh 5",
            "Chi Nhánh 6",
            "Chi Nhánh 7"});
            this.cboChiNhanh.Location = new System.Drawing.Point(353, 108);
            this.cboChiNhanh.Name = "cboChiNhanh";
            this.cboChiNhanh.Size = new System.Drawing.Size(531, 33);
            this.cboChiNhanh.TabIndex = 4;
            // 
            // txtMaThuCung
            // 
            this.txtMaThuCung.Location = new System.Drawing.Point(353, 174);
            this.txtMaThuCung.Name = "txtMaThuCung";
            this.txtMaThuCung.Size = new System.Drawing.Size(531, 30);
            this.txtMaThuCung.TabIndex = 5;
            // 
            // dtpNgayHen
            // 
            this.dtpNgayHen.Location = new System.Drawing.Point(353, 239);
            this.dtpNgayHen.Name = "dtpNgayHen";
            this.dtpNgayHen.Size = new System.Drawing.Size(531, 30);
            this.dtpNgayHen.TabIndex = 6;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnXacNhan.Location = new System.Drawing.Point(353, 461);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(175, 51);
            this.btnXacNhan.TabIndex = 7;
            this.btnXacNhan.Text = "Xác Nhận";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // lblKhungGio
            // 
            this.lblKhungGio.AutoSize = true;
            this.lblKhungGio.Location = new System.Drawing.Point(79, 309);
            this.lblKhungGio.Name = "lblKhungGio";
            this.lblKhungGio.Size = new System.Drawing.Size(123, 25);
            this.lblKhungGio.TabIndex = 8;
            this.lblKhungGio.Text = "- Khung Giờ:";
            // 
            // cboKhungGio
            // 
            this.cboKhungGio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhungGio.FormattingEnabled = true;
            this.cboKhungGio.Items.AddRange(new object[] {
            "8:00 - 9:00",
            "9:00 - 10:00",
            "10:00 - 11:00"});
            this.cboKhungGio.Location = new System.Drawing.Point(353, 306);
            this.cboKhungGio.Name = "cboKhungGio";
            this.cboKhungGio.Size = new System.Drawing.Size(531, 33);
            this.cboKhungGio.TabIndex = 9;
            this.cboKhungGio.SelectedIndexChanged += new System.EventHandler(this.cboKhungGio_SelectedIndexChanged);
            // 
            // lblBacSi
            // 
            this.lblBacSi.AutoSize = true;
            this.lblBacSi.Location = new System.Drawing.Point(79, 380);
            this.lblBacSi.Name = "lblBacSi";
            this.lblBacSi.Size = new System.Drawing.Size(125, 25);
            this.lblBacSi.TabIndex = 10;
            this.lblBacSi.Text = "- Bác sĩ trực: ";
            // 
            // txtTenBacSi
            // 
            this.txtTenBacSi.Location = new System.Drawing.Point(353, 377);
            this.txtTenBacSi.Name = "txtTenBacSi";
            this.txtTenBacSi.ReadOnly = true;
            this.txtTenBacSi.Size = new System.Drawing.Size(531, 30);
            this.txtTenBacSi.TabIndex = 11;
            // 
            // ucDatLich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.txtTenBacSi);
            this.Controls.Add(this.lblBacSi);
            this.Controls.Add(this.cboKhungGio);
            this.Controls.Add(this.lblKhungGio);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.dtpNgayHen);
            this.Controls.Add(this.txtMaThuCung);
            this.Controls.Add(this.cboChiNhanh);
            this.Controls.Add(this.lblMTC);
            this.Controls.Add(this.lblNgay);
            this.Controls.Add(this.lblCN);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucDatLich";
            this.Size = new System.Drawing.Size(1210, 660);
            this.Load += new System.EventHandler(this.ucDatLich_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblCN;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.Label lblMTC;
        private System.Windows.Forms.ComboBox cboChiNhanh;
        private System.Windows.Forms.TextBox txtMaThuCung;
        private System.Windows.Forms.DateTimePicker dtpNgayHen;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Label lblKhungGio;
        private System.Windows.Forms.ComboBox cboKhungGio;
        private System.Windows.Forms.Label lblBacSi;
        private System.Windows.Forms.TextBox txtTenBacSi;
    }
}
