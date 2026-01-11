namespace BACSI
{
    partial class ucLichSuKham
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
            this.lblNhapMa = new System.Windows.Forms.Label();
            this.txtMaPetLS = new System.Windows.Forms.TextBox();
            this.btnTimKiemLS = new System.Windows.Forms.Button();
            this.pnlThongTin = new System.Windows.Forms.Panel();
            this.tblThongTin = new System.Windows.Forms.TableLayoutPanel();
            this.grbTiemNgua = new System.Windows.Forms.GroupBox();
            this.dgvTiemPhong = new System.Windows.Forms.DataGridView();
            this.grbKhamBenh = new System.Windows.Forms.GroupBox();
            this.dgvKhamBenh = new System.Windows.Forms.DataGridView();
            this.pnlThongTin.SuspendLayout();
            this.tblThongTin.SuspendLayout();
            this.grbTiemNgua.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiemPhong)).BeginInit();
            this.grbKhamBenh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhamBenh)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNhapMa
            // 
            this.lblNhapMa.AutoSize = true;
            this.lblNhapMa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblNhapMa.Location = new System.Drawing.Point(90, 36);
            this.lblNhapMa.Name = "lblNhapMa";
            this.lblNhapMa.Size = new System.Drawing.Size(382, 25);
            this.lblNhapMa.TabIndex = 0;
            this.lblNhapMa.Text = "NHẬP MÃ THÚ CƯNG ĐỂ XEM LỊCH SỬ:";
            // 
            // txtMaPetLS
            // 
            this.txtMaPetLS.Location = new System.Drawing.Point(478, 37);
            this.txtMaPetLS.Name = "txtMaPetLS";
            this.txtMaPetLS.Size = new System.Drawing.Size(263, 27);
            this.txtMaPetLS.TabIndex = 1;
            // 
            // btnTimKiemLS
            // 
            this.btnTimKiemLS.Location = new System.Drawing.Point(947, 36);
            this.btnTimKiemLS.Name = "btnTimKiemLS";
            this.btnTimKiemLS.Size = new System.Drawing.Size(136, 29);
            this.btnTimKiemLS.TabIndex = 2;
            this.btnTimKiemLS.Text = "Tìm Kiếm";
            this.btnTimKiemLS.UseVisualStyleBackColor = true;
            this.btnTimKiemLS.Click += new System.EventHandler(this.btnTimKiemLS_Click);
            // 
            // pnlThongTin
            // 
            this.pnlThongTin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pnlThongTin.Controls.Add(this.lblNhapMa);
            this.pnlThongTin.Controls.Add(this.btnTimKiemLS);
            this.pnlThongTin.Controls.Add(this.txtMaPetLS);
            this.pnlThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTin.Location = new System.Drawing.Point(0, 0);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Size = new System.Drawing.Size(1400, 100);
            this.pnlThongTin.TabIndex = 3;
            // 
            // tblThongTin
            // 
            this.tblThongTin.ColumnCount = 1;
            this.tblThongTin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblThongTin.Controls.Add(this.grbTiemNgua, 0, 1);
            this.tblThongTin.Controls.Add(this.grbKhamBenh, 0, 0);
            this.tblThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblThongTin.Location = new System.Drawing.Point(0, 100);
            this.tblThongTin.Name = "tblThongTin";
            this.tblThongTin.RowCount = 2;
            this.tblThongTin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblThongTin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblThongTin.Size = new System.Drawing.Size(1400, 500);
            this.tblThongTin.TabIndex = 5;
            // 
            // grbTiemNgua
            // 
            this.grbTiemNgua.Controls.Add(this.dgvTiemPhong);
            this.grbTiemNgua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbTiemNgua.Location = new System.Drawing.Point(3, 253);
            this.grbTiemNgua.Name = "grbTiemNgua";
            this.grbTiemNgua.Size = new System.Drawing.Size(1394, 244);
            this.grbTiemNgua.TabIndex = 6;
            this.grbTiemNgua.TabStop = false;
            this.grbTiemNgua.Text = "LỊCH SỬ TIÊM NGỪA";
            // 
            // dgvTiemPhong
            // 
            this.dgvTiemPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTiemPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTiemPhong.Location = new System.Drawing.Point(3, 23);
            this.dgvTiemPhong.Name = "dgvTiemPhong";
            this.dgvTiemPhong.RowHeadersWidth = 51;
            this.dgvTiemPhong.RowTemplate.Height = 24;
            this.dgvTiemPhong.Size = new System.Drawing.Size(1388, 218);
            this.dgvTiemPhong.TabIndex = 0;
            // 
            // grbKhamBenh
            // 
            this.grbKhamBenh.Controls.Add(this.dgvKhamBenh);
            this.grbKhamBenh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbKhamBenh.Location = new System.Drawing.Point(3, 3);
            this.grbKhamBenh.Name = "grbKhamBenh";
            this.grbKhamBenh.Size = new System.Drawing.Size(1394, 244);
            this.grbKhamBenh.TabIndex = 5;
            this.grbKhamBenh.TabStop = false;
            this.grbKhamBenh.Text = "LỊCH SỬ KHÁM BỆNH";
            // 
            // dgvKhamBenh
            // 
            this.dgvKhamBenh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhamBenh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhamBenh.Location = new System.Drawing.Point(3, 23);
            this.dgvKhamBenh.Name = "dgvKhamBenh";
            this.dgvKhamBenh.RowHeadersWidth = 51;
            this.dgvKhamBenh.RowTemplate.Height = 24;
            this.dgvKhamBenh.Size = new System.Drawing.Size(1388, 218);
            this.dgvKhamBenh.TabIndex = 0;
            // 
            // ucLichSuKham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblThongTin);
            this.Controls.Add(this.pnlThongTin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucLichSuKham";
            this.Size = new System.Drawing.Size(1400, 600);
            this.pnlThongTin.ResumeLayout(false);
            this.pnlThongTin.PerformLayout();
            this.tblThongTin.ResumeLayout(false);
            this.grbTiemNgua.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiemPhong)).EndInit();
            this.grbKhamBenh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhamBenh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNhapMa;
        private System.Windows.Forms.TextBox txtMaPetLS;
        private System.Windows.Forms.Button btnTimKiemLS;
        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.TableLayoutPanel tblThongTin;
        private System.Windows.Forms.GroupBox grbTiemNgua;
        private System.Windows.Forms.DataGridView dgvTiemPhong;
        private System.Windows.Forms.GroupBox grbKhamBenh;
        private System.Windows.Forms.DataGridView dgvKhamBenh;
    }
}
