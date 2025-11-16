using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NotUygulamasi
{
    public partial class Form1 : Form
    {
        private string notlarKlasoru = "Notlar";
        private string aktifDosya = "";

        public Form1()
        {
            InitializeComponent();
            KlasorKontrol();
            NotlariYukle();
        }

        private void KlasorKontrol()
        {
            if (!Directory.Exists(notlarKlasoru))
            {
                Directory.CreateDirectory(notlarKlasoru);
            }
        }

        private void NotlariYukle()
        {
            listBoxNotlar.Items.Clear();
            if (Directory.Exists(notlarKlasoru))
            {
                string[] dosyalar = Directory.GetFiles(notlarKlasoru, "*.txt");
                foreach (string dosya in dosyalar)
                {
                    listBoxNotlar.Items.Add(Path.GetFileNameWithoutExtension(dosya));
                }
            }
        }

        private void btnYeniNot_Click(object sender, EventArgs e)
        {
            richTextBoxNot.Clear();
            txtNotBaslik.Clear();
            aktifDosya = "";
            txtNotBaslik.Focus();
            lblDurum.Text = "Yeni not oluşturuluyor...";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNotBaslik.Text))
            {
                MessageBox.Show("Lütfen not başlığı girin!", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string dosyaAdi = txtNotBaslik.Text.Trim();
                // Geçersiz karakterleri temizle
                foreach (char c in Path.GetInvalidFileNameChars())
                {
                    dosyaAdi = dosyaAdi.Replace(c, '_');
                }

                string dosyaYolu = Path.Combine(notlarKlasoru, dosyaAdi + ".txt");
                File.WriteAllText(dosyaYolu, richTextBoxNot.Text);
                
                aktifDosya = dosyaYolu;
                NotlariYukle();
                
                lblDurum.Text = $"Not kaydedildi: {dosyaAdi}";
                lblDurum.ForeColor = Color.FromArgb(46, 204, 113);
                
                // Kaydedilen notu listede seç
                int index = listBoxNotlar.Items.IndexOf(dosyaAdi);
                if (index >= 0)
                {
                    listBoxNotlar.SelectedIndex = index;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblDurum.Text = "Kaydetme başarısız!";
                lblDurum.ForeColor = Color.FromArgb(231, 76, 60);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (listBoxNotlar.SelectedItem == null)
            {
                MessageBox.Show("Lütfen silmek istediğiniz notu seçin!", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult sonuc = MessageBox.Show(
                "Bu notu silmek istediğinizden emin misiniz?", 
                "Onay", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (sonuc == DialogResult.Yes)
            {
                try
                {
                    string secilenNot = listBoxNotlar.SelectedItem.ToString();
                    string dosyaYolu = Path.Combine(notlarKlasoru, secilenNot + ".txt");
                    
                    if (File.Exists(dosyaYolu))
                    {
                        File.Delete(dosyaYolu);
                        NotlariYukle();
                        richTextBoxNot.Clear();
                        txtNotBaslik.Clear();
                        aktifDosya = "";
                        lblDurum.Text = "Not silindi";
                        lblDurum.ForeColor = Color.FromArgb(231, 76, 60);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Silme hatası: {ex.Message}", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxNotlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxNotlar.SelectedItem != null)
            {
                try
                {
                    string secilenNot = listBoxNotlar.SelectedItem.ToString();
                    string dosyaYolu = Path.Combine(notlarKlasoru, secilenNot + ".txt");
                    
                    if (File.Exists(dosyaYolu))
                    {
                        richTextBoxNot.Text = File.ReadAllText(dosyaYolu);
                        txtNotBaslik.Text = secilenNot;
                        aktifDosya = dosyaYolu;
                        lblDurum.Text = $"Not yüklendi: {secilenNot}";
                        lblDurum.ForeColor = Color.FromArgb(52, 152, 219);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Not açma hatası: {ex.Message}", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            string aramaMetni = txtArama.Text.ToLower();
            
            if (string.IsNullOrWhiteSpace(aramaMetni))
            {
                NotlariYukle();
                return;
            }

            listBoxNotlar.Items.Clear();
            
            if (Directory.Exists(notlarKlasoru))
            {
                string[] dosyalar = Directory.GetFiles(notlarKlasoru, "*.txt");
                foreach (string dosya in dosyalar)
                {
                    string dosyaAdi = Path.GetFileNameWithoutExtension(dosya);
                    string icerik = File.ReadAllText(dosya);
                    
                    if (dosyaAdi.ToLower().Contains(aramaMetni) || 
                        icerik.ToLower().Contains(aramaMetni))
                    {
                        listBoxNotlar.Items.Add(dosyaAdi);
                    }
                }
            }
            
            lblDurum.Text = $"{listBoxNotlar.Items.Count} not bulundu";
            lblDurum.ForeColor = Color.FromArgb(155, 89, 182);
        }

        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArama.Text))
            {
                NotlariYukle();
                lblDurum.Text = "Tüm notlar gösteriliyor";
                lblDurum.ForeColor = Color.FromArgb(149, 165, 166);
            }
        }
    }
}

