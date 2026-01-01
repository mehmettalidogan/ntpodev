# 🎤 Speech to Text - Konuşmadan Yazıya

Selam! Bu uygulama konuştuğun şeyleri yazıya çeviren basit ama şık bir program.

## 🎯 Ne İşe Yarar?

Mikrofona konuş, uygulama ne dediğini anlasın ve metin olarak yazsın. Dictation (dikte) uygulaması gibi düşün.

## ✨ Özellikler

- **Gerçek Zamanlı Tanıma**: Konuştukça yazar
- **Türkçe ve İngilizce**: Sisteminizde hangi dil varsa
- **Dark Mode**: Modern, gözü yormayan arayüz
- **Kopyala**: Metni hızlıca panoya kopyala
- **Temizle**: Baştan başla
- **Güvenlik Göstergesi**: Mikrofon açık mı kapalı mı belli

## 🚀 Nasıl Çalıştırılır?

### Gereksinimler:

1. **Windows Speech Recognition** yüklü olmalı
2. **Mikrofon** çalışır durumda
3. **Dil paketi** kurulu (TR veya EN-US)

### İlk Kurulum:

**Windows Speech Recognition Aktif Et:**

1. `Ayarlar` > `Zaman ve dil` > `Konuşma` git
2. "Konuşma tanıma"yı aç
3. Mikrofonunu ayarla ve test et
4. Dil paketi yoksa indir

**Mikrofon İzni:**

1. `Ayarlar` > `Gizlilik ve güvenlik` > `Mikrofon`
2. "Masaüstü uygulamalarının mikrofona erişmesine izin ver" - AÇ
3. Denetim Masası > Ses > Kayıt sekmesi
4. Mikrofonunu varsayılan yap

### Kullanım:

1. Uygulamayı başlat
2. Ortadaki büyük mikrofon butonuna tıkla
3. Yeşil olduğunda konuşmaya başla
4. Dediklerin yazıya dönüşür
5. Bitince tekrar tıkla (kapat)
6. "Kopyala" ile metni panoya al

## 🎨 Arayüz

**Dark Mode Tasarım:**
- Koyu arka plan (göz yormuyor)
- Modern kart stil
- Büyük, kullanımı kolay butonlar
- Renkli durum göstergeleri

**Renk Kodları:**
- 🔴 Kırmızı: Kapalı
- 🟢 Yeşil: Dinleniyor
- 🟡 Turuncu: Hata/Uyarı

## 🛠️ Teknik Detaylar

### Kullanılan API:

- **System.Speech.Recognition**: Windows'un yerleşik speech API'si
- **SpeechRecognitionEngine**: Ana motor
- **DictationGrammar**: Serbest konuşma tanıma

### Nasıl Çalışır:

1. Sisteme yüklü tanıyıcıları listele
2. TR varsa onu seç, yoksa EN-US
3. Mikrofonu varsayılan olarak ayarla
4. `RecognizeAsync` başlat
5. Her kelime tanındığında event tetiklenir
6. Metin kutusuna ekle

### Güven Skoru:

Kod %68'den yüksek güvene sahip kelimeleri kabul eder:

```csharp
if (e.Result.Confidence > 0.68)
{
    // Metni ekle
}
```

Bu değeri değiştirerek daha hassas veya daha toleranslı yapabilirsin.

## 💡 Kullanım İpuçları

**Daha İyi Tanıma İçin:**

- 🎤 Kaliteli mikrofon kullan
- 🔇 Sessiz ortamda konuş
- 🗣️ Net ve yavaş konuş
- 📏 Mikrofondan 15-30 cm uzakta ol
- 🎵 Arka plan müziğini kapat

**Püf Noktaları:**

- Noktalama işaretleri için: "nokta", "virgül", "soru işareti" de
- Yeni satır için: "yeni satır" veya "enter"
- Düzeltme yaparken klavye kullan
- Uzun metinlerde ara ver (motor dinlensin)

## 🐛 Sorun Giderme

### "Tanıyıcı Bulunamadı" Hatası:

**Çözüm:**
1. Windows Speech Recognition kurulu mu kontrol et
2. Dil paketi indir (TR-TR veya EN-US)
3. Bilgisayarı yeniden başlat
4. Programı tekrar aç

### "Mikrofon Bulunamadı" Hatası:

**Çözüm:**
1. Mikrofon takılı mı kontrol et
2. Ayarlar > Gizlilik > Mikrofon izinlerini kontrol et
3. Ses Denetim Masası'ndan mikrofonu varsayılan yap
4. Başka program mikrofonu kullanıyor olabilir (Zoom, Teams)

### Kelimeler Hatalı Tanınıyor:

**Çözüm:**
- Daha net konuş
- Mikrofonuna yaklaş
- Windows Speech Recognition'ı eğit (Kontrol Paneli'nden)
- Arka plan gürültüsünü azalt

### Program Donuyor:

**Çözüm:**
- Mikrofonu kapat ve tekrar aç
- Programı yeniden başlat
- Mikrofon driver'ını güncelle

## 🎓 Gelişmiş Özellikler

### Custom Commands (İleride):

Özel komutlar eklenebilir:
- "temizle" → Metni sil
- "kaydet" → Dosyaya kaydet
- "gönder" → Mail ile gönder

### Dil Değiştirme:

Kod içinden dil değiştirilebilir:

```csharp
// Türkçe için
new System.Globalization.CultureInfo("tr-TR")

// İngilizce için
new System.Globalization.CultureInfo("en-US")
```

### Dosyaya Kaydetme:

"Kaydet" butonu ekleyerek:
```csharp
File.WriteAllText("transkript.txt", textBox.Text);
```

## 📝 Kullanım Senaryoları

**Notlar:**
- Toplantı notları
- Ders notları
- Hızlı fikirler

**Yazma:**
- Makale taslağı
- E-posta yazma
- Rapor oluşturma

**Erişilebilirlik:**
- Klavye kullanamayanlara yardım
- Hızlı yazma gerektiğinde
- Eller meşgul iken

## 🔒 Gizlilik

- **İnternet kullanmaz**: Her şey lokal
- **Kayıt tutmaz**: Metin sadece ekranda
- **Microsoft'a gitmez**: Windows API lokal çalışır
- **Sen kontroldesin**: İstediğin zaman kapat

## 🌍 Desteklenen Diller

Program otomatik olarak sisteminde yüklü dili seçer:

**Tercih sırası:**
1. Türkçe (tr-TR) - varsa
2. İngilizce (en-US) - yoksa
3. Diğer diller - sistemde ne varsa

**Yeni Dil Eklemek:**

1. Windows Settings > Time & Language > Language
2. Preferred language ekle
3. Speech paketini indir
4. Programı yeniden başlat

## 💻 Kod Yapısı

### Ana Sınıflar:

**MainForm**
- UI yönetimi
- Event handling

**SetupSpeechRecognition()**
- Tanıyıcı yükle
- Dil seç
- Mikrofon bağla

**MicButton_Click()**
- Başlat/durdur
- Icon değiştir
- Durum güncelle

**SpeechRecognized Event**
- Kelime geldi
- Güven kontrolü
- Metne ekle

## 🎨 UI Özellikleri

- **Rounded Corners**: Yuvarlatılmış kenarlar
- **Gradient Buttons**: Renk geçişli butonlar
- **Custom Icons**: El çizimi simgeler
- **Responsive**: Pencere boyutuna uyum
- **Smooth Animations**: Yumuşak geçişler

## 📚 Öğrendiklerim

Bu projede:
- Speech Recognition API kullanımı
- Event-driven programming
- Custom UI çizimi (GDI+)
- Thread-safe UI update
- Error handling
- Windows API entegrasyonu

## 🔮 Gelecek Özellikler

- [ ] Özel komutlar
- [ ] Dosyaya otomatik kaydetme
- [ ] Farklı dil seçimi (UI'dan)
- [ ] Ses kaydı (backup)
- [ ] Transkript export (Word, PDF)
- [ ] Cloud sync
- [ ] Gerçek zamanlı çeviri
- [ ] Noktalama otomasyonu

## 🤝 Katkı

Fikirlerin varsa:
- Issue aç
- Pull request gönder
- Yeni özellik öner

---

**Konuş ve yaz!** 

Bu uygulama yazma hızını 2-3 katına çıkarabilir.

**İpucu:** Mikrofon kalitesi çok önemli! İyi bir mikrofon yatırımı işe yarar.

**Not:** Windows Speech Recognition mükemmel değil, bazen hata yapabilir. Sabırlı ol! 😊


