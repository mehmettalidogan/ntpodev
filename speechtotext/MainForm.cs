using System;
using System.Drawing;
using System.Speech.Recognition;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SpeechToText
{
    public partial class MainForm : Form
    {
        private SpeechRecognitionEngine speechRecognitionEngine;
        private TextBox textBox;
        private Button micButton;
        private Label statusLabel;
        private Button copyButton, clearButton;
        private Panel mainCard;
        private bool isListening = false;

        public MainForm()
        {
            InitializeComponent();
            SetupSpeechRecognition();
        }

        private void InitializeComponent()
        {
            // --------- Koyu Arka Plan ve Modern Kart Stil ---------
            this.Text = "🎤 Speech to Text | Dark Mode";
            this.Size = new Size(720, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(32, 34, 39); // koyu arka plan
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            // Ana kart paneli
            mainCard = new Panel
            {
                BackColor = Color.FromArgb(44, 47, 55), // biraz daha açık ton
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(28, 28, 28, 24)
            };
            mainCard.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.ClientSize.Width, this.ClientSize.Height, 28, 28));
            this.Controls.Add(mainCard);
            this.Resize += MainForm_Resize;
            mainCard.Resize += MainCard_Resize;

            // Başlık ve durum
            var header = new Label
            {
                Text = "🎙️ Konuşmadan Metne",
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                ForeColor = Color.FromArgb(182, 222, 242),
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(10, 6)
            };
            mainCard.Controls.Add(header);

            statusLabel = new Label
            {
                Text = "● Kapalı",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(244, 67, 54), // kırmızı başta
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(16, 44)
            };
            mainCard.Controls.Add(statusLabel);

            // ----------- Büyük Mikrofon Butonu -----------
            micButton = new Button
            {
                Image = GetMicIcon(false),
                Size = new Size(80, 80),
                FlatStyle = FlatStyle.Flat,
                Location = new Point((mainCard.ClientSize.Width - 80) / 2, 76),
                BackColor = Color.FromArgb(34, 198, 174),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            micButton.FlatAppearance.BorderSize = 0;
            micButton.Click += MicButton_Click;
            mainCard.Controls.Add(micButton);

            // ---------- Metin Kutusu ----------
            textBox = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 13F),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(mainCard.ClientSize.Width - 60, 150),
                Location = new Point(30, 176),
                BackColor = Color.FromArgb(25, 28, 33),
                ForeColor = Color.White,
                ReadOnly = false
            };
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mainCard.Controls.Add(textBox);

            // ----------- Iconlu Copy & Temizle Butonları ------------
            copyButton = new Button
            {
                Text = "  Kopyala",
                Image = GetCopyIcon(),
                ImageAlign = ContentAlignment.MiddleLeft,
                Size = new Size(115, 41),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 224, 140),
                ForeColor = Color.FromArgb(30, 30, 30),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(mainCard.ClientSize.Width - 260, mainCard.ClientSize.Height - 60)
            };
            copyButton.FlatAppearance.BorderSize = 0;
            copyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            copyButton.Click += CopyButton_Click;
            mainCard.Controls.Add(copyButton);

            clearButton = new Button
            {
                Text = "  Temizle",
                Image = GetTrashIcon(),
                ImageAlign = ContentAlignment.MiddleLeft,
                Size = new Size(120, 41),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(mainCard.ClientSize.Width - 130, mainCard.ClientSize.Height - 60)
            };
            clearButton.FlatAppearance.BorderSize = 0;
            clearButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            clearButton.Click += ClearButton_Click;
            mainCard.Controls.Add(clearButton);
        }
        // --------------- ROUND RECT HELPER ---------------
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        // --------------- İCONLAR ---------------
        private Image GetMicIcon(bool on)
        {
            Bitmap bmp = new Bitmap(50, 50);
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            Pen p = new Pen(on ? Color.FromArgb(52,224,140) : Color.FromArgb(182, 222, 242), 5f);
            g.DrawEllipse(p, 6, 8, 36, 30);
            g.DrawLine(p, 25, 38, 25, 48); // dik sap
            g.DrawArc(p, 15, 40, 20, 10, 0, 180); // alt yarım yay
            g.DrawEllipse(new Pen(on ? Color.FromArgb(52,224,140) : Color.FromArgb(182, 222, 242), 2f), 17, 18, 16, 15);
            return bmp;
        }
        private Image GetCopyIcon()
        {
            Bitmap bmp = new Bitmap(24, 24);
            using var g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            g.FillRectangle(Brushes.White, 5, 6, 14, 14);
            g.DrawRectangle(new Pen(Color.FromArgb(52,224,140), 2), 5, 6, 14, 14);
            g.FillRectangle(new SolidBrush(Color.FromArgb(52,224,140)), 8, 3, 14, 14);
            g.DrawRectangle(new Pen(Color.FromArgb(52,224,140), 2), 8, 3, 14, 14);
            return bmp;
        }
        private Image GetTrashIcon()
        {
            Bitmap bmp = new Bitmap(24, 24);
            using var g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            g.FillRectangle(new SolidBrush(Color.FromArgb(244, 67, 54)), 6, 10, 12, 8);
            g.DrawRectangle(new Pen(Color.White, 2), 6, 10, 12, 8);
            g.FillRectangle(Brushes.White, 9, 14, 2, 5);
            g.FillRectangle(Brushes.White, 13, 14, 2, 5);
            g.DrawLine(new Pen(Color.White, 2), 8, 8, 16, 8);
            return bmp;
        }
        // --------- SPEECH SETUP ---------
        private void SetupSpeechRecognition()
        {
            try
            {
                // Yüklü tanıyıcıları listele ve tercihen TR, yoksa EN seç
                var recognizers = SpeechRecognitionEngine.InstalledRecognizers();
                string available;
                if (recognizers.Count == 0)
                {
                    available = "(yok)";
                }
                else
                {
                    var list = new System.Collections.Generic.List<string>();
                    foreach (var r in recognizers) list.Add($"{r.Culture.Name}:{r.Name}");
                    available = string.Join(", ", list);
                }
                RecognizerInfo selected = null;
                foreach (var r in recognizers)
                {
                    if (r.Culture.TwoLetterISOLanguageName.Equals("tr", StringComparison.OrdinalIgnoreCase))
                    {
                        selected = r; break;
                    }
                }
                if (selected == null)
                {
                    foreach (var r in recognizers)
                    {
                        if (r.Culture.TwoLetterISOLanguageName.Equals("en", StringComparison.OrdinalIgnoreCase))
                        {
                            selected = r; break;
                        }
                    }
                }

                if (selected == null)
                {
                    speechRecognitionEngine = null;
                    micButton.Enabled = false;
                    MessageBox.Show($"Herhangi bir Speech Recognition tanıyıcısı bulunamadı.\n\nMevcut tanıyıcılar: {available}.\n\nLütfen Ayarlar > Zaman ve dil > Konuşma bölümünden bir konuşma dil paketi (TR veya EN-US) kurun ve Konuşma Tanıma'yı etkinleştirin.", "Tanıyıcı Yok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    statusLabel.Text = "● Tanıyıcı yok";
                    statusLabel.ForeColor = Color.Orange;
                    return;
                }

                speechRecognitionEngine = new SpeechRecognitionEngine(selected);
                speechRecognitionEngine.LoadGrammar(new DictationGrammar());
                try
                {
                    speechRecognitionEngine.SetInputToDefaultAudioDevice();
                }
                catch (InvalidOperationException)
                {
                    // Ses girişi yoksa kullanıcıya yol göster
                    micButton.Enabled = false;
                    statusLabel.Text = "● Mikrofon bulunamadı";
                    statusLabel.ForeColor = Color.Orange;
                    MessageBox.Show("Varsayılan ses girişi bulunamadı. Lütfen\n1) Ayarlar > Gizlilik ve güvenlik > Mikrofon: Masaüstü uygulamalarına izin verin\n2) Denetim Masası > Ses > Kayıt sekmesi: Mikrofonu etkinleştirip Varsayılan yapın\n3) Harici mikrofon kullanıyorsanız takılı olduğundan emin olun.", "Mikrofon Yok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                speechRecognitionEngine.SpeechRecognized += SpeechRecognitionEngine_SpeechRecognized;
                micButton.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Speech Recognition kurulurken hata: {ex.Message}\n\nWindows Speech Recognition özelliğinin etkin olduğundan emin olun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                statusLabel.Text = "● Mikrofon bulunamadı";
                statusLabel.ForeColor = Color.Orange;
                micButton.Enabled = false;
            }
        }
        private void MicButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (speechRecognitionEngine == null)
                {
                    SetupSpeechRecognition();
                    if (speechRecognitionEngine == null)
                    {
                        MessageBox.Show("Mikrofon başlatılamadı. Uygun tanıyıcı kurulumu gerekli.", "Başlatma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (isListening)
                {
                    speechRecognitionEngine.RecognizeAsyncStop();
                    statusLabel.Text = "● Kapalı";
                    statusLabel.ForeColor = Color.FromArgb(244, 67, 54);
                    isListening = false;
                    micButton.Image = GetMicIcon(false);
                }
                else
                {
                    speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                    statusLabel.Text = "● Dinleniyor";
                    statusLabel.ForeColor = Color.FromArgb(52, 224, 140);
                    isListening = true;
                    micButton.Image = GetMicIcon(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Mikrofon açılırken hata: {ex.Message}");
            }
        }
        private void SpeechRecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence > 0.68)
            {
                this.Invoke(new Action(() =>
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                        textBox.Text = e.Result.Text;
                    else
                        textBox.Text += " " + e.Result.Text;
                }));
            }
        }
        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                Clipboard.SetText(textBox.Text);
                MessageBox.Show("Metin panoya kopyalandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kopyalanacak metin yok!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tüm metni temizlemek istediğinizden emin misiniz?", "Temizle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                textBox.Clear();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (speechRecognitionEngine != null)
                {
                    if (isListening)
                    {
                        speechRecognitionEngine.RecognizeAsyncCancel();
                    }
                    speechRecognitionEngine.Dispose();
                }
            }
            catch { }
            base.OnFormClosing(e);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // mainCard is Dock=Fill, rounded region should adapt to client area
            mainCard.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, mainCard.Width, mainCard.Height, 28, 28));
        }

        private void MainCard_Resize(object sender, EventArgs e)
        {
            // Center mic button horizontally
            micButton.Location = new Point((mainCard.ClientSize.Width - micButton.Width) / 2, micButton.Location.Y);

            // Stretch textbox width with margins
            textBox.Width = mainCard.ClientSize.Width - 60;

            // Position action buttons bottom-right with margin
            int margin = 30;
            clearButton.Location = new Point(mainCard.ClientSize.Width - clearButton.Width - margin, mainCard.ClientSize.Height - clearButton.Height - margin);
            copyButton.Location = new Point(clearButton.Left - copyButton.Width - 12, clearButton.Top);
        }
    }
}


