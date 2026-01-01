# 📝 Not Uygulaması - Basit ve Etkili Not Defteri

Merhaba! İşte sade ama kullanışlı bir not alma uygulaması. Windows Notepad gibi ama biraz daha yetenekli.

## 🎯 Ne İşe Yarar?

Hızlı notlar almak, fikirleri kaydetmek, liste yapmak için kullanılan bir uygulama. Tüm notları düzenli şekilde "Notlar" klasöründe tutar.

## ✨ Özellikler

- **Yeni Not Oluştur**: Hızlıca boş not aç
- **Kaydet**: Notlarını otomatik klasöre kaydet
- **Listele**: Tüm notlarını sol panelde gör
- **Düzenle**: İstediğin notu açıp düzenle
- **Sil**: Gereksiz notları kaldır
- **Ara**: Not başlığı veya içeriği ile ara
- **Durum Göstergesi**: Ne olduğunu her zaman bil

## 🚀 Nasıl Kullanılır?

### Yeni Not Yazmak:

1. "Yeni Not" butonuna tıkla
2. Başlık gir (üstteki kutucuk)
3. İçeriği yaz (büyük metin kutusu)
4. "Kaydet" butonuna bas
5. Not "Notlar" klasörüne kaydedilir

### Mevcut Notu Açmak:

1. Sol taraftaki listeden notuna tıkla
2. Otomatik olarak başlık ve içerik yüklenir
3. Değişiklik yap
4. "Kaydet" ile güncelle

### Not Aramak:

1. Üstteki arama kutusuna yaz
2. Enter'a bas veya "Ara" butonuna tıkla
3. Eşleşen notlar gösterilir
4. Temizlemek için arama kutusunu boşalt

### Not Silmek:

1. Listeden silinecek notu seç
2. "Sil" butonuna tıkla
3. Onay sor, "Evet" de
4. Not kalıcı olarak silinir

## 🗂️ Dosya Yapısı

```
notugulamasi/
├── Notlar/              ← Tüm notlar burada
│   ├── Alışveriş.txt
│   ├── TODO Liste.txt
│   ├── Fikirler.txt
│   └── ...
├── Form1.cs             ← Ana kod
└── Program.cs           ← Başlangıç
```

## 🛠️ Teknik Detaylar

### Özellikler:

- **Otomatik Klasör**: "Notlar" yoksa oluşturur
- **Geçersiz Karakter**: Dosya adında kullanılamaz karakterleri '_' ile değiştirir
- **Real-time Arama**: Yazarken arar
- **Renk Kodlaması**: Durum mesajları renkli
- **Seçim Hatırlatma**: Kaydettiğin not listede seçili kalır

### Renk Sistemati:

- 🟢 **Yeşil**: Başarılı işlem (kaydedildi)
- 🔵 **Mavi**: Bilgi (yüklendi)
- 🔴 **Kırmızı**: Silme/hata
- 🟣 **Mor**: Arama sonucu
- ⚫ **Gri**: Genel durum

## 💡 İpuçları

**Hızlı Kullanım:**
- Not başlığı kısa ve öz olsun
- Dosya ismi olarak kullanılacağını unutma
- Tarih eklemek istersen: "2026-01-01 Notum"

**Düzenli Kal:**
- Gereksiz notları sil
- Benzer notları birleştir
- Açıklayıcı başlıklar kullan

**Yedekleme:**
- "Notlar" klasörünü düzenli yedekle
- Cloud'a kopyala (Dropbox, OneDrive vs.)
- Önemli notları ayrı yere kaydet

## 🎨 Tasarım

Modern flat design kullanıldı:
- Temiz ve minimal arayüz
- Büyük, okunaklı butonlar
- Anlaşılır iconlar
- Durum çubuğu ile bilgilendirme

## 🔒 Güvenlik

- Notlar düz metin (.txt) olarak kaydedilir
- Şifreleme YOK
- Herkes okuyabilir
- Hassas bilgiler için NOT UYGUN

## 📱 Kullanım Senaryoları

**Günlük Notlar:**
- Bugün neler yaptım?
- Yarın ne yapacağım?

**TODO Liste:**
- Alışveriş listesi
- Yapılacaklar
- Hedefler

**Fikirler:**
- Proje fikirleri
- Yazı konuları
- İş planları

**Öğrenme:**
- Ders notları
- Kod snippetleri
- Linkler ve kaynaklar

## 🐛 Bilinen Limitler

- Çok büyük notlarda (>1MB) yavaşlama olabilir
- Resim veya dosya ekleyemezsin
- Formatting yok (bold, italic vs.)
- Print özelliği yok

## 🔥 Gelecek Özellikler

Eklenebilecek şeyler:
- [ ] Rich text desteği (formatting)
- [ ] Kategoriler/etiketler
- [ ] Not sıralama (tarih, isim)
- [ ] Export (PDF, Word)
- [ ] Şifreleme
- [ ] Bulut senkronizasyon
- [ ] Dark mode
- [ ] Font seçimi
- [ ] Print özelliği

## 💻 Kod Yapısı

### Ana Metodlar:

**KlasorKontrol()**
- "Notlar" klasörü var mı kontrol eder
- Yoksa oluşturur

**NotlariYukle()**
- Klasördeki tüm .txt dosyalarını listeler
- ListBox'a ekler

**btnKaydet_Click()**
- Başlık kontrolü
- Geçersiz karakter temizleme
- Dosyaya yazma
- Liste güncelleme

**btnSil_Click()**
- Onay alma
- Dosya silme
- Liste güncelleme

**listBoxNotlar_SelectedIndexChanged()**
- Seçilen notu yükle
- Başlık ve içeriği göster

**btnAra_Click()**
- Başlık ve içerikte ara
- Eşleşenleri filtrele

## 🎓 Öğrendiklerim

Bu projede:
- File I/O işlemleri
- ListBox kullanımı
- String manipülasyonu
- Path işlemleri
- Event handling
- UI feedback (durum mesajları)

## 🤝 Katkıda Bulun

Geliştirme fikirlerin varsa:
1. Fork yap
2. Özelliği ekle
3. Pull request aç

## 📜 Lisans

Bu proje eğitim amaçlıdır, istediğin gibi kullanabilirsin!

---

**Basit ama işe yarıyor!** Bazen en basit çözümler en iyisidir. 😊

**Not:** Hassas bilgiler için daha güvenli uygulamalar kullan!


