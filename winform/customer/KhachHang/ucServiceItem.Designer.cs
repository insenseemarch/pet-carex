namespace KhachHang
{
    partial class ucServiceItem
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
            this.lblMota = new System.Windows.Forms.Label();
            this.btnTieuDe = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.picDV = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDV)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMota
            // 
            this.lblMota.AutoSize = true;
            this.lblMota.Location = new System.Drawing.Point(3, 330);
            this.lblMota.Name = "lblMota";
            this.lblMota.Size = new System.Drawing.Size(64, 25);
            this.lblMota.TabIndex = 2;
            this.lblMota.Text = "MoTa";
            // 
            // btnTieuDe
            // 
            this.btnTieuDe.AutoSize = true;
            this.btnTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnTieuDe.Location = new System.Drawing.Point(120, 375);
            this.btnTieuDe.Name = "btnTieuDe";
            this.btnTieuDe.Size = new System.Drawing.Size(181, 43);
            this.btnTieuDe.TabIndex = 3;
            this.btnTieuDe.Text = "Đặt Lịch Hẹn";
            this.btnTieuDe.UseVisualStyleBackColor = true;
            this.btnTieuDe.Click += new System.EventHandler(this.btnTieuDe_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblTieuDe.Location = new System.Drawing.Point(129, 278);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(102, 31);
            this.lblTieuDe.TabIndex = 4;
            this.lblTieuDe.Text = "TieuDe";
            // 
            // picDV
            // 
            this.picDV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picDV.Location = new System.Drawing.Point(107, 42);
            this.picDV.Name = "picDV";
            this.picDV.Size = new System.Drawing.Size(214, 204);
            this.picDV.TabIndex = 0;
            this.picDV.TabStop = false;
            // 
            // ucServiceItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.btnTieuDe);
            this.Controls.Add(this.lblMota);
            this.Controls.Add(this.picDV);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucServiceItem";
            this.Size = new System.Drawing.Size(500, 450);
            ((System.ComponentModel.ISupportInitialize)(this.picDV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picDV;
        private System.Windows.Forms.Label lblMota;
        private System.Windows.Forms.Button btnTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
    }
}
