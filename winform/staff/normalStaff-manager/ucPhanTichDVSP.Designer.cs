namespace PetCareX
{
    partial class ucPhanTichDVSP
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnXem = new System.Windows.Forms.Button();
            this.numTopN = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cboLoaiBaoCao = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1920, 36);
            this.label1.TabIndex = 7;
            this.label1.Text = "Phân Tích Dịch Vụ Và Sản Phẩm";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.Controls.Add(this.btnXem);
            this.panel1.Controls.Add(this.numTopN);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cboLoaiBaoCao);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 150);
            this.panel1.TabIndex = 8;
            // 
            // btnXem
            // 
            this.btnXem.BackColor = System.Drawing.Color.LimeGreen;
            this.btnXem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXem.Location = new System.Drawing.Point(854, 101);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(194, 28);
            this.btnXem.TabIndex = 14;
            this.btnXem.Text = "Xem Thống Kê";
            this.btnXem.UseVisualStyleBackColor = false;
            // 
            // numTopN
            // 
            this.numTopN.Location = new System.Drawing.Point(1076, 54);
            this.numTopN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTopN.Name = "numTopN";
            this.numTopN.Size = new System.Drawing.Size(161, 22);
            this.numTopN.TabIndex = 13;
            this.numTopN.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(619, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "Số Lượng (Top):";
            // 
            // cboLoaiBaoCao
            // 
            this.cboLoaiBaoCao.FormattingEnabled = true;
            this.cboLoaiBaoCao.Items.AddRange(new object[] {
            "Top dịch vụ trong 6 tháng",
            "Top loài/giống trong 6 tháng"});
            this.cboLoaiBaoCao.Location = new System.Drawing.Point(1076, 16);
            this.cboLoaiBaoCao.Name = "cboLoaiBaoCao";
            this.cboLoaiBaoCao.Size = new System.Drawing.Size(313, 24);
            this.cboLoaiBaoCao.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(619, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 23);
            this.label2.TabIndex = 10;
            this.label2.Text = "Loại Báo Cáo Phân Tích:  ";
            // 
            // dgvKetQua
            // 
            this.dgvKetQua.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvKetQua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKetQua.GridColor = System.Drawing.SystemColors.Control;
            this.dgvKetQua.Location = new System.Drawing.Point(0, 186);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.RowHeadersWidth = 51;
            this.dgvKetQua.RowTemplate.Height = 24;
            this.dgvKetQua.Size = new System.Drawing.Size(1920, 894);
            this.dgvKetQua.TabIndex = 9;
            // 
            // ucPhanTichDVSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvKetQua);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "ucPhanTichDVSP";
            this.Size = new System.Drawing.Size(1920, 1080);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numTopN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboLoaiBaoCao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.DataGridView dgvKetQua;
    }
}
