# OptimaTech.BM.User.Api

Repository ini berisi API untuk sistem **OptimaTech Building Manager** yang berfungsi untuk manajemen data pengguna. API ini menyediakan layanan backend untuk pengelolaan data pengguna dan otentikasi.

## **Fitur**

- **Manajemen Pengguna**: Operasi CRUD untuk data pengguna.
- **Otentikasi**: Mendukung otorisasi berbasis token.
- **Dokumentasi API**: Swagger terintegrasi untuk eksplorasi API.

## **Teknologi yang Digunakan**

- **.NET 8.0**: Framework utama pengembangan API.
- **Entity Framework Core**: ORM untuk akses data.
- **FluentValidation**: Validasi input data.
- **Swashbuckle**: Swagger untuk dokumentasi API.

## **Instruksi Setup**

### 1. Clone Repository
```bash
git clone https://github.com/Faqihyugos/optimatech.bm.user.api.git
cd optimatech.bm.user.api
```

### 2. Konfigurasi Port
API menggunakan port berikut (diatur dalam `launchSettings.json`):
- **HTTP**: `http://localhost:5031`
- **HTTPS**: `https://localhost:7066`

Jika port ini tidak tersedia, sesuaikan konfigurasi di file `launchSettings.json`.

### 3. Menjalankan Aplikasi
Pasang dependensi dan jalankan aplikasi:
```bash
dotnet run --launch-profile https --project ./OptimaTech.BuildingManager.User.Api
```
API dapat diakses melalui:
- **Swagger UI**: [http://localhost:5031/swagger](http://localhost:5031/swagger)

- **Swagger UI**: [https://localhost:7066/swagger](https://localhost:7066/swagger)

---

## **Setup Database**

### Menambahkan Migrasi Baru
Gunakan perintah berikut untuk membuat migrasi:
```bash
dotnet ef migrations add InitialCreate --project ./OptimaTech.BuildingManager.User.Infrastructure --startup-project ./OptimaTech.BuildingManager.User.Api
```

### Menerapkan Migrasi ke Database
Untuk menerapkan migrasi:
```bash
dotnet ef database update --project ./OptimaTech.BuildingManager.User.Infrastructure --startup-project ./OptimaTech.BuildingManager.User.Api
```

Pastikan string koneksi diatur dengan benar di file `appsettings.json`.

---

## **Kontribusi**

Jika Anda ingin berkontribusi, silakan fork repository ini dan ajukan pull request. Pastikan kode yang ditambahkan memenuhi standar dan memiliki unit test.
