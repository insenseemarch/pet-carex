namespace BACSI
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
            this.pnlInTro = new System.Windows.Forms.Panel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnKeToa = new System.Windows.Forms.Button();
            this.btnBenhAn = new System.Windows.Forms.Button();
            this.btnXemLichSuKham = new System.Windows.Forms.Button();
            this.btnTraCuu = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlInTro.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlInTro
            // 
            this.pnlInTro.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlInTro.Controls.Add(this.pnlMenu);
            this.pnlInTro.Controls.Add(this.picLogo);
            this.pnlInTro.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInTro.Location = new System.Drawing.Point(0, 0);
            this.pnlInTro.Name = "pnlInTro";
            this.pnlInTro.Size = new System.Drawing.Size(1482, 150);
            this.pnlInTro.TabIndex = 0;
            this.pnlInTro.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlInTro_Paint);
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.btnKeToa);
            this.pnlMenu.Controls.Add(this.btnBenhAn);
            this.pnlMenu.Controls.Add(this.btnXemLichSuKham);
            this.pnlMenu.Controls.Add(this.btnTraCuu);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMenu.Location = new System.Drawing.Point(200, 100);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(1282, 50);
            this.pnlMenu.TabIndex = 1;
            // 
            // btnKeToa
            // 
            this.btnKeToa.Location = new System.Drawing.Point(960, 0);
            this.btnKeToa.Name = "btnKeToa";
            this.btnKeToa.Size = new System.Drawing.Size(320, 50);
            this.btnKeToa.TabIndex = 3;
            this.btnKeToa.Text = "KÊ TOA THUỐC";
            this.btnKeToa.UseVisualStyleBackColor = true;
            this.btnKeToa.Click += new System.EventHandler(this.btnKeToa_Click);
            // 
            // btnBenhAn
            // 
            this.btnBenhAn.Location = new System.Drawing.Point(640, 0);
            this.btnBenhAn.Name = "btnBenhAn";
            this.btnBenhAn.Size = new System.Drawing.Size(320, 50);
            this.btnBenhAn.TabIndex = 2;
            this.btnBenhAn.Text = "PHIẾU KHÁM BỆNH";
            this.btnBenhAn.UseVisualStyleBackColor = true;
            this.btnBenhAn.Click += new System.EventHandler(this.btnBenhAn_Click);
            // 
            // btnXemLichSuKham
            // 
            this.btnXemLichSuKham.Location = new System.Drawing.Point(320, 0);
            this.btnXemLichSuKham.Name = "btnXemLichSuKham";
            this.btnXemLichSuKham.Size = new System.Drawing.Size(320, 50);
            this.btnXemLichSuKham.TabIndex = 1;
            this.btnXemLichSuKham.Text = "LỊCH SỬ KHÁM";
            this.btnXemLichSuKham.UseVisualStyleBackColor = true;
            this.btnXemLichSuKham.Click += new System.EventHandler(this.btnXemLichSuKham_Click_1);
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.Location = new System.Drawing.Point(0, 0);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(320, 50);
            this.btnTraCuu.TabIndex = 0;
            this.btnTraCuu.Text = "TRA CỨU";
            this.btnTraCuu.UseVisualStyleBackColor = true;
            this.btnTraCuu.Click += new System.EventHandler(this.btnTraCuu_Click);
            // 
            // picLogo
            // 
            this.picLogo.BackgroundImage = global::BACSI.Properties.Resources.Gemini_Generated_Image_nu7a6xnu7a6xnu7a;
            this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(200, 150);
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.pnlContent.Location = new System.Drawing.Point(0, 150);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1482, 683);
            this.pnlContent.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 833);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlInTro);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.pnlInTro.ResumeLayout(false);
            this.pnlMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInTro;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnKeToa;
        private System.Windows.Forms.Button btnBenhAn;
        private System.Windows.Forms.Button btnXemLichSuKham;
        private System.Windows.Forms.Button btnTraCuu;
        private System.Windows.Forms.Panel pnlContent;
    }
}

