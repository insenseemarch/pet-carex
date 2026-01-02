namespace BACSI
{
    partial class ucKeToa
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
            this.pnlThongTin = new System.Windows.Forms.Panel();
            this.btnLuuToanBo = new System.Windows.Forms.Button();
            this.txtTenToa = new System.Windows.Forms.TextBox();
            this.lblTenToa = new System.Windows.Forms.Label();
            this.txtMaDV = new System.Windows.Forms.TextBox();
            this.lblMaDichVu = new System.Windows.Forms.Label();
            this.txtMathucung = new System.Windows.Forms.TextBox();
            this.lblMaPet = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grbChiTiet = new System.Windows.Forms.GroupBox();
            this.dgvKeToa = new System.Windows.Forms.DataGridView();
            this.MaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Soluong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lieuluong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ghichu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlThongTin.SuspendLayout();
            this.grbChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeToa)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlThongTin
            // 
            this.pnlThongTin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlThongTin.Controls.Add(this.btnLuuToanBo);
            this.pnlThongTin.Controls.Add(this.txtTenToa);
            this.pnlThongTin.Controls.Add(this.lblTenToa);
            this.pnlThongTin.Controls.Add(this.txtMaDV);
            this.pnlThongTin.Controls.Add(this.lblMaDichVu);
            this.pnlThongTin.Controls.Add(this.txtMathucung);
            this.pnlThongTin.Controls.Add(this.lblMaPet);
            this.pnlThongTin.Controls.Add(this.lblTitle);
            this.pnlThongTin.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlThongTin.Location = new System.Drawing.Point(0, 0);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Size = new System.Drawing.Size(350, 600);
            this.pnlThongTin.TabIndex = 0;
            // 
            // btnLuuToanBo
            // 
            this.btnLuuToanBo.Location = new System.Drawing.Point(48, 508);
            this.btnLuuToanBo.Name = "btnLuuToanBo";
            this.btnLuuToanBo.Size = new System.Drawing.Size(243, 37);
            this.btnLuuToanBo.TabIndex = 8;
            this.btnLuuToanBo.Text = "LƯU và CẬP NHẬT";
            this.btnLuuToanBo.UseVisualStyleBackColor = true;
            this.btnLuuToanBo.Click += new System.EventHandler(this.btnLuuToanBo_Click);
            // 
            // txtTenToa
            // 
            this.txtTenToa.Location = new System.Drawing.Point(29, 425);
            this.txtTenToa.Name = "txtTenToa";
            this.txtTenToa.Size = new System.Drawing.Size(292, 27);
            this.txtTenToa.TabIndex = 7;
            // 
            // lblTenToa
            // 
            this.lblTenToa.AutoSize = true;
            this.lblTenToa.ForeColor = System.Drawing.Color.White;
            this.lblTenToa.Location = new System.Drawing.Point(3, 390);
            this.lblTenToa.Name = "lblTenToa";
            this.lblTenToa.Size = new System.Drawing.Size(80, 20);
            this.lblTenToa.TabIndex = 6;
            this.lblTenToa.Text = "Tên Toa: ";
            // 
            // txtMaDV
            // 
            this.txtMaDV.Location = new System.Drawing.Point(29, 314);
            this.txtMaDV.Name = "txtMaDV";
            this.txtMaDV.Size = new System.Drawing.Size(292, 27);
            this.txtMaDV.TabIndex = 5;
            // 
            // lblMaDichVu
            // 
            this.lblMaDichVu.AutoSize = true;
            this.lblMaDichVu.ForeColor = System.Drawing.Color.White;
            this.lblMaDichVu.Location = new System.Drawing.Point(3, 279);
            this.lblMaDichVu.Name = "lblMaDichVu";
            this.lblMaDichVu.Size = new System.Drawing.Size(107, 20);
            this.lblMaDichVu.TabIndex = 4;
            this.lblMaDichVu.Text = "Mã Dịch Vụ: ";
            // 
            // txtMathucung
            // 
            this.txtMathucung.Location = new System.Drawing.Point(29, 199);
            this.txtMathucung.Name = "txtMathucung";
            this.txtMathucung.Size = new System.Drawing.Size(292, 27);
            this.txtMathucung.TabIndex = 3;
            // 
            // lblMaPet
            // 
            this.lblMaPet.AutoSize = true;
            this.lblMaPet.ForeColor = System.Drawing.Color.White;
            this.lblMaPet.Location = new System.Drawing.Point(3, 164);
            this.lblMaPet.Name = "lblMaPet";
            this.lblMaPet.Size = new System.Drawing.Size(119, 20);
            this.lblMaPet.TabIndex = 2;
            this.lblMaPet.Text = "Mã Thú Cưng: ";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(50, 46);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(312, 41);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "THÔNG TIN CHUNG ";
            // 
            // grbChiTiet
            // 
            this.grbChiTiet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grbChiTiet.Controls.Add(this.dgvKeToa);
            this.grbChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbChiTiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.grbChiTiet.ForeColor = System.Drawing.Color.White;
            this.grbChiTiet.Location = new System.Drawing.Point(350, 0);
            this.grbChiTiet.Name = "grbChiTiet";
            this.grbChiTiet.Size = new System.Drawing.Size(1050, 600);
            this.grbChiTiet.TabIndex = 1;
            this.grbChiTiet.TabStop = false;
            this.grbChiTiet.Text = "THÔNG TIN TOA THUỐC";
            // 
            // dgvKeToa
            // 
            this.dgvKeToa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKeToa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKeToa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaSP,
            this.Soluong,
            this.Lieuluong,
            this.Ghichu});
            this.dgvKeToa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKeToa.Location = new System.Drawing.Point(3, 26);
            this.dgvKeToa.Name = "dgvKeToa";
            this.dgvKeToa.RowHeadersWidth = 51;
            this.dgvKeToa.RowTemplate.Height = 24;
            this.dgvKeToa.Size = new System.Drawing.Size(1044, 571);
            this.dgvKeToa.TabIndex = 0;
            // 
            // MaSP
            // 
            this.MaSP.HeaderText = "MÃ THUỐC";
            this.MaSP.MinimumWidth = 6;
            this.MaSP.Name = "MaSP";
            // 
            // Soluong
            // 
            this.Soluong.HeaderText = "SỐ LƯỢNG";
            this.Soluong.MinimumWidth = 6;
            this.Soluong.Name = "Soluong";
            // 
            // Lieuluong
            // 
            this.Lieuluong.HeaderText = "LIỀU LƯỢNG ";
            this.Lieuluong.MinimumWidth = 6;
            this.Lieuluong.Name = "Lieuluong";
            // 
            // Ghichu
            // 
            this.Ghichu.HeaderText = "GHI CHÚ / CĂN DẶN";
            this.Ghichu.MinimumWidth = 6;
            this.Ghichu.Name = "Ghichu";
            // 
            // ucKeToa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.grbChiTiet);
            this.Controls.Add(this.pnlThongTin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucKeToa";
            this.Size = new System.Drawing.Size(1400, 600);
            this.pnlThongTin.ResumeLayout(false);
            this.pnlThongTin.PerformLayout();
            this.grbChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeToa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.TextBox txtMaDV;
        private System.Windows.Forms.Label lblMaDichVu;
        private System.Windows.Forms.TextBox txtMathucung;
        private System.Windows.Forms.Label lblMaPet;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnLuuToanBo;
        private System.Windows.Forms.TextBox txtTenToa;
        private System.Windows.Forms.Label lblTenToa;
        private System.Windows.Forms.GroupBox grbChiTiet;
        private System.Windows.Forms.DataGridView dgvKeToa;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Soluong;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lieuluong;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ghichu;
    }
}
