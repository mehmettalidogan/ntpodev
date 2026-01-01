using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FlappyBird
{
    public class Pipe
    {
        public float X { get; private set; }
        public float GapY { get; private set; }
        public int Width { get; private set; }
        public int GapHeight { get; private set; }
        public int FormHeight { get; private set; }

        public Pipe(float x, float gapY, int width, int gapHeight, int formHeight)
        {
            this.X = x;
            this.GapY = gapY;
            this.Width = width;
            this.GapHeight = gapHeight;
            this.FormHeight = formHeight;
        }

        public void Update(float speed)
        {
            X -= speed;
        }

        public bool CheckCollision(Rectangle birdBounds)
        {
            // Üst boru
            Rectangle topPipe = new Rectangle((int)X, 0, Width, (int)GapY);
            
            // Alt boru (zemin 50 pixel yüksekliğinde)
            Rectangle bottomPipe = new Rectangle((int)X, (int)(GapY + GapHeight), Width, FormHeight - (int)(GapY + GapHeight) - 50);

            return birdBounds.IntersectsWith(topPipe) || birdBounds.IntersectsWith(bottomPipe);
        }

        public void Draw(Graphics g, Brush brush)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float bottomPipeY = GapY + GapHeight;
            float bottomPipeHeight = FormHeight - bottomPipeY - 50; // 50 zemin yüksekliği

            // Boru gölgeleri
            using (var shadowBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0)))
            {
                g.FillRectangle(shadowBrush, X + 3, 3, Width, GapY);
                g.FillRectangle(shadowBrush, X + 3 - 5, GapY - 20 + 3, Width + 10, 20);
                g.FillRectangle(shadowBrush, X + 3, bottomPipeY + 3, Width, bottomPipeHeight);
                g.FillRectangle(shadowBrush, X + 3 - 5, bottomPipeY + 3, Width + 10, 20);
            }

            // Üst boru (gradient efekti)
            using (var topPipeBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new PointF(X, 0), 
                new PointF(X + Width, 0),
                Color.FromArgb(34, 139, 34),  // Orman yeşili
                Color.FromArgb(0, 100, 0)     // Koyu yeşil
            ))
            {
                g.FillRectangle(topPipeBrush, X, 0, Width, GapY);
            }
            
            // Üst boru başlığı (3D efekt)
            using (var capBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new PointF(X - 5, GapY - 20), 
                new PointF(X + Width + 5, GapY - 20),
                Color.FromArgb(50, 205, 50),  // Açık yeşil
                Color.FromArgb(34, 139, 34)   // Orman yeşili
            ))
            {
                g.FillRectangle(capBrush, X - 5, GapY - 20, Width + 10, 20);
            }

            // Alt boru (gradient efekti)
            using (var bottomPipeBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new PointF(X, bottomPipeY), 
                new PointF(X + Width, bottomPipeY),
                Color.FromArgb(34, 139, 34),  // Orman yeşili
                Color.FromArgb(0, 100, 0)     // Koyu yeşil
            ))
            {
                g.FillRectangle(bottomPipeBrush, X, bottomPipeY, Width, bottomPipeHeight);
            }
            
            // Alt boru başlığı (3D efekt)
            using (var bottomCapBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new PointF(X - 5, bottomPipeY), 
                new PointF(X + Width + 5, bottomPipeY),
                Color.FromArgb(50, 205, 50),  // Açık yeşil
                Color.FromArgb(34, 139, 34)   // Orman yeşili
            ))
            {
                g.FillRectangle(bottomCapBrush, X - 5, bottomPipeY, Width + 10, 20);
            }

            // Boru detayları (çizgiler)
            using (var detailPen = new Pen(Color.FromArgb(0, 100, 0), 1))
            {
                // Üst boru dikey çizgileri
                for (int i = 1; i < 4; i++)
                {
                    float lineX = X + (Width / 4) * i;
                    g.DrawLine(detailPen, lineX, 0, lineX, GapY);
                }
                
                // Alt boru dikey çizgileri
                for (int i = 1; i < 4; i++)
                {
                    float lineX = X + (Width / 4) * i;
                    g.DrawLine(detailPen, lineX, bottomPipeY, lineX, bottomPipeY + bottomPipeHeight);
                }
            }

            // Boru kenarlıkları (3D efekt)
            using (var borderPen = new Pen(Color.FromArgb(0, 50, 0), 2))
            {
                // Üst boru kenarlığı
                g.DrawRectangle(borderPen, X, 0, Width, GapY);
                g.DrawRectangle(borderPen, X - 5, GapY - 20, Width + 10, 20);
                
                // Alt boru kenarlığı
                g.DrawRectangle(borderPen, X, bottomPipeY, Width, bottomPipeHeight);
                g.DrawRectangle(borderPen, X - 5, bottomPipeY, Width + 10, 20);
            }

            // Parlak kenarlık efekti
            using (var highlightPen = new Pen(Color.FromArgb(100, 255, 255, 255), 1))
            {
                // Üst boru sol kenar
                g.DrawLine(highlightPen, X + 1, 0, X + 1, GapY);
                g.DrawLine(highlightPen, X - 4, GapY - 19, X - 4, GapY);
                
                // Alt boru sol kenar
                g.DrawLine(highlightPen, X + 1, bottomPipeY, X + 1, bottomPipeY + bottomPipeHeight);
                g.DrawLine(highlightPen, X - 4, bottomPipeY + 1, X - 4, bottomPipeY + 19);
            }
        }
    }
}
