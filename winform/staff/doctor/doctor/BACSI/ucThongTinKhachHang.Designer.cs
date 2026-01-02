namespace BACSI
{
    partial class ucThongTinKhachHang
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
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.grBOutput = new System.Windows.Forms.GroupBox();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.grbInput = new System.Windows.Forms.GroupBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.lblNhapMa = new System.Windows.Forms.Label();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.grBOutput.SuspendLayout();
            this.grbInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.AutoSize = true;
            this.lblMainTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainTitle.ForeColor = System.Drawing.Color.Red;
            this.lblMainTitle.Location = new System.Drawing.Point(401, 57);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(712, 54);
            this.lblMainTitle.TabIndex = 11;
            this.lblMainTitle.Text = "TRA CỨU THÔNG TIN KHÁCH HÀNG";
            this.lblMainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(0, 0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(108, 41);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "Quay Lại";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // grBOutput
            // 
            this.grBOutput.BackColor = System.Drawing.Color.White;
            this.grBOutput.Controls.Add(this.lblGioiTinh);
            this.grBOutput.Controls.Add(this.lblEmail);
            this.grBOutput.Controls.Add(this.lblNgaySinh);
            this.grBOutput.Controls.Add(this.lblSDT);
            this.grBOutput.Controls.Add(this.lblTenKH);
            this.grBOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grBOutput.Location = new System.Drawing.Point(557, 147);
            this.grBOutput.Name = "grBOutput";
            this.grBOutput.Size = new System.Drawing.Size(800, 400);
            this.grBOutput.TabIndex = 13;
            this.grBOutput.TabStop = false;
            this.grBOutput.Text = "THÔNG TIN KHÁCH HÀNG";
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblGioiTinh.Location = new System.Drawing.Point(476, 70);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(112, 25);
            this.lblGioiTinh.TabIndex = 4;
            this.lblGioiTinh.Text = "Giới Tính: ";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblEmail.Location = new System.Drawing.Point(10, 280);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(78, 25);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email: ";
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblNgaySinh.Location = new System.Drawing.Point(10, 210);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(119, 25);
            this.lblNgaySinh.TabIndex = 2;
            this.lblNgaySinh.Text = "Ngày Sinh:";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblSDT.Location = new System.Drawing.Point(10, 140);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(163, 25);
            this.lblSDT.TabIndex = 1;
            this.lblSDT.Text = "Số Điện Thoại: ";
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTenKH.Location = new System.Drawing.Point(10, 70);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(185, 25);
            this.lblTenKH.TabIndex = 0;
            this.lblTenKH.Text = "Tên Khách hàng: ";
            // 
            // grbInput
            // 
            this.grbInput.BackColor = System.Drawing.Color.White;
            this.grbInput.Controls.Add(this.btnTimKiem);
            this.grbInput.Controls.Add(this.lblNhapMa);
            this.grbInput.Controls.Add(this.txtMaKH);
            this.grbInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbInput.Location = new System.Drawing.Point(47, 147);
            this.grbInput.Name = "grbInput";
            this.grbInput.Size = new System.Drawing.Size(463, 400);
            this.grbInput.TabIndex = 12;
            this.grbInput.TabStop = false;
            this.grbInput.Text = "NHẬP MÃ KHÁCH HÀNG CẦN TÌM";
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.Location = new System.Drawing.Point(145, 246);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(160, 50);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // lblNhapMa
            // 
            this.lblNhapMa.AutoSize = true;
            this.lblNhapMa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNhapMa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblNhapMa.Location = new System.Drawing.Point(6, 57);
            this.lblNhapMa.Name = "lblNhapMa";
            this.lblNhapMa.Size = new System.Drawing.Size(273, 31);
            this.lblNhapMa.TabIndex = 0;
            this.lblNhapMa.Text = "Nhập Mã Khách Hàng:";
            // 
            // txtMaKH
            // 
            this.txtMaKH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtMaKH.Location = new System.Drawing.Point(29, 117);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(350, 34);
            this.txtMaKH.TabIndex = 1;
            // 
            // ucThongTinKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.lblMainTitle);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.grBOutput);
            this.Controls.Add(this.grbInput);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucThongTinKhachHang";
            this.Size = new System.Drawing.Size(1400, 600);
            this.grBOutput.ResumeLayout(false);
            this.grBOutput.PerformLayout();
            this.grbInput.ResumeLayout(false);
            this.grbInput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMainTitle;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.GroupBox grBOutput;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.GroupBox grbInput;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Label lblNhapMa;
        private System.Windows.Forms.TextBox txtMaKH;
    }
}
