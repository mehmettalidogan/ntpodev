# Detaylı Kullanım Kılavuzu - Windows Forms

## İçindekiler
1. [İlk Kurulum](#ilk-kurulum)
2. [Arayüz Tanıtımı](#arayüz-tanıtımı)
3. [Özellik Detayları](#özellik-detayları)
4. [İpuçları ve Püf Noktaları](#ipuçları-ve-püf-noktaları)
5. [Sorun Giderme](#sorun-giderme)

---

## İlk Kurulum

### 1. Gereksinimleri Kontrol Edin

```powershell
# .NET versiyonunu kontrol edin
dotnet --version
# Çıktı: 6.0.x veya üzeri olmalı
```

### 2. Projeyi Derleyin

```bash
cd opencv
dotnet restore
dotnet build
```

### 3. Model Dosyalarını İndirin

**Windows için:**
```powershell
.\ModelIndir.ps1
```

**Manuel indirme:**
1. [OpenCV GitHub](https://github.com/opencv/opencv/tree/master/data/haarcascades) adresine gidin
2. `haarcascade_frontalface_default.xml` dosyasını indirin
3. Proje klasörüne kopyalayın

### 4. Uygulamayı Başlatın

```bash
dotnet run
```

veya Visual Studio'da F5'e basın.

---

## Arayüz Tanıtımı

### Ana Pencere Bölgeleri

```
┌─────────────────────────────────┬──────────────────┐
│                                 │  Kontrol Paneli  │
│                                 │                  │
│        Video Görüntüsü         │  • Kamera        │
│         (800x600)               │  • Dosya         │
│                                 │  • Modlar        │
│                                 │  • Özellikler    │
│                                 │  • Ayarlar       │
│                                 │  • Bilgi         │
└─────────────────────────────────┴──────────────────┘
│              Durum Çubuğu                          │
└────────────────────────────────────────────────────┘
```

### Kontrol Paneli Elemanları

#### 1. Kamera Kontrolleri
- **📹 Kamerayı Başlat**: Webcam'i açar ve görüntü akışını başlatır
- **⏸ Durdur**: Kamerayı durdurur ve kaynakları serbest bırakır

#### 2. Dosya İşlemleri
- **📁 Görüntü Aç**: Bilgisayardan görüntü dosyası yükler
- **📷 Snapshot**: Anlık görüntü alır ve kaydeder
- **Mod Seçimi**: Görüntü işleme modunu seçer

#### 3. Aktif Özellikler (Checkbox'lar)
- **✓ Yüz Tanıma**: Yüz tespitini aktif eder
- **✓ Hareket Tespiti**: Hareket algılamayı aktif eder
- **✓ Kırmızı Renk Filtresi**: Renk bazlı nesne bulmayı aktif eder

#### 4. Görüntü Ayarları
- **Parlaklık Slider**: -100 ile +100 arası (-50 = daha koyu, +50 = daha parlak)
- **Kontrast Slider**: 0.0 ile 3.0 arası (1.0 = normal, 2.0 = yüksek kontrast)

#### 5. Bilgi Paneli
- **FPS**: Saniyedeki kare sayısı (frames per second)
- **Tespit**: Tespit edilen nesne/yüz sayısı
- **Çözünürlük**: Görüntü boyutu (genişlik x yükseklik)

---

## Özellik Detayları

### Mod 1: Normal
**Ne Yapar:**
- Kameradan gelen ham görüntüyü gösterir
- Hiçbir işleme uygulanmaz

**Kullanım Senaryoları:**
- Kamera açısını ayarlamak için
- Aydınlatmayı kontrol etmek için
- Temel görüntü kalitesini görmek için

---

### Mod 2: Yüz Tanıma

**Ne Yapar:**
- Haar Cascade Classifier kullanır
- Görüntüdeki yüzleri tespit eder
- Her yüzü yeşil dikdörtgenle işaretler
- Üzerine "Yuz #N" etiketi yazar

**Nasıl Çalışır:**
1. Görüntü gri tona çevrilir
2. Histogram eşitleme uygulanır
3. Cascade classifier yüzleri arar
4. Bulunan yüzler işaretlenir

**Parametreler:**
- `scaleFactor`: 1.1 (varsayılan)
- `minNeighbors`: 5
- `minSize`: 30x30 piksel

**İpuçları:**
- İyi aydınlatma önemlidir
- Yüzünüz kameraya dönük olmalı
- 50cm - 2m arası mesafe ideal
- Aşırı parlaklık/karanlık sorun yaratabilir

**Performans:**
- Ortalama: 25-30 FPS
- CPU kullanımı: Orta

---

### Mod 3: Kenar Tespiti

**Ne Yapar:**
- Canny kenar tespiti algoritması kullanır
- Nesnelerin sınırlarını bulur
- Kenarları beyaz çizgilerle gösterir

**Nasıl Çalışır:**
1. Gri tona çevirme
2. Gaussian blur (gürültü azaltma)
3. Gradyan hesaplama
4. Non-maximum suppression
5. Hysteresis thresholding

**Parametreler:**
- Alt eşik: 50
- Üst eşik: 150

**Kullanım Alanları:**
- Nesne sınırlarını bulmak
- Şekil analizi
- Görüntü segmentasyonu
- Kontür tespiti için ön işleme

**İpuçları:**
- Düz, tek renkli arka plan en iyi sonucu verir
- Aşırı detaylar karmaşık görüntü oluşturur
- Parlaklık/kontrast ayarıyla sonucu iyileştirebilirsiniz

---

### Mod 4: Gri Ton

**Ne Yapar:**
- Renkli görüntüyü siyah-beyaza çevirir
- Klasik güvenlik kamerası görünümü

**Kullanım Alanları:**
- Işık koşullarını test etmek
- Gece görüşü simülasyonu
- Bellek tasarrufu (üçte bir boyut)

---

### Mod 5: Bulanıklaştırma

**Ne Yapar:**
- Gaussian blur filtresi uygular
- Görüntüyü yumuşatır

**Parametreler:**
- Kernel boyutu: 25x25

**Kullanım Alanları:**
- Gürültü azaltma
- Arka plan flu efekti
- Küçük detayları gizleme
- Pikselleşmeyi azaltma

---

### Mod 6: Renk Filtresi

**Ne Yapar:**
- HSV renk uzayında kırmızı nesneleri bulur
- Kontürleri yeşil çizgilerle gösterir
- Nesne sayısını bildirir

**Nasıl Çalışır:**
1. BGR → HSV dönüşümü
2. Renk aralığı maskeleme (0-10 ve 170-180 derece)
3. Morfolojik işlemler (open/close)
4. Kontür bulma
5. Küçük nesneleri filtreleme (min alan: 500px²)

**Renk Aralığını Değiştirme:**
MainForm.cs'de şu satırları bulun:
```csharp
var maske = taniyici.RenkMaskesiOlustur(islenmisMat, 
    new Scalar(0, 120, 70),      // Alt sınır (H, S, V)
    new Scalar(10, 255, 255));   // Üst sınır (H, S, V)
```

**Farklı Renkler için HSV Değerleri:**

| Renk | Alt Sınır | Üst Sınır |
|------|-----------|-----------|
| Kırmızı | (0, 120, 70) | (10, 255, 255) |
| Kırmızı2 | (170, 120, 70) | (180, 255, 255) |
| Yeşil | (40, 50, 50) | (80, 255, 255) |
| Mavi | (100, 100, 100) | (130, 255, 255) |
| Sarı | (20, 100, 100) | (30, 255, 255) |
| Turuncu | (10, 100, 100) | (25, 255, 255) |
| Mor | (130, 50, 50) | (160, 255, 255) |

---

### Ek Özellikler (Checkbox'lar)

Bu özellikler seçilen moddan bağımsız olarak çalışır ve aynı anda aktif olabilir.

#### ✓ Yüz Tanıma
- Hangi mod seçiliyse üstüne yüz tespiti ekler
- Yüzler mavi dikdörtgenle işaretlenir
- Tespit sayısına eklenir

#### ✓ Hareket Tespiti
- Önceki ve mevcut kare arasındaki farkı bulur
- Hareketli bölgeleri kırmızı dikdörtgenle işaretler
- "HAREKET!" etiketi gösterir
- Minimum alan: 1000 piksel

**Kullanım Senaryoları:**
- Güvenlik kamerası
- Otomasyon sistemleri
- Varlık tespiti

#### ✓ Kırmızı Renk Filtresi
- Kırmızı nesnelerin merkezini sarı nokta ile işaretler
- Seçilen modun üzerine eklenir

---

## İpuçları ve Püf Noktaları

### Performans İyileştirme

1. **FPS Düşükse:**
   - Tek bir mod kullanın
   - Ek özellikleri kapatın
   - Parlaklık/Kontrast ayarlarını varsayılana getirin

2. **CPU Kullanımını Azaltmak:**
   ```csharp
   // MainForm.cs → KameraLoop metodunda
   Thread.Sleep(30); // 30'u 50'ye çıkarın (daha düşük FPS)
   ```

3. **Bellek Tasarrufu:**
   - Snapshot'ları düzenli silin
   - Uygulamayı kullanmadığınızda kamerayı durdurun

### En İyi Görüntü Kalitesi İçin

1. **Aydınlatma:**
   - Yüzünüze doğru ışık gelsin
   - Arka planda güçlü ışık kaynağı olmasın
   - Doğal ışık en iyi sonucu verir

2. **Kamera Konumu:**
   - Göz hizasında
   - 50cm - 150cm arası mesafe
   - Sabit ve titremeden

3. **Arka Plan:**
   - Tek renkli ve düz
   - Fazla detay olmasın
   - Hareketli objelerden kaçının

### Snapshot İpuçları

1. **Otomatik İsimlendirme:**
   - Format: `snapshot_YYYYMMDD_HHMMSS.jpg`
   - Örnek: `snapshot_20241031_143025.jpg`

2. **Kaliteli Snapshot:**
   - Kamerayı durdurun
   - İstediğiniz modu ayarlayın
   - Snapshot alın
   - Dosya proje klasörüne kaydedilir

---

## Sorun Giderme

### Problem: "Kamera açılamadı!" hatası

**Çözüm 1: Başka Uygulama Kullanıyor**
- Skype, Teams, Zoom gibi uygulamaları kapatın
- Task Manager'da kamera kullanan processları kontrol edin

**Çözüm 2: İzin Sorunu**
1. Windows Ayarlar → Gizlilik ve güvenlik
2. Kamera → Uygulamaların kamerayı kullanmasına izin ver
3. Masaüstü uygulamalarına izin verin

**Çözüm 3: Farklı Kamera ID'si**
MainForm.cs'de 140. satırda:
```csharp
// Değiştir
kamera = new VideoCapture(0);
// Şöyle deneyin
kamera = new VideoCapture(1); // veya 2, 3
```

### Problem: Cascade dosyası bulunamadı

**Belirti:**
- "Uyarı - Cascade dosyaları yüklenemedi" mesajı
- Yüz tanıma çalışmıyor

**Çözüm:**
```powershell
# 1. Script ile indir
.\ModelIndir.ps1

# 2. Manuel kontrol
Get-Item haarcascade_frontalface_default.xml
# Dosya proje klasöründe olmalı

# 3. Yoksa manuel indir
# https://github.com/opencv/opencv/tree/master/data/haarcascades
```

### Problem: Düşük FPS (< 15)

**Olası Sebepler:**
- Çok fazla özellik aktif
- Zayıf donanım
- Yüksek çözünürlük

**Çözümler:**
1. Tek mod kullanın
2. Ek özellikleri kapatın
3. Arkaplan uygulamalarını kapatın

### Problem: Yüz tanımıyor

**Kontrol Listesi:**
- [ ] Cascade dosyası yüklü mü?
- [ ] Yeterli aydınlatma var mı?
- [ ] Yüz kameraya dönük mü?
- [ ] Çok yakın/uzak değil mi?
- [ ] Gözlük takıyorsanız yansıma yapıyor mu?

**İyileştirmeler:**
```csharp
// NesneTaniyici.cs → YuzleriTespitEt metodunda
// minNeighbors değerini azaltın (daha hassas ama daha fazla yanlış pozitif)
var yuzler = cascade.DetectMultiScale(
    gri,
    scaleFactor: 1.1,
    minNeighbors: 3,  // 5'ten 3'e düşürdük
    minSize: new Size(30, 30)
);
```

### Problem: Uygulama donuyor

**Çözüm:**
1. Kamerayı durdurun
2. Uygulamayı kapatın
3. Task Manager'dan zorla sonlandırın
4. Yeniden başlatın

**Önleme:**
- Snapshot alırken bekleyin
- Modları sık sık değiştirmeyin
- Ayarları yavaşça değiştirin

### Problem: Snapshot kaydedilmiyor

**Kontrol:**
```powershell
# Proje klasörü yazılabilir mi?
Test-Path -Path . -PathType Container
# True dönmeli

# Snapshot dosyalarını listele
Get-ChildItem -Filter "snapshot_*.jpg"
```

---

## Gelişmiş Kullanım

### Özel Renk Bulma

1. MainForm.cs'yi açın
2. `KameraLoop` metodunu bulun
3. "Renk Filtresi" case'inde HSV değerlerini değiştirin:

```csharp
case "Renk Filtresi":
    // Yeşil nesne bulmak için:
    var maske = taniyici.RenkMaskesiOlustur(islenmisMat, 
        new Scalar(40, 50, 50),     // Yeşil alt sınır
        new Scalar(80, 255, 255));  // Yeşil üst sınır
    // ... kalan kod
    break;
```

### Video Kaydetme (İsteğe Bağlı Özellik)

MainForm.cs'ye ekleyebilirsiniz:

```csharp
private VideoWriter? videoWriter;
private bool kayitYapiliyor = false;

private void BtnKayitBaslat_Click(object sender, EventArgs e)
{
    if (!kayitYapiliyor && suankiGoruntu != null)
    {
        string dosyaAdi = $"kayit_{DateTime.Now:yyyyMMdd_HHmmss}.mp4";
        videoWriter = new VideoWriter(
            dosyaAdi,
            FourCC.MP4V,
            30.0,
            new OpenCvSharp.Size(suankiGoruntu.Width, suankiGoruntu.Height)
        );
        kayitYapiliyor = true;
    }
}

// KameraLoop'ta
if (kayitYapiliyor && videoWriter != null)
{
    videoWriter.Write(islenmisMat);
}
```

---

## Kısayol Tuşları (İsteğe Bağlı Eklenebilir)

MainForm'a KeyPreview = true ekleyerek:

```csharp
protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
{
    switch (keyData)
    {
        case Keys.Space:
            BtnSnapshot_Click(null, null);
            return true;
        case Keys.Escape:
            if (kameraAktif) KamerayiDurdur();
            return true;
        case Keys.F1:
            cmbModlar.SelectedIndex = 0; // Normal
            return true;
        // ... diğer modlar
    }
    return base.ProcessCmdKey(ref msg, keyData);
}
```

---

Bu kılavuz ile uygulamayı eksiksiz kullanabilirsiniz. Daha fazla soru için issue açabilirsiniz!

İyi çalışmalar! 🚀
