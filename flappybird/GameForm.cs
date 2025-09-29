using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class GameForm : Form
    {
        // Oyun sabitleri
        private const int FORM_WIDTH = 800;
        private const int FORM_HEIGHT = 600;
        private const int BIRD_SIZE = 30;
        private const int PIPE_WIDTH = 60;
        private const int PIPE_GAP = 150;
        private const int GRAVITY = 1;
        private const int JUMP_FORCE = -15;
        private const int PIPE_SPEED = 3;

        // Oyun değişkenleri
        private Bird bird = null!;
        private List<Pipe> pipes = null!;
        private System.Windows.Forms.Timer gameTimer = null!;
        private int score;
        private bool gameStarted;
        private bool gameOver;
        private Random random = null!;

        // Grafik nesneleri
        private Brush pipeBrush = null!;
        private LinearGradientBrush backgroundBrush = null!;
        private Font scoreFont = null!;
        private Font gameFont = null!;
        private List<Cloud> clouds = null!;
        private float groundOffset;

        public GameForm()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeComponent()
        {
            this.Text = "Flappy Bird";
            this.Size = new Size(FORM_WIDTH, FORM_HEIGHT);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.KeyPreview = true;
            this.DoubleBuffered = true;

            // Event handlers
            this.Paint += GameForm_Paint;
            this.KeyDown += GameForm_KeyDown;
        }

        private void InitializeGame()
        {
            // Grafik nesnelerini başlat
            pipeBrush = new SolidBrush(Color.FromArgb(34, 139, 34)); // Orman yeşili
            backgroundBrush = new LinearGradientBrush(
                new Point(0, 0), 
                new Point(0, FORM_HEIGHT),
                Color.FromArgb(135, 206, 250), // Açık mavi
                Color.FromArgb(255, 182, 193)  // Açık pembe
            );
            scoreFont = new Font("Arial", 24, FontStyle.Bold);
            gameFont = new Font("Arial", 18, FontStyle.Bold);

            // Oyun nesnelerini başlat
            bird = new Bird(FORM_WIDTH / 4, FORM_HEIGHT / 2, BIRD_SIZE);
            pipes = new List<Pipe>();
            clouds = new List<Cloud>();
            random = new Random();
            groundOffset = 0;
            
            // Bulutları başlat
            InitializeClouds();
            
            // Timer'ı başlat
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 20; // 50 FPS
            gameTimer.Tick += GameTimer_Tick;

            ResetGame();
        }

        private void ResetGame()
        {
            // Timer'ı durdur
            gameTimer.Stop();
            
            bird.Reset(FORM_WIDTH / 4, FORM_HEIGHT / 2);
            pipes.Clear();
            score = 0;
            gameStarted = false;
            gameOver = false;
            
            // İlk boruları ekle
            AddPipe();
            
            // Ekranı yenile
            this.Invalidate();
        }

        private void StartGame()
        {
            if (!gameStarted && !gameOver)
            {
                gameStarted = true;
                gameTimer.Start();
            }
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            if (gameOver) return;

            // Kuşu güncelle
            bird.Update(GRAVITY);

            // Boruları güncelle
            UpdatePipes();

            // Bulutları güncelle
            UpdateClouds();

            // Zemin hareketini güncelle
            groundOffset -= 2;
            if (groundOffset <= -50) groundOffset = 0;

            // Çarpışma kontrolü
            CheckCollisions();

            // Ekranı yenile
            this.Invalidate();
        }

        private void UpdatePipes()
        {
            // Mevcut boruları hareket ettir
            foreach (var pipe in pipes.ToList())
            {
                pipe.Update(PIPE_SPEED);

                // Ekrandan çıkan boruları sil
                if (pipe.X + PIPE_WIDTH < 0)
                {
                    pipes.Remove(pipe);
                    score++;
                }
            }

            // Yeni boru ekle
            if (pipes.Count == 0 || pipes.Last().X < FORM_WIDTH - 300)
            {
                AddPipe();
            }
        }

        private void AddPipe()
        {
            int gapY = random.Next(100, FORM_HEIGHT - PIPE_GAP - 100);
            pipes.Add(new Pipe(FORM_WIDTH, gapY, PIPE_WIDTH, PIPE_GAP, FORM_HEIGHT));
        }

        private void CheckCollisions()
        {
            // Zemin ve tavan kontrolü
            if (bird.Y <= 0 || bird.Y + BIRD_SIZE >= FORM_HEIGHT - 50)
            {
                GameOver();
                return;
            }

            // Boru çarpışma kontrolü
            foreach (var pipe in pipes)
            {
                if (pipe.CheckCollision(bird.GetBounds()))
                {
                    GameOver();
                    return;
                }
            }
        }

        private void GameOver()
        {
            gameOver = true;
            gameTimer.Stop();
        }

        private void GameForm_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Arka planı çiz (gradient)
            g.FillRectangle(backgroundBrush, 0, 0, FORM_WIDTH, FORM_HEIGHT);

            // Bulutları çiz
            DrawClouds(g);

            // Boruları çiz
            foreach (var pipe in pipes)
            {
                pipe.Draw(g, pipeBrush);
            }

            // Kuşu çiz
            bird.Draw(g);

            // Zemini çiz (pattern ile)
            DrawGround(g);

            // Skor paneli çiz
            DrawScore(g);

            // Oyun mesajları
            if (!gameStarted && !gameOver)
            {
                DrawMessage(g, "🎮 SPACE tuşuna basarak başlayın!", Color.White, -50);
            }
            else if (gameOver)
            {
                DrawMessage(g, "💀 OYUN BİTTİ!", Color.Red, -30);
                DrawMessage(g, "R tuşuna basarak yeniden başlayın", Color.Yellow, 10);
            }
        }

        private void GameForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (!gameStarted)
                {
                    StartGame();
                }
                else if (!gameOver)
                {
                    bird.Jump(JUMP_FORCE);
                }
            }
            else if (e.KeyCode == Keys.R && gameOver)
            {
                ResetGame();
            }
        }

        private void InitializeClouds()
        {
            for (int i = 0; i < 5; i++)
            {
                clouds.Add(new Cloud(
                    random.Next(0, FORM_WIDTH),
                    random.Next(50, 200),
                    random.Next(60, 120),
                    random.Next(30, 60),
                    random.Next(1, 3)
                ));
            }
        }

        private void UpdateClouds()
        {
            foreach (var cloud in clouds)
            {
                cloud.Update();
                if (cloud.X + cloud.Width < 0)
                {
                    cloud.Reset(FORM_WIDTH + random.Next(50, 200), random.Next(50, 200));
                }
            }
        }

        private void DrawClouds(Graphics g)
        {
            foreach (var cloud in clouds)
            {
                cloud.Draw(g);
            }
        }

        private void DrawGround(Graphics g)
        {
            // Zemin arka planı
            using (var groundBrush = new LinearGradientBrush(
                new Point(0, FORM_HEIGHT - 50),
                new Point(0, FORM_HEIGHT),
                Color.FromArgb(139, 69, 19), // Kahverengi
                Color.FromArgb(160, 82, 45)  // Daha açık kahverengi
            ))
            {
                g.FillRectangle(groundBrush, 0, FORM_HEIGHT - 50, FORM_WIDTH, 50);
            }

            // Zemin pattern (çizgiler)
            using (var pen = new Pen(Color.FromArgb(101, 67, 33), 2))
            {
                for (int x = (int)groundOffset; x < FORM_WIDTH + 50; x += 50)
                {
                    g.DrawLine(pen, x, FORM_HEIGHT - 50, x + 25, FORM_HEIGHT);
                }
            }

            // Çim detayları
            using (var grassBrush = new SolidBrush(Color.FromArgb(34, 139, 34)))
            {
                for (int x = (int)groundOffset; x < FORM_WIDTH + 20; x += 20)
                {
                    g.FillRectangle(grassBrush, x, FORM_HEIGHT - 52, 3, 8);
                    g.FillRectangle(grassBrush, x + 5, FORM_HEIGHT - 50, 2, 6);
                    g.FillRectangle(grassBrush, x + 10, FORM_HEIGHT - 51, 3, 7);
                }
            }
        }

        private void DrawScore(Graphics g)
        {
            // Skor paneli arka planı
            using (var scoreBrush = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
            {
                g.FillRoundedRectangle(scoreBrush, 10, 10, 150, 50, 10);
            }

            // Skor yazısı
            g.DrawString($"🏆 Skor: {score}", scoreFont, Brushes.Gold, 20, 20);
        }

        private void DrawMessage(Graphics g, string message, Color color, int offsetY)
        {
            SizeF messageSize = g.MeasureString(message, gameFont);
            float x = (FORM_WIDTH - messageSize.Width) / 2;
            float y = FORM_HEIGHT / 2 + offsetY;

            // Mesaj arka planı
            using (var messageBrush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
            {
                g.FillRoundedRectangle(messageBrush, x - 10, y - 5, messageSize.Width + 20, messageSize.Height + 10, 10);
            }

            // Mesaj yazısı
            using (var textBrush = new SolidBrush(color))
            {
                g.DrawString(message, gameFont, textBrush, x, y);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            gameTimer?.Stop();
            gameTimer?.Dispose();
            pipeBrush?.Dispose();
            backgroundBrush?.Dispose();
            scoreFont?.Dispose();
            gameFont?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
