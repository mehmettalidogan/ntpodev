using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectDetection
{
    public partial class MainForm : Form
    {
        private VideoCapture? kamera;
        private Mat? suankiGoruntu;
        private CancellationTokenSource? cancelToken;
        private bool kameraAktif = false;
        private NesneTaniyici taniyici;
        private YOLODetector? yoloDetector;
        private MobileNetDetector? mobileNetDetector;
        private string secilenMod = "Normal";
        
        // UI Elementleri
        private PictureBox pictureBox;
        private Panel kontrolPanel;
        private Button btnKameraBaslat;
        private Button btnKameraDurdur;
        private Button btnDosyaAc;
        private Button btnSnapshot;
        private ComboBox cmbModlar;
        private Label lblDurum;
        private GroupBox grpAyarlar;
        private TrackBar trackParlaklık;
        private TrackBar trackKontrast;
        private Label lblParlaklık;
        private Label lblKontrast;
        private CheckBox chkYuzTanima;
        private CheckBox chkHareketTespit;
        private CheckBox chkRenkFiltre;
        private Label lblBilgi;

        public MainForm()
        {
            InitializeComponent();
            taniyici = new NesneTaniyici();
            
            // MobileNet-SSD modelini yükle (YOLO'ya alternatif - daha küçük)
            mobileNetDetector = new MobileNetDetector();
            bool mobileNetYuklendi = mobileNetDetector.ModelYukle();
            
            // YOLO modelini yükle (opsiyonel)
            yoloDetector = new YOLODetector();
            bool yoloYuklendi = yoloDetector.ModelYukle(
                weightsPath: "models/yolov3-tiny.weights",
                configPath: "models/yolov3-tiny.cfg",
                namesPath: "models/coco.names"
            );
            
            // Cascade dosyalarını yükle
            bool cascadeYuklendi = taniyici.CascadeDosyalariniYukle(
                yuzCascade: "models/haarcascade_frontalface_default.xml",
                gozCascade: "models/haarcascade_eye.xml"
            );
            
            // Durum mesajı
            if (mobileNetYuklendi || yoloYuklendi)
            {
                string modelAdi = mobileNetYuklendi ? "MobileNet" : "YOLO";
                lblDurum.Text = $"✓ Hazır - {modelAdi} nesne tanıma aktif!";
                lblDurum.ForeColor = Color.Green;
            }
            else if (cascadeYuklendi)
            {
                lblDurum.Text = "Cascade yüklendi - Nesne tanıma modeli eksik";
                lblDurum.ForeColor = Color.Orange;
            }
            else
            {
                lblDurum.Text = "Uyarı - Model dosyaları yüklenemedi";
                lblDurum.ForeColor = Color.Red;
            }
        }

        private void InitializeComponent()
        {
            this.Text = "OpenCV Nesne Tanıma Uygulaması";
            this.Size = new System.Drawing.Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = true;

            // Ana PictureBox
            pictureBox = new PictureBox
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(800, 600),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black
            };
            this.Controls.Add(pictureBox);

            // Kontrol Paneli
            kontrolPanel = new Panel
            {
                Location = new System.Drawing.Point(820, 10),
                Size = new System.Drawing.Size(360, 640),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke
            };
            this.Controls.Add(kontrolPanel);

            int yPos = 10;

            // Başlık
            Label lblBaslik = new Label
            {
                Location = new System.Drawing.Point(10, yPos),
                Size = new System.Drawing.Size(340, 30),
                Text = "Kontrol Paneli",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            kontrolPanel.Controls.Add(lblBaslik);
            yPos += 40;

            // Kamera Kontrolleri
            GroupBox grpKamera = new GroupBox
            {
                Location = new System.Drawing.Point(10, yPos),
                Size = new System.Drawing.Size(340, 90),
                Text = "Kamera Kontrolleri"
            };
            kontrolPanel.Controls.Add(grpKamera);

            btnKameraBaslat = new Button
            {
                Location = new System.Drawing.Point(10, 25),
                Size = new System.Drawing.Size(150, 50),
                Text = "📹 Kamerayı Başlat",
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnKameraBaslat.Click += BtnKameraBaslat_Click;
            grpKamera.Controls.Add(btnKameraBaslat);

            btnKameraDurdur = new Button
            {
                Location = new System.Drawing.Point(170, 25),
                Size = new System.Drawing.Size(150, 50),
                Text = "⏸ Durdur",
                BackColor = Color.LightCoral,
                Enabled = false,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnKameraDurdur.Click += BtnKameraDurdur_Click;
            grpKamera.Controls.Add(btnKameraDurdur);

            yPos += 100;

            // Dosya İşlemleri
            GroupBox grpDosya = new GroupBox
            {
                Location = new System.Drawing.Point(10, yPos),
                Size = new System.Drawing.Size(340, 100),
                Text = "Dosya İşlemleri"
            };
            kontrolPanel.Controls.Add(grpDosya);

            btnDosyaAc = new Button
            {
                Location = new System.Drawing.Point(10, 25),
                Size = new System.Drawing.Size(150, 30),
                Text = "📁 Görüntü Aç"
            };
            btnDosyaAc.Click += BtnDosyaAc_Click;
            grpDosya.Controls.Add(btnDosyaAc);

            btnSnapshot = new Button
            {
                Location = new System.Drawing.Point(170, 25),
                Size = new System.Drawing.Size(150, 30),
                Text = "📷 Snapshot",
                Enabled = false
            };
            btnSnapshot.Click += BtnSnapshot_Click;
            grpDosya.Controls.Add(btnSnapshot);

            Label lblMod = new Label
            {
                Location = new System.Drawing.Point(10, 60),
                Size = new System.Drawing.Size(80, 25),
                Text = "Mod Seçimi:",
                TextAlign = ContentAlignment.MiddleLeft
            };
            grpDosya.Controls.Add(lblMod);

            cmbModlar = new ComboBox
            {
                Location = new System.Drawing.Point(100, 60),
                Size = new System.Drawing.Size(220, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbModlar.Items.AddRange(new object[] {
                "Normal",
                "🎯 Nesne Tanıma (MobileNet)",
                "🎯 Nesne Tanıma (YOLO)",
                "Yüz Tanıma",
                "Kenar Tespiti",
                "Gri Ton",
                "Bulanıklaştırma",
                "Renk Filtresi"
            });
            cmbModlar.SelectedIndex = 0;
            cmbModlar.SelectedIndexChanged += CmbModlar_SelectedIndexChanged;
            grpDosya.Controls.Add(cmbModlar);

            yPos += 110;

            // Özellikler
            GroupBox grpOzellikler = new GroupBox
            {
                Location = new System.Drawing.Point(10, yPos),
                Size = new System.Drawing.Size(340, 120),
                Text = "Aktif Özellikler"
            };
            kontrolPanel.Controls.Add(grpOzellikler);

            chkYuzTanima = new CheckBox
            {
                Location = new System.Drawing.Point(10, 25),
                Size = new System.Drawing.Size(320, 25),
                Text = "✓ Yüz Tanıma",
                Checked = false
            };
            grpOzellikler.Controls.Add(chkYuzTanima);

            chkHareketTespit = new CheckBox
            {
                Location = new System.Drawing.Point(10, 55),
                Size = new System.Drawing.Size(320, 25),
                Text = "✓ Hareket Tespiti",
                Checked = false
            };
            grpOzellikler.Controls.Add(chkHareketTespit);

            chkRenkFiltre = new CheckBox
            {
                Location = new System.Drawing.Point(10, 85),
                Size = new System.Drawing.Size(320, 25),
                Text = "✓ Kırmızı Renk Filtresi",
                Checked = false
            };
            grpOzellikler.Controls.Add(chkRenkFiltre);

            yPos += 130;

            // Ayarlar
            grpAyarlar = new GroupBox
            {
                Location = new System.Drawing.Point(10, yPos),
                Size = new System.Drawing.Size(340, 140),
                Text = "Görüntü Ayarları"
            };
            kontrolPanel.Controls.Add(grpAyarlar);

            lblParlaklık = new Label
            {
                Location = new System.Drawing.Point(10, 25),
                Size = new System.Drawing.Size(120, 20),
                Text = "Parlaklık: 0"
            };
            grpAyarlar.Controls.Add(lblParlaklık);

            trackParlaklık = new TrackBar
            {
                Location = new System.Drawing.Point(10, 45),
                Size = new System.Drawing.Size(320, 45),
                Minimum = -100,
                Maximum = 100,
                Value = 0,
                TickFrequency = 10
            };
            trackParlaklık.ValueChanged += TrackParlaklık_ValueChanged;
            grpAyarlar.Controls.Add(trackParlaklık);

            lblKontrast = new Label
            {
                Location = new System.Drawing.Point(10, 80),
                Size = new System.Drawing.Size(120, 20),
                Text = "Kontrast: 1.0"
            };
            grpAyarlar.Controls.Add(lblKontrast);

            trackKontrast = new TrackBar
            {
                Location = new System.Drawing.Point(10, 100),
                Size = new System.Drawing.Size(320, 45),
                Minimum = 0,
                Maximum = 30,
                Value = 10,
                TickFrequency = 5
            };
            trackKontrast.ValueChanged += TrackKontrast_ValueChanged;
            grpAyarlar.Controls.Add(trackKontrast);

            yPos += 150;

            // Bilgi Paneli
            lblBilgi = new Label
            {
                Location = new System.Drawing.Point(10, yPos),
                Size = new System.Drawing.Size(340, 80),
                Text = "FPS: 0\nTespit: 0\nÇözünürlük: -",
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Font = new Font("Consolas", 9),
                Padding = new Padding(5)
            };
            kontrolPanel.Controls.Add(lblBilgi);

            // Durum Çubuğu
            lblDurum = new Label
            {
                Location = new System.Drawing.Point(10, 620),
                Size = new System.Drawing.Size(800, 25),
                Text = "Hazır",
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.LightYellow,
                Padding = new Padding(5, 0, 0, 0)
            };
            this.Controls.Add(lblDurum);
        }

        private void BtnKameraBaslat_Click(object? sender, EventArgs e)
        {
            if (kameraAktif) return;

            try
            {
                kamera = new VideoCapture(0);
                if (!kamera.IsOpened())
                {
                    MessageBox.Show("Kamera açılamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                kameraAktif = true;
                btnKameraBaslat.Enabled = false;
                btnKameraDurdur.Enabled = true;
                btnSnapshot.Enabled = true;
                lblDurum.Text = "Kamera aktif";
                lblDurum.ForeColor = Color.Green;

                cancelToken = new CancellationTokenSource();
                Task.Run(() => KameraLoop(cancelToken.Token));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKameraDurdur_Click(object? sender, EventArgs e)
        {
            KamerayiDurdur();
        }

        private void KamerayiDurdur()
        {
            if (!kameraAktif) return;

            kameraAktif = false;
            cancelToken?.Cancel();
            
            Thread.Sleep(100); // Kamera loop'unun bitmesini bekle
            
            kamera?.Release();
            kamera?.Dispose();
            kamera = null;

            btnKameraBaslat.Enabled = true;
            btnKameraDurdur.Enabled = false;
            btnSnapshot.Enabled = false;
            lblDurum.Text = "Kamera durduruldu";
            lblDurum.ForeColor = Color.Gray;
        }

        private Mat? oncekiKare = null;
        private DateTime sonFPSGuncelleme = DateTime.Now;
        private int frameSayaci = 0;

        private void KameraLoop(CancellationToken token)
        {
            while (kameraAktif && !token.IsCancellationRequested)
            {
                try
                {
                    suankiGoruntu = new Mat();
                    kamera?.Read(suankiGoruntu);

                    if (suankiGoruntu.Empty())
                        continue;

                    var islenmisMat = suankiGoruntu.Clone();

                    // Parlaklık ve kontrast ayarları
                    double kontrast = trackKontrast.Value / 10.0;
                    int parlaklik = trackParlaklık.Value;
                    if (kontrast != 1.0 || parlaklik != 0)
                    {
                        islenmisMat = taniyici.ParlaklikKontrastAyarla(islenmisMat, kontrast, parlaklik);
                    }

                    // Mod işlemleri
                    int tespit = 0;
                    string mesaj = "";
                    
                    switch (secilenMod)
                    {
                        case "🎯 Nesne Tanıma (MobileNet)":
                            if (mobileNetDetector != null && mobileNetDetector.ModelYukluMu())
                            {
                                var nesneler = mobileNetDetector.NesneleriTespitEt(islenmisMat, guvenEsigi: 0.4f);
                                mobileNetDetector.NesneleriCiz(islenmisMat, nesneler);
                                tespit = nesneler.Count;
                                mesaj = mobileNetDetector.NesneOzetiOlustur(nesneler);
                            }
                            else
                            {
                                Cv2.PutText(islenmisMat, "MobileNet modeli yuklenemedi!",
                                    new OpenCvSharp.Point(10, 30),
                                    HersheyFonts.HersheySimplex,
                                    0.7, new Scalar(0, 0, 255), 2);
                            }
                            break;

                        case "🎯 Nesne Tanıma (YOLO)":
                            if (yoloDetector != null && yoloDetector.ModelYukluMu())
                            {
                                var nesneler = yoloDetector.NesneleriTespitEt(islenmisMat, guvenEsigi: 0.4f);
                                yoloDetector.NesneleriCiz(islenmisMat, nesneler);
                                tespit = nesneler.Count;
                                mesaj = yoloDetector.NesneOzetiOlustur(nesneler);
                            }
                            else
                            {
                                Cv2.PutText(islenmisMat, "YOLO modeli yuklenemedi!",
                                    new OpenCvSharp.Point(10, 30),
                                    HersheyFonts.HersheySimplex,
                                    0.7, new Scalar(0, 0, 255), 2);
                            }
                            break;

                        case "Yüz Tanıma":
                            var yuzler = taniyici.YuzleriTespitEt(islenmisMat);
                            taniyici.YuzleriCiz(islenmisMat, yuzler);
                            tespit = yuzler.Length;
                            break;

                        case "Kenar Tespiti":
                            var kenarlar = taniyici.KenarTespiti(islenmisMat);
                            islenmisMat = kenarlar;
                            Cv2.CvtColor(islenmisMat, islenmisMat, ColorConversionCodes.GRAY2BGR);
                            break;

                        case "Gri Ton":
                            Cv2.CvtColor(islenmisMat, islenmisMat, ColorConversionCodes.BGR2GRAY);
                            Cv2.CvtColor(islenmisMat, islenmisMat, ColorConversionCodes.GRAY2BGR);
                            break;

                        case "Bulanıklaştırma":
                            Cv2.GaussianBlur(islenmisMat, islenmisMat, new OpenCvSharp.Size(25, 25), 0);
                            break;

                        case "Renk Filtresi":
                            var maske = taniyici.RenkMaskesiOlustur(islenmisMat, 
                                new Scalar(0, 120, 70), new Scalar(10, 255, 255));
                            var konturlar = taniyici.KonturleriBul(maske, 500);
                            taniyici.KonturleriCiz(islenmisMat, konturlar);
                            tespit = konturlar.Count;
                            break;
                    }

                    // Ek özellikler
                    if (chkYuzTanima.Checked && secilenMod != "Yüz Tanıma")
                    {
                        var yuzler = taniyici.YuzleriTespitEt(islenmisMat);
                        taniyici.YuzleriCiz(islenmisMat, yuzler, new Scalar(255, 0, 0));
                        tespit += yuzler.Length;
                    }

                    if (chkHareketTespit.Checked && oncekiKare != null)
                    {
                        var hareketMaskesi = taniyici.HareketTespiti(oncekiKare, islenmisMat);
                        var hareketKonturlari = taniyici.KonturleriBul(hareketMaskesi, 1000);
                        
                        foreach (var kontur in hareketKonturlari)
                        {
                            var kutu = Cv2.BoundingRect(kontur);
                            Cv2.Rectangle(islenmisMat, kutu, new Scalar(0, 0, 255), 2);
                        }
                        tespit += hareketKonturlari.Count;
                    }

                    if (chkRenkFiltre.Checked && secilenMod != "Renk Filtresi")
                    {
                        var maske = taniyici.RenkMaskesiOlustur(islenmisMat,
                            new Scalar(0, 120, 70), new Scalar(10, 255, 255));
                        var konturlar = taniyici.KonturleriBul(maske, 500);
                        
                        foreach (var kontur in konturlar)
                        {
                            var kutu = Cv2.BoundingRect(kontur);
                            Cv2.Circle(islenmisMat, 
                                new OpenCvSharp.Point(kutu.X + kutu.Width/2, kutu.Y + kutu.Height/2), 
                                5, new Scalar(0, 255, 255), -1);
                        }
                    }

                    oncekiKare = suankiGoruntu.Clone();

                    // FPS hesaplama
                    frameSayaci++;
                    var simdi = DateTime.Now;
                    if ((simdi - sonFPSGuncelleme).TotalSeconds >= 1)
                    {
                        double fps = frameSayaci / (simdi - sonFPSGuncelleme).TotalSeconds;
                        string bilgi = $"FPS: {fps:F1}\nTespit: {tespit}\nÇözünürlük: {islenmisMat.Width}x{islenmisMat.Height}";
                        
                        if (!string.IsNullOrEmpty(mesaj))
                        {
                            bilgi += $"\n\nBulunan:\n{mesaj}";
                        }
                        
                        this.Invoke((MethodInvoker)delegate {
                            lblBilgi.Text = bilgi;
                        });
                        
                        frameSayaci = 0;
                        sonFPSGuncelleme = simdi;
                    }

                    // Bitmap'e çevir ve göster
                    var bitmap = BitmapConverter.ToBitmap(islenmisMat);
                    
                    this.Invoke((MethodInvoker)delegate {
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = bitmap;
                    });

                    Thread.Sleep(30); // ~33 FPS
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Kamera loop hatası: {ex.Message}");
                }
            }
        }

        private void BtnDosyaAc_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "Görüntü Dosyaları|*.jpg;*.jpeg;*.png;*.bmp|Tüm Dosyalar|*.*",
                Title = "Görüntü Seç"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var mat = Cv2.ImRead(dialog.FileName);
                    if (!mat.Empty())
                    {
                        var bitmap = BitmapConverter.ToBitmap(mat);
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = bitmap;
                        lblDurum.Text = $"Görüntü yüklendi: {dialog.FileName}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Görüntü yüklenemedi: {ex.Message}", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnSnapshot_Click(object? sender, EventArgs e)
        {
            if (suankiGoruntu != null && !suankiGoruntu.Empty())
            {
                string dosyaAdi = $"snapshot_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
                Cv2.ImWrite(dosyaAdi, suankiGoruntu);
                MessageBox.Show($"Kaydedildi: {dosyaAdi}", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CmbModlar_SelectedIndexChanged(object? sender, EventArgs e)
        {
            secilenMod = cmbModlar.SelectedItem?.ToString() ?? "Normal";
            lblDurum.Text = $"Mod değiştirildi: {secilenMod}";
        }

        private void TrackParlaklık_ValueChanged(object? sender, EventArgs e)
        {
            lblParlaklık.Text = $"Parlaklık: {trackParlaklık.Value}";
        }

        private void TrackKontrast_ValueChanged(object? sender, EventArgs e)
        {
            lblKontrast.Text = $"Kontrast: {trackKontrast.Value / 10.0:F1}";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            KamerayiDurdur();
            base.OnFormClosing(e);
        }
    }
}



