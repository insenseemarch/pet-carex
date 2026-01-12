namespace KhachHang
{
    partial class ucDichVu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDichVu));
            this.tLPDichVu = new System.Windows.Forms.TableLayoutPanel();
            this.imageListDichVu = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // tLPDichVu
            // 
            this.tLPDichVu.ColumnCount = 2;
            this.tLPDichVu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tLPDichVu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tLPDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tLPDichVu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLPDichVu.Location = new System.Drawing.Point(0, 0);
            this.tLPDichVu.Name = "tLPDichVu";
            this.tLPDichVu.Padding = new System.Windows.Forms.Padding(60, 40, 60, 40);
            this.tLPDichVu.RowCount = 1;
            this.tLPDichVu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLPDichVu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tLPDichVu.Size = new System.Drawing.Size(1215, 666);
            this.tLPDichVu.TabIndex = 0;
            // 
            // imageListDichVu
            // 
            this.imageListDichVu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDichVu.ImageStream")));
            this.imageListDichVu.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDichVu.Images.SetKeyName(0, "Gemini_Generated_Image_29i6yy29i6yy29i6.png");
            this.imageListDichVu.Images.SetKeyName(1, "Gemini_Generated_Image_g605ssg605ssg605.png");
            // 
            // ucDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.tLPDichVu);
            this.Name = "ucDichVu";
            this.Size = new System.Drawing.Size(1215, 666);
            this.Load += new System.EventHandler(this.ucDichVu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tLPDichVu;
        private System.Windows.Forms.ImageList imageListDichVu;
    }
}
