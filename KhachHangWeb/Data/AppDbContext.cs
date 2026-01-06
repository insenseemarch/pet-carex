using System;
using System.Collections.Generic;
using KhachHangWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace KhachHangWeb.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<chinhanh> chinhanhs { get; set; }

    public virtual DbSet<chitietkhambenh> chitietkhambenhs { get; set; }

    public virtual DbSet<chitietmuasanpham> chitietmuasanphams { get; set; }

    public virtual DbSet<chitiettiemphong> chitiettiemphongs { get; set; }

    public virtual DbSet<danhgium> danhgia { get; set; }

    public virtual DbSet<dichvu> dichvus { get; set; }

    public virtual DbSet<hoadon> hoadons { get; set; }

    public virtual DbSet<khachhang> khachhangs { get; set; }

    public virtual DbSet<lichsulamviec> lichsulamviecs { get; set; }

    public virtual DbSet<nhanvien> nhanviens { get; set; }

    public virtual DbSet<sanpham> sanphams { get; set; }

    public virtual DbSet<taikhoankhachhang> taikhoankhachhangs { get; set; }

    public virtual DbSet<taikhoannhanvien> taikhoannhanviens { get; set; }

    public virtual DbSet<thucung> thucungs { get; set; }

    public virtual DbSet<thuocsudung> thuocsudungs { get; set; }

    public virtual DbSet<tiemgoi> tiemgois { get; set; }

    public virtual DbSet<tiemphong> tiemphongs { get; set; }

    public virtual DbSet<toathuoc> toathuocs { get; set; }

    public virtual DbSet<vacxin> vacxins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=petcarx;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<chinhanh>(entity =>
        {
            entity.HasKey(e => e.macn).HasName("PK__chinhanh__7A21F8407E8AD318");

            entity.ToTable("chinhanh");

            entity.HasIndex(e => e.sdt, "UQ__chinhanh__DDDFB483A43CC479").IsUnique();

            entity.Property(e => e.macn)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.diachi).HasMaxLength(255);
            entity.Property(e => e.sdt)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.tencn).HasMaxLength(100);

            entity.HasMany(d => d.madvs).WithMany(p => p.macns)
                .UsingEntity<Dictionary<string, object>>(
                    "dichvutaichinhanh",
                    r => r.HasOne<dichvu>().WithMany()
                        .HasForeignKey("madv")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_dvcn_dv"),
                    l => l.HasOne<chinhanh>().WithMany()
                        .HasForeignKey("macn")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_dvcn_cn"),
                    j =>
                    {
                        j.HasKey("macn", "madv").HasName("PK__dichvuta__FD83E642CBFC92AE");
                        j.ToTable("dichvutaichinhanh");
                        j.IndexerProperty<string>("macn")
                            .HasMaxLength(10)
                            .IsUnicode(false);
                        j.IndexerProperty<string>("madv")
                            .HasMaxLength(10)
                            .IsUnicode(false);
                    });

            entity.HasMany(d => d.masps).WithMany(p => p.macns)
                .UsingEntity<Dictionary<string, object>>(
                    "sanphamtaichinhanh",
                    r => r.HasOne<sanpham>().WithMany()
                        .HasForeignKey("masp")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_spcn_sp"),
                    l => l.HasOne<chinhanh>().WithMany()
                        .HasForeignKey("macn")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_spcn_cn"),
                    j =>
                    {
                        j.HasKey("macn", "masp").HasName("PK__sanphamt__4D83EF270D2999C0");
                        j.ToTable("sanphamtaichinhanh");
                        j.IndexerProperty<string>("macn")
                            .HasMaxLength(10)
                            .IsUnicode(false);
                        j.IndexerProperty<string>("masp")
                            .HasMaxLength(10)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<chitietkhambenh>(entity =>
        {
            entity.HasKey(e => new { e.madv, e.mathucung, e.ngaysudung, e.mabs }).HasName("PK__chitietk__941382613D049266");

            entity.ToTable("chitietkhambenh", tb => tb.HasTrigger("trg_only_bs_kham"));

            entity.Property(e => e.madv)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.mathucung)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ngaysudung).HasColumnType("datetime");
            entity.Property(e => e.mabs)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.chandoan).HasMaxLength(255);
            entity.Property(e => e.ghichu).HasMaxLength(255);
            entity.Property(e => e.madanhgia)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.matoathuoc)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.trieuchung).HasMaxLength(255);

            entity.HasOne(d => d.mabsNavigation).WithMany(p => p.chitietkhambenhs)
                .HasForeignKey(d => d.mabs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ckb_bs");

            entity.HasOne(d => d.madanhgiaNavigation).WithMany(p => p.chitietkhambenhs)
                .HasForeignKey(d => d.madanhgia)
                .HasConstraintName("fk_ckb_dg");

            entity.HasOne(d => d.madvNavigation).WithMany(p => p.chitietkhambenhs)
                .HasForeignKey(d => d.madv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ckb_dv");

            entity.HasOne(d => d.mathucungNavigation).WithMany(p => p.chitietkhambenhs)
                .HasForeignKey(d => d.mathucung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ckb_tc");

            entity.HasOne(d => d.matoathuocNavigation).WithMany(p => p.chitietkhambenhs)
                .HasForeignKey(d => d.matoathuoc)
                .HasConstraintName("fk_ckb_toa");
        });

        modelBuilder.Entity<chitietmuasanpham>(entity =>
        {
            entity.HasKey(e => new { e.mahd, e.masp }).HasName("PK__chitietm__4D8317B9E0448974");

            entity.ToTable("chitietmuasanpham");

            entity.Property(e => e.mahd)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.masp)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.thanhtien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.mahdNavigation).WithMany(p => p.chitietmuasanphams)
                .HasForeignKey(d => d.mahd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ctm_hd");

            entity.HasOne(d => d.maspNavigation).WithMany(p => p.chitietmuasanphams)
                .HasForeignKey(d => d.masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ctm_sp");
        });

        modelBuilder.Entity<chitiettiemphong>(entity =>
        {
            entity.HasKey(e => new { e.stt, e.madv, e.mathucung, e.mavacxin, e.mabs }).HasName("PK__chitiett__008B4FEBB14D3300");

            entity.ToTable("chitiettiemphong", tb => tb.HasTrigger("trg_tiemphong_vacxin_het_han"));

            entity.Property(e => e.madv)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.mathucung)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.mavacxin)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.mabs)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.madanhgia)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.trangthai).HasMaxLength(50);

            entity.HasOne(d => d.mabsNavigation).WithMany(p => p.chitiettiemphongs)
                .HasForeignKey(d => d.mabs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cttp_bs");

            entity.HasOne(d => d.madanhgiaNavigation).WithMany(p => p.chitiettiemphongs)
                .HasForeignKey(d => d.madanhgia)
                .HasConstraintName("fk_cttp_dg");

            entity.HasOne(d => d.madvNavigation).WithMany(p => p.chitiettiemphongs)
                .HasForeignKey(d => d.madv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cttp_dv");

            entity.HasOne(d => d.mathucungNavigation).WithMany(p => p.chitiettiemphongs)
                .HasForeignKey(d => d.mathucung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cttp_tc");

            entity.HasOne(d => d.mavacxinNavigation).WithMany(p => p.chitiettiemphongs)
                .HasForeignKey(d => d.mavacxin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cttp_vx");
        });

        modelBuilder.Entity<danhgium>(entity =>
        {
            entity.HasKey(e => e.madanhgia).HasName("PK__danhgia__7E089258C5F2C457");

            entity.Property(e => e.madanhgia)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.binhluan).HasMaxLength(255);
            entity.Property(e => e.madv)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.makh)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.manv)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.madvNavigation).WithMany(p => p.danhgia)
                .HasForeignKey(d => d.madv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dg_dv");

            entity.HasOne(d => d.makhNavigation).WithMany(p => p.danhgia)
                .HasForeignKey(d => d.makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dg_kh");

            entity.HasOne(d => d.manvNavigation).WithMany(p => p.danhgia)
                .HasForeignKey(d => d.manv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dg_nv");
        });

        modelBuilder.Entity<dichvu>(entity =>
        {
            entity.HasKey(e => e.madv).HasName("PK__dichvu__7A21E029D743258D");

            entity.ToTable("dichvu");

            entity.Property(e => e.madv)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.loai).HasMaxLength(50);
            entity.Property(e => e.tendv).HasMaxLength(100);
        });

        modelBuilder.Entity<hoadon>(entity =>
        {
            entity.HasKey(e => e.mahd).HasName("PK__hoadon__7A2100DEEB251EE3");

            entity.ToTable("hoadon", tb =>
                {
                    tb.HasTrigger("trg_hoadon_cong_diem");
                    tb.HasTrigger("trg_hoadon_tru_tonkho");
                    tb.HasTrigger("trg_kiemsoat_nv_lap_hoadon");
                });

            entity.Property(e => e.mahd)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.hinhthucthanhtoan).HasMaxLength(50);
            entity.Property(e => e.khuyenmai).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.macn)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.makh)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.makham)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.manvlap)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.mathucung)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.matiem)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ngaylap).HasColumnType("datetime");
            entity.Property(e => e.thanhtien)
                .HasComputedColumnSql("([tongtien]-[khuyenmai])", true)
                .HasColumnType("decimal(19, 2)");
            entity.Property(e => e.tongtien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.trangthai).HasMaxLength(50);

            entity.HasOne(d => d.macnNavigation).WithMany(p => p.hoadons)
                .HasForeignKey(d => d.macn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_hd_cn");

            entity.HasOne(d => d.makhNavigation).WithMany(p => p.hoadons)
                .HasForeignKey(d => d.makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_hd_kh");

            entity.HasOne(d => d.makhamNavigation).WithMany(p => p.hoadonmakhamNavigations)
                .HasForeignKey(d => d.makham)
                .HasConstraintName("fk_hd_makham");

            entity.HasOne(d => d.manvlapNavigation).WithMany(p => p.hoadons)
                .HasForeignKey(d => d.manvlap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_hd_nv");

            entity.HasOne(d => d.mathucungNavigation).WithMany(p => p.hoadons)
                .HasForeignKey(d => d.mathucung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_hd_tc");

            entity.HasOne(d => d.matiemNavigation).WithMany(p => p.hoadonmatiemNavigations)
                .HasForeignKey(d => d.matiem)
                .HasConstraintName("fk_hd_matiem");
        });

        modelBuilder.Entity<khachhang>(entity =>
        {
            entity.HasKey(e => e.makh).HasName("PK__khachhan__7A21BB4C8BAD46ED");

            entity.ToTable("khachhang");

            entity.HasIndex(e => e.cccd, "UQ__khachhan__37D42BFA26AD3400").IsUnique();

            entity.HasIndex(e => e.email, "UQ__khachhan__AB6E6164AE221447").IsUnique();

            entity.HasIndex(e => e.sdt, "UQ__khachhan__DDDFB48330C94A2F").IsUnique();

            entity.Property(e => e.makh)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.cccd)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.gioitinh).HasMaxLength(10);
            entity.Property(e => e.hoten).HasMaxLength(100);
            entity.Property(e => e.sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<lichsulamviec>(entity =>
        {
            entity.HasKey(e => new { e.manv, e.ngaybdtaicnmoi }).HasName("PK__lichsula__3AD8F63BC9F5A183");

            entity.ToTable("lichsulamviec");

            entity.Property(e => e.manv)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.macncu)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.macnmoi)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.macncuNavigation).WithMany(p => p.lichsulamviecmacncuNavigations)
                .HasForeignKey(d => d.macncu)
                .HasConstraintName("fk_lslv_cncu");

            entity.HasOne(d => d.macnmoiNavigation).WithMany(p => p.lichsulamviecmacnmoiNavigations)
                .HasForeignKey(d => d.macnmoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_lslv_cnmoi");

            entity.HasOne(d => d.manvNavigation).WithMany(p => p.lichsulamviecs)
                .HasForeignKey(d => d.manv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_lslv_nhanvien");
        });

        modelBuilder.Entity<nhanvien>(entity =>
        {
            entity.HasKey(e => e.manv).HasName("PK__nhanvien__7A21B37DE92F40F9");

            entity.ToTable("nhanvien", tb => tb.HasTrigger("trg_1_quanly_moi_chinhanh"));

            entity.Property(e => e.manv)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.chucvu).HasMaxLength(50);
            entity.Property(e => e.gioitinh).HasMaxLength(10);
            entity.Property(e => e.hoten).HasMaxLength(100);
            entity.Property(e => e.loainv).HasMaxLength(50);
            entity.Property(e => e.luongcoban).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.macn)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.macnNavigation).WithMany(p => p.nhanviens)
                .HasForeignKey(d => d.macn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_nv_cn");
        });

        modelBuilder.Entity<sanpham>(entity =>
        {
            entity.HasKey(e => e.masp).HasName("PK__sanpham__7A2176721D408FEC");

            entity.ToTable("sanpham");

            entity.Property(e => e.masp)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.loaisp).HasMaxLength(50);
            entity.Property(e => e.tensp).HasMaxLength(100);
        });

        modelBuilder.Entity<taikhoankhachhang>(entity =>
        {
            entity.HasKey(e => e.tendangnhap).HasName("PK__taikhoan__CE900A1F8C1A5623");

            entity.ToTable("taikhoankhachhang");

            entity.Property(e => e.tendangnhap)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.capbac).HasMaxLength(50);
            entity.Property(e => e.makh)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.matkhau)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.trangthai).HasMaxLength(50);

            entity.HasOne(d => d.makhNavigation).WithMany(p => p.taikhoankhachhangs)
                .HasForeignKey(d => d.makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tkkh_kh");
        });

        modelBuilder.Entity<taikhoannhanvien>(entity =>
        {
            entity.HasKey(e => e.tendangnhap).HasName("PK__taikhoan__CE900A1FAD233A32");

            entity.ToTable("taikhoannhanvien");

            entity.HasIndex(e => e.manv, "UQ__taikhoan__7A21B37CAC9D4317").IsUnique();

            entity.Property(e => e.tendangnhap)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.manv)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.matkhau)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.trangthai).HasMaxLength(50);
            entity.Property(e => e.vaitro).HasMaxLength(50);

            entity.HasOne(d => d.manvNavigation).WithOne(p => p.taikhoannhanvien)
                .HasForeignKey<taikhoannhanvien>(d => d.manv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tknv_nv");
        });

        modelBuilder.Entity<thucung>(entity =>
        {
            entity.HasKey(e => e.mathucung).HasName("PK__thucung__CED5B003C7E09D48");

            entity.ToTable("thucung");

            entity.Property(e => e.mathucung)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.gioitinh).HasMaxLength(10);
            entity.Property(e => e.giong).HasMaxLength(50);
            entity.Property(e => e.loai).HasMaxLength(50);
            entity.Property(e => e.makh)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.tenthucung).HasMaxLength(100);
            entity.Property(e => e.tinhtrangsuckhoe).HasMaxLength(255);

            entity.HasOne(d => d.makhNavigation).WithMany(p => p.thucungs)
                .HasForeignKey(d => d.makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tc_kh");
        });

        modelBuilder.Entity<thuocsudung>(entity =>
        {
            entity.HasKey(e => new { e.matoathuoc, e.masp }).HasName("PK__thuocsud__DA7E14B85D4636E4");

            entity.ToTable("thuocsudung", tb => tb.HasTrigger("trg_thuocsudung_het_han"));

            entity.Property(e => e.matoathuoc)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.masp)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ghichu).HasMaxLength(255);
            entity.Property(e => e.lieuluong).HasMaxLength(100);

            entity.HasOne(d => d.maspNavigation).WithMany(p => p.thuocsudungs)
                .HasForeignKey(d => d.masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tsd_sptcn");

            entity.HasOne(d => d.matoathuocNavigation).WithMany(p => p.thuocsudungs)
                .HasForeignKey(d => d.matoathuoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tsd_toa");
        });

        modelBuilder.Entity<tiemgoi>(entity =>
        {
            entity.HasKey(e => e.madv).HasName("PK__tiemgoi__7A21E029AAA03CEA");

            entity.ToTable("tiemgoi");

            entity.Property(e => e.madv)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.phantramgiamgia).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.madvNavigation).WithOne(p => p.tiemgoi)
                .HasForeignKey<tiemgoi>(d => d.madv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tg_dv");
        });

        modelBuilder.Entity<tiemphong>(entity =>
        {
            entity.HasKey(e => e.madv).HasName("PK__tiemphon__7A21E02985CC5862");

            entity.ToTable("tiemphong");

            entity.Property(e => e.madv)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.lieuluong)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.madvNavigation).WithOne(p => p.tiemphong)
                .HasForeignKey<tiemphong>(d => d.madv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tp_dv");
        });

        modelBuilder.Entity<toathuoc>(entity =>
        {
            entity.HasKey(e => e.matoathuoc).HasName("PK__toathuoc__EDDC03DF66A3C372");

            entity.ToTable("toathuoc");

            entity.Property(e => e.matoathuoc)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.tentoathuoc).HasMaxLength(100);
        });

        modelBuilder.Entity<vacxin>(entity =>
        {
            entity.HasKey(e => e.mavacxin).HasName("PK__vacxin__0CCF50DC44FB32E1");

            entity.ToTable("vacxin");

            entity.Property(e => e.mavacxin)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.loaivacxin).HasMaxLength(50);
            entity.Property(e => e.tenvacxin).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
