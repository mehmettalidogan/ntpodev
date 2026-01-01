# 🖼️ Image Processing ML - Görüntü İşleme ve Makine Öğrenmesi

Selam! Bu proje görüntü işleme algoritmaları ve temel makine öğrenmesi yöntemlerini birleştiren kapsamlı bir uygulama.

## 🎯 Ne İşe Yarar?

Bir resim yükle, üzerine çeşitli filtreler uygula, makine öğrenmesi algoritmaları dene. Photoshop'un çok çok basit versiyonu + biraz matematik ve ML.

## ✨ Ana Özellikler

### 📷 Görüntü Filtreleri:

1. **Blur (Bulanıklaştırma)**: Resmi yumuşatır, konvolüsyon matrisi ile
2. **Edge Detection (Kenar Tespiti)**: Sobel filtresi ile kenarları bulur
3. **Sharpen (Keskinleştirme)**: Detayları artırır
4. **Brightness (Parlaklık)**: Resmi aydınlatır veya koyulaştırır
5. **Contrast (Kontrast)**: Renk farklarını artırır
6. **Sepia**: Nostaljik vintage efekti
7. **Invert (Tersine Çevir)**: Renkleri ters çevirir
8. **Grayscale (Gri Ton)**: Siyah-beyaz yapar

### 🤖 Makine Öğrenmesi:

1. **Linear Regression**: Doğrusal regresyon, tahmin yapar
2. **K-Means Clustering**: Veri noktalarını gruplara ayırır
3. **Matrix Operations**: Matris çarpımı ve işlemleri

### 📊 Analiz Araçları:

- **Histogram**: Piksel dağılımını gösterir
- **Görüntü Bilgisi**: Boyut, çözünürlük vs.

## 🚀 Nasıl Kullanılır?

### Görüntü İşleme:

1. "Görüntü Yükle" butonuna tıkla
2. Bir resim seç (jpg, png, bmp)
3. İstediğin filtreye tıkla
4. Sol tarafta orijinal, sağ tarafta işlenmiş hali görünür
5. Beğendiysen "Kaydet" ile dışa aktar

### Makine Öğrenmesi:

1. "Linear Regression" veya "K-Means" butonuna tıkla
2. Algoritma örnek veri ile çalışır
3. Sonuçları metin kutusunda görebilirsin
4. Matris çarpımı için "Matrix Multiply" butonunu kullan

## 🛠️ Teknik Mimari

### Models (Veri Yapıları):

**Image.cs**
- Piksel verilerini tutar
- RGB değerlerini yönetir
- Histogram hesaplama
- Bitmap dönüşümü

**Matrix.cs**
- Matris işlemleri için
- Boyut kontrolü
- Değer get/set metodları

**DataPoint.cs**
- ML algoritmaları için veri noktası

**Point2D.cs**
- K-Means için 2D koordinat

### Services (Algoritmalar):

**IImageFilter (Interface)**
- Tüm filtreler bu interface'i implement eder
- `Apply(Image img)` metodu zorunlu

**Filtreler:**
- `BlurFilter`: 3x3 konvolüsyon
- `EdgeDetectionFilter`: Sobel operatörü
- `SharpenFilter`: Keskinleştirme matrisi
- `BrightnessFilter`: Piksel değeri ekleme
- `ContrastFilter`: Faktör ile çarpma
- `SepiaFilter`: Renk matrisi dönüşümü
- `InvertFilter`: 255 - değer
- `GrayscaleFilter`: Ağırlıklı ortalama

**ML Servisleri:**
- `LinearRegression`: Least squares yöntemi
- `KMeansClustering`: Kümeleme algoritması
- `MatrixOperations`: Temel matris işlemleri

**ImageProcessor**
- Genel görüntü işlemleri koordinatörü

## 💡 Filtre Nasıl Çalışır?

### Blur (Bulanıklaştırma):

```
[1 1 1]
[1 1 1] / 9
[1 1 1]
```

Her pikseli komşularının ortalaması ile değiştirir.

### Edge Detection (Sobel):

```
[-1 0 1]      [-1 -2 -1]
[-2 0 2]  ve  [ 0  0  0]
[-1 0 1]      [ 1  2  1]
```

Yatay ve dikey gradyanları bulup birleştirir.

### Sharpen (Keskinleştirme):

```
[ 0 -1  0]
[-1  5 -1]
[ 0 -1  0]
```

Merkezi güçlendirir, kenarları vurgular.

## 🎓 Öğrendiklerim

Bu projede:
- **Konvolüsyon**: Nasıl uygulanır, neden işe yarar
- **Renk Uzayları**: RGB, HSV, grayscale
- **Matris Operasyonları**: Çarpma, toplama, transpose
- **ML Algoritmaları**: Regression ve clustering
- **Bitmap İşleme**: C# ile nasıl piksel manipülasyonu yapılır
- **Interface Pattern**: Genişletilebilir kod yapısı

## 🔬 Makine Öğrenmesi Detayları

### Linear Regression:

```
y = mx + b
```

Eğim (m) ve kesişim (b) hesaplanır. Örnek:
- Veri: (1,2), (2,4), (3,6)
- Sonuç: m=2, b=0 → y=2x

### K-Means Clustering:

1. K adet merkez rastgele seç
2. Her noktayı en yakın merkeze ata
3. Merkezleri yeniden hesapla
4. Değişim kalmayana kadar tekrarla

Örnek: 7 nokta → 2 küme

## 🎨 Örnek Kullanım Senaryoları

**Fotoğraf Düzenleme:**
- Blur ile arka plan flu yap
- Sharpen ile detay artır
- Brightness ile aydınlat
- Sepia ile vintage efekt

**Analiz:**
- Histogram ile ton dağılımını gör
- Edge detection ile obje sınırlarını bul

**Öğrenme:**
- Linear regression ile trend analizi
- K-Means ile renk paleti çıkarma

## ⚡ Performans

- **Küçük Resimler** (<400x400): Anında
- **Orta Resimler** (400-800): 1-2 saniye
- **Büyük Resimler** (>800): 3-5 saniye

Otomatik olarak 400x400'e küçültülür (performans için).

## 🐛 Bilinen Limitler

- Çok büyük resimler yavaş işlenir
- ML algoritmaları sadece demo veri kullanır
- Bazı filtreler kenar piksellerde soruna yol açabilir

## 🔥 Gelecek Özellikler

- Daha fazla filtre (Gaussian blur, median filter)
- Neural network desteği
- Batch processing (toplu işlem)
- Video işleme
- Real-time kamera filtresi
- Undo/Redo sistemi

## 📚 Kullanılan Algoritmalar

- **Konvolüsyon**: 3x3 kernel
- **Sobel Operatörü**: Kenar tespiti
- **Least Squares**: Linear regression
- **Lloyd's Algorithm**: K-Means
- **Euclidean Distance**: Mesafe hesabı

## 💻 Kod Örnekleri

### Yeni Filtre Eklemek:

```csharp
public class MyFilter : IImageFilter
{
    public string Name => "My Cool Filter";
    
    public Image Apply(Image img)
    {
        Image result = new Image(img.Width, img.Height);
        // ... işlemler ...
        return result;
    }
}
```

### UI'a Eklemek:

```csharp
private void btnMyFilter_Click(object sender, EventArgs e)
{
    ApplyFilter(new MyFilter());
}
```

---

**Not:** Bu eğitim projesidir. Üretim ortamı için OpenCV veya AForge.NET gibi kütüphaneler önerilir!


