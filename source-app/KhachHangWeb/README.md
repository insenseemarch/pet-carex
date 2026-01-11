# PetCareX Web (KhachHangWeb)

## Yeu cau
- .NET 8 SDK (x64)
- SQL Server (Express hoac full) + SSMS
- (Tuy chon) Visual Studio 2022 hoac VS Code

## Lenh cai dat (may moi)
Neu chi co VS Code + SSMS, can cai .NET 8 SDK:

Option A (winget):
```
winget install Microsoft.DotNet.SDK.8
```

Option B (tai thu cong):
- Tai .NET 8 SDK (Windows x64) tai:
  https://dotnet.microsoft.com/download/dotnet/8.0

Kiem tra:
```
dotnet --version
dotnet --list-sdks
```

Tai cac package:
```
dotnet restore
```

## Cai dat database
1) Tao database `petcarx` va chay file SQL:
   - `no-index.sql`
2) Can co du lieu toi thieu:
   - `chinhanh`, `nhanvien`, `taikhoannhanvien`
   - `khachhang`, `taikhoankhachhang`
   - `sanpham`, `sanphamtaichinhanh`
   - `thucung`

## Cau hinh connection string
Tao file `appsettings.Development.json` (khong commit):
```
{
  "ConnectionStrings": {
    "Default": "Data Source=localhost\\SQLEXPRESS;Initial Catalog=petcarx;Integrated Security=True;TrustServerCertificate=True"
  }
}
```
Neu dung SQL login, them `User ID` va `Password`.

## Chay ung dung
```
dotnet restore
dotnet build
dotnet run
```
Mo URL tren console (vi du: `http://localhost:5290`).

## Luu y
- Project dung Database-First (scaffold). Khong can migration.
- Neu build bi loi file lock, tat app dang chay roi build lai.
- Dang nhap mac dinh dung bang `taikhoankhachhang`.

## Chuc nang hien tai
- Shop: tim theo ten/loai + loc theo loai/gia
- Cart + Checkout (SP: `sp_lap_hoadon`, `sp_thanhtoan_hoadon`)
- Dat lich kham + xem lich bac si
- Lich su thu cung: mua/kham/tiem
- Trang ca nhan va dang ky

