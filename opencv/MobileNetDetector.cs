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
    /// MobileNet-SSD modeli ile nesne tanıma (YOLO'ya alternatif)
    /// Daha küçük model (~20 MB), aynı nesneleri tanır
    /// </summary>
    public class MobileNetDetector
    {
        private Net? net;
        private string[] siniflar = Array.Empty<string>();
        private bool yuklu = false;

        // COCO sınıfları (Türkçe çeviri)
        private readonly Dictionary<string, string> sinifCevirileri = new Dictionary<string, string>
        {
            {"background", "Arka Plan"},
            {"aeroplane", "Uçak"},
            {"bicycle", "Bisiklet"},
            {"bird", "Kuş"},
            {"boat", "Tekne"},
            {"bottle", "Şişe"},
            {"bus", "Otobüs"},
            {"car", "Araba"},
            {"cat", "Kedi"},
            {"chair", "Sandalye"},
            {"cow", "İnek"},
            {"diningtable", "Yemek Masası"},
            {"dog", "Köpek"},
            {"horse", "At"},
            {"motorbike", "Motosiklet"},
            {"person", "İnsan"},
            {"pottedplant", "Saksı Bitkisi"},
            {"sheep", "Koyun"},
            {"sofa", "Kanepe"},
            {"train", "Tren"},
            {"tvmonitor", "Televizyon"}
        };

        /// <summary>
        /// MobileNet-SSD modelini yükler
        /// </summary>
        public bool ModelYukle(string protoPath = "models/MobileNetSSD_deploy.prototxt",
                               string modelPath = "models/MobileNetSSD_deploy.caffemodel")
        {
            try
            {
                // Model dosyalarını kontrol et
                if (!File.Exists(protoPath) || !File.Exists(modelPath))
                {
                    Console.WriteLine($"MobileNet model dosyaları bulunamadı!");
                    Console.WriteLine($"Proto: {protoPath} - {File.Exists(protoPath)}");
                    Console.WriteLine($"Model: {modelPath} - {File.Exists(modelPath)}");
                    return false;
                }

                // Pascal VOC sınıfları
                siniflar = new string[]
                {
                    "background", "aeroplane", "bicycle", "bird", "boat",
                    "bottle", "bus", "car", "cat", "chair", "cow",
                    "diningtable", "dog", "horse", "motorbike", "person",
                    "pottedplant", "sheep", "sofa", "train", "tvmonitor"
                };

                // Caffe modelini yükle
                net = CvDnn.ReadNetFromCaffe(protoPath, modelPath);
                net.SetPreferableBackend(Backend.OPENCV);
                net.SetPreferableTarget(Target.CPU);

                yuklu = true;
                Console.WriteLine($"MobileNet-SSD modeli yüklendi! {siniflar.Length} sınıf tanımlanıyor.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MobileNet model yükleme hatası: {ex.Message}");
                return false;
            }
        }

        public bool ModelYukluMu() => yuklu;

        /// <summary>
        /// Görüntüden nesneleri tespit eder
        /// </summary>
        public List<DetectedObject> NesneleriTespitEt(Mat goruntu, float guvenEsigi = 0.5f)
        {
            if (!yuklu || net == null || goruntu.Empty())
                return new List<DetectedObject>();

            var sonuclar = new List<DetectedObject>();

            try
            {
                int yukseklik = goruntu.Height;
                int genislik = goruntu.Width;

                // Blob oluştur (MobileNet input: 300x300)
                var blob = CvDnn.BlobFromImage(
                    goruntu,
                    scaleFactor: 0.007843,  // 1/127.5
                    size: new CvSize(300, 300),
                    mean: new Scalar(127.5, 127.5, 127.5),
                    swapRB: false,
                    crop: false
                );

                net.SetInput(blob);
                var detection = net.Forward();

                // Sonuçları işle
                // Output shape: [1, 1, N, 7]
                // Her tespit: [batchId, classId, confidence, left, top, right, bottom]
                
                var detectionMat = detection.Reshape(1, detection.Size(2));
                
                for (int i = 0; i < detectionMat.Rows; i++)
                {
                    float confidence = detectionMat.At<float>(i, 2);

                    if (confidence > guvenEsigi)
                    {
                        int classId = (int)detectionMat.At<float>(i, 1);
                        
                        // Koordinatları al (0-1 arası normalize edilmiş)
                        int left = (int)(detectionMat.At<float>(i, 3) * genislik);
                        int top = (int)(detectionMat.At<float>(i, 4) * yukseklik);
                        int right = (int)(detectionMat.At<float>(i, 5) * genislik);
                        int bottom = (int)(detectionMat.At<float>(i, 6) * yukseklik);

                        int width = right - left;
                        int height = bottom - top;

                        // Sınırları kontrol et
                        if (left < 0) left = 0;
                        if (top < 0) top = 0;
                        if (width <= 0 || height <= 0) continue;
                        if (left + width > genislik) width = genislik - left;
                        if (top + height > yukseklik) height = yukseklik - top;

                        string sinifAdi = classId < siniflar.Length ? siniflar[classId] : "unknown";
                        string turkceAd = sinifCevirileri.ContainsKey(sinifAdi) ? 
                            sinifCevirileri[sinifAdi] : sinifAdi;

                        // Background sınıfını atlama
                        if (classId == 0) continue;

                        sonuclar.Add(new DetectedObject
                        {
                            Sinif = turkceAd,
                            SinifId = classId,
                            Guven = confidence,
                            Kutu = new Rect(left, top, width, height)
                        });
                    }
                }

                blob.Dispose();
                detection.Dispose();
                detectionMat.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MobileNet tespit hatası: {ex.Message}");
            }

            return sonuclar;
        }

        /// <summary>
        /// Tespit edilen nesneleri görüntüye çizer
        /// </summary>
        public void NesneleriCiz(Mat goruntu, List<DetectedObject> nesneler)
        {
            var renkler = new Dictionary<int, Scalar>();
            var rnd = new Random(42);

            foreach (var nesne in nesneler)
            {
                // Her sınıf için sabit renk
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

                // Etiket
                string etiket = $"{nesne.Sinif} {nesne.Guven:P0}";
                
                var etiketBoyutu = Cv2.GetTextSize(etiket, HersheyFonts.HersheySimplex, 0.6, 1, out int baseline);
                
                int etiketY = nesne.Kutu.Y - 10;
                if (etiketY < 0) etiketY = nesne.Kutu.Y + nesne.Kutu.Height + 20;

                // Etiket arka planı
                Cv2.Rectangle(
                    goruntu,
                    new CvPoint(nesne.Kutu.X, etiketY - etiketBoyutu.Height - 5),
                    new CvPoint(nesne.Kutu.X + etiketBoyutu.Width + 5, etiketY + baseline),
                    renk,
                    -1
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
        /// Nesne özetini oluşturur
        /// </summary>
        public string NesneOzetiOlustur(List<DetectedObject> nesneler)
        {
            if (nesneler.Count == 0)
                return "Nesne bulunamadı";

            var gruplar = nesneler
                .GroupBy(n => n.Sinif)
                .OrderByDescending(g => g.Count())
                .Take(5);

            var ozet = string.Join(", ", gruplar.Select(g =>
                g.Count() > 1 ? $"{g.Count()} {g.Key}" : g.Key
            ));

            return ozet;
        }
    }
}

