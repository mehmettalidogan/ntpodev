# 🐦 Flappy Bird - Klasik Oyun Klonu

Merhaba! Hepimizin bildiği ve sinir olduğu o ünlü Flappy Bird oyununun C# ile yazılmış versiyonu.

## 🎯 Oyun Nedir?

Biliyorsun zaten! Kuşu uçur, borulara çarpma, yer ile de çarpma. Mümkün olduğunca uzağa git. Basit ama bağımlılık yapan bir oyun.

## ✨ Özellikler

- **Smooth Animasyonlar**: 50 FPS akıcı oynanış
- **Güzel Grafikler**: Gradient arka plan, renkli borular, bulutlar
- **Fizik Motoru**: Yerçekimi ve zıplama mekaniği
- **Skor Sistemi**: Kaç borudan geçtiğini sayar
- **Hareketli Zemin**: Pattern ile kayan zemin efekti
- **Bulutlar**: Arka planda yavaşça hareket eden bulutlar
- **Oyun Döngüsü**: Başla, oyna, öl, tekrar başla

## 🎮 Nasıl Oynanır?

### Kontroller:
- **SPACE (Boşluk)**: Kuş zıplar
- **R**: Oyunu yeniden başlat (öldükten sonra)

### Amaç:
Kuşu boruların arasından geçir. Her geçtiğin boru +1 puan. Yere düşme, tavana çarpma, borulara çarpma!

## 🚀 Çalıştırma

1. Visual Studio veya Rider ile `FlappyBird.csproj`'yi aç
2. F5'e bas
3. SPACE ile başla
4. Eğlen (ve sinirlen 😄)

## 🛠️ Teknik Detaylar

### Sınıf Yapısı:

**Bird.cs**
- Kuşun pozisyonu ve hareketi
- Yerçekimi uygulaması
- Zıplama mekanizması
- Çizim metodu

**Pipe.cs**
- Boru çiftleri (üst ve alt)
- Hareket fonksiyonu
- Çarpışma tespiti
- Gap (boşluk) hesaplaması

**Cloud.cs**
- Dekoratif bulutlar
- Yavaş hareket
- Farklı boyutlar

**GameForm.cs**
- Ana oyun döngüsü
- Render pipeline
- Input yönetimi
- Çarpışma kontrolü

### Oyun Sabitleri:

```csharp
GRAVITY = 1            // Yerçekimi kuvveti
JUMP_FORCE = -15       // Zıplama gücü
PIPE_SPEED = 3         // Boru hızı
PIPE_GAP = 150         // Borular arası boşluk
```

Bu değerleri değiştirerek oyunu zorlaştırabilir veya kolaylaştırabilirsin!

## 🎨 Grafik Özellikleri

- **Gradient Background**: Gökyüzünden ufka doğru renk geçişi
- **Anti-Aliasing**: Düzgün kenarlar için
- **Custom Drawing**: Her şey kod ile çizilmiş (sprite yok)
- **Animated Ground**: Sürekli kayan zemin pattern'i
- **UI Elements**: Modern skor paneli ve bildirimler

## 💡 İpuçları

- Küçük dokunuşlar yap, sürekli basma
- Ritm yakala, her boru arasında 1-2 tıklama yeter
- Sabırlı ol, acele etme
- İlk 10 skorda zorlanman normal, sonra alışırsın

## 🏆 Zorluk Ayarları

Kod içinde bu değerleri değiştirerek zorluğu ayarla:

**Kolay Mod:**
```csharp
PIPE_GAP = 200         // Daha geniş boşluk
PIPE_SPEED = 2         // Daha yavaş
```

**Zor Mod:**
```csharp
PIPE_GAP = 120         // Dar boşluk
PIPE_SPEED = 5         // Hızlı hareket
GRAVITY = 2            // Daha ağır
```

**Kabus Mod:**
```csharp
PIPE_GAP = 100
PIPE_SPEED = 7
JUMP_FORCE = -12       // Daha az zıplama
```

## 🎓 Öğrendiklerim

Bu projeyi yaparken:
- Game loop nasıl yapılır öğrendim
- Double buffering ile flicker'ı çözdüm
- Çarpışma tespiti (collision detection) uyguladım
- Timer tabanlı animasyonları anladım
- Custom drawing ile grafik oluşturmayı öğrendim

## 🐛 Bilinen "Özellikler"

- Çok hızlı spam yapınca kuş ekrandan çıkabiliyor (bilerek bıraktım, speedrun için 😄)
- İlk zıplamada bazen geç tepki verir (warm-up lazım)

## 🔥 Gelecek Geliştirmeler

- **High score**: En yüksek skoru kaydet
- **Power-ups**: Kalkan, yavaşlatma, küçülme
- **Farklı kuşlar**: Seçilebilir karakterler
- **Gece modu**: Karanlık tema
- **Ses efektleri**: Zıplama, puan, ölüm sesleri
- **Multiplayer**: İki kuş yarışsın!

## 📊 Performans

- **FPS**: ~50 (20ms timer interval)
- **CPU**: %5-10 (optimize edilmiş)
- **RAM**: ~50MB
- **Render Time**: <5ms per frame

## 🎮 Oyun Mekaniği Detayları

### Fizik:
Her frame'de:
1. Yerçekimi kuşa uygulanır (velocity += GRAVITY)
2. Kuş pozisyonu güncellenir (y += velocity)
3. SPACE basıldığında velocity = JUMP_FORCE

### Boru Üretimi:
- Her boru ekrandan çıkınca yeni boru eklenir
- Boşluk yüksekliği rastgele (ama mantıklı aralıkta)
- Minimum 2, maksimum 3 boru ekranda

### Çarpışma:
Kuş rectangl'ı borularla kesişiyor mu diye kontrol edilir.

---

**Eğlen!** Bu oyun bağımlılık yapabilir, sorumlu şekilde oyna 😄

**Not:** Original Flappy Bird'ün tribute'u olarak yapıldı, eğitim amaçlıdır.


