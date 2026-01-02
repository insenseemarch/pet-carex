namespace BACSI
{
    partial class ucTraCuuThuoc
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
            this.btnBack = new System.Windows.Forms.Button();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.lblNhapMa = new System.Windows.Forms.Label();
            this.txtMaThuoc = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.grbInput = new System.Windows.Forms.GroupBox();
            this.grBOutput = new System.Windows.Forms.GroupBox();
            this.lblTenThuoc = new System.Windows.Forms.Label();
            this.lblLoaiThuoc = new System.Windows.Forms.Label();
            this.lblSoLuongTon = new System.Windows.Forms.Label();
            this.lblGia = new System.Windows.Forms.Label();
            this.grbInput.SuspendLayout();
            this.grBOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(108, 41);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Quay Lại";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click_1);
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.AutoSize = true;
            this.lblMainTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainTitle.ForeColor = System.Drawing.Color.Red;
            this.lblMainTitle.Location = new System.Drawing.Point(410, 60);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(579, 54);
            this.lblMainTitle.TabIndex = 1;
            this.lblMainTitle.Text = "TRA CỨU THÔNG TIN THUỐC";
            this.lblMainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMainTitle.Click += new System.EventHandler(this.lblMainTitle_Click);
            // 
            // lblNhapMa
            // 
            this.lblNhapMa.AutoSize = true;
            this.lblNhapMa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNhapMa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblNhapMa.Location = new System.Drawing.Point(6, 57);
            this.lblNhapMa.Name = "lblNhapMa";
            this.lblNhapMa.Size = new System.Drawing.Size(206, 31);
            this.lblNhapMa.TabIndex = 0;
            this.lblNhapMa.Text = "Nhập Mã Thuốc:";
            // 
            // txtMaThuoc
            // 
            this.txtMaThuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaThuoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtMaThuoc.Location = new System.Drawing.Point(29, 117);
            this.txtMaThuoc.Name = "txtMaThuoc";
            this.txtMaThuoc.Size = new System.Drawing.Size(350, 34);
            this.txtMaThuoc.TabIndex = 1;
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
            // grbInput
            // 
            this.grbInput.BackColor = System.Drawing.Color.White;
            this.grbInput.Controls.Add(this.btnTimKiem);
            this.grbInput.Controls.Add(this.lblNhapMa);
            this.grbInput.Controls.Add(this.txtMaThuoc);
            this.grbInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbInput.Location = new System.Drawing.Point(50, 150);
            this.grbInput.Name = "grbInput";
            this.grbInput.Size = new System.Drawing.Size(450, 400);
            this.grbInput.TabIndex = 4;
            this.grbInput.TabStop = false;
            this.grbInput.Text = "NHẬP THUỐC TÌM KIẾM";
            // 
            // grBOutput
            // 
            this.grBOutput.BackColor = System.Drawing.Color.White;
            this.grBOutput.Controls.Add(this.lblGia);
            this.grBOutput.Controls.Add(this.lblSoLuongTon);
            this.grBOutput.Controls.Add(this.lblLoaiThuoc);
            this.grBOutput.Controls.Add(this.lblTenThuoc);
            this.grBOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grBOutput.Location = new System.Drawing.Point(560, 150);
            this.grBOutput.Name = "grBOutput";
            this.grBOutput.Size = new System.Drawing.Size(800, 400);
            this.grBOutput.TabIndex = 5;
            this.grBOutput.TabStop = false;
            this.grBOutput.Text = "THÔNG TIN THUỐC";
            // 
            // lblTenThuoc
            // 
            this.lblTenThuoc.AutoSize = true;
            this.lblTenThuoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTenThuoc.Location = new System.Drawing.Point(10, 70);
            this.lblTenThuoc.Name = "lblTenThuoc";
            this.lblTenThuoc.Size = new System.Drawing.Size(124, 25);
            this.lblTenThuoc.TabIndex = 0;
            this.lblTenThuoc.Text = "Tên Thuốc:";
            // 
            // lblLoaiThuoc
            // 
            this.lblLoaiThuoc.AutoSize = true;
            this.lblLoaiThuoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblLoaiThuoc.Location = new System.Drawing.Point(10, 140);
            this.lblLoaiThuoc.Name = "lblLoaiThuoc";
            this.lblLoaiThuoc.Size = new System.Drawing.Size(127, 25);
            this.lblLoaiThuoc.TabIndex = 1;
            this.lblLoaiThuoc.Text = "Loại Thuốc:";
            // 
            // lblSoLuongTon
            // 
            this.lblSoLuongTon.AutoSize = true;
            this.lblSoLuongTon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblSoLuongTon.Location = new System.Drawing.Point(10, 210);
            this.lblSoLuongTon.Name = "lblSoLuongTon";
            this.lblSoLuongTon.Size = new System.Drawing.Size(156, 25);
            this.lblSoLuongTon.TabIndex = 2;
            this.lblSoLuongTon.Text = "Số Lượng Tồn:";
            // 
            // lblGia
            // 
            this.lblGia.AutoSize = true;
            this.lblGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblGia.Location = new System.Drawing.Point(10, 280);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(119, 25);
            this.lblGia.TabIndex = 3;
            this.lblGia.Text = "Giá Thuốc:";
            // 
            // ucTraCuuThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.grBOutput);
            this.Controls.Add(this.grbInput);
            this.Controls.Add(this.lblMainTitle);
            this.Controls.Add(this.btnBack);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucTraCuuThuoc";
            this.Size = new System.Drawing.Size(1400, 600);
            this.grbInput.ResumeLayout(false);
            this.grbInput.PerformLayout();
            this.grBOutput.ResumeLayout(false);
            this.grBOutput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblMainTitle;
        private System.Windows.Forms.Label lblNhapMa;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtMaThuoc;
        private System.Windows.Forms.GroupBox grbInput;
        private System.Windows.Forms.GroupBox grBOutput;
        private System.Windows.Forms.Label lblTenThuoc;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.Label lblSoLuongTon;
        private System.Windows.Forms.Label lblLoaiThuoc;
    }
}
