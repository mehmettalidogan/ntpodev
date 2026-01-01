# 🔌 Chat Socket App - TCP Tabanlı Mesajlaşma

Hey! Bu proje klasik TCP socket'ler kullanarak çalışan bir chat uygulaması. Yani ham socket programlama - internetin temellerinden biri!

## 🎯 Ne Yapar?

Bir sunucu ve birden fazla istemci arasında mesajlaşma sağlar. Düşün ki kendi WhatsApp sunucunu kurdun, ama çok çok daha basit versiyonu.

## ✨ Özellikler

- **Server-Client Mimarisi**: Klasik TCP socket yapısı
- **Çoklu İstemci Desteği**: Aynı anda birçok kişi bağlanabilir
- **Client Manager**: İstemcileri merkezi olarak yönet
- **Message Handler**: Mesajları işle ve yönlendir
- **Socket Helper**: Socket işlemlerini kolaylaştıran yardımcı sınıf
- **Temiz Kod Yapısı**: Service pattern kullanımı

## 🚀 Nasıl Çalıştırılır?

### Ana Ekran:
Programı çalıştırdığında 3 buton görürsün:
- **Server**: Sunucu modunda aç
- **Client**: İstemci modunda aç
- **Çıkış**: Programı kapat

### Sunucu Olarak Çalıştırmak:

1. "Server" butonuna tıkla
2. Port numarası belirle (örn: 8080)
3. "Başlat" de
4. Sunucu artık bağlantıları bekliyor!
5. Hangi istemcilerin bağlı olduğunu görebilirsin

### İstemci Olarak Çalıştırmak:

1. "Client" butonuna tıkla
2. Kullanıcı adını gir
3. Sunucu IP adresi (localhost veya 127.0.0.1)
4. Port numarası (sunucudakiyle aynı olmalı)
5. "Bağlan" de
6. Mesaj yazmaya başla!

## 🛠️ Teknik Mimari

### Models (Veri Yapıları):
- **ChatMessage**: Mesaj yapısı (kim, ne zaman, ne yazdı)
- **ClientInfo**: İstemci bilgileri (ID, isim, IP, socket)
- **ServerConfig**: Sunucu ayarları

### Services (İş Mantığı):
- **ClientManager**: İstemci listesini yönetir
- **MessageHandler**: Mesajları işler ve yönlendirir
- **SocketHelper**: Socket bağlantı ve okuma/yazma işlemleri

### Forms (Arayüz):
- **MainForm**: Ana menü
- **ServerForm**: Sunucu arayüzü
- **ClientForm**: İstemci arayüzü

## 💡 Nasıl Çalışır?

1. **Sunucu Açılır**: Belirtilen portta dinlemeye başlar
2. **İstemci Bağlanır**: Sunucuya TCP bağlantısı kurar
3. **Mesaj Gönderilir**: İstemci mesajı sunucuya yollar
4. **Sunucu Dağıtır**: Tüm bağlı istemcilere mesajı iletir
5. **İstemci Alır**: Diğer kullanıcıların mesajlarını görür

## 🎮 Test Senaryosu

Tek başına test etmek için:
1. Programdan 1 sunucu aç
2. Programdan 2-3 istemci aç (farklı pencereler)
3. Her istemciden mesaj gönder
4. Hepsinde mesajların göründüğünü gör!

## 🔥 Avantajlar

- **Hafif ve Hızlı**: Minimum bağımlılık, maksimum performans
- **Öğrenme Amaçlı**: Socket programlamayı öğrenmek için mükemmel
- **Genişletilebilir**: Dosya transferi, görüntü paylaşımı eklenebilir
- **Güvenilir**: TCP kullandığı için mesajlar kaybolmaz

## ⚠️ Dikkat Edilmesi Gerekenler

- **Port Çakışması**: Kullandığın portun başka bir program tarafından kullanılmadığından emin ol
- **Firewall**: Windows Firewall portu engelleyebilir, izin vermelisin
- **Yerel Ağ**: Aynı ağdaki başka bilgisayarlardan da bağlanılabilir (IP adresini değiştirerek)

## 🌐 Uzak Bağlantı

Farklı ağlardan bağlanmak için:
1. Sunucu bilgisayarın dış IP'sini öğren
2. Modem/router'da port forwarding yap
3. İstemciler dış IP ile bağlansın

**Uyarı:** Güvenlik riskleri var, sadece test için kullan!

## 📝 Kod Kalitesi

Bu projede özellikle temiz kod yazmaya dikkat ettim:
- Interface kullanımı (IClientManager, IMessageHandler)
- Separation of concerns (her sınıf tek bir şey yapar)
- Null check'ler ve hata yakalama
- Thread-safe operasyonlar

## 🐛 Bilinen Sorunlar

- Bağlantı aniden kesilirse bazen hata mesajı görülür
- Çok fazla mesaj gönderilirse buffer dolabilir
- Binary veri (dosya) desteği yok

## 💭 Gelecek İyileştirmeler

- Şifrelenmiş mesajlaşma (SSL/TLS)
- Dosya transfer özelliği
- Kullanıcı odaları/grupları
- Mesaj geçmişi kaydetme
- Emoji ve resim desteği

---

**Not:** Bu klasik TCP socket örneği. Üretim ortamı için SignalR veya gRPC gibi modern çözümler tercih edilebilir!


