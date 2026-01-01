using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using CvSize = OpenCvSharp.Size;
using CvPoint = OpenCvSharp.Point;

namespace ObjectDetection
{
    /// <summary>
    /// Nesne tanıma işlemleri için yardımcı sınıf
    /// </summary>
    public class NesneTaniyici
    {
        private CascadeClassifier? yuzKasiflayici;
        private CascadeClassifier? gozKasiflayici;

        public NesneTaniyici()
        {
            // Constructor - gerektiğinde cascade dosyaları yüklenebilir
        }

        /// <summary>
        /// Haar Cascade dosyalarını yükler
        /// </summary>
        public bool CascadeDosyalariniYukle(string yuzCascade = "haarcascade_frontalface_default.xml",
                                            string gozCascade = "haarcascade_eye.xml")
        {
            try
            {
                if (System.IO.File.Exists(yuzCascade))
                {
                    yuzKasiflayici = new CascadeClassifier(yuzCascade);
                }

                if (System.IO.File.Exists(gozCascade))
                {
                    gozKasiflayici = new CascadeClassifier(gozCascade);
                }

                return yuzKasiflayici != null && !yuzKasiflayici.Empty();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cascade yükleme hatası: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Görüntüde yüz tespit eder
        /// </summary>
        public Rect[] YuzleriTespitEt(Mat goruntu, double olcekFaktoru = 1.1, int minKomsu = 5)
        {
            if (yuzKasiflayici == null || yuzKasiflayici.Empty())
            {
                return Array.Empty<Rect>();
            }

            var griGoruntu = new Mat();
            Cv2.CvtColor(goruntu, griGoruntu, ColorConversionCodes.BGR2GRAY);
            Cv2.EqualizeHist(griGoruntu, griGoruntu);

            return yuzKasiflayici.DetectMultiScale(
                griGoruntu,
                scaleFactor: olcekFaktoru,
                minNeighbors: minKomsu,
                minSize: new CvSize(30, 30)
            );
        }

        /// <summary>
        /// Tespit edilen yüzleri görüntüye çizer
        /// </summary>
        public void YuzleriCiz(Mat goruntu, Rect[] yuzler, Scalar? renk = null, int kalinlik = 2)
        {
            Scalar cizimRengi = renk ?? new Scalar(0, 255, 0);

            for (int i = 0; i < yuzler.Length; i++)
            {
                var yuz = yuzler[i];
                
                // Dikdörtgen çiz
                Cv2.Rectangle(goruntu, yuz, cizimRengi, kalinlik);
                
                // Etiket ekle
                string etiket = $"Yuz #{i + 1}";
                Cv2.PutText(
                    goruntu,
                    etiket,
                    new OpenCvSharp.Point(yuz.X, yuz.Y - 10),
                    HersheyFonts.HersheySimplex,
                    0.6,
                    cizimRengi,
                    2
                );

                // Gözleri tespit et (yüz bölgesi içinde)
                if (gozKasiflayici != null && !gozKasiflayici.Empty())
                {
                    var yuzBolgesi = new Mat(goruntu, yuz);
                    var griYuzBolgesi = new Mat();
                    Cv2.CvtColor(yuzBolgesi, griYuzBolgesi, ColorConversionCodes.BGR2GRAY);

                    var gozler = gozKasiflayici.DetectMultiScale(
                        griYuzBolgesi,
                        scaleFactor: 1.1,
                        minNeighbors: 3,
                        minSize: new CvSize(20, 20)
                    );

                    // Gözleri çiz
                    foreach (var goz in gozler)
                    {
                        var gozKonumu = new OpenCvSharp.Rect(
                            yuz.X + goz.X,
                            yuz.Y + goz.Y,
                            goz.Width,
                            goz.Height
                        );
                        Cv2.Rectangle(goruntu, gozKonumu, new Scalar(255, 0, 0), 2);
                    }
                }
            }
        }

        /// <summary>
        /// Kenar tespiti yapar (Canny algoritması)
        /// </summary>
        public Mat KenarTespiti(Mat goruntu, double esik1 = 50, double esik2 = 150)
        {
            var griGoruntu = new Mat();
            
            if (goruntu.Channels() == 3)
            {
                Cv2.CvtColor(goruntu, griGoruntu, ColorConversionCodes.BGR2GRAY);
            }
            else
            {
                griGoruntu = goruntu.Clone();
            }

            // Gürültü azaltma
            var bulanik = new Mat();
            Cv2.GaussianBlur(griGoruntu, bulanik, new CvSize(5, 5), 1.4);

            // Kenar tespiti
            var kenarlar = new Mat();
            Cv2.Canny(bulanik, kenarlar, esik1, esik2);

            return kenarlar;
        }

        /// <summary>
        /// Kontür bulma ve çizme
        /// </summary>
        public List<OpenCvSharp.Point[]> KonturleriBul(Mat goruntu, double minAlan = 100)
        {
            Cv2.FindContours(
                goruntu,
                out var konturlar,
                out _,
                RetrievalModes.External,
                ContourApproximationModes.ApproxSimple
            );

            // Küçük kontürleri filtrele
            var filtreliKonturlar = new List<OpenCvSharp.Point[]>();
            foreach (var kontur in konturlar)
            {
                double alan = Cv2.ContourArea(kontur);
                if (alan >= minAlan)
                {
                    filtreliKonturlar.Add(kontur);
                }
            }

            return filtreliKonturlar;
        }

        /// <summary>
        /// Kontürleri görüntüye çizer
        /// </summary>
        public void KonturleriCiz(Mat goruntu, List<OpenCvSharp.Point[]> konturlar, Scalar? renk = null, int kalinlik = 2)
        {
            Scalar cizimRengi = renk ?? new Scalar(0, 255, 0);
            
            for (int i = 0; i < konturlar.Count; i++)
            {
                Cv2.DrawContours(goruntu, new[] { konturlar[i] }, -1, cizimRengi, kalinlik);

                    // Sınırlayıcı kutu çiz
                    var dikdortgen = Cv2.BoundingRect(konturlar[i]);
                    Cv2.Rectangle(goruntu, dikdortgen, new Scalar(255, 0, 255), 1);

                    // Alan bilgisini yaz
                    double alan = Cv2.ContourArea(konturlar[i]);
                    Cv2.PutText(
                        goruntu,
                        $"Alan: {alan:F0}",
                        new OpenCvSharp.Point(dikdortgen.X, dikdortgen.Y - 5),
                    HersheyFonts.HersheySimplex,
                    0.4,
                    cizimRengi,
                    1
                );
            }
        }

        /// <summary>
        /// HSV renk aralığında nesne bulur
        /// </summary>
        public Mat RenkMaskesiOlustur(Mat goruntu, Scalar altSinir, Scalar ustSinir)
        {
            var hsvGoruntu = new Mat();
            Cv2.CvtColor(goruntu, hsvGoruntu, ColorConversionCodes.BGR2HSV);

            var maske = new Mat();
            Cv2.InRange(hsvGoruntu, altSinir, ustSinir, maske);

            // Morfolojik işlemlerle gürültüyü azalt
            var cekirdek = Cv2.GetStructuringElement(MorphShapes.Ellipse, new CvSize(5, 5));
            Cv2.MorphologyEx(maske, maske, MorphTypes.Open, cekirdek);
            Cv2.MorphologyEx(maske, maske, MorphTypes.Close, cekirdek);

            return maske;
        }

        /// <summary>
        /// İki görüntü arasındaki hareketi tespit eder
        /// </summary>
        public Mat HareketTespiti(Mat onceki, Mat sonraki, int esikDeger = 25)
        {
            // Farkı al
            var fark = new Mat();
            Cv2.Absdiff(onceki, sonraki, fark);

            // Gri tona çevir
            var griFark = new Mat();
            if (fark.Channels() == 3)
            {
                Cv2.CvtColor(fark, griFark, ColorConversionCodes.BGR2GRAY);
            }
            else
            {
                griFark = fark.Clone();
            }

            // Bulanıklaştır
            var bulanik = new Mat();
            Cv2.GaussianBlur(griFark, bulanik, new CvSize(21, 21), 0);

            // Eşikleme
            var ikiliGoruntu = new Mat();
            Cv2.Threshold(bulanik, ikiliGoruntu, esikDeger, 255, ThresholdTypes.Binary);

            return ikiliGoruntu;
        }

        /// <summary>
        /// Görüntünün parlaklığını ve kontrastını ayarlar
        /// </summary>
        public Mat ParlaklikKontrastAyarla(Mat goruntu, double alfa = 1.0, int beta = 0)
        {
            var sonuc = new Mat();
            goruntu.ConvertTo(sonuc, -1, alfa, beta);
            return sonuc;
        }

        /// <summary>
        /// Görüntüyü döndürür
        /// </summary>
        public Mat GoruntuDondur(Mat goruntu, double aci, OpenCvSharp.Point2f? merkez = null)
        {
            OpenCvSharp.Point2f merkezNokta = merkez ?? new OpenCvSharp.Point2f(goruntu.Width / 2, goruntu.Height / 2);
            
            var donusDonusumu = Cv2.GetRotationMatrix2D(merkezNokta, aci, 1.0);
            
            var sonuc = new Mat();
            Cv2.WarpAffine(goruntu, sonuc, donusDonusumu, goruntu.Size());
            
            return sonuc;
        }

        /// <summary>
        /// Görüntüyü yeniden boyutlandırır
        /// </summary>
        public Mat GoruntuBoyutlandir(Mat goruntu, int genislik, int yukseklik, InterpolationFlags interpolasyon = InterpolationFlags.Linear)
        {
            var sonuc = new Mat();
            Cv2.Resize(goruntu, sonuc, new CvSize(genislik, yukseklik), 0, 0, interpolasyon);
            return sonuc;
        }

        /// <summary>
        /// Histogram eşitleme uygular (kontrast iyileştirme)
        /// </summary>
        public Mat HistogramEsitle(Mat goruntu)
        {
            if (goruntu.Channels() == 1)
            {
                var sonuc = new Mat();
                Cv2.EqualizeHist(goruntu, sonuc);
                return sonuc;
            }
            else
            {
                // Renkli görüntü için YCrCb uzayında sadece Y kanalını eşitle
                var ycrcb = new Mat();
                Cv2.CvtColor(goruntu, ycrcb, ColorConversionCodes.BGR2YCrCb);

                var kanallar = Cv2.Split(ycrcb);
                Cv2.EqualizeHist(kanallar[0], kanallar[0]);
                
                var sonuc = new Mat();
                Cv2.Merge(kanallar, ycrcb);
                Cv2.CvtColor(ycrcb, sonuc, ColorConversionCodes.YCrCb2BGR);

                return sonuc;
            }
        }
    }
}


