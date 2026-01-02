namespace PetCareX
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUserStatus = new System.Windows.Forms.Label();
            this.lblLoginLogout = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlTieuDe = new System.Windows.Forms.Label();
            this.pnlHienThi = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlHienThi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Cornsilk;
            this.panel1.Controls.Add(this.lblUserStatus);
            this.panel1.Controls.Add(this.lblLoginLogout);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pnlTieuDe);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1067, 96);
            this.panel1.TabIndex = 0;
            // 
            // lblUserStatus
            // 
            this.lblUserStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUserStatus.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserStatus.Location = new System.Drawing.Point(592, 0);
            this.lblUserStatus.Name = "lblUserStatus";
            this.lblUserStatus.Size = new System.Drawing.Size(320, 96);
            this.lblUserStatus.TabIndex = 4;
            this.lblUserStatus.Text = "Text";
            // 
            // lblLoginLogout
            // 
            this.lblLoginLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLoginLogout.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLoginLogout.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginLogout.ForeColor = System.Drawing.Color.Blue;
            this.lblLoginLogout.Location = new System.Drawing.Point(912, 0);
            this.lblLoginLogout.Name = "lblLoginLogout";
            this.lblLoginLogout.Size = new System.Drawing.Size(155, 96);
            this.lblLoginLogout.TabIndex = 2;
            this.lblLoginLogout.Text = "Đăng nhập";
            this.lblLoginLogout.Click += new System.EventHandler(this.lblLoginLogout_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-26, -28);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 152);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pnlTieuDe
            // 
            this.pnlTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTieuDe.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTieuDe.ForeColor = System.Drawing.Color.Red;
            this.pnlTieuDe.Location = new System.Drawing.Point(0, 0);
            this.pnlTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pnlTieuDe.Name = "pnlTieuDe";
            this.pnlTieuDe.Size = new System.Drawing.Size(1067, 96);
            this.pnlTieuDe.TabIndex = 1;
            this.pnlTieuDe.Text = "HỆ THỐNG QUẢN LÝ PETCAREX";
            this.pnlTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHienThi
            // 
            this.pnlHienThi.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlHienThi.Controls.Add(this.pictureBox2);
            this.pnlHienThi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHienThi.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlHienThi.Location = new System.Drawing.Point(0, 96);
            this.pnlHienThi.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHienThi.Name = "pnlHienThi";
            this.pnlHienThi.Size = new System.Drawing.Size(1067, 458);
            this.pnlHienThi.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1067, 458);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.pnlHienThi);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlHienThi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlHienThi;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label pnlTieuDe;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblLoginLogout;
        private System.Windows.Forms.Label lblUserStatus;
    }
}

