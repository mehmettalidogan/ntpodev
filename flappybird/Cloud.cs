using System;
using System.Drawing;

namespace FlappyBird
{
    public class Cloud
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float Speed { get; private set; }

        public Cloud(float x, float y, int width, int height, float speed)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Speed = speed;
        }

        public void Update()
        {
            X -= Speed;
        }

        public void Reset(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public void Draw(Graphics g)
        {
            
            using (var cloudBrush = new SolidBrush(Color.FromArgb(200, 255, 255, 255)))
            {
                
                g.FillEllipse(cloudBrush, X, Y, Width, Height);
            
    
                g.FillEllipse(cloudBrush, X - Width * 0.3f, Y + Height * 0.3f, Width * 0.8f, Height * 0.7f);
                g.FillEllipse(cloudBrush, X + Width * 0.3f, Y + Height * 0.2f, Width * 0.6f, Height * 0.8f);
                g.FillEllipse(cloudBrush, X + Width * 0.6f, Y + Height * 0.4f, Width * 0.5f, Height * 0.6f);
                g.FillEllipse(cloudBrush, X - Width * 0.1f, Y - Height * 0.2f, Width * 0.7f, Height * 0.8f);
            }

            
            using (var shadowBrush = new SolidBrush(Color.FromArgb(50, 128, 128, 128)))
            {
                g.FillEllipse(shadowBrush, X + 2, Y + 2, Width, Height);
            }
        }
    }
}
