# Socket.IO Chat Uygulaması

Bu proje, C# Windows Forms kullanarak Socket.IO ile gerçek zamanlı chat uygulamasıdır.

## Özellikler

- ✅ Gerçek zamanlı mesajlaşma
- ✅ Kullanıcı adı ile giriş
- ✅ Çevrimiçi kullanıcı listesi
- ✅ Bağlantı durumu gösterimi
- ✅ Modern Windows Forms arayüzü
- ✅ Hata yönetimi

## Kurulum ve Çalıştırma

### 1. Node.js Sunucusu

```bash
# Bağımlılıkları yükle
npm install

# Sunucuyu başlat
npm start
```

Sunucu `http://localhost:3000` adresinde çalışacaktır.

### 2. C# Windows Forms Uygulaması

```bash
# .NET bağımlılıklarını yükle
dotnet restore

# Uygulamayı derle ve çalıştır
dotnet run
```

## Kullanım

1. **Sunucuyu başlatın**: `npm start` komutu ile Node.js sunucusunu çalıştırın
2. **C# uygulamasını açın**: `dotnet run` ile Windows Forms uygulamasını başlatın
3. **Kullanıcı adı girin**: Varsayılan bir kullanıcı adı otomatik oluşturulur, değiştirebilirsiniz
4. **Bağlan**: "Bağlan" butonuna tıklayın
5. **Mesajlaşın**: Mesaj kutusuna yazın ve Enter'a basın veya "Gönder" butonuna tıklayın

## Teknik Detaylar

### C# Tarafı
- **Framework**: .NET 6.0 Windows Forms
- **Socket.IO Client**: SocketIOClient NuGet paketi
- **JSON**: Newtonsoft.Json

### Node.js Tarafı
- **Framework**: Express.js
- **WebSocket**: Socket.IO
- **Port**: 3000

## Dosya Yapısı

```
chatapp/
├── ChatApp.csproj          # C# proje dosyası
├── MainForm.cs             # Ana form ve chat mantığı
├── Program.cs              # Uygulama giriş noktası
├── server.js               # Node.js Socket.IO sunucusu
├── package.json            # Node.js bağımlılıkları
└── README.md               # Bu dosya
```

## Geliştirme

### Yeni Özellikler Eklemek İçin:

1. **Sunucu tarafında** (`server.js`):
   - Yeni socket event'leri ekleyin
   - İş mantığını implement edin

2. **İstemci tarafında** (`MainForm.cs`):
   - Yeni UI kontrolleri ekleyin
   - Socket event handler'ları yazın
   - Form tasarımını güncelleyin

## Sorun Giderme

- **Bağlantı sorunu**: Sunucunun çalıştığından emin olun (`npm start`)
- **Port çakışması**: `server.js` dosyasında PORT değişkenini değiştirin
- **Derleme hatası**: `dotnet restore` komutunu çalıştırın

## Lisans

MIT License
