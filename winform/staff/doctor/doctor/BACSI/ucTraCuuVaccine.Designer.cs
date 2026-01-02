namespace BACSI
{
    partial class ucTraCuuVaccine
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
            this.txtMaVC = new System.Windows.Forms.TextBox();
            this.grBOutput = new System.Windows.Forms.GroupBox();
            this.lblNgayHetHan = new System.Windows.Forms.Label();
            this.lblNgaySanXuat = new System.Windows.Forms.Label();
            this.lblLoaiVC = new System.Windows.Forms.Label();
            this.lblTenVC = new System.Windows.Forms.Label();
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
            this.lblMainTitle.Location = new System.Drawing.Point(410, 60);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(608, 54);
            this.lblMainTitle.TabIndex = 7;
            this.lblMainTitle.Text = "TRA CỨU THÔNG TIN VACCINE";
            this.lblMainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(3, 3);
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
            this.lblNhapMa.Size = new System.Drawing.Size(225, 31);
            this.lblNhapMa.TabIndex = 0;
            this.lblNhapMa.Text = "Nhập Mã Vaccine:";
            // 
            // txtMaVC
            // 
            this.txtMaVC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtMaVC.Location = new System.Drawing.Point(29, 117);
            this.txtMaVC.Name = "txtMaVC";
            this.txtMaVC.Size = new System.Drawing.Size(350, 34);
            this.txtMaVC.TabIndex = 1;
            // 
            // grBOutput
            // 
            this.grBOutput.BackColor = System.Drawing.Color.White;
            this.grBOutput.Controls.Add(this.lblNgayHetHan);
            this.grBOutput.Controls.Add(this.lblNgaySanXuat);
            this.grBOutput.Controls.Add(this.lblLoaiVC);
            this.grBOutput.Controls.Add(this.lblTenVC);
            this.grBOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grBOutput.Location = new System.Drawing.Point(560, 150);
            this.grBOutput.Name = "grBOutput";
            this.grBOutput.Size = new System.Drawing.Size(800, 400);
            this.grBOutput.TabIndex = 9;
            this.grBOutput.TabStop = false;
            this.grBOutput.Text = "THÔNG TIN THUỐC";
            // 
            // lblNgayHetHan
            // 
            this.lblNgayHetHan.AutoSize = true;
            this.lblNgayHetHan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblNgayHetHan.Location = new System.Drawing.Point(10, 280);
            this.lblNgayHetHan.Name = "lblNgayHetHan";
            this.lblNgayHetHan.Size = new System.Drawing.Size(159, 25);
            this.lblNgayHetHan.TabIndex = 3;
            this.lblNgayHetHan.Text = "Ngày Hết Hạn: ";
            // 
            // lblNgaySanXuat
            // 
            this.lblNgaySanXuat.AutoSize = true;
            this.lblNgaySanXuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblNgaySanXuat.Location = new System.Drawing.Point(10, 210);
            this.lblNgaySanXuat.Name = "lblNgaySanXuat";
            this.lblNgaySanXuat.Size = new System.Drawing.Size(171, 25);
            this.lblNgaySanXuat.TabIndex = 2;
            this.lblNgaySanXuat.Text = "Ngày Sản Xuất: ";
            // 
            // lblLoaiVC
            // 
            this.lblLoaiVC.AutoSize = true;
            this.lblLoaiVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblLoaiVC.Location = new System.Drawing.Point(10, 140);
            this.lblLoaiVC.Name = "lblLoaiVC";
            this.lblLoaiVC.Size = new System.Drawing.Size(144, 25);
            this.lblLoaiVC.TabIndex = 1;
            this.lblLoaiVC.Text = "Loại Vaccine:";
            // 
            // lblTenVC
            // 
            this.lblTenVC.AutoSize = true;
            this.lblTenVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTenVC.Location = new System.Drawing.Point(10, 70);
            this.lblTenVC.Name = "lblTenVC";
            this.lblTenVC.Size = new System.Drawing.Size(147, 25);
            this.lblTenVC.TabIndex = 0;
            this.lblTenVC.Text = "Tên Vaccine: ";
            // 
            // grbInput
            // 
            this.grbInput.BackColor = System.Drawing.Color.White;
            this.grbInput.Controls.Add(this.btnTimKiem);
            this.grbInput.Controls.Add(this.lblNhapMa);
            this.grbInput.Controls.Add(this.txtMaVC);
            this.grbInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbInput.Location = new System.Drawing.Point(50, 150);
            this.grbInput.Name = "grbInput";
            this.grbInput.Size = new System.Drawing.Size(450, 400);
            this.grbInput.TabIndex = 8;
            this.grbInput.TabStop = false;
            this.grbInput.Text = "NHẬP VACCINE TÌM KIẾM";
            // 
            // ucTraCuuVaccine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMainTitle);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.grBOutput);
            this.Controls.Add(this.grbInput);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucTraCuuVaccine";
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
        private System.Windows.Forms.TextBox txtMaVC;
        private System.Windows.Forms.GroupBox grBOutput;
        private System.Windows.Forms.Label lblNgayHetHan;
        private System.Windows.Forms.Label lblNgaySanXuat;
        private System.Windows.Forms.Label lblLoaiVC;
        private System.Windows.Forms.Label lblTenVC;
        private System.Windows.Forms.GroupBox grbInput;
    }
}
