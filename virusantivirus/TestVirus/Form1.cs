using System;
using System.IO;
using System.Windows.Forms;

namespace TestVirus
{
    public partial class Form1 : Form
    {
        private Button btnActivate;
        private Button btnDeactivate;
        private Label lblStatus;
        private TextBox txtLog;
        private string virusFolder;

        public Form1()
        {
            InitializeComponent();
            // Test virüsü için klasör yolu (Masaüstünde güvenli bir konum)
            virusFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TestVirusFiles");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form ayarları
            this.Text = "Test Virüsü (Zararsız) - Eğitim Amaçlı";
            this.Size = new System.Drawing.Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            
            // Başlık
            Label lblTitle = new Label();
            lblTitle.Text = "⚠️ TEST VIRÜSÜ (ZARARSIZ) ⚠️";
            lblTitle.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.Red;
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(100, 20);
            this.Controls.Add(lblTitle);
            
            // Açıklama
            Label lblDesc = new Label();
            lblDesc.Text = "Bu program sadece test dosyaları oluşturur.\nBilgisayarınıza zarar vermez!";
            lblDesc.Font = new System.Drawing.Font("Arial", 10);
            lblDesc.ForeColor = System.Drawing.Color.White;
            lblDesc.AutoSize = true;
            lblDesc.Location = new System.Drawing.Point(120, 60);
            this.Controls.Add(lblDesc);
            
            // Aktif et butonu
            btnActivate = new Button();
            btnActivate.Text = "Virüsü Aktif Et";
            btnActivate.Size = new System.Drawing.Size(150, 40);
            btnActivate.Location = new System.Drawing.Point(50, 120);
            btnActivate.BackColor = System.Drawing.Color.DarkRed;
            btnActivate.ForeColor = System.Drawing.Color.White;
            btnActivate.FlatStyle = FlatStyle.Flat;
            btnActivate.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            btnActivate.Click += BtnActivate_Click;
            this.Controls.Add(btnActivate);
            
            // Deaktif et butonu
            btnDeactivate = new Button();
            btnDeactivate.Text = "Virüsü Deaktif Et";
            btnDeactivate.Size = new System.Drawing.Size(150, 40);
            btnDeactivate.Location = new System.Drawing.Point(280, 120);
            btnDeactivate.BackColor = System.Drawing.Color.DarkGreen;
            btnDeactivate.ForeColor = System.Drawing.Color.White;
            btnDeactivate.FlatStyle = FlatStyle.Flat;
            btnDeactivate.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            btnDeactivate.Click += BtnDeactivate_Click;
            this.Controls.Add(btnDeactivate);
            
            // Durum etiketi
            lblStatus = new Label();
            lblStatus.Text = "Durum: Pasif";
            lblStatus.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            lblStatus.ForeColor = System.Drawing.Color.Yellow;
            lblStatus.AutoSize = true;
            lblStatus.Location = new System.Drawing.Point(180, 180);
            this.Controls.Add(lblStatus);
            
            // Log alanı
            txtLog = new TextBox();
            txtLog.Multiline = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new System.Drawing.Size(440, 120);
            txtLog.Location = new System.Drawing.Point(20, 220);
            txtLog.BackColor = System.Drawing.Color.Black;
            txtLog.ForeColor = System.Drawing.Color.LightGreen;
            txtLog.Font = new System.Drawing.Font("Consolas", 9);
            txtLog.ReadOnly = true;
            this.Controls.Add(txtLog);
            
            this.ResumeLayout();
        }

        private void BtnActivate_Click(object sender, EventArgs e)
        {
            try
            {
                // Test virüsü klasörünü oluştur
                if (!Directory.Exists(virusFolder))
                {
                    Directory.CreateDirectory(virusFolder);
                }

                // Zararsız test dosyaları oluştur
                for (int i = 1; i <= 5; i++)
                {
                    string fileName = Path.Combine(virusFolder, $"virus_test_{i}.txt");
                    File.WriteAllText(fileName, $"VIRUS_SIGNATURE_TEST_{i}\nBu bir test dosyasıdır.\nOluşturulma Zamanı: {DateTime.Now}\n");
                    AddLog($"Test dosyası oluşturuldu: virus_test_{i}.txt");
                }

                // İmza dosyası oluştur (antivirüs bunu tespit edecek)
                string signatureFile = Path.Combine(virusFolder, "virus.signature");
                File.WriteAllText(signatureFile, "TEST_VIRUS_SIGNATURE_2025");
                
                lblStatus.Text = "Durum: Aktif ⚠️";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                AddLog("✓ Test virüsü aktif edildi!");
                AddLog($"✓ Konum: {virusFolder}");
                MessageBox.Show($"Test virüsü aktif edildi!\n\nDosyalar oluşturuldu: {virusFolder}\n\nAntivirüs programı ile temizleyebilirsiniz.", 
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddLog($"✗ Hata: {ex.Message}");
            }
        }

        private void BtnDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(virusFolder))
                {
                    Directory.Delete(virusFolder, true);
                    lblStatus.Text = "Durum: Pasif ✓";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    AddLog("✓ Test virüsü deaktif edildi!");
                    AddLog("✓ Tüm test dosyaları silindi.");
                    MessageBox.Show("Test virüsü deaktif edildi!\nTüm test dosyaları silindi.", 
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "Durum: Pasif ✓";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    MessageBox.Show("Test dosyaları zaten mevcut değil.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddLog($"✗ Hata: {ex.Message}");
            }
        }

        private void AddLog(string message)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
        }
    }
}




