-- =========================================
-- database: pet-carx
-- =========================================

create database petcarx;
go

use petcarx;
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
    sdt varchar(15) unique not null,
    giomocua time not null,
    giodongcua time not null,

    constraint c_gio
        check(giomocua < giodongcua)
)

-- =========================================
-- bảng nhanvien
-- thông tin nhân viên tại chi nhánh
-- =========================================
create table nhanvien 
(
    manv varchar(10) primary key,
    macn varchar(10) not null,
    hoten nvarchar(100) not null,
    ngaysinh date not null,
    gioitinh nvarchar(10),
    ngayvaolam date not null,
    chucvu nvarchar(50) not null,
    luongcoban decimal(18,2) not null,
    loainv nvarchar(50) not null,

    constraint fk_nv_cn
        foreign key (macn) references chinhanh(macn),

    constraint ck_nv_ngay
        check (ngaysinh < ngayvaolam),

    constraint ck_nv_tuoi
        check (datediff(year, ngaysinh, getdate()) >= 18),

    constraint ck_nv_gioitinh
        check (gioitinh in (N'Nam', N'Nữ', N'Khác')),

    constraint ck_nv_chucvu
        check (chucvu in (
            N'Nhân viên',
            N'Quản lý',
            N'Giám đốc'
        )),

    constraint ck_nv_loainv
        check (loainv in (
            N'Quản lý chi nhánh',
            N'Nhân viên tiếp tân',
            N'Bác sĩ thú y',
            N'Giám đốc',
            N'Nhân viên bán hàng'
        )),

    constraint ck_nv_luong
        check (luongcoban > 0)
)

-- =========================================
-- bảng taikhoannhanvien
-- tài khoản đăng nhập vào hệ thống của nhân viên
-- =========================================
create table taikhoannhanvien 
(
    tendangnhap varchar(50) primary key,
    manv varchar(10) unique not null,
    matkhau varchar(255) not null,
    vaitro nvarchar(50) not null,
    trangthai nvarchar(50) not null,

    constraint fk_tknv_nv
        foreign key (manv) references nhanvien(manv),

    constraint ck_tknv_vaitro
        check (vaitro in (
            N'Quản lý chi nhánh',
            N'Nhân viên tiếp tân',
            N'Bác sĩ thú y',
            N'Giám đốc'
        )),

    constraint ck_tknv_trangthai
        check (trangthai in (
            N'Đang hoạt động',
            N'Vô hiệu hóa'
        ))
)

-- =========================================
-- bảng khachhang
-- lưu thông tin khách hàng
-- =========================================
create table khachhang 
(
    makh varchar(10) primary key,
    hoten nvarchar(100) not null,
    sdt char(10) unique not null,
    email varchar(100) unique,
    cccd varchar(20) unique,
    gioitinh nvarchar(10),
    ngaysinh date,

    constraint ck_kh_gioitinh
        check (gioitinh in (N'Nam', N'Nữ', N'Khác'))
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
    trangthai nvarchar(50) not null,

    constraint fk_tkkh_kh
        foreign key (makh) references khachhang(makh),

    constraint ck_tkkh_trangthai
        check (trangthai in (
            N'Đang hoạt động',
            N'Vô hiệu hóa'
        )),

    constraint ck_tkkh_capbac
        check (capbac in (
            N'Cơ bản',
            N'Thân thiết',
            N'VIP'
        ))
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
    giong nvarchar(50) not null,
    ngaysinh date,
    gioitinh nvarchar(10),
    tinhtrangsuckhoe nvarchar(255),

    constraint fk_tc_kh
        foreign key (makh) references khachhang(makh),

    constraint ck_tc_gioitinh
        check (gioitinh in (N'Đực', N'Cái', N'Không rõ'))
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
    gia decimal(18,2) not null,

    constraint ck_dv_loai
        check (loai in (N'Khám bệnh', N'Tiêm phòng')),

    constraint ck_dv_gia
        check (gia > 0)
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
    hsd date,

    constraint ck_sp_soluongton
        check(soluongton >= 0),

    constraint ck_sp_loaisp
        check (loaisp in (N'Thức ăn', N'Thuốc', N'Phụ kiện')),

    constraint ck_sp_gia
        check(gia > 0)
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
    ngayhethan date not null,

    constraint c_vacxin
        check(ngaysanxuat < ngayhethan)
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
        foreign key (macnmoi) references chinhanh(macn)
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

    constraint fk_tsd_sptcn
        foreign key (masp) references sanpham(masp),

    constraint ck_tsd_soluong
        check (soluong > 0)
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
    phantramgiamgia decimal(5,2),
    constraint fk_tg_dv 
        foreign key (madv) references tiemphong(madv),

    constraint ck_tg_sothang
        check (sothang > 0)
)

-- =========================================
-- bảng danhgia
-- đánh giá chất lượng phục vụ
-- =========================================
create table danhgia 
(
    madanhgia varchar(10),
    madv varchar(10) not null,
    manv varchar(10) not null,
    makh varchar(10) not null,
    ngaydanhgia datetime not null,
    diemchatluongdv int not null,
    diemthaidonv int not null,
    mucdohailong int not null,
    binhluan nvarchar(255),

    constraint pk_danhgia
        primary key nonclustered (madanhgia, ngaydanhgia),

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
        ),

    constraint ck_dg_ngaydanhgia
        check (ngaydanhgia <= getdate())
)

create unique nonclustered index ux_dg_madanhgia
on danhgia (madanhgia)

-- =========================================
-- bảng chitietkhambenh
-- lưu thông tin của một lần khám bệnh
-- =========================================
create table chitietkhambenh 
(
    madv varchar(10) not null,
    mathucung varchar(10) not null,
    ngaysudung datetime not null,
    mabs varchar(10) not null,
    trieuchung nvarchar(255),
    chandoan nvarchar(255),
    matoathuoc varchar(10),
    ngaytaikham date,
    madanhgia varchar(10),
    ghichu nvarchar(255),

    primary key nonclustered (madv, mathucung, ngaysudung, mabs),

    constraint fk_ckb_dv
        foreign key (madv) references dichvu(madv),

    constraint fk_ckb_tc
        foreign key (mathucung) references thucung(mathucung),

    constraint fk_ckb_bs
        foreign key (mabs) references nhanvien(manv),

    constraint fk_ckb_toa
        foreign key (matoathuoc) references toathuoc(matoathuoc),

    constraint ck_ckb_ngaytaikham
        check (ngaytaikham > ngaysudung),

    constraint fk_ckb_dg
        foreign key (madanhgia) references danhgia(madanhgia)
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
    ngaytiem datetime not null,
    trangthai nvarchar(50),
    madanhgia varchar(10),

    primary key nonclustered (stt, madv, mathucung, mavacxin, mabs),

    constraint fk_cttp_dv
        foreign key (madv) references tiemphong(madv),

    constraint fk_cttp_tc
        foreign key (mathucung) references thucung(mathucung),

    constraint fk_cttp_vx
        foreign key (mavacxin) references vacxin(mavacxin),

    constraint fk_cttp_bs
        foreign key (mabs) references nhanvien(manv),

    constraint fk_cttp_dg
        foreign key (madanhgia) references danhgia(madanhgia)
)

-- =========================================
-- bảng hoadon
-- lập hóa đơn cho khách hàng
-- =========================================
set quoted_identifier on;
go
create table hoadon 
(
    mahd varchar(10),
    mathucung varchar(10) not null,
    manvlap varchar(10) not null,
    macn varchar(10) not null,
    makh varchar(10) not null,
    makham varchar(10),
    matiem varchar(10),
    ngaylap datetime not null,
    tongtien decimal(18,2) not null,
    khuyenmai decimal(18,2) not null,
    thanhtien as (tongtien - khuyenmai) persisted,
    hinhthucthanhtoan nvarchar(50),
    trangthai nvarchar(50) not null,

    constraint pk_hoadon
        primary key nonclustered (mahd, ngaylap),

    constraint fk_hd_tc
        foreign key (mathucung) references thucung(mathucung),

    constraint fk_hd_nv
        foreign key (manvlap) references nhanvien(manv),

    constraint fk_hd_cn
        foreign key (macn) references chinhanh(macn),

    constraint fk_hd_kh
        foreign key (makh) references khachhang(makh),

    constraint fk_hd_makham
        foreign key (makham) references dichvu(madv),
    
    constraint fk_hd_matiem
        foreign key (matiem) references dichvu(madv)
)

create unique nonclustered index ux_hd_mahd
on hoadon (mahd)
go

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
        check (soluong > 0),

    constraint ck_ctm_thanhtien
        check (thanhtien > 0)
)
go