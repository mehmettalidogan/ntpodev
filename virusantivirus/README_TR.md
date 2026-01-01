# 🛡️ Virus & Antivirus Simülasyonu

**⚠️ DİKKAT: Bu proje tamamen EĞİTİM AMAÇLIDIR!**

Merhaba! Bu proje virüs ve antivirüs yazılımlarının nasıl çalıştığını anlamak için oluşturulmuş bir simülasyon projesi.

## 🎯 Ne İçerir?

İki ayrı proje:
1. **TestVirus**: Zararsız "virüs" simülasyonu
2. **Antivirus**: Bu virüsü tespit eden ve temizleyen program

## ⚠️ ÖNEMLİ UYARILAR

**BU BİR EĞİTİM PROJESİDİR!**

- ❌ Gerçek bir virüs DEĞİLDİR
- ❌ Hiçbir zarar vermez
- ❌ Kötü amaçlı kullanılamaz
- ✅ Sadece nasıl çalıştığını gösterir
- ✅ Güvenlik farkındalığı için
- ✅ Eğitim ve öğrenme amaçlı

**ETİK KULLANIM ZORUNLUDUR!**

## 📚 Neler Öğrenilir?

- Virüs nasıl çalışır?
- Antivirüs nasıl tespit eder?
- Dosya hash'leme
- İmza tabanlı tespit
- Davranış analizi
- Karantina sistemi

## 🦠 TestVirus Projesi

### Ne Yapar?

Zararsız şekilde:
- Kendini tanıtır
- Sistem bilgisi gösterir
- Log dosyası oluşturur
- Hiçbir zarar vermez!

### Özellikler:

- **Pasif Çalışma**: Sadece mesaj gösterir
- **Log Tutma**: Ne yaptığını yazar
- **Tespit Edilebilir**: Antivirüs tarafından bulunur
- **Temizlenebilir**: Antivirüs silebilir

### Kod Yapısı:

```csharp
// Basit bir konsol uygulaması
Console.WriteLine("Bu bir test virüsüdür");
// Sistem bilgisi topla
// Log yaz
// Kendi kendini kopyalama (SİMÜLE)
```

## 🛡️ Antivirus Projesi

### Ne Yapar?

- Dosyaları tarar
- Virüs imzalarını arar
- Tespit ederse uyarır
- Karantinaya alır veya siler

### Özellikler:

**Tarama Modları:**
- 🔍 Hızlı Tarama
- 🔎 Tam Tarama
- 📁 Özel Klasör Tarama

**Tespit Yöntemleri:**
- İmza tabanlı (signature-based)
- Hash kontrolü
- Dosya boyutu
- Oluşturulma tarihi

**İşlemler:**
- Karantina
- Silme
- Raporlama
- Güncel tutma

## 🚀 Nasıl Kullanılır?

### TestVirus'ü Çalıştırmak:

1. `TestVirus` projesini aç
2. Derle (Release mode)
3. Exe'yi çalıştır
4. Mesajları oku
5. Kapat

**Uyarı:** Windows Defender gerçek virüs sanabilir! Güvenli olduğunu biliyorsun.

### Antivirus'ü Çalıştırmak:

1. `Antivirus` projesini aç
2. Derle ve çalıştır
3. Tarama türünü seç
4. Tara butonuna tıkla
5. TestVirus exe'si bulunursa uyarı verir
6. Karantina veya sil

## 🛠️ Teknik Mimari

### Virus Detection Methods:

**1. Signature-Based (İmza Tabanlı):**
```csharp
// Bilinen virüs imzaları
string[] virusSignatures = {
    "TestVirus",
    "MaliciousCode",
    "EvilProgram"
};
```

**2. Hash-Based (Hash Tabanlı):**
```csharp
// Dosya hash'i hesapla
string fileHash = ComputeSHA256(filePath);
// Kara listeyle karşılaştır
if (blacklistHashes.Contains(fileHash))
    // Virüs bulundu!
```

**3. Behavior-Based (Davranış Tabanlı):**
- Suspicious API calls
- Registry değişiklikleri
- Network aktivitesi
- Dosya işlemleri

### Antivirus Components:

**Scanner:**
- Dosya sistemini tara
- Her dosyayı kontrol et
- Şüpheli dosyaları listele

**Detector:**
- İmza karşılaştır
- Hash kontrol et
- Heuristic analiz

**Cleaner:**
- Karantinaya al
- Sil
- Log tut

**Updater:**
- Virüs tanımlarını güncelle
- Imza database'i indir

## 💡 Nasıl Çalışır?

### Virüs Tespiti Akışı:

```
1. Dosya seç
   ↓
2. Hash hesapla
   ↓
3. İmza ara
   ↓
4. Karşılaştır
   ↓
5. Bulundu mu?
   ├─ EVET → Karantina/Sil
   └─ HAYIR → Güvenli
```

### Karantina Sistemi:

1. Virüslü dosya bulundu
2. Dosyayı özel klasöre taşı
3. İzinleri kaldır
4. Kullanıcıyı bilgilendir
5. Log kaydet

## 🎓 Öğrenme Hedefleri

**Anlayacakların:**

- 🧬 Virüsler nasıl tespit edilir
- 🛡️ Antivirüsler nasıl çalışır
- 🔐 Hash'leme nedir
- 📝 İmza tabanlı tespit
- 🔍 Heuristic analiz
- 🗂️ Karantina mekanizması

**Güvenlik Bilinci:**

- Güvenilmeyen dosyalar açma
- Antivirüs önemini anlama
- Güvenli yazılım geliştirme
- Ethical hacking temelleri

## 🔬 Test Senaryoları

### Senaryo 1: Basit Tespit

1. TestVirus'ü derle
2. Masaüstüne kopyala
3. Antivirus'ü çalıştır
4. Masaüstünü tara
5. TestVirus tespit edilmeli

### Senaryo 2: Karantina

1. Virüs tara ve bul
2. "Karantina" seç
3. Dosya özel klasöre taşınmalı
4. Artık çalıştırılamaz olmalı

### Senaryo 3: Temizleme

1. Karantinadaki dosyayı gör
2. "Sil" seç
3. Kalıcı olarak silinmeli
4. Log'da kayıt olmalı

## 🐛 Bilinen "Özellikler"

- Windows Defender TestVirus'ü gerçek virüs sanabilir (normal)
- SmartScreen uyarı verebilir (beklenen)
- Bazı antivirüsler derhal sileb

ilir (güvenlik)

## 📝 Gerçek Dünyada

**Gerçek Antivirüs Özellikleri:**

- ☁️ Cloud tarama
- 🧠 AI/ML tabanlı tespit
- 🌐 Real-time protection
- 🔄 Otomatik güncelleme
- 🛡️ Firewall entegrasyonu
- 📧 Email tarama
- 🌍 Web protection

**Bu projede YOK:**

- Gelişmiş heuristic
- Sandbox analizi
- Rootkit tespiti
- Kernel-mode driver
- Network monitoring
- Real-time protection

## 🔐 Güvenlik İpuçları

**Kendini Korumak İçin:**

1. ✅ İyi bir antivirüs kullan
2. ✅ Yazılımları güncel tut
3. ✅ Bilinmeyen dosyalar açma
4. ✅ Mail eklerinde dikkatli ol
5. ✅ Düzenli tarama yap
6. ✅ Firewall aç
7. ✅ Güvenli internet kullan

## ⚖️ Yasal Uyarı

**Türkiye Cumhuriyeti Kanunları:**

Zararlı yazılım yapmak veya yaymak:
- TCK 243/3 - Bilişim sisteminin işleyişini engelleme
- TCK 244 - Verileri yok etme
- Ciddi cezai yaptırımlar

**SADECE EĞİTİM İÇİN KULLAN!**

## 🤝 Etik Kullanım Kuralları

**Yapabilirsin:**
- 🟢 Kendi bilgisayarında test et
- 🟢 Sanal makinede dene
- 🟢 Öğren ve paylaş
- 🟢 Güvenlik araştırması yap

**YAPAMAZSIN:**
- 🔴 Başkalarına zarar ver
- 🔴 Yaygınlaştır
- 🔴 Kötü amaçlı kullan
- 🔴 İzinsiz teste tabi tut

## 📚 Daha Fazla Öğren

İlgileniyorsan:
- **Malware Analysis** kursları
- **Reverse Engineering**
- **Cybersecurity** eğitimleri
- **CEH (Certified Ethical Hacker)**
- **OSCP** sertifikası

## 🔮 Geliştirme Fikirleri

Bu projeyi geliştirebilirsin:
- [ ] Gerçek hash database
- [ ] Heuristic engine
- [ ] Sandbox entegrasyonu
- [ ] Cloud scanning
- [ ] Gerçek zamanlı koruma
- [ ] Yedekleme sistemi
- [ ] Rapor oluşturma

## 💻 Kod Örnekleri

### Hash Hesaplama:

```csharp
using (var sha256 = SHA256.Create())
{
    byte[] bytes = File.ReadAllBytes(path);
    byte[] hash = sha256.ComputeHash(bytes);
    return BitConverter.ToString(hash).Replace("-", "");
}
```

### İmza Tarama:

```csharp
string content = File.ReadAllText(path);
foreach (var signature in virusSignatures)
{
    if (content.Contains(signature))
        return true; // Virüs bulundu
}
```

## 🎯 Sonuç

Bu proje ile:
- Virüslerin nasıl çalıştığını anladın
- Antivirüslerin mantığını öğrendin
- Güvenlik bilincin arttı
- Etik hacking'e giriş yaptın

**BİLGİYİ SORUMLU KULLAN!**

---

**Tekrar Hatırlatma:**

Bu proje tamamen eğitim amaçlıdır. Gerçek virüs geliştirmek veya yaymak suçtur ve ciddi sonuçları vardır.

**ETİK OL, GÜVEN KAZAN!**

**Siber güvenlik uzmanları dünyayı daha güvenli yapar, zararlı hale getirmez!**

---

Detaylı kurulum ve kullanım için `KULLANIM_KILAVUZU.md` dosyasına bakabilirsin!


