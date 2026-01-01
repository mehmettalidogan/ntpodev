# 🛡️ Virüs & Antivirüs Eğitim Projesi

Bu proje, eğitim amaçlı olarak zararsız bir "test virüsü" ve onu tespit edip temizleyen bir antivirüs programı içerir.

## ⚠️ ÖNEMLİ NOTLAR

- **Bu programlar tamamen zararsızdır!** Bilgisayarınıza hiçbir zarar vermez.
- Sadece masaüstünde "TestVirusFiles" adında bir klasör oluşturur ve içinde test dosyaları barındırır.
- Gerçek bir virüs değildir, sadece eğitim amaçlıdır.
- Windows sistem dosyalarına veya kayıt defterine dokunmaz.

## 📋 Gereksinimler

- Windows 10/11
- .NET 6.0 SDK veya daha yenisi
- Visual Studio 2022 (önerilen) veya Visual Studio Code

## 🚀 Kurulum ve Çalıştırma

### Visual Studio ile:

1. `VirusAntivirusProject.sln` dosyasını Visual Studio ile açın
2. Solution Explorer'da projeyi seçin
3. Her iki projeyi de derleyin (Build > Build Solution veya F6)

### Komut Satırı ile:

```bash
# Her iki projeyi de derlemek için:
dotnet build VirusAntivirusProject.sln
```

## 🎮 Nasıl Kullanılır?

### 1. Test Virüsünü Çalıştırma

```bash
cd TestVirus
dotnet run
```

**Veya** Visual Studio'da TestVirus projesine sağ tıklayıp "Set as Startup Project" seçin ve F5'e basın.

**Program ne yapar?**
- "Virüsü Aktif Et" butonuna tıkladığınızda masaüstünüzde `TestVirusFiles` klasörü oluşturur
- İçine 5 adet test dosyası ve bir imza dosyası yerleştirir
- Bu dosyalar tamamen zararsızdır, sadece metin dosyalarıdır

### 2. Antivirüs Programını Çalıştırma

```bash
cd Antivirus
dotnet run
```

**Veya** Visual Studio'da Antivirus projesine sağ tıklayıp "Set as Startup Project" seçin ve F5'e basın.

**Program ne yapar?**
- "Tarama Başlat" butonuna tıkladığınızda masaüstünüzdeki `TestVirusFiles` klasörünü tarar
- Test virüsü dosyalarını tespit eder ve listeler
- "Tehditleri Temizle" butonu ile bu dosyaları siler

## 📖 Proje Yapısı

```
virusantivirus/
├── TestVirus/               # Test Virüsü Uygulaması
│   ├── TestVirus.csproj    # Proje dosyası
│   ├── Form1.cs            # Ana form (UI + Logic)
│   └── Program.cs          # Giriş noktası
│
├── Antivirus/              # Antivirüs Uygulaması
│   ├── Antivirus.csproj    # Proje dosyası
│   ├── Form1.cs            # Ana form (UI + Logic)
│   └── Program.cs          # Giriş noktası
│
├── VirusAntivirusProject.sln  # Solution dosyası
└── README.md               # Bu dosya
```

## 🔍 Nasıl Çalışır?

### Test Virüsü:
1. Masaüstünde `TestVirusFiles` klasörü oluşturur
2. İçine `virus_test_1.txt` - `virus_test_5.txt` adlı dosyalar yerleştirir
3. Bir `virus.signature` imza dosyası oluşturur
4. Bu dosyalar sadece metin içerir, zararsızdır

### Antivirüs:
1. Masaüstündeki `TestVirusFiles` klasörünü tarar
2. İçindeki dosya adlarında "virus" veya "signature" kelimelerini arar
3. Bulduğu dosyaları "tehdit" olarak işaretler
4. Kullanıcı onayı ile bu dosyaları siler

## 🎓 Öğrenme Hedefleri

Bu proje ile şunları öğrenebilirsiniz:

- ✅ C# Windows Forms uygulaması geliştirme
- ✅ Dosya sistemi işlemleri (oluşturma, okuma, silme)
- ✅ Kullanıcı arayüzü tasarımı
- ✅ Event handling (buton tıklama olayları)
- ✅ ProgressBar ve ListBox kullanımı
- ✅ Exception handling (hata yönetimi)
- ✅ Basit imza tabanlı virüs tespit mantığı

## 🧪 Test Senaryosu

1. İlk olarak **Test Virüsü** programını çalıştırın
2. "Virüsü Aktif Et" butonuna tıklayın
3. Masaüstünüzde `TestVirusFiles` klasörünün oluştuğunu görün
4. Ardından **Antivirüs** programını çalıştırın
5. "Tarama Başlat" butonuna tıklayın
6. Program tehditleri tespit edecek ve listeleyecek
7. "Tehditleri Temizle" butonuna tıklayın
8. Test dosyaları silinecek

## 🔒 Güvenlik

- ❌ Gerçek kötü amaçlı yazılım değildir
- ❌ Sistem dosyalarına dokunmaz
- ❌ Kayıt defterini değiştirmez
- ❌ Ağ bağlantısı kurmaz
- ❌ Kendini kopyalamaz veya yaymaz
- ✅ Sadece masaüstünde belirli bir klasörde çalışır
- ✅ Tamamen şeffaf ve açık kaynak kodludur

## 📝 Lisans

Bu proje eğitim amaçlıdır ve özgürce kullanılabilir.

## ⚡ Hızlı Başlangıç

```bash
# Projeyi klonladıktan veya indirdikten sonra:

# Test virüsünü çalıştır
cd TestVirus
dotnet run

# Yeni bir terminal/komut istemi açın ve antivirüsü çalıştır
cd Antivirus
dotnet run
```

## 🤝 Katkıda Bulunma

Eğitim amaçlı bir projedir. İyileştirme önerileriniz için pull request gönderebilirsiniz.

## 📧 İletişim

Sorularınız için issue açabilirsiniz.

---

**Not:** Bu proje sadece eğitim amaçlıdır. Gerçek antivirüs yazılımları çok daha karmaşık algoritmalar, makine öğrenmesi ve heuristik analiz kullanır.




