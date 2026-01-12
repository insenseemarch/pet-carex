# pet-carx – workflow & git guide

---

## 🔎 Tổng quan
- Repository chứa hai ứng dụng chính:
  - `KhachHangWeb/` — web cho khách hàng.
  - `StaffWeb/` — web cho nhân viên/nhà quản lý.
- Database: ứng dụng sử dụng SQL Server (tập lệnh schema có sẵn trong folder `database`).

---

## ✨ Yêu cầu trước khi bắt đầu
- Hệ điều hành: Windows (tài liệu này viết theo Windows)
- .NET SDK 8.0 (hoặc phiên bản tương thích, kiểm tra bằng `dotnet --version`)
  - Tải: https://dotnet.microsoft.com
- SQL Server
  - Công cụ quản lý: SQL Server Management Studio (SSMS)

---

## 1) Thiết lập database
1. Tạo một database mới (ví dụ: `petcarx`) trên SQL Server bằng SSMS (hoặc bằng `sqlcmd`).
2. Chạy các script tạo schema và nạp dữ liệu từ thư mục `database` (được chia thành `script/` cho schema và `generate-data/` cho dữ liệu mẫu):
   - Chạy tập lệnh tạo cấu trúc chính:
     ```powershell
     sqlcmd -S <SERVER> -Q "CREATE DATABASE PetCareX;"
     sqlcmd -S <SERVER> -d PetCareX -i d:\PetCareXWeb\database\script\create-table.sql
     ```
   - (Tuỳ chọn) Chạy các script bổ sung (index / partition):
     ```powershell
     sqlcmd -S <SERVER> -d PetCareX -i d:\PetCareXWeb\database\script\index-partition.sql
     ```
   - Tạo trigger / function / procedure:
     ```powershell
     sqlcmd -S <SERVER> -d PetCareX -i d:\PetCareXWeb\database\script\trigger-function-procedure.sql
     ```
   - Nạp dữ liệu mẫu (thư mục `database/generate-data`): các file đã được đánh số `01_*`, `02_*`, ... để chạy theo thứ tự. Ví dụ (PowerShell):
     ```powershell
     Get-ChildItem 'd:\PetCareXWeb\database\generate-data' -Filter '*.sql' | Sort-Object Name | ForEach-Object { sqlcmd -S <SERVER> -d PetCareX -i $_.FullName }
     ```
   - Nếu bạn có `generate-data.zip`, giải nén vào thư mục `database/generate-data` trước khi chạy.

3. Kiểm tra rằng các bảng (ví dụ: `chinhanh`, `dichvu`, `thucung`,...) đã tồn tại và dữ liệu mẫu đã được nạp.

---

## 2) Cấu hình connection string & secrets
- Mở `appsettings.Development.json` (hoặc `appsettings.json`) trong cả `KhachHangWeb` và `StaffWeb` và chỉnh `ConnectionStrings:Default` để trỏ tới database vừa tạo. Ví dụ:
```json
"ConnectionStrings": {
  "Default": "Server=(localdb)\\mssqllocaldb;Database=petcarx;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
- Nếu dùng môi trường Production, cập nhật biến môi trường hoặc `appsettings.Production.json` tương ứng.
- Nếu ứng dụng yêu cầu các bí mật (SMTP, API keys), kiểm tra `appsettings.*.json` hoặc `User Secrets` (sử dụng `dotnet user-secrets` nếu cần).

---

## 3) Restore & Build
Mở PowerShell (hoặc terminal trong VS Code) và chạy tại thư mục gốc của workspace hoặc từng project:

1. Restore NuGet packages:
```powershell
cd d:\PetCareXWeb\KhachHangWeb
dotnet restore

cd d:\PetCareXWeb\StaffWeb
dotnet restore
```

2. Build project:
```powershell
dotnet build d:\PetCareXWeb\KhachHangWeb\KhachHangWeb.csproj -c Debug
dotnet build d:\PetCareXWeb\StaffWeb\StaffWeb.csproj -c Debug
```

3. (Tuỳ chọn) Publish cho môi trường Production:
```powershell
dotnet publish d:\PetCareXWeb\KhachHangWeb\KhachHangWeb.csproj -c Release -o d:\PetCareXWeb\publish\KhachHangWeb
```

---

## 4) Chạy ứng dụng (development)
- Chạy từng ứng dụng trong terminal riêng:
```powershell
# Khách hàng web
cd d:\your-path\pet-carx\source-app\KhachHangWeb
dotnet run --project KhachHangWeb.csproj

# Staff web
cd d:\your-path\pet-carx\source-app\StaffWeb
dotnet run --project StaffWeb.csproj
```
- Mặc định ứng dụng sẽ chạy trên một cổng (port). Kiểm tra output terminal để biết URL (ví dụ `https://localhost:5001`).
- Hoặc mở bằng Visual Studio và nhấn F5 (IIS Express) nếu bạn thích debug bằng IDE.

---

## 5) Kiểm tra chức năng chính (smoke tests)
Mở trình duyệt và kiểm tra các route chính:
- Trang chủ: `https://localhost:<port>/` (Trang chủ hiển thị sản phẩm và link đặt lịch)
- Đặt lịch: `https://localhost:<port>/booking` (Kiểm tra chỉ có **Khám bệnh** xuất hiện trong Dịch vụ — thay đổi đã áp dụng)
- Mua hàng / giỏ hàng / thanh toán: `/shop`, `/cart`, `/checkout`
- Đăng nhập / Đăng ký: `/account/login`, `/account/register`

Kiểm tra thêm:
- Tạo tài khoản mới -> đăng nhập -> thêm thú cưng -> đặt lịch
- Mua hàng: thêm sản phẩm vào giỏ -> thanh toán

---

## 7) Lỗi thường gặp & cách khắc phục
- Lỗi thiếu .NET SDK: chạy `dotnet --info` để kiểm tra; cài đặt .NET SDK tương ứng.
- Lỗi kết nối DB: kiểm tra `ConnectionStrings` và đảm bảo SQL Server đang chạy, tài khoản có quyền tạo DB.
- Lỗi thiếu package/restore: chạy `dotnet restore` và kiểm tra internet/nuget source.
- Lỗi EF: cài `dotnet-ef` nếu cần: `dotnet tool install --global dotnet-ef`.

---

## 8) Commands tóm tắt (Quick reference)
- Kiểm tra .NET: `dotnet --version`
- Restore & build: `dotnet restore` → `dotnet build`
- Chạy: `dotnet run --project <path-to-csproj>`
- Publish Release: `dotnet publish -c Release -o <output-folder>`
- Chạy SQL script: `sqlcmd -S <server> -i <path-to-sql>`

---

## 9) Ghi chú thêm
- Thư mục `database/` chứa các script chính:
  - `database/script/` — script tạo bảng, index, partition và các object cơ sở dữ liệu (triggers, procedures).
  - `database/generate-data/` — script nạp dữ liệu mẫu (được đánh số để chạy theo thứ tự).
- Mọi thay đổi cấu hình (connection strings, ports, API keys) **nên** lưu trong `appsettings.Development.json` (hoặc user secrets) để không ảnh hưởng môi trường production.