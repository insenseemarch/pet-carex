namespace PetCareX
{
    partial class ucTkBacSi
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.btnLoc = new System.Windows.Forms.Button();
            this.clbBacSi = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboChiNhanh = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chartBacSi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label5 = new System.Windows.Forms.Label();
            this.cboLoaiThongKe = new System.Windows.Forms.ComboBox();
            this.lblTu = new System.Windows.Forms.Label();
            this.numTuNam = new System.Windows.Forms.NumericUpDown();
            this.lblDen = new System.Windows.Forms.Label();
            this.numDenNam = new System.Windows.Forms.NumericUpDown();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBacSi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTuNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDenNam)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 30);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.ForestGreen;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1920, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "THỐNG KÊ DOANH THU BÁC SĨ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panelFilter.Controls.Add(this.dtpDenNgay);
            this.panelFilter.Controls.Add(this.dtpTuNgay);
            this.panelFilter.Controls.Add(this.numDenNam);
            this.panelFilter.Controls.Add(this.lblDen);
            this.panelFilter.Controls.Add(this.numTuNam);
            this.panelFilter.Controls.Add(this.lblTu);
            this.panelFilter.Controls.Add(this.cboLoaiThongKe);
            this.panelFilter.Controls.Add(this.label5);
            this.panelFilter.Controls.Add(this.btnLoc);
            this.panelFilter.Controls.Add(this.clbBacSi);
            this.panelFilter.Controls.Add(this.label3);
            this.panelFilter.Controls.Add(this.cboChiNhanh);
            this.panelFilter.Controls.Add(this.label2);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 30);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1920, 188);
            this.panelFilter.TabIndex = 1;
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.LimeGreen;
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoc.Location = new System.Drawing.Point(923, 148);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(115, 39);
            this.btnLoc.TabIndex = 13;
            this.btnLoc.Text = "Thống Kê";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // clbBacSi
            // 
            this.clbBacSi.FormattingEnabled = true;
            this.clbBacSi.Location = new System.Drawing.Point(1406, 9);
            this.clbBacSi.Name = "clbBacSi";
            this.clbBacSi.Size = new System.Drawing.Size(251, 72);
            this.clbBacSi.TabIndex = 3;
            this.clbBacSi.SelectedIndexChanged += new System.EventHandler(this.clbBacSi_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.2F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(1200, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Chọn Bác Sĩ Khảo Sát:";
            // 
            // cboChiNhanh
            // 
            this.cboChiNhanh.FormattingEnabled = true;
            this.cboChiNhanh.Items.AddRange(new object[] {
            "Chi Nhánh 1",
            "Chi Nhánh 2",
            "Chi Nhánh 3"});
            this.cboChiNhanh.Location = new System.Drawing.Point(608, 6);
            this.cboChiNhanh.Name = "cboChiNhanh";
            this.cboChiNhanh.Size = new System.Drawing.Size(121, 24);
            this.cboChiNhanh.TabIndex = 1;
            this.cboChiNhanh.SelectedIndexChanged += new System.EventHandler(this.cboChiNhanh_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.2F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(420, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Chọn Chi Nhánh: ";
            // 
            // chartBacSi
            // 
            chartArea1.Name = "ChartArea1";
            this.chartBacSi.ChartAreas.Add(chartArea1);
            this.chartBacSi.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartBacSi.Legends.Add(legend1);
            this.chartBacSi.Location = new System.Drawing.Point(0, 218);
            this.chartBacSi.Name = "chartBacSi";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartBacSi.Series.Add(series1);
            this.chartBacSi.Size = new System.Drawing.Size(1920, 862);
            this.chartBacSi.TabIndex = 2;
            this.chartBacSi.Text = "chart";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.2F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(821, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 21);
            this.label5.TabIndex = 20;
            this.label5.Text = "Loại Thống Kê:";
            // 
            // cboLoaiThongKe
            // 
            this.cboLoaiThongKe.FormattingEnabled = true;
            this.cboLoaiThongKe.Items.AddRange(new object[] {
            "Ngày",
            "Tháng",
            "Năm"});
            this.cboLoaiThongKe.Location = new System.Drawing.Point(1009, 44);
            this.cboLoaiThongKe.Name = "cboLoaiThongKe";
            this.cboLoaiThongKe.Size = new System.Drawing.Size(121, 24);
            this.cboLoaiThongKe.TabIndex = 21;
            // 
            // lblTu
            // 
            this.lblTu.AutoSize = true;
            this.lblTu.Font = new System.Drawing.Font("Segoe UI", 9.2F, System.Drawing.FontStyle.Bold);
            this.lblTu.Location = new System.Drawing.Point(420, 81);
            this.lblTu.Name = "lblTu";
            this.lblTu.Size = new System.Drawing.Size(34, 21);
            this.lblTu.TabIndex = 22;
            this.lblTu.Text = "Từ:";
            // 
            // numTuNam
            // 
            this.numTuNam.Location = new System.Drawing.Point(609, 82);
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
            this.numTuNam.TabIndex = 23;
            this.numTuNam.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Font = new System.Drawing.Font("Segoe UI", 9.2F, System.Drawing.FontStyle.Bold);
            this.lblDen.Location = new System.Drawing.Point(1200, 91);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(45, 21);
            this.lblDen.TabIndex = 24;
            this.lblDen.Text = "Đến:";
            // 
            // numDenNam
            // 
            this.numDenNam.Location = new System.Drawing.Point(1406, 92);
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
            this.numDenNam.TabIndex = 25;
            this.numDenNam.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(609, 81);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(121, 22);
            this.dtpTuNgay.TabIndex = 26;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(1405, 92);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(121, 22);
            this.dtpDenNgay.TabIndex = 27;
            // 
            // ucTkBacSi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartBacSi);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panel1);
            this.Name = "ucTkBacSi";
            this.Size = new System.Drawing.Size(1920, 1080);
            this.Load += new System.EventHandler(this.ucTkBacSi_Load);
            this.panel1.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBacSi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTuNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDenNam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboChiNhanh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbBacSi;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBacSi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboLoaiThongKe;
        private System.Windows.Forms.Label lblTu;
        private System.Windows.Forms.NumericUpDown numTuNam;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.NumericUpDown numDenNam;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
    }
}
