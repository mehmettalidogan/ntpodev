using OpenCvSharp;
using OpenCvSharp.Dnn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CvSize = OpenCvSharp.Size;
using CvPoint = OpenCvSharp.Point;

namespace ObjectDetection
{
    /// <summary>
    /// YOLO modeli ile gerçek zamanlı nesne tanıma
    /// </summary>
    public class YOLODetector
    {
        private Net? net;
        private string[] siniflar = Array.Empty<string>();
        private string[] cikisKatmanAdlari = Array.Empty<string>();
        private bool yuklu = false;

        // COCO veri seti sınıfları (Türkçe)
        private readonly Dictionary<string, string> sinifCevirileri = new Dictionary<string, string>
        {
            {"person", "İnsan"},
            {"bicycle", "Bisiklet"},
            {"car", "Araba"},
            {"motorcycle", "Motosiklet"},
            {"airplane", "Uçak"},
            {"bus", "Otobüs"},
            {"train", "Tren"},
            {"truck", "Kamyon"},
            {"boat", "Tekne"},
            {"traffic light", "Trafik Lambası"},
            {"fire hydrant", "Yangın Musluğu"},
            {"stop sign", "Dur İşareti"},
            {"parking meter", "Park Saati"},
            {"bench", "Bank"},
            {"bird", "Kuş"},
            {"cat", "Kedi"},
            {"dog", "Köpek"},
            {"horse", "At"},
            {"sheep", "Koyun"},
            {"cow", "İnek"},
            {"elephant", "Fil"},
            {"bear", "Ayı"},
            {"zebra", "Zebra"},
            {"giraffe", "Zürafa"},
            {"backpack", "Sırt Çantası"},
            {"umbrella", "Şemsiye"},
            {"handbag", "El Çantası"},
            {"tie", "Kravat"},
            {"suitcase", "Bavul"},
            {"frisbee", "Frizbi"},
            {"skis", "Kayak"},
            {"snowboard", "Snowboard"},
            {"sports ball", "Top"},
            {"kite", "Uçurtma"},
            {"baseball bat", "Beyzbol Sopası"},
            {"baseball glove", "Beyzbol Eldiveni"},
            {"skateboard", "Kaykay"},
            {"surfboard", "Sörf Tahtası"},
            {"tennis racket", "Tenis Raketi"},
            {"bottle", "Şişe"},
            {"wine glass", "Şarap Kadehi"},
            {"cup", "Fincan"},
            {"fork", "Çatal"},
            {"knife", "Bıçak"},
            {"spoon", "Kaşık"},
            {"bowl", "Kase"},
            {"banana", "Muz"},
            {"apple", "Elma"},
            {"sandwich", "Sandviç"},
            {"orange", "Portakal"},
            {"broccoli", "Brokoli"},
            {"carrot", "Havuç"},
            {"hot dog", "Hot Dog"},
            {"pizza", "Pizza"},
            {"donut", "Donut"},
            {"cake", "Pasta"},
            {"chair", "Sandalye"},
            {"couch", "Kanepe"},
            {"potted plant", "Saksı Bitkisi"},
            {"bed", "Yatak"},
            {"dining table", "Yemek Masası"},
            {"toilet", "Tuvalet"},
            {"tv", "Televizyon"},
            {"laptop", "Laptop"},
            {"mouse", "Fare"},
            {"remote", "Kumanda"},
            {"keyboard", "Klavye"},
            {"cell phone", "Telefon"},
            {"microwave", "Mikrodalga"},
            {"oven", "Fırın"},
            {"toaster", "Ekmek Kızartma Makinesi"},
            {"sink", "Lavabo"},
            {"refrigerator", "Buzdolabı"},
            {"book", "Kitap"},
            {"clock", "Saat"},
            {"vase", "Vazo"},
            {"scissors", "Makas"},
            {"teddy bear", "Ayıcık"},
            {"hair drier", "Saç Kurutma Makinesi"},
            {"toothbrush", "Diş Fırçası"}
        };

        /// <summary>
        /// YOLO modelini yükler
        /// </summary>
        public bool ModelYukle(string weightsPath = "yolov3-tiny.weights", 
                               string configPath = "yolov3-tiny.cfg",
                               string namesPath = "coco.names")
        {
            try
            {
                // Model dosyalarını kontrol et
                if (!File.Exists(weightsPath) || !File.Exists(configPath))
                {
                    Console.WriteLine($"YOLO model dosyaları bulunamadı!");
                    Console.WriteLine($"Weights: {weightsPath} - Var mı? {File.Exists(weightsPath)}");
                    Console.WriteLine($"Config: {configPath} - Var mı? {File.Exists(configPath)}");
                    return false;
                }

                // Sınıf isimlerini yükle
                if (File.Exists(namesPath))
                {
                    siniflar = File.ReadAllLines(namesPath);
                }
                else
                {
                    // Varsayılan COCO sınıfları
                    siniflar = sinifCevirileri.Keys.ToArray();
                }

                // YOLO ağını yükle
                net = CvDnn.ReadNetFromDarknet(configPath, weightsPath);
                net.SetPreferableBackend(Backend.OPENCV);
                net.SetPreferableTarget(Target.CPU);

                // Çıkış katmanlarını al
                cikisKatmanAdlari = net.GetUnconnectedOutLayersNames();

                yuklu = true;
                Console.WriteLine($"YOLO modeli başarıyla yüklendi! {siniflar.Length} sınıf tanımlanıyor.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"YOLO model yükleme hatası: {ex.Message}");
                return false;
            }
        }

        public bool ModelYukluMu() => yuklu;

        /// <summary>
        /// Görüntüden nesneleri tespit eder
        /// </summary>
        public List<DetectedObject> NesneleriTespitEt(Mat goruntu, float guvenEsigi = 0.5f, float nmsEsigi = 0.4f)
        {
            if (!yuklu || net == null || goruntu.Empty())
                return new List<DetectedObject>();

            var sonuclar = new List<DetectedObject>();

            try
            {
                int yukseklik = goruntu.Height;
                int genislik = goruntu.Width;

                // Blob oluştur (YOLO input: 416x416)
                var blob = CvDnn.BlobFromImage(
                    goruntu,
                    scaleFactor: 1.0 / 255.0,
                    size: new CvSize(416, 416),
                    swapRB: true,
                    crop: false
                );

                net.SetInput(blob);

                // İleri besleme
                var cikisMatlar = cikisKatmanAdlari.Select(_ => new Mat()).ToArray();
                net.Forward(cikisMatlar, cikisKatmanAdlari);

                // Sonuçları işle
                var kutular = new List<Rect>();
                var guvenler = new List<float>();
                var sinifIdleri = new List<int>();

                foreach (var cikti in cikisMatlar)
                {
                    for (int i = 0; i < cikti.Rows; i++)
                    {
                        // Her tespit: [x, y, w, h, objectness, class1_prob, class2_prob, ...]
                        float objectness = cikti.At<float>(i, 4);

                        if (objectness > guvenEsigi)
                        {
                            // En yüksek olasılıklı sınıfı bul
                            float maxSkor = 0;
                            int maxSinifId = 0;

                            for (int j = 5; j < cikti.Cols; j++)
                            {
                                float skor = cikti.At<float>(i, j);
                                if (skor > maxSkor)
                                {
                                    maxSkor = skor;
                                    maxSinifId = j - 5;
                                }
                            }

                            float guven = objectness * maxSkor;

                            if (guven > guvenEsigi)
                            {
                                // Koordinatları hesapla
                                float merkezX = cikti.At<float>(i, 0) * genislik;
                                float merkezY = cikti.At<float>(i, 1) * yukseklik;
                                float w = cikti.At<float>(i, 2) * genislik;
                                float h = cikti.At<float>(i, 3) * yukseklik;

                                int x = (int)(merkezX - w / 2);
                                int y = (int)(merkezY - h / 2);

                                kutular.Add(new Rect(x, y, (int)w, (int)h));
                                guvenler.Add(guven);
                                sinifIdleri.Add(maxSinifId);
                            }
                        }
                    }
                }

                // Non-maximum suppression (çakışan kutuları temizle)
                CvDnn.NMSBoxes(kutular, guvenler, guvenEsigi, nmsEsigi, out int[] indeksler);

                // Sonuçları hazırla
                foreach (int idx in indeksler)
                {
                    string sinifAdi = sinifIdleri[idx] < siniflar.Length ? 
                        siniflar[sinifIdleri[idx]] : "unknown";
                    
                    string turkceAd = sinifCevirileri.ContainsKey(sinifAdi) ? 
                        sinifCevirileri[sinifAdi] : sinifAdi;

                    sonuclar.Add(new DetectedObject
                    {
                        Sinif = turkceAd,
                        SinifId = sinifIdleri[idx],
                        Guven = guvenler[idx],
                        Kutu = kutular[idx]
                    });
                }

                // Dispose blobs
                blob.Dispose();
                foreach (var mat in cikisMatlar)
                    mat.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nesne tespit hatası: {ex.Message}");
            }

            return sonuclar;
        }

        /// <summary>
        /// Tespit edilen nesneleri görüntüye çizer
        /// </summary>
        public void NesneleriCiz(Mat goruntu, List<DetectedObject> nesneler)
        {
            var renkler = new Dictionary<int, Scalar>();
            var rnd = new Random(42); // Sabit seed - her çalıştırmada aynı renkler

            foreach (var nesne in nesneler)
            {
                // Her sınıf için sabit bir renk
                if (!renkler.ContainsKey(nesne.SinifId))
                {
                    renkler[nesne.SinifId] = new Scalar(
                        rnd.Next(50, 256),
                        rnd.Next(50, 256),
                        rnd.Next(50, 256)
                    );
                }

                var renk = renkler[nesne.SinifId];

                // Kutuyu çiz
                Cv2.Rectangle(goruntu, nesne.Kutu, renk, 2);

                // Etiket oluştur
                string etiket = $"{nesne.Sinif} {nesne.Guven:P0}";
                
                // Etiket arka planı
                var etiketBoyutu = Cv2.GetTextSize(etiket, HersheyFonts.HersheySimplex, 0.6, 1, out int baseline);
                
                int etiketY = nesne.Kutu.Y - 10;
                if (etiketY < 0) etiketY = nesne.Kutu.Y + nesne.Kutu.Height + 20;

                Cv2.Rectangle(
                    goruntu,
                    new CvPoint(nesne.Kutu.X, etiketY - etiketBoyutu.Height - 5),
                    new CvPoint(nesne.Kutu.X + etiketBoyutu.Width + 5, etiketY + baseline),
                    renk,
                    -1 // Dolu dikdörtgen
                );

                // Etiket metni
                Cv2.PutText(
                    goruntu,
                    etiket,
                    new CvPoint(nesne.Kutu.X + 2, etiketY - 2),
                    HersheyFonts.HersheySimplex,
                    0.6,
                    Scalar.White,
                    1,
                    LineTypes.AntiAlias
                );
            }
        }

        /// <summary>
        /// Tespit edilen nesnelerin özetini verir
        /// </summary>
        public string NesneOzetiOlustur(List<DetectedObject> nesneler)
        {
            if (nesneler.Count == 0)
                return "Nesne bulunamadı";

            var gruplar = nesneler
                .GroupBy(n => n.Sinif)
                .OrderByDescending(g => g.Count())
                .Take(5); // En çok bulunan 5 nesne

            var ozet = string.Join(", ", gruplar.Select(g => 
                g.Count() > 1 ? $"{g.Count()} {g.Key}" : g.Key
            ));

            return ozet;
        }
    }

    /// <summary>
    /// Tespit edilen nesne bilgisi
    /// </summary>
    public class DetectedObject
    {
        public string Sinif { get; set; } = "";
        public int SinifId { get; set; }
        public float Guven { get; set; }
        public Rect Kutu { get; set; }
    }
}

