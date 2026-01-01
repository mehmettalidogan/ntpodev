using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessingML.Models
{
    /// <summary>
    /// Image sınıfı - Görüntü verilerini tutar
    /// </summary>
    public class Image
    {
        private int[,] pixels;
        
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        public Image(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Genişlik ve yükseklik pozitif olmalıdır.");
                
            Width = width;
            Height = height;
            pixels = new int[height, width];
        }
        
        /// <summary>
        /// Bitmap'ten Image oluşturur (grayscale)
        /// </summary>
        public static Image FromBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");
            
            Image img = new Image(bitmap.Width, bitmap.Height);
            
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    // Grayscale dönüşümü (Luminosity method)
                    int gray = (int)(pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114);
                    img.SetPixel(y, x, gray);
                }
            }
            
            return img;
        }
        
        /// <summary>
        /// Image'ı Bitmap'e çevirir
        /// </summary>
        public Bitmap ToBitmap()
        {
            Bitmap bitmap = new Bitmap(Width, Height);
            
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int value = pixels[y, x];
                    Color color = Color.FromArgb(value, value, value);
                    bitmap.SetPixel(x, y, color);
                }
            }
            
            return bitmap;
        }
        
        public void SetPixel(int y, int x, int value)
        {
            ValidateCoordinates(y, x);
            pixels[y, x] = Math.Max(0, Math.Min(255, value));
        }
        
        public int GetPixel(int y, int x)
        {
            ValidateCoordinates(y, x);
            return pixels[y, x];
        }
        
        private void ValidateCoordinates(int y, int x)
        {
            if (y < 0 || y >= Height || x < 0 || x >= Width)
                throw new IndexOutOfRangeException("Geçersiz piksel koordinatı.");
        }
        
        public Image Clone()
        {
            Image clone = new Image(Width, Height);
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    clone.SetPixel(i, j, pixels[i, j]);
                }
            }
            return clone;
        }
        
        /// <summary>
        /// Histogram hesaplar (256 seviye)
        /// </summary>
        public int[] GetHistogram()
        {
            int[] histogram = new int[256];
            
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    histogram[pixels[y, x]]++;
                }
            }
            
            return histogram;
        }
    }
}

