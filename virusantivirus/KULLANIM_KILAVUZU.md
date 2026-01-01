# 🎯 Kullanım Kılavuzu - Virüs & Antivirüs Projesi

## 🚀 Hızlı Başlangıç

### Adım 1: Projeyi Derleme

**Visual Studio ile:**
1. `VirusAntivirusProject.sln` dosyasına çift tıklayın
2. Visual Studio açıldığında üstteki menüden **Build** → **Build Solution** seçin
3. Veya klavyeden **F6** tuşuna basın

**Komut Satırı ile:**
```powershell
# PowerShell veya CMD'de proje klasöründe:
dotnet build VirusAntivirusProject.sln
```

### Adım 2: Test Virüsünü Çalıştırma

**Visual Studio ile:**
1. Solution Explorer'da **TestVirus** projesine sağ tıklayın
2. **Set as Startup Project** seçin
3. **F5** tuşuna basın veya yeşil ▶️ butonuna tıklayın

**Komut Satırı ile:**
```powershell
cd TestVirus
dotnet run
```

### Adım 3: Test Virüsünü Aktif Etme

1. Program açıldığında **"Virüsü Aktif Et"** butonuna tıklayın
2. Program masaüstünüzde `TestVirusFiles` klasörü oluşturacak
3. İçine 5 test dosyası yerleştirecek
4. "Başarılı" mesajını göreceksiniz

### Adım 4: Antivirüs Programını Çalıştırma

**Visual Studio ile:**
1. Solution Explorer'da **Antivirus** projesine sağ tıklayın
2. **Set as Startup Project** seçin
3. **F5** tuşuna basın

**Komut Satırı ile:**
```powershell
# Yeni bir terminal penceresi açın
cd Antivirus
dotnet run
```

### Adım 5: Virüs Taraması Yapma

1. Antivirüs programında **"🔍 Tarama Başlat"** butonuna tıklayın
2. Program masaüstünüzdeki test virüs dosyalarını tarayacak
3. Bulduğu tehditleri listeleyecek
4. **"🧹 Tehditleri Temizle"** butonu aktif hale gelecek

### Adım 6: Tehditleri Temizleme

1. **"🧹 Tehditleri Temizle"** butonuna tıklayın
2. Onay mesajında **"Yes"** seçin
3. Program test virüs dosyalarını silecek
4. "Temizleme başarılı" mesajını göreceksiniz

## 📸 Ekran Görüntüleri Açıklaması

### Test Virüsü Programı:
```
┌─────────────────────────────────────┐
│  ⚠️ TEST VIRÜSÜ (ZARARSIZ) ⚠️      │
│  Bu program sadece test dosyaları   │
│  oluşturur. Bilgisayarınıza         │
│  zarar vermez!                      │
│                                     │
│  [Virüsü Aktif Et]  [Deaktif Et]   │
│                                     │
│  Durum: Pasif                       │
│  ─────────────────────────────────  │
│  Log:                               │
│  [HH:mm:ss] Mesajlar...            │
└─────────────────────────────────────┘
```

### Antivirüs Programı:
```
┌─────────────────────────────────────┐
│  🛡️ ANTİVİRÜS PROGRAMI             │
│  Bilgisayarınızı test virüslerinden│
│  koruyun                            │
│                                     │
│  [🔍 Tarama Başlat] [🧹 Temizle]   │
│  ▓▓▓▓▓▓▓▓▓▓▓░░░░░░░░░ 50%         │
│                                     │
│  Durum: Hazır  |  Bulunan Tehdit: 0│
│                                     │
│  Tespit Edilen Tehditler:          │
│  ┌─────────────────────────────┐   │
│  │ ⚠️ virus_test_1.txt         │   │
│  │ ⚠️ virus_test_2.txt         │   │
│  └─────────────────────────────┘   │
│                                     │
│  Tarama Geçmişi:                   │
│  [HH:mm:ss] === TARAMA BAŞLAT === │
└─────────────────────────────────────┘
```

## 🎮 Farklı Kullanım Senaryoları

### Senaryo 1: İlk Kullanım
1. Test Virüsü → Aktif Et
2. Antivirüs → Tarama Yap
3. Antivirüs → Temizle
4. **Sonuç:** Tüm test dosyaları silindi ✓

### Senaryo 2: Tekrar Test
1. Test Virüsü → Aktif Et (yeni dosyalar oluştur)
2. Antivirüs → Tarama Yap
3. **Sonuç:** 5 tehdit bulundu
4. Test Virüsü → Deaktif Et (virüsün kendi temizlemesi)

### Senaryo 3: Temiz Sistem Taraması
1. Antivirüs → Tarama Yap (virüs aktif değilken)
2. **Sonuç:** Hiçbir tehdit bulunamadı ✓

## 🔍 Dosyaların Konumu

Test virüs dosyaları şurada oluşturulur:
```
C:\Users\[KullanıcıAdınız]\Desktop\TestVirusFiles\
```

İçindeki dosyalar:
- `virus_test_1.txt`
- `virus_test_2.txt`
- `virus_test_3.txt`
- `virus_test_4.txt`
- `virus_test_5.txt`
- `virus.signature`

## ❓ Sık Sorulan Sorular

### S: Bu gerçekten zararsız mı?
**C:** Evet! Sadece masaüstünde bir klasör oluşturup içine metin dosyaları yerleştirir. Sistem dosyalarına, kayıt defterine veya başka önemli yerlere dokunmaz.

### S: Gerçek virüs tarayıcım bunu tehdit olarak gösterir mi?
**C:** Hayır, çünkü sadece normal metin dosyalarıdır. Hiçbir kötü niyetli kod içermez.

### S: Programı nasıl kaldırabilirim?
**C:** Basitçe proje klasörünü silin. Masaüstünüzde `TestVirusFiles` klasörü varsa onu da silebilirsiniz.

### S: .NET 6.0 SDK nereden indirilir?
**C:** [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download) adresinden ücretsiz indirebilirsiniz.

### S: Visual Studio Code ile çalışır mı?
**C:** Evet! VS Code ile de çalıştırabilirsiniz. C# extension'ını yükleyin ve `dotnet run` komutunu kullanın.

## 🛠️ Sorun Giderme

### Hata: "SDK bulunamadı"
**Çözüm:** .NET 6.0 SDK veya üstünü yükleyin
```powershell
# SDK versiyonunu kontrol edin:
dotnet --version
```

### Hata: "Form yüklenemiyor"
**Çözüm:** Windows Forms workload'ı yükleyin
```powershell
# Visual Studio Installer'dan:
# .NET desktop development workload'ını işaretleyin
```

### Hata: "Dosya erişim hatası"
**Çözüm:** Programı yönetici olarak çalıştırın veya başka bir konum seçin

## 💡 Geliştirme İpuçları

Projeyi geliştirmek isterseniz:

1. **Daha fazla tespit kuralı ekleyin:**
```csharp
// Form1.cs içinde BtnScan_Click metodunda:
if (fileName.Contains("virus") || 
    fileName.Contains("malware") || 
    fileName.EndsWith(".suspicious"))
{
    detectedThreats.Add(file);
}
```

2. **Gerçek zamanlı koruma ekleyin:**
```csharp
// FileSystemWatcher kullanarak dosya değişikliklerini izleyin
private FileSystemWatcher watcher;
```

3. **Karantina özelliği ekleyin:**
```csharp
// Dosyaları silmek yerine karantina klasörüne taşıyın
string quarantinePath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
    "Quarantine"
);
```

## 📚 Öğrenme Kaynakları

- C# Windows Forms: [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)
- Dosya İşlemleri: [System.IO Namespace](https://docs.microsoft.com/en-us/dotnet/api/system.io)
- Event Handling: [Event Handling Tutorial](https://docs.microsoft.com/en-us/dotnet/standard/events/)

## 🎓 Proje Hedefleri

Bu projeyi tamamladığınızda şunları öğrenmiş olacaksınız:

- ✅ Windows Forms uygulaması tasarlama
- ✅ Buton ve event'lerle çalışma
- ✅ Dosya ve klasör işlemleri
- ✅ ListBox, ProgressBar, TextBox kullanımı
- ✅ Exception handling (try-catch)
- ✅ MessageBox ile kullanıcı etkileşimi
- ✅ Basit virüs tespit mantığı

## 🤝 Destek

Sorunlarla karşılaşırsanız:
1. README.md dosyasını tekrar okuyun
2. Hata mesajını dikkatlice inceleyin
3. .NET SDK'nın yüklü olduğundan emin olun
4. Projeyi temiz bir şekilde yeniden derleyin

---

**İyi öğrenmeler! 🚀**




