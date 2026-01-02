namespace PetCareX
{
    partial class ucConBaoCaoKhachHang
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
            this.label2 = new System.Windows.Forms.Label();
            this.cboLoaiKH = new System.Windows.Forms.ComboBox();
            this.btnXemBaoCao = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvBaoCao = new System.Windows.Forms.DataGridView();
            this.pnlLienLac = new System.Windows.Forms.Panel();
            this.btnGuiSMS = new System.Windows.Forms.Button();
            this.btnGuiEmail = new System.Windows.Forms.Button();
            this.rtxtNoiDung = new System.Windows.Forms.RichTextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).BeginInit();
            this.pnlLienLac.SuspendLayout();
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
            this.label1.TabIndex = 2;
            this.label1.Text = "Báo Cáo Khách Hàng";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(656, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Chọn Loại Khách Hàng: ";
            // 
            // cboLoaiKH
            // 
            this.cboLoaiKH.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiKH.FormattingEnabled = true;
            this.cboLoaiKH.Items.AddRange(new object[] {
            "Mới",
            "Cũ"});
            this.cboLoaiKH.Location = new System.Drawing.Point(1162, 18);
            this.cboLoaiKH.Name = "cboLoaiKH";
            this.cboLoaiKH.Size = new System.Drawing.Size(148, 25);
            this.cboLoaiKH.TabIndex = 4;
            // 
            // btnXemBaoCao
            // 
            this.btnXemBaoCao.BackColor = System.Drawing.Color.LimeGreen;
            this.btnXemBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemBaoCao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemBaoCao.Location = new System.Drawing.Point(915, 66);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.Size = new System.Drawing.Size(162, 28);
            this.btnXemBaoCao.TabIndex = 5;
            this.btnXemBaoCao.Text = "Xem Báo Cáo";
            this.btnXemBaoCao.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnXemBaoCao);
            this.panel1.Controls.Add(this.cboLoaiKH);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 100);
            this.panel1.TabIndex = 6;
            // 
            // dgvBaoCao
            // 
            this.dgvBaoCao.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvBaoCao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoCao.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvBaoCao.Location = new System.Drawing.Point(0, 186);
            this.dgvBaoCao.Name = "dgvBaoCao";
            this.dgvBaoCao.ReadOnly = true;
            this.dgvBaoCao.RowHeadersWidth = 51;
            this.dgvBaoCao.RowTemplate.Height = 24;
            this.dgvBaoCao.Size = new System.Drawing.Size(1920, 350);
            this.dgvBaoCao.TabIndex = 7;
            // 
            // pnlLienLac
            // 
            this.pnlLienLac.BackColor = System.Drawing.SystemColors.Control;
            this.pnlLienLac.Controls.Add(this.btnGuiSMS);
            this.pnlLienLac.Controls.Add(this.btnGuiEmail);
            this.pnlLienLac.Controls.Add(this.rtxtNoiDung);
            this.pnlLienLac.Controls.Add(this.txtEmail);
            this.pnlLienLac.Controls.Add(this.txtSDT);
            this.pnlLienLac.Controls.Add(this.txtHoTen);
            this.pnlLienLac.Controls.Add(this.label6);
            this.pnlLienLac.Controls.Add(this.label5);
            this.pnlLienLac.Controls.Add(this.label4);
            this.pnlLienLac.Controls.Add(this.label3);
            this.pnlLienLac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLienLac.Location = new System.Drawing.Point(0, 536);
            this.pnlLienLac.Name = "pnlLienLac";
            this.pnlLienLac.Size = new System.Drawing.Size(1920, 544);
            this.pnlLienLac.TabIndex = 8;
            // 
            // btnGuiSMS
            // 
            this.btnGuiSMS.BackColor = System.Drawing.Color.Aqua;
            this.btnGuiSMS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuiSMS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuiSMS.Location = new System.Drawing.Point(1408, 215);
            this.btnGuiSMS.Name = "btnGuiSMS";
            this.btnGuiSMS.Size = new System.Drawing.Size(162, 28);
            this.btnGuiSMS.TabIndex = 12;
            this.btnGuiSMS.Text = "Gửi SMS";
            this.btnGuiSMS.UseVisualStyleBackColor = false;
            // 
            // btnGuiEmail
            // 
            this.btnGuiEmail.BackColor = System.Drawing.Color.Yellow;
            this.btnGuiEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuiEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuiEmail.Location = new System.Drawing.Point(527, 215);
            this.btnGuiEmail.Name = "btnGuiEmail";
            this.btnGuiEmail.Size = new System.Drawing.Size(162, 28);
            this.btnGuiEmail.TabIndex = 11;
            this.btnGuiEmail.Text = "Gửi Email";
            this.btnGuiEmail.UseVisualStyleBackColor = false;
            // 
            // rtxtNoiDung
            // 
            this.rtxtNoiDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtNoiDung.ForeColor = System.Drawing.Color.Gray;
            this.rtxtNoiDung.Location = new System.Drawing.Point(713, 94);
            this.rtxtNoiDung.Name = "rtxtNoiDung";
            this.rtxtNoiDung.Size = new System.Drawing.Size(675, 96);
            this.rtxtNoiDung.TabIndex = 10;
            this.rtxtNoiDung.Text = "Viết tin nhắn...";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(1515, 47);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(338, 22);
            this.txtEmail.TabIndex = 9;
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(987, 47);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(338, 22);
            this.txtSDT.TabIndex = 8;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(418, 45);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(338, 22);
            this.txtHoTen.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1385, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 23);
            this.label6.TabIndex = 6;
            this.label6.Text = "Email:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(827, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 23);
            this.label5.TabIndex = 5;
            this.label5.Text = "Số Điện Thoại:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(290, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "Họ Và Tên:";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1920, 36);
            this.label3.TabIndex = 3;
            this.label3.Text = "Liên Lạc Khách Hàng";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucConBaoCaoKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLienLac);
            this.Controls.Add(this.dgvBaoCao);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "ucConBaoCaoKhachHang";
            this.Padding = new System.Windows.Forms.Padding(0, 50, 0, 0);
            this.Size = new System.Drawing.Size(1920, 1080);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).EndInit();
            this.pnlLienLac.ResumeLayout(false);
            this.pnlLienLac.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboLoaiKH;
        private System.Windows.Forms.Button btnXemBaoCao;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvBaoCao;
        private System.Windows.Forms.Panel pnlLienLac;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtxtNoiDung;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGuiSMS;
        private System.Windows.Forms.Button btnGuiEmail;
    }
}
