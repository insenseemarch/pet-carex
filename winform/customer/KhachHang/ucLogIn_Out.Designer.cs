namespace KhachHang
{
    partial class ucLogIn_Out
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
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.lblDangNhap = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.lnkGoRegister = new System.Windows.Forms.LinkLabel();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.pnlRegister = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblXacNhanLai = new System.Windows.Forms.Label();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.txtPass_Register = new System.Windows.Forms.TextBox();
            this.lblPass_Register = new System.Windows.Forms.Label();
            this.txtUser_Register = new System.Windows.Forms.TextBox();
            this.lblUser_Register = new System.Windows.Forms.Label();
            this.lblDangKy = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.txtGioiTinh = new System.Windows.Forms.TextBox();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.pnlLogin.SuspendLayout();
            this.pnlRegister.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pnlLogin.Controls.Add(this.lblThongBao);
            this.pnlLogin.Controls.Add(this.lnkGoRegister);
            this.pnlLogin.Controls.Add(this.btnDangNhap);
            this.pnlLogin.Controls.Add(this.txtPass);
            this.pnlLogin.Controls.Add(this.txtUser);
            this.pnlLogin.Controls.Add(this.lblPass);
            this.pnlLogin.Controls.Add(this.lblUser);
            this.pnlLogin.Controls.Add(this.lblDangNhap);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlLogin.Location = new System.Drawing.Point(0, 0);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(1000, 650);
            this.pnlLogin.TabIndex = 0;
            // 
            // lblDangNhap
            // 
            this.lblDangNhap.AutoSize = true;
            this.lblDangNhap.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDangNhap.ForeColor = System.Drawing.Color.Blue;
            this.lblDangNhap.Location = new System.Drawing.Point(394, 35);
            this.lblDangNhap.Name = "lblDangNhap";
            this.lblDangNhap.Size = new System.Drawing.Size(293, 60);
            this.lblDangNhap.TabIndex = 1;
            this.lblDangNhap.Text = "ĐĂNG NHẬP";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(217, 147);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(135, 20);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "Tên Đăng Nhập: ";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.ForeColor = System.Drawing.Color.White;
            this.lblPass.Location = new System.Drawing.Point(217, 243);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(90, 20);
            this.lblPass.TabIndex = 3;
            this.lblPass.Text = "Mật Khẩu: ";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(392, 144);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(333, 27);
            this.txtUser.TabIndex = 4;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(392, 243);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(333, 27);
            this.txtPass.TabIndex = 5;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.BackColor = System.Drawing.Color.Lime;
            this.btnDangNhap.Location = new System.Drawing.Point(404, 396);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(279, 37);
            this.btnDangNhap.TabIndex = 6;
            this.btnDangNhap.Text = "ĐĂNG NHẬP";
            this.btnDangNhap.UseVisualStyleBackColor = false;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // lnkGoRegister
            // 
            this.lnkGoRegister.AutoSize = true;
            this.lnkGoRegister.Location = new System.Drawing.Point(547, 322);
            this.lnkGoRegister.Name = "lnkGoRegister";
            this.lnkGoRegister.Size = new System.Drawing.Size(69, 20);
            this.lnkGoRegister.TabIndex = 7;
            this.lnkGoRegister.TabStop = true;
            this.lnkGoRegister.Text = "Đăng ký";
            this.lnkGoRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGoRegister_LinkClicked);
            // 
            // lblThongBao
            // 
            this.lblThongBao.AutoSize = true;
            this.lblThongBao.ForeColor = System.Drawing.Color.White;
            this.lblThongBao.Location = new System.Drawing.Point(353, 322);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(210, 20);
            this.lblThongBao.TabIndex = 8;
            this.lblThongBao.Text = "Nếu chưa có tài khoản, hãy";
            // 
            // pnlRegister
            // 
            this.pnlRegister.Controls.Add(this.textBox1);
            this.pnlRegister.Controls.Add(this.lblXacNhanLai);
            this.pnlRegister.Controls.Add(this.btnDangKy);
            this.pnlRegister.Controls.Add(this.txtPass_Register);
            this.pnlRegister.Controls.Add(this.lblPass_Register);
            this.pnlRegister.Controls.Add(this.txtUser_Register);
            this.pnlRegister.Controls.Add(this.lblUser_Register);
            this.pnlRegister.Controls.Add(this.lblDangKy);
            this.pnlRegister.Controls.Add(this.dtpNgaySinh);
            this.pnlRegister.Controls.Add(this.lblNgaySinh);
            this.pnlRegister.Controls.Add(this.txtGioiTinh);
            this.pnlRegister.Controls.Add(this.lblGioiTinh);
            this.pnlRegister.Controls.Add(this.txtCCCD);
            this.pnlRegister.Controls.Add(this.lblCCCD);
            this.pnlRegister.Controls.Add(this.txtEmail);
            this.pnlRegister.Controls.Add(this.lblEmail);
            this.pnlRegister.Controls.Add(this.txtSDT);
            this.pnlRegister.Controls.Add(this.lblSDT);
            this.pnlRegister.Controls.Add(this.txtHoTen);
            this.pnlRegister.Controls.Add(this.lblHoTen);
            this.pnlRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRegister.Location = new System.Drawing.Point(0, 0);
            this.pnlRegister.Name = "pnlRegister";
            this.pnlRegister.Size = new System.Drawing.Size(1000, 650);
            this.pnlRegister.TabIndex = 10;
            this.pnlRegister.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(441, 445);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(207, 27);
            this.textBox1.TabIndex = 32;
            // 
            // lblXacNhanLai
            // 
            this.lblXacNhanLai.AutoSize = true;
            this.lblXacNhanLai.Location = new System.Drawing.Point(259, 448);
            this.lblXacNhanLai.Name = "lblXacNhanLai";
            this.lblXacNhanLai.Size = new System.Drawing.Size(162, 20);
            this.lblXacNhanLai.TabIndex = 31;
            this.lblXacNhanLai.Text = "Nhập Lại Mật Khẩu: ";
            // 
            // btnDangKy
            // 
            this.btnDangKy.BackColor = System.Drawing.Color.Green;
            this.btnDangKy.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnDangKy.Location = new System.Drawing.Point(392, 499);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(212, 45);
            this.btnDangKy.TabIndex = 30;
            this.btnDangKy.Text = "Đăng Ký";
            this.btnDangKy.UseVisualStyleBackColor = false;
            // 
            // txtPass_Register
            // 
            this.txtPass_Register.Location = new System.Drawing.Point(441, 393);
            this.txtPass_Register.Name = "txtPass_Register";
            this.txtPass_Register.Size = new System.Drawing.Size(207, 27);
            this.txtPass_Register.TabIndex = 29;
            // 
            // lblPass_Register
            // 
            this.lblPass_Register.AutoSize = true;
            this.lblPass_Register.Location = new System.Drawing.Point(259, 396);
            this.lblPass_Register.Name = "lblPass_Register";
            this.lblPass_Register.Size = new System.Drawing.Size(90, 20);
            this.lblPass_Register.TabIndex = 28;
            this.lblPass_Register.Text = "Mật Khẩu: ";
            // 
            // txtUser_Register
            // 
            this.txtUser_Register.Location = new System.Drawing.Point(441, 339);
            this.txtUser_Register.Name = "txtUser_Register";
            this.txtUser_Register.Size = new System.Drawing.Size(207, 27);
            this.txtUser_Register.TabIndex = 27;
            // 
            // lblUser_Register
            // 
            this.lblUser_Register.AutoSize = true;
            this.lblUser_Register.Location = new System.Drawing.Point(259, 342);
            this.lblUser_Register.Name = "lblUser_Register";
            this.lblUser_Register.Size = new System.Drawing.Size(135, 20);
            this.lblUser_Register.TabIndex = 26;
            this.lblUser_Register.Text = "Tên Đăng Nhập: ";
            // 
            // lblDangKy
            // 
            this.lblDangKy.AutoSize = true;
            this.lblDangKy.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDangKy.ForeColor = System.Drawing.Color.Blue;
            this.lblDangKy.Location = new System.Drawing.Point(382, 35);
            this.lblDangKy.Name = "lblDangKy";
            this.lblDangKy.Size = new System.Drawing.Size(222, 60);
            this.lblDangKy.TabIndex = 25;
            this.lblDangKy.Text = "ĐĂNG KÝ";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Location = new System.Drawing.Point(538, 235);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(271, 27);
            this.dtpNgaySinh.TabIndex = 24;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Location = new System.Drawing.Point(425, 240);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(95, 20);
            this.lblNgaySinh.TabIndex = 23;
            this.lblNgaySinh.Text = "Ngày Sinh: ";
            // 
            // txtGioiTinh
            // 
            this.txtGioiTinh.Location = new System.Drawing.Point(538, 177);
            this.txtGioiTinh.Name = "txtGioiTinh";
            this.txtGioiTinh.Size = new System.Drawing.Size(207, 27);
            this.txtGioiTinh.TabIndex = 22;
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Location = new System.Drawing.Point(425, 177);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(86, 20);
            this.lblGioiTinh.TabIndex = 21;
            this.lblGioiTinh.Text = "Giới Tính: ";
            // 
            // txtCCCD
            // 
            this.txtCCCD.Location = new System.Drawing.Point(538, 117);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(207, 27);
            this.txtCCCD.TabIndex = 20;
            // 
            // lblCCCD
            // 
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.Location = new System.Drawing.Point(425, 120);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(68, 20);
            this.lblCCCD.TabIndex = 19;
            this.lblCCCD.Text = "CCCD: ";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(123, 177);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(207, 27);
            this.txtEmail.TabIndex = 18;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(21, 180);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(61, 20);
            this.lblEmail.TabIndex = 17;
            this.lblEmail.Text = "Email: ";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(123, 237);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(207, 27);
            this.txtSDT.TabIndex = 16;
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(21, 240);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(52, 20);
            this.lblSDT.TabIndex = 15;
            this.lblSDT.Text = "SĐT: ";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(123, 117);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(207, 27);
            this.txtHoTen.TabIndex = 14;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(21, 120);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(96, 20);
            this.lblHoTen.TabIndex = 13;
            this.lblHoTen.Text = "Họ và Tên: ";
            // 
            // ucLogIn_Out
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.pnlRegister);
            this.Controls.Add(this.pnlLogin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucLogIn_Out";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.ucLogIn_Out_Load);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlRegister.ResumeLayout(false);
            this.pnlRegister.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Label lblDangNhap;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.LinkLabel lnkGoRegister;
        private System.Windows.Forms.Panel pnlRegister;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblXacNhanLai;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.TextBox txtPass_Register;
        private System.Windows.Forms.Label lblPass_Register;
        private System.Windows.Forms.TextBox txtUser_Register;
        private System.Windows.Forms.Label lblUser_Register;
        private System.Windows.Forms.Label lblDangKy;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.TextBox txtGioiTinh;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.Label lblCCCD;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblHoTen;
    }
}
