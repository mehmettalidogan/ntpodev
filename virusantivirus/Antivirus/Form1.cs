using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Antivirus
{
    public partial class Form1 : Form
    {
        private Button btnScan;
        private Button btnClean;
        private Label lblStatus;
        private TextBox txtLog;
        private ProgressBar progressBar;
        private ListBox lstThreats;
        private Label lblThreatsFound;
        private string virusFolder;
        private List<string> detectedThreats;

        public Form1()
        {
            InitializeComponent();
            virusFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TestVirusFiles");
            detectedThreats = new List<string>();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form ayarları
            this.Text = "Antivirüs Programı - Eğitim Amaçlı";
            this.Size = new System.Drawing.Size(600, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            
            // Başlık
            Label lblTitle = new Label();
            lblTitle.Text = "🛡️ ANTİVİRÜS PROGRAMI";
            lblTitle.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 120, 215);
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(150, 20);
            this.Controls.Add(lblTitle);
            
            // Açıklama
            Label lblDesc = new Label();
            lblDesc.Text = "Bilgisayarınızı test virüslerinden koruyun";
            lblDesc.Font = new System.Drawing.Font("Arial", 10);
            lblDesc.ForeColor = System.Drawing.Color.Gray;
            lblDesc.AutoSize = true;
            lblDesc.Location = new System.Drawing.Point(170, 55);
            this.Controls.Add(lblDesc);
            
            // Tarama butonu
            btnScan = new Button();
            btnScan.Text = "🔍 Tarama Başlat";
            btnScan.Size = new System.Drawing.Size(200, 45);
            btnScan.Location = new System.Drawing.Point(50, 100);
            btnScan.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            btnScan.ForeColor = System.Drawing.Color.White;
            btnScan.FlatStyle = FlatStyle.Flat;
            btnScan.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            btnScan.Click += BtnScan_Click;
            this.Controls.Add(btnScan);
            
            // Temizleme butonu
            btnClean = new Button();
            btnClean.Text = "🧹 Tehditleri Temizle";
            btnClean.Size = new System.Drawing.Size(200, 45);
            btnClean.Location = new System.Drawing.Point(320, 100);
            btnClean.BackColor = System.Drawing.Color.FromArgb(16, 124, 16);
            btnClean.ForeColor = System.Drawing.Color.White;
            btnClean.FlatStyle = FlatStyle.Flat;
            btnClean.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            btnClean.Enabled = false;
            btnClean.Click += BtnClean_Click;
            this.Controls.Add(btnClean);
            
            // Progress bar
            progressBar = new ProgressBar();
            progressBar.Size = new System.Drawing.Size(520, 25);
            progressBar.Location = new System.Drawing.Point(40, 165);
            progressBar.Style = ProgressBarStyle.Continuous;
            this.Controls.Add(progressBar);
            
            // Durum etiketi
            lblStatus = new Label();
            lblStatus.Text = "Durum: Hazır";
            lblStatus.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            lblStatus.ForeColor = System.Drawing.Color.Green;
            lblStatus.AutoSize = true;
            lblStatus.Location = new System.Drawing.Point(40, 200);
            this.Controls.Add(lblStatus);
            
            // Tehdit sayısı etiketi
            lblThreatsFound = new Label();
            lblThreatsFound.Text = "Bulunan Tehdit: 0";
            lblThreatsFound.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            lblThreatsFound.ForeColor = System.Drawing.Color.Red;
            lblThreatsFound.AutoSize = true;
            lblThreatsFound.Location = new System.Drawing.Point(400, 200);
            this.Controls.Add(lblThreatsFound);
            
            // Tehditler listesi
            Label lblThreatsTitle = new Label();
            lblThreatsTitle.Text = "Tespit Edilen Tehditler:";
            lblThreatsTitle.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            lblThreatsTitle.AutoSize = true;
            lblThreatsTitle.Location = new System.Drawing.Point(40, 230);
            this.Controls.Add(lblThreatsTitle);
            
            lstThreats = new ListBox();
            lstThreats.Size = new System.Drawing.Size(520, 80);
            lstThreats.Location = new System.Drawing.Point(40, 255);
            lstThreats.Font = new System.Drawing.Font("Consolas", 9);
            this.Controls.Add(lstThreats);
            
            // Log alanı başlık
            Label lblLogTitle = new Label();
            lblLogTitle.Text = "Tarama Geçmişi:";
            lblLogTitle.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            lblLogTitle.AutoSize = true;
            lblLogTitle.Location = new System.Drawing.Point(40, 345);
            this.Controls.Add(lblLogTitle);
            
            // Log alanı
            txtLog = new TextBox();
            txtLog.Multiline = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new System.Drawing.Size(520, 120);
            txtLog.Location = new System.Drawing.Point(40, 370);
            txtLog.BackColor = System.Drawing.Color.White;
            txtLog.ForeColor = System.Drawing.Color.Black;
            txtLog.Font = new System.Drawing.Font("Consolas", 9);
            txtLog.ReadOnly = true;
            this.Controls.Add(txtLog);
            
            this.ResumeLayout();
        }

        private void BtnScan_Click(object sender, EventArgs e)
        {
            detectedThreats.Clear();
            lstThreats.Items.Clear();
            progressBar.Value = 0;
            lblStatus.Text = "Durum: Taranıyor...";
            lblStatus.ForeColor = System.Drawing.Color.Orange;
            btnScan.Enabled = false;
            btnClean.Enabled = false;
            
            AddLog("=== TARAMA BAŞLATILDI ===");
            AddLog($"Hedef Konum: {virusFolder}");
            
            Application.DoEvents();
            
            try
            {
                // Tarama simülasyonu
                for (int i = 0; i <= 100; i += 10)
                {
                    progressBar.Value = i;
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();
                }
                
                // Virüs klasörünü kontrol et
                if (Directory.Exists(virusFolder))
                {
                    string[] files = Directory.GetFiles(virusFolder);
                    
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        
                        // Virüs imzası kontrolü
                        if (fileName.Contains("virus") || fileName.Contains("signature"))
                        {
                            detectedThreats.Add(file);
                            lstThreats.Items.Add($"⚠️ {fileName}");
                            AddLog($"✗ TEHDİT TESPİT EDİLDİ: {fileName}");
                        }
                    }
                    
                    if (detectedThreats.Count > 0)
                    {
                        lblStatus.Text = "Durum: Tehdit Bulundu! ⚠️";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        lblThreatsFound.Text = $"Bulunan Tehdit: {detectedThreats.Count}";
                        btnClean.Enabled = true;
                        AddLog($"! TOPLAM {detectedThreats.Count} TEHDİT BULUNDİ!");
                        MessageBox.Show($"{detectedThreats.Count} adet tehdit tespit edildi!\n\n'Tehditleri Temizle' butonuna tıklayarak bunları kaldırabilirsiniz.", 
                            "Tehdit Bulundu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        lblStatus.Text = "Durum: Temiz ✓";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        lblThreatsFound.Text = "Bulunan Tehdit: 0";
                        AddLog("✓ Tehdit bulunamadı. Sistem temiz!");
                        MessageBox.Show("Tarama tamamlandı!\n\nHiçbir tehdit bulunamadı. Sisteminiz temiz.", 
                            "Tarama Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    lblStatus.Text = "Durum: Temiz ✓";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    lblThreatsFound.Text = "Bulunan Tehdit: 0";
                    AddLog("✓ Test virüsü klasörü bulunamadı. Sistem temiz!");
                    MessageBox.Show("Tarama tamamlandı!\n\nHiçbir tehdit bulunamadı.", 
                        "Tarama Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Durum: Hata ✗";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                AddLog($"✗ Hata: {ex.Message}");
                MessageBox.Show($"Tarama sırasında hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnScan.Enabled = true;
                progressBar.Value = 100;
            }
        }

        private void BtnClean_Click(object sender, EventArgs e)
        {
            if (detectedThreats.Count == 0)
            {
                MessageBox.Show("Temizlenecek tehdit bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            DialogResult result = MessageBox.Show(
                $"{detectedThreats.Count} adet tehdit silinecek. Devam etmek istiyor musunuz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            
            if (result == DialogResult.Yes)
            {
                AddLog("=== TEMİZLEME BAŞLATILDI ===");
                int cleaned = 0;
                
                try
                {
                    foreach (string threat in detectedThreats)
                    {
                        if (File.Exists(threat))
                        {
                            File.Delete(threat);
                            cleaned++;
                            AddLog($"✓ Silindi: {Path.GetFileName(threat)}");
                        }
                    }
                    
                    // Klasör boşsa sil
                    if (Directory.Exists(virusFolder) && Directory.GetFiles(virusFolder).Length == 0)
                    {
                        Directory.Delete(virusFolder);
                        AddLog($"✓ Boş klasör silindi: {virusFolder}");
                    }
                    
                    detectedThreats.Clear();
                    lstThreats.Items.Clear();
                    lblThreatsFound.Text = "Bulunan Tehdit: 0";
                    lblStatus.Text = "Durum: Temizlendi ✓";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    btnClean.Enabled = false;
                    
                    AddLog($"✓ BAŞARILI! {cleaned} tehdit temizlendi.");
                    MessageBox.Show($"Temizleme başarılı!\n\n{cleaned} adet tehdit silindi.\nSisteminiz artık güvende.", 
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    AddLog($"✗ Temizleme hatası: {ex.Message}");
                    MessageBox.Show($"Temizleme sırasında hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddLog(string message)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
        }
    }
}




