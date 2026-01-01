# 🤖 ChatBot LLM - Yapay Zeka Sohbet Arkadaşın

Selam! Bu proje bir AI chatbot uygulaması. Hem offline (simülasyon) hem de online (gerçek OpenAI API) modda çalışabiliyor.

## 🎯 Nedir Bu?

Basitçe söylemek gerekirse, bu bir yapay zeka sohbet botu. ChatGPT gibi düşün ama çok daha basit ve masaüstü uygulaması olarak. Sorularını soruyorsun, bot cevap veriyor.

## ✨ Özellikler

- **İki Mod:**
  - 📵 **Offline Mod**: İnternet gerekmez, basit cevaplar verir (demo amaçlı)
  - 🌐 **Online Mod**: OpenAI API kullanarak gerçek AI cevapları alır
  
- **Konuşma Geçmişi**: Tüm konuşmalarını kaydeder ve tekrar okuyabilirsin
- **Mesaj Dışa Aktarma**: Konuşmalarını .txt dosyası olarak kaydedebilirsin
- **Mesaj İçe Aktarma**: Önceki konuşmaları geri yükleyebilirsin
- **Model Seçimi**: GPT-3.5-turbo, GPT-4 gibi farklı modeller seçebilirsin
- **System Prompt**: Botun davranışını özelleştirebilirsin
- **Renkli Arayüz**: Her konuşanın kendine has rengi var

## 🚀 Nasıl Kullanılır?

### Offline Mod (Kolay):

1. Projeyi aç ve çalıştır (Visual Studio ile)
2. Bir kullanıcı adı gir
3. Direkt mesaj yazmaya başla!
4. Bot basit şekilde cevap verecek

### Online Mod (Gerçek AI):

1. Ayarlar butonuna tıkla (⚙️)
2. OpenAI API anahtarını gir
3. "Online Mod Kullan" seçeneğini işaretle
4. İstersen model seçimini yap (GPT-3.5, GPT-4 vs.)
5. System prompt ile botun karakterini belirle (opsiyonel)
6. Kaydet ve kapat
7. Artık gerçek AI ile konuşuyorsun!

## 🔑 OpenAI API Anahtarı Nasıl Alınır?

1. [OpenAI Platform](https://platform.openai.com) sitesine git
2. Hesap oluştur (veya giriş yap)
3. API Keys bölümüne git
4. "Create new secret key" de
5. Anahtarı kopyala ve ayarlara yapıştır

**Önemli:** API kullanımı ücretli! Ama ilk hesaplar genelde ücretsiz kredi ile geliyor.

## 🛠️ Teknik Detaylar

**Mimari:**
- MVVM benzeri yapı
- Service pattern (ILLMService, IMessageStorage)
- Dependency injection kullanımı

**Servisler:**
- `OpenAIService`: Offline mod için basit yanıtlar
- `RealOpenAIService`: Gerçek OpenAI API entegrasyonu
- `InMemoryStorage`: Konuşmaları hafızada tutar
- `ChatBot`: Ana bot mantığı

**Modeller:**
- `User`: Kullanıcı bilgileri
- `Message`: Mesaj yapısı
- `Conversation`: Konuşma yönetimi

## 💡 Kullanım İpuçları

- **Enter ile Gönder**: Shift+Enter yapmadan Enter'a basarsan mesaj gider
- **Temizle**: Konuşma geçmişini temizleyebilirsin
- **Geçmiş**: Tüm konuşma geçmişini popup'ta görebilirsin
- **Kaydet**: Önemli konuşmaları kaydetmeyi unutma!

## 🎨 Arayüz

Modern ve temiz bir arayüz tasarladım:
- Mavi başlıklar (kullanıcı mesajları)
- Yeşil başlıklar (bot mesajları)
- Kırmızı başlıklar (sistem mesajları/hatalar)
- Üst kısımda mod göstergesi (Online/Offline)

## 📝 System Prompt Örnekleri

Botun kişiliğini değiştirebilirsin:

```
Sen yardımsever bir asistansın.
```

```
Sen komik ve esprili bir arkadaşsın. Her cevaba bir şaka kat.
```

```
Sen ciddi bir profesörsün. Bilimsel ve detaylı cevaplar ver.
```

## 🐛 Bilinen Sorunlar

- Offline modda cevaplar çok basit (çünkü gerçek AI değil)
- API hatalarında bazen detaylı mesaj gösterilmiyor
- Çok uzun konuşmalarda token limiti aşılabilir

## 📚 Öğrendiklerim

Bu projeyi yaparken async/await kullanımını, API entegrasyonunu ve servis pattern'ini öğrendim. Ayrıca OpenAI API'nin nasıl çalıştığını anlamak çok eğlenceliydi!

---

**Uyarı:** API anahtarını kimseyle paylaşma! settings.txt dosyası gizli tutulmalı.


