using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FlappyBird
{
    public class Bird
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float VelocityY { get; private set; }
        public int Size { get; private set; }

        private float initialX;
        private float initialY;

        public Bird(float x, float y, int size)
        {
            this.initialX = x;
            this.initialY = y;
            this.X = x;
            this.Y = y;
            this.Size = size;
            this.VelocityY = 0;
        }

        public void Update(float gravity)
        {
        
            VelocityY += gravity;
            Y += VelocityY;
        }

        public void Jump(float jumpForce)
        {
            VelocityY = jumpForce;
        }

        public void Reset(float x, float y)
        {
            this.X = x;
            this.Y = y;
            this.VelocityY = 0;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)X, (int)Y, Size, Size);
        }

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            
            using (var shadowBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0)))
            {
                g.FillEllipse(shadowBrush, X + 3, Y + 3, Size, Size);
            }

            
            using (var bodyBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new PointF(X, Y), 
                new PointF(X + Size, Y + Size),
                Color.FromArgb(255, 215, 0), // Altın sarısı
                Color.FromArgb(255, 165, 0)  // Turuncu
            ))
            {
                g.FillEllipse(bodyBrush, X, Y, Size, Size);
            }

            
            using (var pen = new Pen(Color.FromArgb(218, 165, 32), 2))
            {
                g.DrawEllipse(pen, X, Y, Size, Size);
            }

           
            float wingOffset = Math.Abs(VelocityY) > 5 ? 3 : 0;
            using (var wingBrush = new SolidBrush(Color.FromArgb(255, 140, 0)))
            {
                PointF[] wing = {
                    new PointF(X + Size * 0.2f, Y + Size * 0.3f + wingOffset),
                    new PointF(X - Size * 0.2f, Y + Size * 0.1f + wingOffset),
                    new PointF(X - Size * 0.1f, Y + Size * 0.6f + wingOffset),
                    new PointF(X + Size * 0.3f, Y + Size * 0.7f + wingOffset)
                };
                g.FillPolygon(wingBrush, wing);
            }

            
            g.FillEllipse(Brushes.White, X + Size * 0.55f, Y + Size * 0.15f, Size * 0.25f, Size * 0.25f);
            
            
            g.FillEllipse(Brushes.Black, X + Size * 0.62f, Y + Size * 0.22f, Size * 0.12f, Size * 0.12f);
            
            
            g.FillEllipse(Brushes.White, X + Size * 0.65f, Y + Size * 0.24f, Size * 0.05f, Size * 0.05f);
            
            
            using (var beakBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new PointF(X + Size, Y + Size * 0.4f),
                new PointF(X + Size + Size * 0.3f, Y + Size * 0.6f),
                Color.Orange,
                Color.FromArgb(255, 140, 0)
            ))
            {
                PointF[] beak = {
                    new PointF(X + Size, Y + Size * 0.4f),
                    new PointF(X + Size + Size * 0.3f, Y + Size * 0.5f),
                    new PointF(X + Size, Y + Size * 0.6f)
                };
                g.FillPolygon(beakBrush, beak);
            }

            
            using (var beakPen = new Pen(Color.FromArgb(255, 140, 0), 1))
            {
                PointF[] beak = {
                    new PointF(X + Size, Y + Size * 0.4f),
                    new PointF(X + Size + Size * 0.3f, Y + Size * 0.5f),
                    new PointF(X + Size, Y + Size * 0.6f)
                };
                g.DrawPolygon(beakPen, beak);
            }
        }
    }
}
