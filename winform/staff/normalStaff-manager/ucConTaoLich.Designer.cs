namespace PetCareX
{
    partial class ucConTaoLich
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
            this.pnlTaoLichContainer = new System.Windows.Forms.Panel();
            this.btnTaoLich = new System.Windows.Forms.Button();
            this.dtpNgayHen = new System.Windows.Forms.DateTimePicker();
            this.cboKhungGio = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboChiNhanh = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDichVu = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaThucung = new System.Windows.Forms.TextBox();
            this.pnlTaoLichContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTaoLichContainer
            // 
            this.pnlTaoLichContainer.BackColor = System.Drawing.Color.SkyBlue;
            this.pnlTaoLichContainer.Controls.Add(this.btnTaoLich);
            this.pnlTaoLichContainer.Controls.Add(this.dtpNgayHen);
            this.pnlTaoLichContainer.Controls.Add(this.cboKhungGio);
            this.pnlTaoLichContainer.Controls.Add(this.label6);
            this.pnlTaoLichContainer.Controls.Add(this.label5);
            this.pnlTaoLichContainer.Controls.Add(this.cboChiNhanh);
            this.pnlTaoLichContainer.Controls.Add(this.label4);
            this.pnlTaoLichContainer.Controls.Add(this.cboDichVu);
            this.pnlTaoLichContainer.Controls.Add(this.label2);
            this.pnlTaoLichContainer.Controls.Add(this.label3);
            this.pnlTaoLichContainer.Controls.Add(this.label1);
            this.pnlTaoLichContainer.Controls.Add(this.txtMaThucung);
            this.pnlTaoLichContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTaoLichContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlTaoLichContainer.Name = "pnlTaoLichContainer";
            this.pnlTaoLichContainer.Padding = new System.Windows.Forms.Padding(20);
            this.pnlTaoLichContainer.Size = new System.Drawing.Size(1920, 1080);
            this.pnlTaoLichContainer.TabIndex = 0;
            // 
            // btnTaoLich
            // 
            this.btnTaoLich.BackColor = System.Drawing.Color.LimeGreen;
            this.btnTaoLich.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoLich.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoLich.Location = new System.Drawing.Point(1197, 426);
            this.btnTaoLich.Name = "btnTaoLich";
            this.btnTaoLich.Size = new System.Drawing.Size(115, 39);
            this.btnTaoLich.TabIndex = 12;
            this.btnTaoLich.Text = "Đăng Ký";
            this.btnTaoLich.UseVisualStyleBackColor = false;
            this.btnTaoLich.Click += new System.EventHandler(this.btnTaoLich_Click);
            // 
            // dtpNgayHen
            // 
            this.dtpNgayHen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHen.Location = new System.Drawing.Point(984, 305);
            this.dtpNgayHen.Name = "dtpNgayHen";
            this.dtpNgayHen.Size = new System.Drawing.Size(121, 22);
            this.dtpNgayHen.TabIndex = 11;
            // 
            // cboKhungGio
            // 
            this.cboKhungGio.FormattingEnabled = true;
            this.cboKhungGio.Items.AddRange(new object[] {
            "08:30 - 09:00",
            "09:00 - 09:30",
            "09:30 - 10:00",
            "14:00 - 14:30",
            "14:30 - 15:00",
            "15:00 - 15:30",
            "15:30 - 16:00"});
            this.cboKhungGio.Location = new System.Drawing.Point(984, 352);
            this.cboKhungGio.Name = "cboKhungGio";
            this.cboKhungGio.Size = new System.Drawing.Size(121, 24);
            this.cboKhungGio.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(760, 348);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 28);
            this.label6.TabIndex = 9;
            this.label6.Text = "Khung Giờ:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(760, 299);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 28);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ngày Khám:";
            // 
            // cboChiNhanh
            // 
            this.cboChiNhanh.FormattingEnabled = true;
            this.cboChiNhanh.Items.AddRange(new object[] {
            "Chi nhánh 1",
            "Chi nhánh 2",
            "Chi nhánh 3"});
            this.cboChiNhanh.Location = new System.Drawing.Point(984, 260);
            this.cboChiNhanh.Name = "cboChiNhanh";
            this.cboChiNhanh.Size = new System.Drawing.Size(121, 24);
            this.cboChiNhanh.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(760, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 28);
            this.label4.TabIndex = 6;
            this.label4.Text = "Chi Nhánh:";
            // 
            // cboDichVu
            // 
            this.cboDichVu.FormattingEnabled = true;
            this.cboDichVu.Items.AddRange(new object[] {
            "Khám Bệnh",
            "Tiêm Phòng"});
            this.cboDichVu.Location = new System.Drawing.Point(984, 218);
            this.cboDichVu.Name = "cboDichVu";
            this.cboDichVu.Size = new System.Drawing.Size(121, 24);
            this.cboDichVu.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(760, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "Loại Dịch Vụ:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(872, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(252, 31);
            this.label3.TabIndex = 3;
            this.label3.Text = "Đăng Ký Lịch Hẹn Mới";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(760, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nhập Mã Thú Cưng:";
            // 
            // txtMaThucung
            // 
            this.txtMaThucung.Location = new System.Drawing.Point(984, 178);
            this.txtMaThucung.Name = "txtMaThucung";
            this.txtMaThucung.Size = new System.Drawing.Size(246, 22);
            this.txtMaThucung.TabIndex = 0;
            // 
            // ucConTaoLich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTaoLichContainer);
            this.Name = "ucConTaoLich";
            this.Size = new System.Drawing.Size(1920, 1080);
            this.pnlTaoLichContainer.ResumeLayout(false);
            this.pnlTaoLichContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTaoLichContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaThucung;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboDichVu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboChiNhanh;
        private System.Windows.Forms.DateTimePicker dtpNgayHen;
        private System.Windows.Forms.ComboBox cboKhungGio;
        private System.Windows.Forms.Button btnTaoLich;
    }
}
