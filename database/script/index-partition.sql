use petcarx
go 

-- =========================================
-- partition
-- =========================================
create partition function pf_nam (datetime)
as range right for values
(
    '2020-01-01',
    '2021-01-01',
    '2022-01-01',
    '2023-01-01',
    '2024-01-01',
    '2025-01-01',
    '2026-01-01'
);
go

create partition scheme ps_nam
as partition pf_nam
all to ([primary])
go

-- hoadon
create clustered index ix_hd_ngaylap
on hoadon (ngaylap, mahd)
on ps_nam(ngaylap)
go

-- chitietkhambenh
create clustered index ix_ckb_ngaysudung
on chitietkhambenh (ngaysudung, madv, mathucung)
on ps_nam(ngaysudung)
go

-- chitiettiemphong
create clustered index ix_cttp_ngaytiem
on chitiettiemphong (ngaytiem, madv, mathucung)
on ps_nam(ngaytiem)
go

-- danhgia
create clustered index ix_dg_ngaydanhgia
on danhgia (ngaydanhgia, madanhgia)
on ps_nam(ngaydanhgia)
go

-- =========================================
-- index
-- =========================================
create nonclustered index ix_kh_sdt
on khachhang (sdt)

create nonclustered index ix_kh_email
on khachhang (email)

create nonclustered index ix_tc_makh
on thucung (makh)

create nonclustered index ix_tc_loai
on thucung (loai)

create nonclustered index ix_hd_macn_ngay
on hoadon (macn, ngaylap)
include (tongtien, thanhtien)

create nonclustered index ix_hd_makh
on hoadon (makh, ngaylap)
include (thanhtien)

create nonclustered index ix_sp_loaisp
on sanpham (loaisp)

create nonclustered index ix_sp_ton
on sanpham (soluongton)

create nonclustered index ix_ctm_masp
on chitietmuasanpham (masp)
include (soluong, thanhtien)

create nonclustered index ix_dg_nv
on danhgia (manv)

create nonclustered index ix_dg_dv
on danhgia (madv)

create nonclustered index ix_nv_macn
on nhanvien (macn)

create nonclustered index ix_nv_loainv
on nhanvien (loainv)

create nonclustered index ix_ckb_mathucung_ngay
on chitietkhambenh (mathucung, ngaysudung);

create nonclustered index ix_cttp_mathucung_ngay
on chitiettiemphong (mathucung, ngaytiem);

create nonclustered index ix_vx_ten_loai
on vacxin (tenvacxin, loaivacxin)

create nonclustered index ix_vx_ngaysanxuat
on vacxin (ngaysanxuat)

create nonclustered index ix_dvcn_macn
on dichvutaichinhanh (macn)

create nonclustered index ix_dvcn_madv
on dichvutaichinhanh (madv)