namespace PetCareX
{
    partial class ucConTraCuuTonKho
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
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboChiNhanh = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvTonKho = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).BeginInit();
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
            this.label1.Text = "Tra Cứu Tồn Kho Sản Phẩm";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Orange;
            this.panel1.Controls.Add(this.txtTimKiem);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cboChiNhanh);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 91);
            this.panel1.TabIndex = 3;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(328, 48);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(245, 22);
            this.txtTimKiem.TabIndex = 3;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nhập Tên/Mã Sản phẩm:";
            // 
            // cboChiNhanh
            // 
            this.cboChiNhanh.FormattingEnabled = true;
            this.cboChiNhanh.Items.AddRange(new object[] {
            "Chi nhánh 1",
            "Chi nhánh 2",
            "Chi nhánh 3"});
            this.cboChiNhanh.Location = new System.Drawing.Point(328, 12);
            this.cboChiNhanh.Name = "cboChiNhanh";
            this.cboChiNhanh.Size = new System.Drawing.Size(121, 24);
            this.cboChiNhanh.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Chọn Chi Nhánh: ";
            // 
            // dgvTonKho
            // 
            this.dgvTonKho.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvTonKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTonKho.Location = new System.Drawing.Point(0, 177);
            this.dgvTonKho.Name = "dgvTonKho";
            this.dgvTonKho.ReadOnly = true;
            this.dgvTonKho.RowHeadersWidth = 51;
            this.dgvTonKho.RowTemplate.Height = 24;
            this.dgvTonKho.Size = new System.Drawing.Size(1920, 903);
            this.dgvTonKho.TabIndex = 4;
            // 
            // ucConTraCuuTonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTonKho);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "ucConTraCuuTonKho";
            this.Padding = new System.Windows.Forms.Padding(0, 50, 0, 0);
            this.Size = new System.Drawing.Size(1920, 1080);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboChiNhanh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvTonKho;
    }
}
