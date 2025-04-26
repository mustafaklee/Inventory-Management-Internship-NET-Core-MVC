Azupir - Inventory Management System (Staj Projesi)
**Proje Hakkında**

- Azupir, üretim yönetim sistemi kapsamında envanter yönetimi, depo işlemleri, mal kabul işlemleri, stok hareketleri, kullanıcı işlemleri gibi çeşitli işlemleri yönetebileceğiniz bir platformdur. Bu projeyi bir .NET Core MVC uygulaması olarak geliştirdim.

**Login Ekrani:**
![login](images/login.png)

**Proje Özellikleri**

**Envanter Yönetimi:**

- Stok Kartları: Stokların tanımlanması, aktif/pasif hale getirilmesi.

- Stok Kodu, Stok Adı, Birim, Stok Tipi, Üretim Türü gibi alanlarla tanımlamalar yapılabilir.

![stok-kartı-tanım](images/stok-kartı-tanım-2.png)

**Kullanıcı Yönetimi:**

- Kullanıcılar için ekleme, düzenleme, silme işlemleri yapılabilir.

- Kullanıcıların farklı rolleri bulunur: Admin, Moderator, Bilgi İşlem, Üretim Planlama.

![create-user-profiles](images/create-user-profiles.png)

**Depo Yönetimi:**

- Depo ekleme ve düzenleme işlemleri yapılabilir.

- Depo türü ve açıklama gibi bilgilerle yeni depo tanımları yapılabilir.

![depo-ekle](images/depo-ekle.png)

**Stok Hareketleri:**

- Stok hareketleri, giriş ve çıkış fişleriyle takip edilebilir.

![/stok-hareketleri](images/stok-hareketleri.png)

**Teknolojiler**

- Backend: .NET Core MVC

- Frontend: HTML, CSS, JavaScript, Bootstrap

- Veritabanı: SQL Server

**Kurulum ve Kullanım**

- Projeyi GitHub'dan indirin:

```bash
git clone https://github.com/mustafaklee/Inventory-Management-Internship-NET-Core-MVC.git
```

- Bağımlılıkları Yükleyin

```bash
dotnet restore
```

- Projeyi çalıştırın:

```bash
dotnet run
```

- Web tarayıcınızda açarak http://localhost:5000 adresine gidin.
