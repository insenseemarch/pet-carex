namespace PetCareX
{
    partial class ucTkSanPham
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.numDenNam = new System.Windows.Forms.NumericUpDown();
            this.numTuNam = new System.Windows.Forms.NumericUpDown();
            this.cboLoaiThongKe = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDen = new System.Windows.Forms.Label();
            this.lblTu = new System.Windows.Forms.Label();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cboChiNhanh = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDenNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTuNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.ForestGreen;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1920, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "THỐNG KÊ DOANH THU SẢN PHẨM";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panelFilter.Controls.Add(this.numDenNam);
            this.panelFilter.Controls.Add(this.numTuNam);
            this.panelFilter.Controls.Add(this.cboLoaiThongKe);
            this.panelFilter.Controls.Add(this.label5);
            this.panelFilter.Controls.Add(this.cboChiNhanh);
            this.panelFilter.Controls.Add(this.dtpDenNgay);
            this.panelFilter.Controls.Add(this.dtpTuNgay);
            this.panelFilter.Controls.Add(this.lblDen);
            this.panelFilter.Controls.Add(this.lblTu);
            this.panelFilter.Controls.Add(this.btnThongKe);
            this.panelFilter.Controls.Add(this.label2);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 30);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1920, 138);
            this.panelFilter.TabIndex = 3;
            // 
            // numDenNam
            // 
            this.numDenNam.Location = new System.Drawing.Point(1238, 58);
            this.numDenNam.Maximum = new decimal(new int[] {
            2026,
            0,
            0,
            0});
            this.numDenNam.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numDenNam.Name = "numDenNam";
            this.numDenNam.Size = new System.Drawing.Size(120, 22);
            this.numDenNam.TabIndex = 22;
            this.numDenNam.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // numTuNam
            // 
            this.numTuNam.Location = new System.Drawing.Point(765, 55);
            this.numTuNam.Maximum = new decimal(new int[] {
            2026,
            0,
            0,
            0});
            this.numTuNam.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numTuNam.Name = "numTuNam";
            this.numTuNam.Size = new System.Drawing.Size(120, 22);
            this.numTuNam.TabIndex = 21;
            this.numTuNam.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // cboLoaiThongKe
            // 
            this.cboLoaiThongKe.FormattingEnabled = true;
            this.cboLoaiThongKe.Items.AddRange(new object[] {
            "Ngày",
            "Tháng",
            "Năm"});
            this.cboLoaiThongKe.Location = new System.Drawing.Point(1237, 6);
            this.cboLoaiThongKe.Name = "cboLoaiThongKe";
            this.cboLoaiThongKe.Size = new System.Drawing.Size(121, 24);
            this.cboLoaiThongKe.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.2F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(1094, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 21);
            this.label5.TabIndex = 19;
            this.label5.Text = "Loại Thống Kê:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(1237, 58);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(121, 22);
            this.dtpDenNgay.TabIndex = 17;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(765, 56);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(121, 22);
            this.dtpTuNgay.TabIndex = 16;
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Font = new System.Drawing.Font("Segoe UI", 9.2F, System.Drawing.FontStyle.Bold);
            this.lblDen.Location = new System.Drawing.Point(1094, 58);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(45, 21);
            this.lblDen.TabIndex = 15;
            this.lblDen.Text = "Đến:";
            // 
            // lblTu
            // 
            this.lblTu.AutoSize = true;
            this.lblTu.Font = new System.Drawing.Font("Segoe UI", 9.2F, System.Drawing.FontStyle.Bold);
            this.lblTu.Location = new System.Drawing.Point(616, 56);
            this.lblTu.Name = "lblTu";
            this.lblTu.Size = new System.Drawing.Size(34, 21);
            this.lblTu.TabIndex = 14;
            this.lblTu.Text = "Từ:";
            // 
            // btnThongKe
            // 
            this.btnThongKe.BackColor = System.Drawing.Color.LimeGreen;
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.Location = new System.Drawing.Point(919, 99);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(115, 39);
            this.btnThongKe.TabIndex = 13;
            this.btnThongKe.Text = "Thống Kê";
            this.btnThongKe.UseVisualStyleBackColor = false;
            // 
            // chartDoanhThu
            // 
            chartArea4.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea4);
            this.chartDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend4);
            this.chartDoanhThu.Location = new System.Drawing.Point(0, 168);
            this.chartDoanhThu.Name = "chartDoanhThu";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartDoanhThu.Series.Add(series4);
            this.chartDoanhThu.Size = new System.Drawing.Size(1920, 912);
            this.chartDoanhThu.TabIndex = 4;
            this.chartDoanhThu.Text = "chart1";
            // 
            // cboChiNhanh
            // 
            this.cboChiNhanh.FormattingEnabled = true;
            this.cboChiNhanh.Items.AddRange(new object[] {
            "Chi Nhánh 1",
            "Chi Nhánh 2",
            "Chi Nhánh 3"});
            this.cboChiNhanh.Location = new System.Drawing.Point(765, 3);
            this.cboChiNhanh.Name = "cboChiNhanh";
            this.cboChiNhanh.Size = new System.Drawing.Size(121, 24);
            this.cboChiNhanh.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.2F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(616, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Chọn Chi Nhánh: ";
            // 
            // ucTkSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartDoanhThu);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.label1);
            this.Name = "ucTkSanPham";
            this.Size = new System.Drawing.Size(1920, 1080);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDenNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTuNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.Label lblTu;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboLoaiThongKe;
        private System.Windows.Forms.NumericUpDown numDenNam;
        private System.Windows.Forms.NumericUpDown numTuNam;
        private System.Windows.Forms.ComboBox cboChiNhanh;
        private System.Windows.Forms.Label label2;
    }
}
