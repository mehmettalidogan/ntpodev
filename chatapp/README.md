# 💬 Modern Chat Uygulaması

Merhaba! Bu proje gerçek zamanlı bir sohbet uygulaması. Hem Node.js ile yazılmış bir sunucu, hem de C# Windows Forms ile yapılmış modern bir masaüstü istemcisi var.

## 🎯 Ne İşe Yarar?

Bu uygulama ile birden fazla kişi aynı anda sohbet edebiliyor. Düşün ki Whatsapp gibi ama çok daha basit ve minimal. Sunucuyu çalıştırıp, birkaç istemci açarak arkadaşlarınla konuşabilirsin.

## ✨ Özellikler

- **Gerçek Zamanlı Mesajlaşma**: Socket.IO sayesinde mesajlar anında ulaşıyor
- **Modern Arayüz**: C# ile yazılmış, renkli ve kullanıcı dostu bir arayüz
- **Kullanıcı Yönetimi**: Her kullanıcıya otomatik rastgele isim verilir (veya kendin belirlersin)
- **Sistem Bildirimleri**: Kim katıldı, kim ayrıldı gibi bildirimler geliyor
- **Zaman Damgası**: Her mesaj hangi saatte gönderildi görebiliyorsun
- **Emoji Desteği**: Mesajlarda emoji kullanabilirsin 😊

## 🚀 Nasıl Çalıştırılır?

### Sunucuyu Başlatmak:

1. Node.js'in kurulu olduğundan emin ol
2. Terminal/CMD aç ve şu klasöre git: `chatapp`
3. İlk defa çalıştırıyorsan, şunu yaz:
   ```bash
   npm install
   ```
4. Sunucuyu başlat:
   ```bash
   node server.js
   ```
5. "Sunucu http://localhost:3000 adresinde çalışıyor" mesajını görmelisin

### İstemciyi Başlatmak:

1. Visual Studio veya Rider ile `ChatApp.csproj` dosyasını aç
2. Projeyi derle ve çalıştır (F5)
3. Kullanıcı adını gir
4. "Bağlan" butonuna tıkla
5. Mesajlaşmaya başla!

## 🛠️ Teknik Detaylar

**Sunucu Tarafı:**
- Node.js + Express
- Socket.IO (WebSocket desteği ile)
- Port: 3000

**İstemci Tarafı:**
- C# .NET 8.0 (Windows Forms)
- SocketIOClient kütüphanesi
- Modern flat design

## 💡 İpuçları

- Birden fazla istemci açarak kendinle sohbet edebilirsin (test için süper!)
- Sunucu konsolda tüm aktiviteleri gösteriyor, hata ayıklama için faydalı
- Enter tuşu ile de mesaj gönderebilirsin, butona tıklamana gerek yok

## 🎨 Tasarım Felsefesi

Arayüzü modern ve minimal tutmaya çalıştım. Renk paleti olarak mavi tonlar kullandım, butonlara emoji ekleyerek daha eğlenceli hale getirdim. Her mesajın yanında kim yazdığı net bir şekilde görünüyor.

## 📝 Notlar

Bu projeyi geliştirirken en çok eğlendiğim kısım Socket.IO ile mesajların anında iletilmesini sağlamaktı. İlk başta biraz kafa karıştırıcı olsa da, öğrendikten sonra gerçekten çok güçlü bir teknoloji.

---

**Not:** Bu bir öğrenme projesi. Ticari kullanım için güvenlik önlemleri eklenmeli!
