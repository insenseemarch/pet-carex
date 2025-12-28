-- =========================================
-- database: petcarex
-- =========================================

create database petcarex;
go

use petcarex;
go

-- =========================================
-- bảng chinhanh
-- lưu thông tin các chi nhánh phòng khám
-- =========================================
create table chinhanh 
(
    macn varchar(10) primary key,
    tencn nvarchar(100) not null,
    diachi nvarchar(255) not null,
    sdt varchar(15) not null,
    giomocua time not null,
    giodongcua time not null
)

-- =========================================
-- bảng nhanvien
-- thông tin nhân viên tại chi nhánh
-- =========================================
create table nhanvien (
    manv varchar(10) primary key,
    macn varchar(10) not null,
    hoten nvarchar(100) not null,
    ngaysinh date not null,
    gioitinh nvarchar(10),
    ngayvaolam date not null,
    chucvu nvarchar(50) not null,
    luongcoban decimal(18,2) not null,
    loainv nvarchar(50) not null
)

-- =========================================
-- bảng taikhoannhanvien
-- tài khoản đăng nhập vào hệ thống của nhân viên
-- =========================================
create table taikhoannhanvien (
    tendangnhap varchar(50) primary key,
    manv varchar(10) not null,
    matkhau varchar(255) not null,
    vaitro nvarchar(50) not null,
    trangthai nvarchar(50) not null
);

-- =========================================
-- bảng khachhang
-- lưu thông tin khách hàng
-- =========================================
create table khachhang (
    makh varchar(10) primary key,
    hoten nvarchar(100) not null,
    sdt varchar(15) not null,
    email varchar(100),
    cccd varchar(20),
    gioitinh nvarchar(10),
    ngaysinh date
) 

-- =========================================
-- bảng taikhoankhachhang
-- tài khoản đăng nhập vào hệ thống của khách hàng
-- =========================================
create table taikhoankhachhang 
(
    tendangnhap varchar(50) primary key,
    makh varchar(10) not null,
    matkhau varchar(255) not null,
    diemtichluy int not null,
    capbac nvarchar(50) not null,
    trangthai nvarchar(50) not null
)

-- =========================================
-- bảng thucung
-- thông tin thú cưng của khách hàng
-- =========================================
create table thucung 
(
    mathucung varchar(10) primary key,
    makh varchar(10) not null,
    tenthucung nvarchar(100) not null,
    loai nvarchar(50) not null,
    giong nvarchar(50),
    ngaysinh date,
    gioitinh nvarchar(10),
    tinhtrangsuckhoe nvarchar(255)
)

-- =========================================
-- bảng dichvu
-- thông tin dịch vụ toàn hệ thống
-- =========================================
create table dichvu 
(
    madv varchar(10) primary key,
    tendv nvarchar(100) not null,
    loai nvarchar(50) not null,
    gia decimal(18,2) not null
)

-- =========================================
-- bảng sanpham
-- sản phẩm và thuốc bán của hệ thống
-- =========================================
create table sanpham 
(
    masp varchar(10) primary key,
    tensp nvarchar(100) not null,
    loaisp nvarchar(50) not null,
    gia decimal(18,2) not null,
    soluongton int not null,
    hsd date
)

-- =========================================
-- bảng vacxin
-- thông tin vắc-xin được dùng để tiêm của hệ thống
-- =========================================
create table vacxin 
(
    mavacxin varchar(10) primary key,
    tenvacxin nvarchar(100) not null,
    loaivacxin nvarchar(50),
    ngaysanxuat date not null,
    ngayhethan date not null
)

-- =========================================
-- bảng lichsulamviec
-- lưu lịch sử chuyển chi nhánh của nhân viên
-- =========================================
create table lichsulamviec 
(
    manv varchar(10) not null,
    ngaybdtaicnmoi date not null,
    macncu varchar(10),
    macnmoi varchar(10) not null,
    ngayketthuccncu date,
    primary key (manv, ngaybdtaicnmoi),
    constraint fk_lslv_nhanvien
        foreign key (manv) references nhanvien(manv),
    constraint fk_lslv_cncu
        foreign key (macncu) references chinhanh(macn),
    constraint fk_lslv_cnmoi
        foreign key (macnmoi) references chinhanh(macn),
    constraint ck_lslv_ngay
        check (ngayketthuccncu is null or ngaybdtaicnmoi >= ngayketthuccncu)
)

-- =========================================
-- bảng dichvutaichinhanh
-- xác định từng chi nhánh có các loại dịch vụ nào
-- =========================================
create table dichvutaichinhanh 
(
    macn varchar(10) not null,
    madv varchar(10) not null,
    primary key (macn, madv),
    constraint fk_dvcn_cn
        foreign key (macn) references chinhanh(macn),
    constraint fk_dvcn_dv
        foreign key (madv) references dichvu(madv)
)

-- =========================================
-- bảng sanphamtaichinhanh
-- xác định từng chi nhánh có bán các sản phẩm nào
-- =========================================
create table sanphamtaichinhanh 
(
    macn varchar(10) not null,
    masp varchar(10) not null,
    primary key (macn, masp),
    constraint fk_spcn_cn
        foreign key (macn) references chinhanh(macn),
    constraint fk_spcn_sp
        foreign key (masp) references sanpham(masp)
)

-- =========================================
-- bảng toathuoc
-- thông tin toa thuốc
-- =========================================
create table toathuoc 
(
    matoathuoc varchar(10) primary key,
    tentoathuoc nvarchar(100) not null
)

-- =========================================
-- bảng thuocsudung
-- chi tiết thuốc được kê trong toa
-- =========================================
create table thuocsudung 
(
    matoathuoc varchar(10) not null,
    masp varchar(10) not null,
    soluong int not null,
    lieuluong nvarchar(100),
    ghichu nvarchar(255),
    primary key (matoathuoc, masp),
    constraint fk_tsd_toa
        foreign key (matoathuoc) references toathuoc(matoathuoc),
    constraint fk_tsd_sp
        foreign key (masp) references sanpham(masp),
    constraint ck_tsd_soluong
        check (soluong > 0)
)

-- =========================================
-- bảng chitietkhambenh
-- lưu thông tin của một lần khám bệnh
-- =========================================
create table chitietkhambenh 
(
    madv varchar(10) not null,
    mathucung varchar(10) not null,
    ngaysudung date not null,
    mabs varchar(10) not null,
    trieuchung nvarchar(255),
    chandoan nvarchar(255),
    matoathuoc varchar(10),
    ngaytaikham date,
    ghichu nvarchar(255),
    primary key (madv, mathucung, ngaysudung, mabs),
    constraint fk_ckb_dv
        foreign key (madv) references dichvu(madv),
    constraint fk_ckb_tc
        foreign key (mathucung) references thucung(mathucung),
    constraint fk_ckb_bs
        foreign key (mabs) references nhanvien(manv),
    constraint fk_ckb_toa
        foreign key (matoathuoc) references toathuoc(matoathuoc),
    constraint ck_ckb_ngaytaikham
        check (ngaytaikham is null or ngaytaikham > ngaysudung)
)

-- =========================================
-- bảng tiemphong
-- thông tin về gói tiêm của một bệnh
-- =========================================
create table tiemphong
(
    madv varchar(10) primary key,
    lieuluong varchar(10),
    constraint fk_tp_dv
        foreign key (madv) references dichvu(madv)
)

-- =========================================
-- bảng tiemgoi
-- thông tin của các gói tiêm theo tháng
-- =========================================
create table tiemgoi 
(
    madv varchar(10) primary key,
    sothang int not null,
    constraint fk_tg_dv
        foreign key (madv) references dichvu(madv),
    constraint ck_tg_sothang
        check (sothang > 0)
)

-- =========================================
-- bảng chitiettiemphong
-- lịch sử tiêm phòng của thú cưng
-- =========================================
create table chitiettiemphong 
(
    stt int not null,
    madv varchar(10) not null,
    mathucung varchar(10) not null,
    mavacxin varchar(10) not null,
    mabs varchar(10) not null,
    ngaytiem date not null,
    trangthai nvarchar(50),
    madanhgia varchar(10),
    primary key (stt, madv, mathucung, mavacxin, mabs),
    constraint fk_cttp_dv
        foreign key (madv) references dichvu(madv),
    constraint fk_cttp_tc
        foreign key (mathucung) references thucung(mathucung),
    constraint fk_cttp_vx
        foreign key (mavacxin) references vacxin(mavacxin),
    constraint fk_cttp_bs
        foreign key (mabs) references nhanvien(manv)
)

-- =========================================
-- bảng hoadon
-- lập hóa đơn cho khách hàng
-- =========================================
create table hoadon 
(
    mahd varchar(10) primary key,
    mathucung varchar(10) not null,
    manvlap varchar(10) not null,
    macn varchar(10) not null,
    makh varchar(10) not null,
    makham varchar(10),
    matiem varchar(10),
    ngaylap date not null,
    tongtien decimal(18,2) not null,
    khuyenmai decimal(18,2) not null,
    thanhtien decimal(18,2) not null,
    hinhthucthanhtoan nvarchar(50),
    trangthai nvarchar(50) not null,
    constraint fk_hd_tc
        foreign key (mathucung) references thucung(mathucung),
    constraint fk_hd_nv
        foreign key (manvlap) references nhanvien(manv),
    constraint fk_hd_cn
        foreign key (macn) references chinhanh(macn),
    constraint fk_hd_kh
        foreign key (makh) references khachhang(makh),
    constraint ck_hd_thanhtien
        check (thanhtien = tongtien - khuyenmai)
)

-- =========================================
-- bảng chitietmuasanpham
-- chi tiết mua sản phẩm của khách hàng theo hóa đơn
-- =========================================
create table chitietmuasanpham 
(
    mahd varchar(10) not null,
    masp varchar(10) not null,
    soluong int not null,
    thanhtien decimal(18,2) not null,
    primary key (mahd, masp),
    constraint fk_ctm_hd
        foreign key (mahd) references hoadon(mahd),
    constraint fk_ctm_sp
        foreign key (masp) references sanpham(masp),
    constraint ck_ctm_soluong
        check (soluong > 0)
)

-- =========================================
-- bảng danhgia
-- đánh giá chất lượng phục vụ
-- =========================================
create table danhgia 
(
    madanhgia varchar(10) primary key,
    madv varchar(10) not null,
    manv varchar(10) not null,
    makh varchar(10) not null,
    ngaydanhgia date not null,
    diemchatluongdv int not null,
    diemthaidonv int not null,
    mucdohailong int not null,
    binhluan nvarchar(255),
    constraint fk_dg_dv
        foreign key (madv) references dichvu(madv),
    constraint fk_dg_nv
        foreign key (manv) references nhanvien(manv),
    constraint fk_dg_kh
        foreign key (makh) references khachhang(makh),
    constraint ck_dg_diem
        check (
            diemchatluongdv between 1 and 5 and
            diemthaidonv between 1 and 5 and
            mucdohailong between 1 and 5
        )
)
go

-- =========================================
-- trigger: chặn kê thuốc hết hạn
-- =========================================
create trigger trg_thuocsudung_het_han
on thuocsudung
after insert, update
as
begin
    if exists (
        select 1
        from inserted i
        join sanpham sp on i.masp = sp.masp
        where sp.hsd is not null
          and sp.hsd < cast(getdate() as date)
    )
    begin
        raiserror (N'Thuốc đã hết hạn, không thể kê toa.', 16, 1);
        rollback tran
    end
end;
go

-- =========================================
-- trigger: chặn sử dụng vắc-xin hết hạn
-- =========================================
create trigger trg_tiemphong_vacxin_het_han
on chitiettiemphong
after insert, update
as
begin
    if exists (
        select 1
        from inserted i
        join vacxin v on i.mavacxin = v.mavacxin
        where v.ngayhethan < cast(getdate() as date)
    )
    begin
        raiserror (N'Vắc-xin đã hết hạn, không thể dùng để tiêm.', 16, 1);
        rollback tran
    end
end;
go


-- =========================================
-- trigger: trừ tồn kho sau khi tính hóa đơn
-- =========================================
create trigger trg_hoadon_tru_tonkho
on hoadon
after update
as
begin
    if exists (
        select 1
        from inserted i
        where i.trangthai = N'Đã thanh toán'
    )
    begin
        update sp
        set sp.soluongton = sp.soluongton - ctm.soluong
        from sanpham sp
        join chitietmuasanpham ctm on sp.masp = ctm.masp
        join inserted i on ctm.mahd = i.mahd;
    end
end;
go

-- =========================================
-- trigger: cộng điểm tích lũy cho khách hàng
-- hiện tại: 1 điểm = 50.000 vnđ
-- =========================================
create trigger trg_hoadon_cong_diem
on hoadon
after update
as
begin
    update tkkh
    set diemtichluy = diemtichluy + (i.thanhtien / 50000)
    from taikhoankhachhang tkkh
    join inserted i on tkkh.makh = i.makh
    where i.trangthai = N'Đã thanh toán';
end;
go

/*
=====================================================
cac rang buoc nghiep vu chua cai trong script nay
(se thuc hien o giai doan 2 – muc vat ly / hieu nang)
=====================================================

3. kiem soat nhan vien lap hoa don
   - manvlap chi duoc la 'le tan' hoac 'ban hang'
   ly do:
   - phu thuoc vai tro, nen xu ly bang procedure / app

4. rang buoc mot dich vu chi duoc danh gia mot lan
   - rang buoc (madv, makh, mathucung) la duy nhat
   ly do:
   - can xu ly theo trang thai hoan tat + thanh toan

5. rang buoc hoa don – ngay su dung dich vu
   - ngaysudung khong duoc lon hon ngaylap
   ly do:
   - lien bang, can trigger theo nghiep vu
=====================================================
*/


-- ==================
create procedure sp_doanhthu_chinhanh_thang
    @macn varchar(10),
    @thang int,
    @nam int
as
begin
    select
        macn,
        sum(thanhtien) as doanhthu
    from hoadon
    where macn = @macn
      and month(ngaylap) = @thang
      and year(ngaylap) = @nam
      and trangthai = N'Đã thanh toán'
    group by macn;
end;
go
-- ==================

