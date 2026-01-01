# 🎤 Speech to Text Converter

Bu uygulama, konuşmayı gerçek zamanlı olarak metne çeviren bir Windows Forms uygulamasıdır.

## Özellikler

- ✨ Gerçek zamanlı konuşma tanıma
- 🎙️ Mikrofon desteği
- 📝 Çok satırlı metin alanı
- 📋 Kopyala butonu
- 🗑️ Temizle butonu
- 🎨 Modern ve kullanıcı dostu arayüz

## Gereksinimler

- Windows 10 veya üzeri
- .NET 8.0 SDK
- Mikrofon erişimi
- Windows Speech Recognition

## Kurulum ve Çalıştırma

### 1. Projeyi Çalıştırın

```bash
cd speechtotext
dotnet restore
dotnet build
dotnet run
```

### 2. Veya Executable Olarak Çalıştırın

```bash
cd speechtotext
dotnet publish -c Release -r win-x64
cd bin/Release/net8.0-windows/win-x64
SpeechToText.exe
```

## Kullanım

1. **Başlat** butonuna tıklayın
2. Konuşmaya başlayın
3. Metninizi gerçek zamanlı olarak göreceksiniz
4. **Durdur** butonuna tıklayarak dinlemeyi durdurun
5. **Kopyala** ile metni kopyalayın
6. **Temizle** ile metni silin

## Geliştirme

```bash
# Bağımlılıkları yükle
dotnet restore

# Projeyi derle
dotnet build

# Projeyi çalıştır
dotnet run
```

## Notlar

- Uygulama, Windows'un yerleşik Speech Recognition API'sini kullanır
- İlk kullanımda Windows Speech Recognition'ı etkinleştirmeniz gerekebilir
- Mikrofon erişim iznine ihtiyaç vardır


