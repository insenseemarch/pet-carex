namespace BACSI
{
    partial class ucThongTinThuCung
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
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.lblNhapMa = new System.Windows.Forms.Label();
            this.txtMaPet = new System.Windows.Forms.TextBox();
            this.grBOutput = new System.Windows.Forms.GroupBox();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.lblTinhTrang = new System.Windows.Forms.Label();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.lblLoaiTC = new System.Windows.Forms.Label();
            this.lblTenTC = new System.Windows.Forms.Label();
            this.grbInput = new System.Windows.Forms.GroupBox();
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
            this.lblMainTitle.Size = new System.Drawing.Size(651, 54);
            this.lblMainTitle.TabIndex = 7;
            this.lblMainTitle.Text = "TRA CỨU THÔNG TIN THÚ CƯNG";
            this.lblMainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(0, 0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(108, 41);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Quay Lại";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
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
            this.lblNhapMa.Size = new System.Drawing.Size(246, 31);
            this.lblNhapMa.TabIndex = 0;
            this.lblNhapMa.Text = "Nhập Mã Thú Cưng:";
            // 
            // txtMaPet
            // 
            this.txtMaPet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaPet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtMaPet.Location = new System.Drawing.Point(29, 117);
            this.txtMaPet.Name = "txtMaPet";
            this.txtMaPet.Size = new System.Drawing.Size(350, 34);
            this.txtMaPet.TabIndex = 1;
            // 
            // grBOutput
            // 
            this.grBOutput.BackColor = System.Drawing.Color.White;
            this.grBOutput.Controls.Add(this.lblGioiTinh);
            this.grBOutput.Controls.Add(this.lblTinhTrang);
            this.grBOutput.Controls.Add(this.lblNgaySinh);
            this.grBOutput.Controls.Add(this.lblLoaiTC);
            this.grBOutput.Controls.Add(this.lblTenTC);
            this.grBOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grBOutput.Location = new System.Drawing.Point(557, 147);
            this.grBOutput.Name = "grBOutput";
            this.grBOutput.Size = new System.Drawing.Size(800, 400);
            this.grBOutput.TabIndex = 9;
            this.grBOutput.TabStop = false;
            this.grBOutput.Text = "THÔNG TIN THÚ CƯNG";
            this.grBOutput.Enter += new System.EventHandler(this.grBOutput_Enter);
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
            // lblTinhTrang
            // 
            this.lblTinhTrang.AutoSize = true;
            this.lblTinhTrang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTinhTrang.Location = new System.Drawing.Point(10, 280);
            this.lblTinhTrang.Name = "lblTinhTrang";
            this.lblTinhTrang.Size = new System.Drawing.Size(232, 25);
            this.lblTinhTrang.TabIndex = 3;
            this.lblTinhTrang.Text = "Tình Trạng Sức Khỏe: ";
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
            // lblLoaiTC
            // 
            this.lblLoaiTC.AutoSize = true;
            this.lblLoaiTC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblLoaiTC.Location = new System.Drawing.Point(10, 140);
            this.lblLoaiTC.Name = "lblLoaiTC";
            this.lblLoaiTC.Size = new System.Drawing.Size(162, 25);
            this.lblLoaiTC.TabIndex = 1;
            this.lblLoaiTC.Text = "Loài Thú Cưng:";
            // 
            // lblTenTC
            // 
            this.lblTenTC.AutoSize = true;
            this.lblTenTC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTenTC.Location = new System.Drawing.Point(10, 70);
            this.lblTenTC.Name = "lblTenTC";
            this.lblTenTC.Size = new System.Drawing.Size(165, 25);
            this.lblTenTC.TabIndex = 0;
            this.lblTenTC.Text = "Tên Thú Cưng: ";
            // 
            // grbInput
            // 
            this.grbInput.BackColor = System.Drawing.Color.White;
            this.grbInput.Controls.Add(this.btnTimKiem);
            this.grbInput.Controls.Add(this.lblNhapMa);
            this.grbInput.Controls.Add(this.txtMaPet);
            this.grbInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbInput.Location = new System.Drawing.Point(47, 147);
            this.grbInput.Name = "grbInput";
            this.grbInput.Size = new System.Drawing.Size(463, 400);
            this.grbInput.TabIndex = 8;
            this.grbInput.TabStop = false;
            this.grbInput.Text = "NHẬP MÃ THÚ CƯNG CẦN TÌM";
            // 
            // ucThongTinThuCung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GreenYellow;
            this.Controls.Add(this.lblMainTitle);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.grBOutput);
            this.Controls.Add(this.grbInput);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucThongTinThuCung";
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
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Label lblNhapMa;
        private System.Windows.Forms.TextBox txtMaPet;
        private System.Windows.Forms.GroupBox grBOutput;
        private System.Windows.Forms.Label lblTinhTrang;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.Label lblLoaiTC;
        private System.Windows.Forms.Label lblTenTC;
        private System.Windows.Forms.GroupBox grbInput;
        private System.Windows.Forms.Label lblGioiTinh;
    }
}
