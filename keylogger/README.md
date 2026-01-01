# ⌨️ Keylogger - Klavye Dinleme Uygulaması

**⚠️ UYARI: Bu proje SADECE EĞİTİM AMAÇLIDIR!**

## 🎯 Nedir Bu?

Bir keylogger - yani klavyeden basılan tuşları kaydeden bir program. Güvenlik araştırması ve eğitim amacıyla yapılmıştır.

## ⚠️ ÖNEMLİ UYARILAR

**BU PROGRAMI SADECE KENDİ BİLGİSAYARINDA VE EĞİTİM AMAÇLI KULLAN!**

- ❌ Başkasının bilgisayarına yüklemen yasadışıdır
- ❌ İzinsiz kullanım suçtur
- ❌ Kişisel verileri çalmak yasaktır
- ✅ Sadece kendi test ortamında kullan
- ✅ Güvenlik araştırması için eğitimsel
- ✅ Nasıl çalıştığını anlamak için

**SORUMLU KULLAN - ETİK OL!**

## 🔍 Ne Yapar?

- Global keyboard hook kullanarak tuşları dinler
- Basılan tuşları kaydeder
- Türkçe karakter desteği (ğ, ü, ş, ı, ö, ç)
- CapsLock durumunu takip eder
- Belirli sayıda tuştan sonra log'u e-posta ile gönderir

## ⚙️ Nasıl Çalışır?

### Teknik:

1. **HootKeys** kütüphanesi ile global keyboard hook
2. Her tuşa event listener ekler
3. Tuşları bir string'de biriktir
4. Sayaç dolunca SMTP ile mail gönder
5. Log'u temizle ve devam et

### Dinlenen Tuşlar:

- Tüm harfler (A-Z)
- Tüm rakamlar (0-9, numpad dahil)
- Türkçe karakterler (Ğ, Ü, Ş, İ, Ö, Ç)
- Space, Enter, Backspace
- Noktalama işaretleri

## 🛠️ Kurulum ve Çalıştırma

**DİKKAT:** Kendi sorumluluğunda!

1. Visual Studio ile aç
2. `Form1.cs` içindeki mail ayarlarını **KENDİ MAİLİNLE** değiştir:
   ```csharp
   client.Credentials = new NetworkCredential("SENIN_MAILIN", "SENIN_APP_PASSWORD");
   msg.From = new MailAddress("SENIN_MAILIN");
   msg.To.Add("ALICI_MAIL");
   ```
3. Derle ve çalıştır
4. Test için bir yere birkaç şey yaz
5. Mail kutunu kontrol et

## 📧 Gmail Ayarları

Gmail kullanıyorsan "App Password" oluştur:

1. Google Account → Security
2. 2-Step Verification'ı aç
3. App passwords → Create new
4. "Mail" seç, şifre al
5. Bu şifreyi kodda kullan (normal şifren değil!)

## 🔐 Güvenlik

**Bu program antivirüsler tarafından zararlı yazılım olarak algılanacaktır!**

Çünkü:
- Global keyboard hook kullanır
- Tuşları kaydeder
- Ağ üzerinden veri gönderir

**Bu normaldir!** Keylogger'lar bu yüzden tehlike olarak görülür.

## 📝 Kod Yapısı

### Ana Fonksiyonlar:

**ListenKeys()**
- Dinlenecek tuşları kaydet
- Hook'ları ekle

**KeyboardCombination()**
- Tuş basıldığında çağrılır
- Log'a ekle
- Sayaç kontrolü

**Mail()**
- SMTP ile Gmail'e gönder
- TLS 1.2 kullanır
- Debug log tutar

## 💡 Koddan Öğrenilebilecekler

- Global keyboard hooking nasıl yapılır
- Event-driven programming
- SMTP mail gönderme
- String manipülasyonu
- Thread safety
- Exception handling

## 🐛 Bilinen Sorunlar

- **Windows Defender** hemen siler (whitelist'e eklemen gerek)
- **Antivirüs** çalışmasını engelleyebilir
- **UAC** yüksek izinli uygulamalarda çalışmayabilir
- **Modern Apps** (UWP) tuşlarını yakalayamaz

## 🔬 Test Senaryoları

**Güvenli Test Ortamı:**
1. Kendi bilgisayarın
2. Virtual machine
3. Sandbox ortam
4. İzole network

**Test:**
- Notepad'e yaz, mail geldi mi?
- CapsLock aç/kapa, büyük/küçük harf farkı var mı?
- Türkçe karakter yaz, doğru kaydediyor mu?

## 🎓 Eğitim Amaçları

Bu proje ile öğrenebileceğin şeyler:

1. **Güvenlik Farkındalığı**: Keylogger nedir, nasıl çalışır
2. **Windows API**: Low-level keyboard hook
3. **C# Events**: Event handling
4. **Network**: SMTP protokolü
5. **Defensive Programming**: Nasıl korunursun

## 🛡️ Nasıl Korunursun?

Keylogger'lara karşı kendini korumak için:

- ✅ İyi bir antivirüs kullan
- ✅ Bilinmeyen programları çalıştırma
- ✅ Virtual keyboard kullan (hassas işlemler için)
- ✅ 2FA (two-factor authentication) kullan
- ✅ Düzenli olarak sistem tara

## ⚖️ Yasal Uyarı

**Türkiye Cumhuriyeti Kanunları:**

Bu tür yazılımları izinsiz kullanmak:
- **TCK Madde 134** - Kişisel verilerin kaydedilmesi (1-3 yıl)
- **TCK Madde 135** - Verileri hukuka aykırı olarak verme (2-4 yıl)
- **TCK Madde 136** - Bilişim sistemine girme (1-2 yıl)

**Bu ciddi suçlardır! Sadece eğitim için kullan!**

## 🔍 Dedektif Olmak

Kendi bilgisayarında keylogger var mı kontrol et:

1. Task Manager → Startup
2. Bilinmeyen programlar
3. Ağ trafiği izle
4. Antivirüs tara
5. Process Explorer kullan

## 📚 Daha Fazla Öğren

İlgileniyorsan şunlara bak:
- **Ethical Hacking** kursları
- **Malware Analysis** 
- **Reverse Engineering**
- **Windows Internals**
- **Security+** sertifikası

## 🤝 Etik Kullanım

Bu bilgiyi kullanırken:
- 🟢 Kendi sistemini test et
- 🟢 Güvenlik açıklarını raporla
- 🟢 Eğitim materyali oluştur
- 🔴 Başkalarını hack'leme
- 🔴 Kişisel veri çalma
- 🔴 İzinsiz sisteme gir

---

**TEKRAR HATIRLATMA:** 

Bu proje tamamen eğitim amaçlıdır. Kötü niyetli kullanım yasadışıdır ve ciddi sonuçları vardır. Etik ol, sorumlu davran!

**BİLGİ GÜÇ DEĞİL, SORUMLULUKT UR!**

---

**Not:** Mail ayarlarındaki kimlik bilgileri GitHub'a push edilmeden önce temizlenmelidir!


