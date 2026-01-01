# 🎥 OpenCV Nesne Tanıma Uygulaması

Merhaba! Bu proje OpenCV kullanarak gerçek zamanlı nesne tanıma yapan profesyonel bir uygulama.

## 🎯 Ne Yapar?

Kamerandan görüntü alır ve:
- Nesneleri tanır (MobileNet-SSD veya YOLO)
- Yüzleri tespit eder
- Kenar tespiti yapar
- Renk filtreleme
- Hareket algılama
- Ve daha fazlası!

## ✨ Süper Özellikler

### 🤖 AI Tabanlı Nesne Tanıma:

**MobileNet-SSD (Önerilen):**
- Hızlı ve hafif model
- 20+ nesne sınıfı (insan, araba, köpek, kedi vs.)
- Real-time çalışır
- CPU'da bile iyi performans

**YOLO (Gelişmiş):**
- Daha hassas tespit
- 80 farklı nesne sınıfı
- Biraz daha yavaş
- GPU varsa harika

### 🎨 Görüntü Modları:

1. **Normal**: Ham kamera görüntüsü
2. **Nesne Tanıma**: AI ile obje tespiti
3. **Yüz Tanıma**: Haar Cascade ile yüz bulma
4. **Kenar Tespiti**: Canny edge detection
5. **Gri Ton**: Siyah-beyaz
6. **Bulanıklaştırma**: Gaussian blur
7. **Renk Filtresi**: Belirli renkleri yakala

### ⚙️ İleri Ayarlar:

- **Parlaklık**: -100 ile +100 arası
- **Kontrast**: 0.0 ile 3.0 arası
- **Aktif Özellikler**: Birden fazla mod aynı anda
- **Snapshot**: Anı yakala ve kaydet

## 🚀 Hızlı Başlangıç

### Gereksinimler:

1. Visual Studio 2022 veya Rider
2. .NET 8.0
3. Webcam (laptop kamerası yeter)
4. Model dosyaları (otomatik indirilir)

### İlk Çalıştırma:

1. Projeyi aç (`ObjectDetection.csproj`)
2. **İlk seferde** `ModelIndir.ps1` scriptini çalıştır:
   ```powershell
   .\ModelIndir.ps1
   ```
   Bu MobileNet modelini indirir (yaklaşık 20MB)

3. F5'e bas ve çalıştır
4. "Kamerayı Başlat" butonuna tıkla
5. Modları dene!

## 📦 Model Dosyaları

### MobileNet-SSD (Otomatik):

Script indirir:
- `mobilenet_ssd.caffemodel` (~20MB)
- `mobilenet_ssd.prototxt`
- `coco.names`

### YOLO (Manuel - Opsiyonel):

Eğer YOLO kullanmak istersen:
1. [YOLO weights](https://pjreddie.com/darknet/yolo/) indir
2. `models/` klasörüne koy:
   - `yolov3-tiny.weights` (~34MB)
   - `yolov3-tiny.cfg`
   - `coco.names`

Detaylı kurulum için `KULLANIM.md`'ye bak!

## 🎮 Kullanım Rehberi

### Kamera Kontrolleri:

1. **Kamerayı Başlat**: Webcam'i aç
2. **Durdur**: Kamerayı kapat
3. **Snapshot**: Mevcut kareyi kaydet

### Mod Seçimi:

Açılır menüden birini seç:
- Normal görüntü için "Normal"
- AI için "Nesne Tanıma (MobileNet)"
- Yüz tespiti için "Yüz Tanıma"
- Kenar çizgileri için "Kenar Tespiti"

### Ayarlar:

**Parlaklık:**
- Sola kaydır: Karanlık
- Sağa kaydır: Aydınlık

**Kontrast:**
- Düşük: Düz görüntü
- Yüksek: Keskin farklar

### Aktif Özellikler:

Checkboxlar ile ek modlar ekle:
- ✓ Yüz Tanıma: Başka mod açıkken de yüz bul
- ✓ Hareket Tespiti: Hareket eden objeleri tespit et
- ✓ Renk Filtresi: Kırmızı nesneleri işaretle

## 🛠️ Teknik Detaylar

### Kullanılan Teknolojiler:

- **OpenCvSharp**: OpenCV'nin C# wrapper'ı
- **DNN Module**: Deep Neural Network desteği
- **Haar Cascade**: Klasik yüz tanıma
- **Canny Algorithm**: Kenar tespiti

### Performans:

- **MobileNet**: ~30-40 FPS (CPU)
- **YOLO**: ~15-25 FPS (CPU)
- **Cascade**: ~50 FPS
- **Memory**: ~200-300 MB

### Nesne Sınıfları:

MobileNet-SSD tanıdığı bazı nesneler:
- person (insan)
- car, truck, bus (araçlar)
- dog, cat, bird (hayvanlar)
- bottle, cup, fork (nesneler)
- laptop, keyboard, mouse (elektronik)
- ve daha fazlası!

## 💡 Kullanım İpuçları

**Daha İyi Tespit İçin:**
- İyi ışıklı ortamda kullan
- Kamerayı sabit tut
- Objeler çok hızlı hareket etmesin
- Kameraya yakın dur (yüz tanıma için)

**Performans İyileştirme:**
- MobileNet kullan (YOLO yerine)
- Parlaklık/kontrast ayarını azalt
- Tek mod kullan (birden fazla değil)
- Çözünürlüğü düşür (koddan)

**Eğlenceli Testler:**
- Aynaya tut - yüz tanımayı test et
- Telefon göster - mobil telefon tespit edilir mi?
- Rengarenk objelerle renk filtresini dene

## 🎨 Özelleştirme

### Yeni Mod Eklemek:

1. `NesneTaniyici.cs`'e yeni metod ekle
2. `MainForm.cs`'deki switch-case'e ekle
3. ComboBox'a yeni item ekle

### Güven Eşiği Ayarı:

```csharp
// Daha az tespit = yüksek eşik
var nesneler = detector.NesneleriTespitEt(mat, guvenEsigi: 0.6f);

// Daha çok tespit = düşük eşik
var nesneler = detector.NesneleriTespitEt(mat, guvenEsigi: 0.3f);
```

### Renk Değiştirme:

```csharp
// Yüz için renk
taniyici.YuzleriCiz(mat, yuzler, new Scalar(0, 255, 0)); // Yeşil

// Nesne için renk
mobileNetDetector.NesneleriCiz(mat, nesneler); // Varsayılan renkler
```

## 🐛 Sorun Giderme

**"Kamera açılamadı" Hatası:**
- Başka uygulama kamera kullanıyor olabilir (Zoom, Teams)
- Webcam izni kontrolü (Ayarlar > Gizlilik)
- Kamera bağlantısını kontrol et

**"Model yüklenemedi" Hatası:**
- `ModelIndir.ps1` çalıştırıldı mı?
- `models/` klasörü var mı?
- Dosya boyutları doğru mu?

**Yavaş Çalışma:**
- MobileNet kullan
- Çözünürlüğü düşür
- Tek mod kullan
- Arka plan programları kapat

**Yüz Tanımıyor:**
- Işığı artır
- Kameraya daha yakın
- Yüzün tamamen görünür olsun
- Gözlük takıyorsan çıkar

## 📚 Nasıl Çalışır?

### MobileNet-SSD:

1. Kameradan kare al
2. Görüntüyü 300x300'e ölçekle
3. Neural network'e gönder
4. Tespit edilen objeler + güven skorları
5. Kutu çiz ve etiketle

### YOLO:

1. Görüntüyü 416x416'ya ölçekle
2. Tek geçişte (You Only Look Once) analiz
3. Grid sistemi ile bölgeleri değerlendir
4. Non-max suppression ile en iyileri seç

### Haar Cascade:

1. Görüntüyü gri tona çevir
2. Önceden eğitilmiş cascade dosyası kullan
3. Dikdörtgen özellikler ara
4. Yüz benzeri desenleri tespit et

## 🎓 Öğrendiklerim

Bu projede:
- Deep Learning modelleri nasıl kullanılır
- OpenCV DNN module
- Real-time görüntü işleme
- Async/await ile threading
- UI responsive tutma
- Performans optimizasyonu

## 🔥 İleri Seviye

**Kendi Modelini Eğit:**
- TensorFlow veya PyTorch kullan
- ONNX formatına çevir
- OpenCV ile yükle

**Yeni Özellikler:**
- Pose estimation (iskelet tespiti)
- Face recognition (kimlik tanıma)
- Object tracking (obje takibi)
- Video kaydetme

**Farklı Kameralar:**
- IP kamera desteği
- USB kamera (index değiştir)
- Video dosya okuma

## 📸 Screenshot'lar

Program çalıştığında:
- Sol: Canlı görüntü (800x600)
- Sağ: Kontrol paneli
- Alt: Durum çubuğu
- Bilgi paneli: FPS, tespit sayısı, çözünürlük

## 🤝 Katkı

Fikirler:
- Daha fazla model desteği
- Mobil uygulama versiyonu
- Cloud AI entegrasyonu
- VR/AR desteği

---

**Eğlen ve öğren!** 

Bu proje AI ve bilgisayarlı görü öğrenmek için harika bir başlangıç noktası.

**Not:** Gerçek dünya uygulamaları için daha optimize edilmiş çözümler gerekebilir!


