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
        ),

    constraint ck_dg_ngaydanhgia
        check (ngaydanhgia <= getdate())
)

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
    ngaytiem date not null,
    trangthai nvarchar(50),
    madanhgia varchar(10),

    primary key (stt, madv, mathucung, mavacxin, mabs),

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
    mahd varchar(10) primary key,
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

-- =========================================
-- function: xác định cấp bậc khách hàng
-- =========================================
create or alter function fn_xac_dinh_capbac
(
    @makh varchar(10),
    @nam int
)
returns nvarchar(50)
as
begin
    declare @tongchitieu decimal(18,2)

    -- tổng chi tiêu đã thanh toán trong năm
    select @tongchitieu = isnull(sum(thanhtien), 0)
    from hoadon
    where makh = @makh
      and trangthai = N'Đã thanh toán'
      and year(ngaylap) = @nam

    if @tongchitieu >= 12000000
        return N'VIP'

    if @tongchitieu >= 5000000
        return N'Thân thiết'

    return N'Cơ bản'
end
go

-- =========================================
-- function: tính khuyến mãi cho dịch vụ
-- =========================================
create or alter function fn_tinh_khuyenmai_dichvu
(
    @madv varchar(10)
)
returns decimal(18,2)
as
begin
    declare @khuyenmai decimal(18,2) = 0
    declare @gia decimal(18,2)
    declare @phantramgiam decimal(5,2)

    -- Kiểm tra có phải gói tiêm không
    if exists (select 1 from tiemgoi where madv = @madv)
    begin
        select 
            @gia = dv.gia,
            @phantramgiam = tg.phantramgiamgia
        from tiemgoi tg
        join dichvu dv on tg.madv = dv.madv
        where tg.madv = @madv

        set @khuyenmai = @gia * @phantramgiam / 100
    end

    return @khuyenmai
end
go

-- =========================================
-- trigger: chặn kê thuốc hết hạn
-- =========================================
create or alter trigger trg_thuocsudung_het_han
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
        ;throw 50001, N'Thuốc đã hết hạn, không thể kê toa.', 1
    end
end
go

-- =========================================
-- trigger: chặn sử dụng vắc-xin hết hạn
-- =========================================
create or alter trigger trg_tiemphong_vacxin_het_han
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
        ;throw 50001, N'Vắc-xin đã hết hạn, không thể dùng để tiêm.', 1
    end
end
go


-- =========================================
-- trigger: trừ tồn kho khi khách hàng đã thanh toán hóa đơn
-- =========================================
create or alter trigger trg_hoadon_tru_tonkho
on hoadon
after update
as
begin
    if exists (
        select 1
        from inserted i
        join deleted d on i.mahd = d.mahd
        where i.trangthai = N'Đã thanh toán'
          and d.trangthai <> N'Đã thanh toán'
    )
    begin
        -- chặn bán vượt tồn kho
        if exists (
            select 1
            from chitietmuasanpham ctm
            join sanpham sp on ctm.masp = sp.masp
            join inserted i on ctm.mahd = i.mahd
            where sp.soluongton < ctm.soluong
        )
        begin
            ;throw 50002, N'Số lượng tồn kho không đủ.', 1
        end

        -- trừ tồn kho
        update sp
        set sp.soluongton = sp.soluongton - ctm.soluong
        from sanpham sp
        join chitietmuasanpham ctm on sp.masp = ctm.masp
        join inserted i on ctm.mahd = i.mahd
    end
end
go

-- =========================================
-- trigger: cộng điểm tích lũy cho khách hàng
-- hiện tại: 1 điểm = 50.000 vnđ
-- =========================================
create or alter trigger trg_hoadon_cong_diem
on hoadon
after update
as
begin
    update tkkh
    set 
        diemtichluy = diemtichluy + (i.thanhtien / 50000),
        capbac = dbo.fn_xac_dinh_capbac(tkkh.makh, year(getdate()))
    from taikhoankhachhang tkkh
    join inserted i on tkkh.makh = i.makh
    join deleted d on i.mahd = d.mahd
    where i.trangthai = N'Đã thanh toán'
      and d.trangthai <> N'Đã thanh toán'
end
go

-- =========================================
-- trigger: 1 chi nhánh chỉ có 1 quản lý
-- =========================================
create or alter trigger trg_1_quanly_moi_chinhanh
on nhanvien
after insert, update
as
begin
    if exists (
        select macn
        from nhanvien
        where chucvu = N'Quản lý'
        group by macn
        having count(*) > 1
    )
    begin
        ;throw 50003, N'Mỗi chi nhánh chỉ được có một quản lý.', 1
    end
end
go

-- =========================================
-- trigger: Chỉ bác sĩ thú y mới được khám / tiêm
-- =========================================
create or alter trigger trg_only_bs_kham
on chitietkhambenh
after insert, update
as
begin
    if exists (
        select 1
        from inserted i
        join nhanvien nv on i.mabs = nv.manv
        where nv.loainv <> N'Bác sĩ thú y'
    )
    begin
        ;throw 50004, N'Chỉ bác sĩ thú y mới được thực hiện chữa bệnh.', 1
    end
end
go

-- =========================================
-- trigger: Chỉ nhân viên tiếp tân mới được lập hóa đơn
-- =========================================
create or alter trigger trg_kiemsoat_nv_lap_hoadon
on hoadon
after insert, update
as
begin
    if exists (
        select 1
        from inserted i
        join nhanvien nv on i.manvlap = nv.manv
        where nv.loainv <> N'Nhân viên tiếp tân'
    )
    begin
        ;throw 50005, N'Chỉ nhân viên tiếp tân mới được phép lập hóa đơn.', 1
    end
end
go

-- =========================================
-- procedure: Đăng nhập
-- =========================================
create or alter procedure sp_dangnhap
(
    @tendangnhap varchar(50),
    @matkhau varchar(255)
)
as
begin
    set nocount on

    -- nhân viên
    if exists (
        select 1
        from taikhoannhanvien
        where tendangnhap = @tendangnhap
          and matkhau = @matkhau
          and trangthai = N'Đang hoạt động'
    )
    begin
        select
            N'nhanvien' as loai,
            tknv.tendangnhap,
            tknv.vaitro,
            nv.manv,
            nv.macn
        from taikhoannhanvien tknv
        join nhanvien nv on tknv.manv = nv.manv
        where tknv.tendangnhap = @tendangnhap
        return;
    end

    -- khách hàng
    if exists (
        select 1
        from taikhoankhachhang
        where tendangnhap = @tendangnhap
          and matkhau = @matkhau
          and trangthai = N'Đang hoạt động'
    )
    begin
        select
            N'khachhang' as loai,
            tendangnhap,
            makh,
            capbac,
            diemtichluy
        from taikhoankhachhang
        where tendangnhap = @tendangnhap
        return;
    end

    ;throw 51001, N'Tên đăng nhập hoặc mật khẩu không đúng.', 1
end
go


-- =========================================
-- procedure: Lễ tân tạo tài khoản khách hàng
-- =========================================
create or alter procedure sp_tao_khachhang
(
    @makh varchar(10),
    @hoten nvarchar(100),
    @sdt char(10),
    @email varchar(100),
    @cccd varchar(20),
    @gioitinh nvarchar(10),
    @ngaysinh date
)
as
begin
    set nocount on

    begin try
        begin tran

        insert into khachhang
        values (@makh, @hoten, @sdt, @email, @cccd, @gioitinh, @ngaysinh);

        insert into taikhoankhachhang
        values (
            @sdt,       -- dùng sdt làm username
            @makh,
            '123456',   -- mật khẩu mặc định
            0,
            N'Cơ bản',
            N'Đang hoạt động'
        );

        commit tran
    end try
    begin catch
        rollback tran
        throw
    end catch
end
go

-- =========================================
-- procedure: Tạo thú cưng
-- =========================================
create or alter procedure sp_tao_thucung
(
    @mathucung varchar(10),
    @makh varchar(10),
    @tenthucung nvarchar(100),
    @loai nvarchar(50),
    @giong nvarchar(50),
    @ngaysinh date,
    @gioitinh nvarchar(10),
    @tinhtrang nvarchar(255)
)
as
begin
    insert into thucung
    values (
        @mathucung, @makh, @tenthucung,
        @loai, @giong, @ngaysinh, @gioitinh, @tinhtrang
    )
end
go

-- =========================================
-- procedure: Ghi nhận lần khám
-- =========================================
create or alter procedure sp_ghinhan_khambenh
(
    @madv varchar(10),
    @mathucung varchar(10),
    @ngaysudung date,
    @mabs varchar(10),
    @trieuchung nvarchar(255),
    @chandoan nvarchar(255),
    @ngaytaikham date
)
as
begin
    insert into chitietkhambenh
    (
        madv, mathucung, ngaysudung, mabs,
        trieuchung, chandoan, ngaytaikham
    )
    values
    (
        @madv, @mathucung, @ngaysudung, @mabs,
        @trieuchung, @chandoan, @ngaytaikham
    )
end
go

-- =========================================
-- procedure: Tạo toa thuốc mới
-- =========================================
create or alter procedure sp_ke_toa_thuoc
(
    @matoathuoc varchar(10),
    @tentoathuoc nvarchar(100)
)
as
begin
    insert into toathuoc
    values (@matoathuoc, @tentoathuoc)
end
go

-- =========================================
-- procedure: Ghi nhận lần tiêm
-- =========================================
create or alter procedure sp_ghinhan_tiemphong
(
    @stt int,
    @madv varchar(10),
    @mathucung varchar(10),
    @mavacxin varchar(10),
    @mabs varchar(10),
    @ngaytiem date
)
as
begin
    set nocount on

    begin try
        begin tran

        -- case 1: đã có lịch tiêm (đặt trước)
        if exists (
            select 1
            from chitiettiemphong
            where stt = @stt
              and madv = @madv
              and mathucung = @mathucung
              and trangthai = N'Chờ tiêm'
        )
        begin
            update chitiettiemphong
            set
                mavacxin = @mavacxin,
                mabs = @mabs,
                ngaytiem = @ngaytiem,
                trangthai = N'Đã tiêm'
            where stt = @stt
              and madv = @madv
              and mathucung = @mathucung
        end
        else
        begin
            -- case 2: không đặt trước -> tiêm trực tiếp
            insert into chitiettiemphong (
                stt, madv, mathucung,
                mavacxin, mabs,
                ngaytiem, trangthai
            )
            values (
                @stt, @madv, @mathucung,
                @mavacxin, @mabs,
                @ngaytiem, N'Đã tiêm'
            )
        end

        commit tran
    end try
    begin catch
        rollback tran
        throw
    end catch
end
go

-- =========================================
-- procedure: Lập hóa đơn
-- =========================================
create or alter procedure sp_lap_hoadon
(
    @mahd varchar(10),
    @mathucung varchar(10),
    @manvlap varchar(10),
    @macn varchar(10),
    @makh varchar(10),
    @tongtien decimal(18,2),
    @khuyenmai decimal(18,2)
)
as
begin
    set nocount on;

    insert into hoadon
    (
        mahd,
        mathucung,
        manvlap,
        macn,
        makh,
        makham,
        matiem,
        ngaylap,
        tongtien,
        khuyenmai,
        hinhthucthanhtoan,
        trangthai
    )
    values
    (
        @mahd,
        @mathucung,
        @manvlap,
        @macn,
        @makh,
        null,
        null,
        getdate(),
        @tongtien,
        @khuyenmai,
        null,
        N'Chưa thanh toán'
    );
end
go

-- =========================================
-- procedure: Thanh toán hóa đơn
-- =========================================
create or alter procedure sp_thanhtoan_hoadon
(
    @mahd varchar(10),
    @hinhthucthanhtoan nvarchar(50)
)
as
begin
    set nocount on;

    update hoadon
    set trangthai = N'Đã thanh toán',
        hinhthucthanhtoan = @hinhthucthanhtoan
    where mahd = @mahd
end
go

-- =========================================
-- procedure: Doanh thu theo chi nhánh & thời gian
-- =========================================
create procedure sp_thongke_doanhthu_chinhanh
(
    @tungay date,
    @denngay date
)
as
begin
    select
        cn.macn,
        cn.tencn,
        count(hd.mahd) as sohoadon,
        sum(hd.thanhtien) as tongdoanhthu
    from hoadon hd
    join chinhanh cn on hd.macn = cn.macn
    where hd.trangthai = N'Đã thanh toán'
      and hd.ngaylap between @tungay and @denngay
    group by cn.macn, cn.tencn
    order by tongdoanhthu desc
end
go

-- =========================================
-- procedure: Thống kê doanh thu theo dịch vụ
-- =========================================
create procedure sp_thongke_doanhthu_dichvu
(
    @tungay date,
    @denngay date
)
as
begin
    select
        dv.madv,
        dv.tendv,
        count(*) as solansudung,
        sum(dv.gia) as tongdoanhthu
    from chitietkhambenh kb
    join dichvu dv on kb.madv = dv.madv
    join hoadon hd on hd.makham = dv.madv
    where hd.trangthai = N'Đã thanh toán'
      and hd.ngaylap between @tungay and @denngay
    group by dv.madv, dv.tendv
    order by tongdoanhthu desc
end
go

-- =========================================
-- procedure: Thống kê sản phẩm bán chạy
-- =========================================
create procedure sp_thongke_sanpham_banchay
(
    @top int
)
as
begin
    select top (@top)
        sp.masp,
        sp.tensp,
        sum(ct.soluong) as tongsoluong,
        sum(ct.thanhtien) as tongdoanhthu
    from chitietmuasanpham ct
    join sanpham sp on ct.masp = sp.masp
    join hoadon hd on ct.mahd = hd.mahd
    where hd.trangthai = N'Đã thanh toán'
    group by sp.masp, sp.tensp
    order by tongdoanhthu desc
end
go

-- =========================================
-- procedure: Thống kê khách hàng theo cấp bậc
-- =========================================
create procedure sp_thongke_khachhang_capbac
as
begin
    select
        capbac,
        count(*) as soluong,
        avg(diemtichluy) as diemtrungbinh
    from taikhoankhachhang
    where trangthai = N'Đang hoạt động'
    group by capbac
end
go

-- =========================================
-- procedure: Thống kê số lần làm việc của bác sĩ
-- =========================================
create procedure sp_thongke_bacsi
as
begin
    select
        nv.manv,
        nv.hoten,
        count(kb.madv) as solankham,
        count(tp.madv) as solantiem
    from nhanvien nv
    left join chitietkhambenh kb on nv.manv = kb.mabs
    left join chitiettiemphong tp on nv.manv = tp.mabs
    where nv.loainv = N'Bác sĩ thú y'
    group by nv.manv, nv.hoten
    order by solankham desc
end
go

-- =========================================
-- procedure: Thống kê vắc-xin sắp hết hạn
-- =========================================
create procedure sp_thongke_vacxin_saphethan
(
    @songay int
)
as
begin
    select
        mavacxin,
        tenvacxin,
        ngayhethan,
        datediff(day, getdate(), ngayhethan) as songayconlai
    from vacxin
    where ngayhethan between getdate() and dateadd(day, @songay, getdate())
    order by ngayhethan
end
go

-- =========================================
-- procedure: Danh sách thú cưng đã tiêm trong kỳ
-- =========================================
create or alter procedure sp_ds_thucung_da_tiem
(
    @macn varchar(10),
    @tungay date,
    @denngay date
)
as
begin
    set nocount on

    select distinct
        tc.mathucung,
        tc.tenthucung,
        tc.loai,
        tc.giong,
        cttp.ngaytiem
    from chitiettiemphong cttp
    join thucung tc on cttp.mathucung = tc.mathucung
    join hoadon hd on hd.matiem = cttp.madv
    where hd.macn = @macn
      and cttp.ngaytiem between @tungay and @denngay
end
go

-- =========================================
-- procedure: Thống kê vắc-xin được dùng nhiều nhất
-- =========================================
create or alter procedure sp_thongke_vacxin
(
    @macn varchar(10)
)
as
begin
    set nocount on

    select 
        v.mavacxin,
        v.tenvacxin,
        count(*) as solantiem
    from chitiettiemphong cttp
    join vacxin v on cttp.mavacxin = v.mavacxin
    join hoadon hd on hd.matiem = cttp.madv
    where hd.macn = @macn
    group by v.mavacxin, v.tenvacxin
    order by solantiem desc
end
go

-- =========================================
-- procedure: Tồn kho sản phẩm tại chi nhánh
-- =========================================
create or alter procedure sp_tonkho_chinhanh
(
    @macn varchar(10)
)
as
begin
    set nocount on

    select 
        sp.masp,
        sp.tensp,
        sp.loaisp,
        sp.soluongton
    from sanpham sp
    join sanphamtaichinhanh spcn on sp.masp = spcn.masp
    where spcn.macn = @macn
    order by sp.soluongton asc
end
go

-- =========================================
-- procedure: Hiệu suất nhân viên chi nhánh
-- =========================================
create or alter procedure sp_hieusuat_nhanvien
(
    @macn varchar(10)
)
as
begin
    set nocount on

    select 
        nv.manv,
        nv.hoten,
        count(hd.mahd) as sohoadon,
        avg(dg.diemthaidonv) as diemtrungbinh
    from nhanvien nv
    left join hoadon hd on nv.manv = hd.manvlap
    left join danhgia dg on nv.manv = dg.manv
    where nv.macn = @macn
    group by nv.manv, nv.hoten
    order by sohoadon desc
end
go

-- =========================================
-- procedure: Dịch vụ doanh thu cao nhất 6 tháng gần nhất
-- =========================================
create or alter procedure sp_top_dichvu_6thang
as
begin
    set nocount on;

    select top 5
        dv.madv,
        dv.tendv,
        sum(hd.thanhtien) as doanhthu
    from hoadon hd
    join dichvu dv on hd.makham = dv.madv or hd.matiem = dv.madv
    where hd.trangthai = N'Đã thanh toán'
      and hd.ngaylap >= dateadd(month, -6, getdate())
    group by dv.madv, dv.tendv
    order by doanhthu desc
end
go

-- =========================================
-- procedure: Thông báo sinh nhật khách hàng / thú cưng
-- =========================================
create or alter procedure sp_nhac_sinh_nhat_khach_hang
as
begin
    select 
        kh.makh,
        kh.hoten,
        kh.sdt,
        kh.email
    from khachhang kh
    where 
        month(kh.ngaysinh) = month(getdate())
        and day(kh.ngaysinh) = day(getdate())
end
go

create or alter procedure sp_nhac_sinh_nhat_thu_cung
as
begin
    select 
        tc.mathucung,
        tc.tenthucung,
        tc.ngaysinh,
        kh.sdt,
        kh.email
    from thucung tc
    join khachhang kh on tc.makh = kh.makh
    where 
        month(tc.ngaysinh) = month(getdate())
        and day(tc.ngaysinh) = day(getdate())
end
go

-- =========================================
-- procedure: Thông báo ngày tái khám
-- =========================================
create or alter procedure sp_nhac_tai_kham
as
begin
    select 
        ckb.mathucung,
        tc.tenthucung,
        kh.hoten,
        ckb.ngaytaikham
    from chitietkhambenh ckb
    join thucung tc on ckb.mathucung = tc.mathucung
    join khachhang kh on tc.makh = kh.makh
    where ckb.ngaytaikham = cast(getdate() as date);
end
go

-- =========================================
-- procedure: Thông báo ngày tiêm phòng
-- =========================================
create or alter procedure sp_nhac_tiem_phong
as
begin
    select 
        cttp.mathucung,
        tc.tenthucung,
        kh.hoten,
        cttp.ngaytiem
    from chitiettiemphong cttp
    join thucung tc on cttp.mathucung = tc.mathucung
    join khachhang kh on tc.makh = kh.makh
    where cttp.ngaytiem = cast(getdate() as date)
end
go


-- =========================================
-- procedure: Nhắc khách hàng lâu không quay lại
-- =========================================
create or alter procedure sp_nhac_khach_lau_khong_quay_lai
    @songay int = 180 -- 6 tháng
as
begin
    select 
        kh.makh,
        kh.hoten,
        max(hd.ngaylap) as lan_cuoi
    from khachhang kh
    left join hoadon hd on kh.makh = hd.makh
    group by kh.makh, kh.hoten
    having datediff(day, max(hd.ngaylap), getdate()) >= @songay
           or max(hd.ngaylap) is null;
end
go