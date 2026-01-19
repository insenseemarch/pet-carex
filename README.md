# pet-carx – Workflow & Git Guide

---

## 👥 Authors
- **23120116**: Nguyễn Việt Cường
- **23120136**: Phạm Nguyễn Thế Khôi
- **23120152**: Hồ Khổng Tuyết Như
- **23120162**: Lê Hải Sơn
- **23120172**: Trần Thị Thủy Tiên

---

## 🔎 Overview
- Repository contains two main applications:
  - `KhachHangWeb/` — customer-facing web application.
  - `StaffWeb/` — staff/management web application.
- Database: application uses SQL Server (schema scripts available in the `database` folder).

---

## ✨ Prerequisites
- Operating System: Windows (this documentation is written for Windows)
- .NET SDK 8.0 (or compatible version, check with `dotnet --version`)
  - Download: https://dotnet.microsoft.com
- SQL Server
  - Management tool: SQL Server Management Studio (SSMS)

---

## 1) Database Setup
1. Create a new database (e.g., `petcarx`) on SQL Server using SSMS (or `sqlcmd`).
2. Run schema creation and data loading scripts from the `database` directory (organized into `script/` for schema and `generate-data/` for sample data):
   - Run main structure creation scripts:
     ```powershell
     sqlcmd -S <SERVER> -Q "CREATE DATABASE PetCareX;"
     sqlcmd -S <SERVER> -d PetCareX -i d:\PetCareXWeb\database\script\create-table.sql
     ```
   - (Optional) Run additional scripts (index/partition):
     ```powershell
     sqlcmd -S <SERVER> -d PetCareX -i d:\PetCareXWeb\database\script\index-partition.sql
     ```
   - Create triggers/functions/procedures:
     ```powershell
     sqlcmd -S <SERVER> -d PetCareX -i d:\PetCareXWeb\database\script\trigger-function-procedure.sql
     ```
   - Load sample data (`database/generate-data` directory): files are numbered `01_*`, `02_*`, etc. to run in order. Example (PowerShell):
     ```powershell
     Get-ChildItem 'd:\PetCareXWeb\database\generate-data' -Filter '*.sql' | Sort-Object Name | ForEach-Object { sqlcmd -S <SERVER> -d PetCareX -i $_.FullName }
     ```
   - If you have `generate-data.zip`, extract it to the `database/generate-data` folder before running.

3. Verify that tables (e.g., `chinhanh`, `dichvu`, `thucung`, etc.) exist and sample data has been loaded.

---

## 2) Configuring Connection Strings & Secrets
- Open `appsettings.Development.json` (or `appsettings.json`) in both `KhachHangWeb` and `StaffWeb` and update `ConnectionStrings:Default` to point to your newly created database. Example:
```json
"ConnectionStrings": {
  "Default": "Server=(localdb)\\mssqllocaldb;Database=petcarx;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
- For Production environments, update environment variables or `appsettings.Production.json` accordingly.
- If the application requires secrets (SMTP, API keys), check `appsettings.*.json` or use `User Secrets` (use `dotnet user-secrets` if needed).

---

## 3) Restore & Build
Open PowerShell (or terminal in VS Code) and run from the workspace root or each project directory:

1. Restore NuGet packages:
```powershell
cd d:\PetCareXWeb\KhachHangWeb
dotnet restore

cd d:\PetCareXWeb\StaffWeb
dotnet restore
```

2. Build projects:
```powershell
dotnet build d:\PetCareXWeb\KhachHangWeb\KhachHangWeb.csproj -c Debug
dotnet build d:\PetCareXWeb\StaffWeb\StaffWeb.csproj -c Debug
```

3. (Optional) Publish for Production environment:
```powershell
dotnet publish d:\PetCareXWeb\KhachHangWeb\KhachHangWeb.csproj -c Release -o d:\PetCareXWeb\publish\KhachHangWeb
```

---

## 4) Running the Application (Development)
- Run each application in separate terminals:
```powershell
# Customer web
cd d:\your-path\pet-carx\source-app\KhachHangWeb
dotnet run --project KhachHangWeb.csproj

# Staff web
cd d:\your-path\pet-carx\source-app\StaffWeb
dotnet run --project StaffWeb.csproj
```
- By default, applications will run on a specific port. Check the terminal output for the URL (e.g., `https://localhost:5001`).
- Alternatively, open with Visual Studio and press F5 (IIS Express) if you prefer debugging with an IDE.

---

## 5) Main Feature Checks (Smoke Tests)
Open a browser and test the main routes:
- Homepage: `https://localhost:<port>/` (Homepage displays products and booking links)
- Booking: `https://localhost:<port>/booking` (Verify that only **Medical Examination** appears in Services — change has been applied)
- Shopping/Cart/Checkout: `/shop`, `/cart`, `/checkout`
- Login/Register: `/account/login`, `/account/register`

Additional checks:
- Create new account -> login -> add pet -> book appointment
- Shopping: add product to cart -> checkout

---

## 6) Common Errors & Troubleshooting
- Missing .NET SDK error: run `dotnet --info` to check; install the appropriate .NET SDK.
- Database connection error: verify `ConnectionStrings` and ensure SQL Server is running with proper account permissions.
- Missing package/restore error: run `dotnet restore` and check internet/nuget source.
- EF error: install `dotnet-ef` if needed: `dotnet tool install --global dotnet-ef`.

---

## 7) Quick Reference Commands
- Check .NET: `dotnet --version`
- Restore & build: `dotnet restore` → `dotnet build`
- Run: `dotnet run --project <path-to-csproj>`
- Publish Release: `dotnet publish -c Release -o <output-folder>`
- Run SQL script: `sqlcmd -S <server> -i <path-to-sql>`

---

## 8) Additional Notes
- The `database/` directory contains main scripts:
  - `database/script/` — scripts for creating tables, indexes, partitions, and database objects (triggers, procedures).
  - `database/generate-data/` — scripts for loading sample data (numbered to run in order).
- All configuration changes (connection strings, ports, API keys) **should** be saved in `appsettings.Development.json` (or user secrets) to avoid affecting the production environment.
