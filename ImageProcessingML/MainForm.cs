using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ImageProcessingML.Models;
using ImageProcessingML.Services;
using MLImage = ImageProcessingML.Models.Image;

namespace ImageProcessingML
{
    public partial class MainForm : Form
    {
        private MatrixOperations matrixOps;
        private ImageProcessor imageProcessor;
        private MLImage originalImage;
        private MLImage processedImage;
        private Bitmap originalBitmap;

        public MainForm()
        {
            InitializeComponent();
            InitializeServices();
        }

        private void InitializeServices()
        {
            matrixOps = new MatrixOperations();
            imageProcessor = new ImageProcessor();
        }
        
        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Title = "Görüntü Seçin";
                
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        originalBitmap = new Bitmap(ofd.FileName);
                        
                        // Boyut kontrolü - çok büyükse yeniden boyutlandır
                        if (originalBitmap.Width > 400 || originalBitmap.Height > 400)
                        {
                            float scale = Math.Min(400f / originalBitmap.Width, 400f / originalBitmap.Height);
                            int newWidth = (int)(originalBitmap.Width * scale);
                            int newHeight = (int)(originalBitmap.Height * scale);
                            Bitmap resized = new Bitmap(originalBitmap, newWidth, newHeight);
                            originalBitmap.Dispose();
                            originalBitmap = resized;
                        }
                        
                        originalImage = MLImage.FromBitmap(originalBitmap);
                        picOriginal.Image = originalBitmap;
                        picOriginal.SizeMode = PictureBoxSizeMode.Zoom;
                        
                        txtOutput.Clear();
                        txtOutput.AppendText("Görüntü yüklendi:\r\n");
                        txtOutput.AppendText(string.Format("Boyut: {0}x{1}\r\n", originalImage.Width, originalImage.Height));
                        txtOutput.AppendText("Filtre uygulayabilirsiniz.\r\n");
                        
                        lblOriginal.Text = string.Format("Orijinal ({0}x{1})", originalImage.Width, originalImage.Height);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Görüntü yükleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            if (processedImage == null)
            {
                MessageBox.Show("Kaydedilecek işlenmiş görüntü yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                sfd.Title = "Görüntüyü Kaydet";
                sfd.FileName = "processed_image";
                
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Bitmap bmp = processedImage.ToBitmap();
                        bmp.Save(sfd.FileName);
                        MessageBox.Show("Görüntü kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Kaydetme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        private void ApplyFilter(IImageFilter filter)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Önce bir görüntü yükleyin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                txtOutput.Clear();
                txtOutput.AppendText(string.Format("=== {0} UYGULANIY OR ===\r\n\r\n", filter.Name.ToUpper()));
                
                DateTime start = DateTime.Now;
                processedImage = filter.Apply(originalImage);
                DateTime end = DateTime.Now;
                
                picProcessed.Image = processedImage.ToBitmap();
                picProcessed.SizeMode = PictureBoxSizeMode.Zoom;
                
                txtOutput.AppendText(string.Format("İşlem süresi: {0} ms\r\n", (end - start).TotalMilliseconds));
                txtOutput.AppendText("Filtre başarıyla uygulandı!\r\n");
                
                lblProcessed.Text = string.Format("İşlenmiş - {0}", filter.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtre uygulama hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnShowHistogram_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Önce bir görüntü yükleyin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            txtOutput.Clear();
            txtOutput.AppendText("=== HISTOGRAM ===\r\n\r\n");
            
            int[] histogram = originalImage.GetHistogram();
            
            // En yüksek değeri bul (normalizasyon için)
            int maxValue = 0;
            for (int i = 0; i < histogram.Length; i++)
            {
                if (histogram[i] > maxValue)
                    maxValue = histogram[i];
            }
            
            // ASCII bar chart göster
            int step = 16; // Her 16 değerde bir göster
            for (int i = 0; i < 256; i += step)
            {
                int sum = 0;
                for (int j = i; j < i + step && j < 256; j++)
                {
                    sum += histogram[j];
                }
                int barLength = (int)((sum / (double)maxValue) * 50);
                string bar = new string('█', barLength);
                txtOutput.AppendText(string.Format("{0,3}-{1,3}: {2}\r\n", i, Math.Min(i + step - 1, 255), bar));
            }
            
            txtOutput.AppendText(string.Format("\r\nToplam piksel: {0}\r\n", originalImage.Width * originalImage.Height));
        }

        private void btnMatrixMultiply_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutput.Clear();
                txtOutput.AppendText("=== MATRIX ÇARPIMI ===\r\n\r\n");

                Matrix matrix1 = new Matrix(2, 3);
                matrix1.SetValue(0, 0, 1); matrix1.SetValue(0, 1, 2); matrix1.SetValue(0, 2, 3);
                matrix1.SetValue(1, 0, 4); matrix1.SetValue(1, 1, 5); matrix1.SetValue(1, 2, 6);

                txtOutput.AppendText("Matrix 1 (2x3):\r\n");
                PrintMatrix(matrix1);

                Matrix matrix2 = new Matrix(3, 2);
                matrix2.SetValue(0, 0, 7); matrix2.SetValue(0, 1, 8);
                matrix2.SetValue(1, 0, 9); matrix2.SetValue(1, 1, 10);
                matrix2.SetValue(2, 0, 11); matrix2.SetValue(2, 1, 12);

                txtOutput.AppendText("\r\nMatrix 2 (3x2):\r\n");
                PrintMatrix(matrix2);

                Matrix result = matrixOps.Multiply(matrix1, matrix2);
                txtOutput.AppendText("\r\nSonuç (2x2):\r\n");
                PrintMatrix(result);

                MessageBox.Show("Matrix çarpımı başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLinearRegression_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutput.Clear();
                txtOutput.AppendText("=== LINEAR REGRESSION ===\r\n\r\n");

                List<DataPoint> trainingData = new List<DataPoint>
                {
                    new DataPoint(1, 2),
                    new DataPoint(2, 4),
                    new DataPoint(3, 6),
                    new DataPoint(4, 8),
                    new DataPoint(5, 10)
                };

                LinearRegression lr = new LinearRegression();
                lr.Train(trainingData);

                txtOutput.AppendText("Eğitim Verileri:\r\n");
                foreach (var point in trainingData)
                {
                    txtOutput.AppendText(string.Format("x={0}, y={1}\r\n", point.X, point.Y));
                }

                txtOutput.AppendText(string.Format("\r\nModel Parametreleri:\r\n"));
                txtOutput.AppendText(string.Format("Slope (Eğim): {0:F4}\r\n", lr.Slope));
                txtOutput.AppendText(string.Format("Intercept (Kesişim): {0:F4}\r\n", lr.Intercept));

                double prediction = lr.Predict(6);
                txtOutput.AppendText(string.Format("\r\nTahmin: x=6 için y={0:F2}\r\n", prediction));

                MessageBox.Show("Linear Regression tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKMeans_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutput.Clear();
                txtOutput.AppendText("=== K-MEANS CLUSTERING ===\r\n\r\n");

                List<Point2D> points = new List<Point2D>
                {
                    new Point2D(1, 1),
                    new Point2D(1.5, 2),
                    new Point2D(3, 4),
                    new Point2D(5, 7),
                    new Point2D(3.5, 5),
                    new Point2D(4.5, 5),
                    new Point2D(3.5, 4.5)
                };

                KMeansClustering kmeans = new KMeansClustering(k: 2, maxIterations: 100);
                kmeans.Fit(points);

                txtOutput.AppendText("Veri Noktaları:\r\n");
                foreach (var point in points)
                {
                    txtOutput.AppendText(string.Format("({0:F1}, {1:F1})\r\n", point.X, point.Y));
                }

                txtOutput.AppendText(string.Format("\r\nKüme Sayısı: {0}\r\n\r\n", kmeans.K));
                
                for (int i = 0; i < kmeans.K; i++)
                {
                    var center = kmeans.GetClusterCenter(i);
                    txtOutput.AppendText(string.Format("Küme {0} Merkezi: ({1:F2}, {2:F2})\r\n", 
                        i + 1, center.X, center.Y));
                }

                MessageBox.Show("K-Means Clustering tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBlurFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter(new BlurFilter());
        }

        private void btnEdgeDetection_Click(object sender, EventArgs e)
        {
            ApplyFilter(new EdgeDetectionFilter());
        }
        
        private void btnSharpen_Click(object sender, EventArgs e)
        {
            ApplyFilter(new SharpenFilter());
        }
        
        private void btnBrightness_Click(object sender, EventArgs e)
        {
            ApplyFilter(new BrightnessFilter(50)); // +50 brightness
        }
        
        private void btnDarken_Click(object sender, EventArgs e)
        {
            ApplyFilter(new BrightnessFilter(-50)); // -50 brightness
        }
        
        private void btnContrast_Click(object sender, EventArgs e)
        {
            ApplyFilter(new ContrastFilter(1.5)); // 1.5x contrast
        }
        
        private void btnSepia_Click(object sender, EventArgs e)
        {
            ApplyFilter(new SepiaFilter());
        }
        
        private void btnInvert_Click(object sender, EventArgs e)
        {
            ApplyFilter(new InvertFilter());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
        }

        private MLImage CreateSampleImage()
        {
            MLImage img = new MLImage(8, 8);
            Random rand = new Random(42);
            
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    img.SetPixel(i, j, rand.Next(0, 256));
                }
            }
            
            return img;
        }

        private void PrintMatrix(Matrix matrix)
        {
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    txtOutput.AppendText(matrix.GetValue(i, j).ToString("F2").PadLeft(8) + " ");
                }
                txtOutput.AppendText("\r\n");
            }
        }

        private void PrintImage(MLImage img)
        {
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    txtOutput.AppendText(img.GetPixel(i, j).ToString().PadLeft(4) + " ");
                }
                txtOutput.AppendText("\r\n");
            }
        }
    }
}

