using System.Drawing;
using System.Windows.Forms;

namespace NotUygulamasi
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelSol;
        private Panel panelSag;
        private Panel panelUst;
        private Panel panelAlt;
        private ListBox listBoxNotlar;
        private RichTextBox richTextBoxNot;
        private TextBox txtNotBaslik;
        private TextBox txtArama;
        private Button btnYeniNot;
        private Button btnKaydet;
        private Button btnSil;
        private Button btnAra;
        private Label lblNotListesi;
        private Label lblNotBaslik;
        private Label lblArama;
        private Label lblDurum;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // Form ayarları
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.BackColor = Color.FromArgb(44, 62, 80);
            this.Text = "Not Uygulaması - Modern Tasarım";
            this.MinimumSize = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Panel Sol - Not Listesi
            this.panelSol = new Panel();
            this.panelSol.BackColor = Color.FromArgb(52, 73, 94);
            this.panelSol.Dock = DockStyle.Left;
            this.panelSol.Width = 300;
            this.panelSol.Padding = new Padding(10);

            // Panel Sağ - Not İçeriği
            this.panelSag = new Panel();
            this.panelSag.BackColor = Color.FromArgb(44, 62, 80);
            this.panelSag.Dock = DockStyle.Fill;
            this.panelSag.Padding = new Padding(15);

            // Panel Üst - Butonlar
            this.panelUst = new Panel();
            this.panelUst.BackColor = Color.FromArgb(34, 49, 63);
            this.panelUst.Dock = DockStyle.Top;
            this.panelUst.Height = 70;
            this.panelUst.Padding = new Padding(10);

            // Panel Alt - Durum Çubuğu
            this.panelAlt = new Panel();
            this.panelAlt.BackColor = Color.FromArgb(34, 49, 63);
            this.panelAlt.Dock = DockStyle.Bottom;
            this.panelAlt.Height = 40;
            this.panelAlt.Padding = new Padding(10, 10, 10, 10);

            // Label - Not Listesi Başlık
            this.lblNotListesi = new Label();
            this.lblNotListesi.Text = "📋 NOTLARIM";
            this.lblNotListesi.ForeColor = Color.FromArgb(236, 240, 241);
            this.lblNotListesi.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblNotListesi.Location = new Point(10, 10);
            this.lblNotListesi.Size = new Size(280, 30);

            // Label - Arama
            this.lblArama = new Label();
            this.lblArama.Text = "🔍 Ara:";
            this.lblArama.ForeColor = Color.FromArgb(189, 195, 199);
            this.lblArama.Font = new Font("Segoe UI", 9F);
            this.lblArama.Location = new Point(10, 50);
            this.lblArama.Size = new Size(60, 25);

            // TextBox - Arama
            this.txtArama = new TextBox();
            this.txtArama.Location = new Point(10, 75);
            this.txtArama.Size = new Size(190, 30);
            this.txtArama.BackColor = Color.FromArgb(44, 62, 80);
            this.txtArama.ForeColor = Color.FromArgb(236, 240, 241);
            this.txtArama.BorderStyle = BorderStyle.FixedSingle;
            this.txtArama.Font = new Font("Segoe UI", 10F);
            this.txtArama.TextChanged += new System.EventHandler(this.txtArama_TextChanged);

            // Button - Ara
            this.btnAra = new Button();
            this.btnAra.Text = "Ara";
            this.btnAra.Location = new Point(205, 75);
            this.btnAra.Size = new Size(85, 30);
            this.btnAra.BackColor = Color.FromArgb(155, 89, 182);
            this.btnAra.ForeColor = Color.White;
            this.btnAra.FlatStyle = FlatStyle.Flat;
            this.btnAra.FlatAppearance.BorderSize = 0;
            this.btnAra.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAra.Cursor = Cursors.Hand;
            this.btnAra.Click += new System.EventHandler(this.btnAra_Click);

            // ListBox - Not Listesi
            this.listBoxNotlar = new ListBox();
            this.listBoxNotlar.Location = new Point(10, 115);
            this.listBoxNotlar.Size = new Size(280, 535);
            this.listBoxNotlar.BackColor = Color.FromArgb(44, 62, 80);
            this.listBoxNotlar.ForeColor = Color.FromArgb(236, 240, 241);
            this.listBoxNotlar.BorderStyle = BorderStyle.FixedSingle;
            this.listBoxNotlar.Font = new Font("Segoe UI", 10F);
            this.listBoxNotlar.ItemHeight = 22;
            this.listBoxNotlar.SelectedIndexChanged += new System.EventHandler(this.listBoxNotlar_SelectedIndexChanged);

            // Button - Yeni Not
            this.btnYeniNot = new Button();
            this.btnYeniNot.Text = "➕ Yeni Not";
            this.btnYeniNot.Location = new Point(20, 15);
            this.btnYeniNot.Size = new Size(140, 40);
            this.btnYeniNot.BackColor = Color.FromArgb(46, 204, 113);
            this.btnYeniNot.ForeColor = Color.White;
            this.btnYeniNot.FlatStyle = FlatStyle.Flat;
            this.btnYeniNot.FlatAppearance.BorderSize = 0;
            this.btnYeniNot.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnYeniNot.Cursor = Cursors.Hand;
            this.btnYeniNot.Click += new System.EventHandler(this.btnYeniNot_Click);

            // Button - Kaydet
            this.btnKaydet = new Button();
            this.btnKaydet.Text = "💾 Kaydet";
            this.btnKaydet.Location = new Point(170, 15);
            this.btnKaydet.Size = new Size(140, 40);
            this.btnKaydet.BackColor = Color.FromArgb(52, 152, 219);
            this.btnKaydet.ForeColor = Color.White;
            this.btnKaydet.FlatStyle = FlatStyle.Flat;
            this.btnKaydet.FlatAppearance.BorderSize = 0;
            this.btnKaydet.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnKaydet.Cursor = Cursors.Hand;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);

            // Button - Sil
            this.btnSil = new Button();
            this.btnSil.Text = "🗑️ Sil";
            this.btnSil.Location = new Point(320, 15);
            this.btnSil.Size = new Size(140, 40);
            this.btnSil.BackColor = Color.FromArgb(231, 76, 60);
            this.btnSil.ForeColor = Color.White;
            this.btnSil.FlatStyle = FlatStyle.Flat;
            this.btnSil.FlatAppearance.BorderSize = 0;
            this.btnSil.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSil.Cursor = Cursors.Hand;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);

            // Label - Not Başlık
            this.lblNotBaslik = new Label();
            this.lblNotBaslik.Text = "✏️ Not Başlığı:";
            this.lblNotBaslik.ForeColor = Color.FromArgb(236, 240, 241);
            this.lblNotBaslik.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblNotBaslik.Location = new Point(15, 15);
            this.lblNotBaslik.Size = new Size(150, 25);

            // TextBox - Not Başlık
            this.txtNotBaslik = new TextBox();
            this.txtNotBaslik.Location = new Point(15, 45);
            this.txtNotBaslik.Size = new Size(840, 30);
            this.txtNotBaslik.BackColor = Color.FromArgb(52, 73, 94);
            this.txtNotBaslik.ForeColor = Color.FromArgb(236, 240, 241);
            this.txtNotBaslik.BorderStyle = BorderStyle.FixedSingle;
            this.txtNotBaslik.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            // RichTextBox - Not İçeriği
            this.richTextBoxNot = new RichTextBox();
            this.richTextBoxNot.Location = new Point(15, 85);
            this.richTextBoxNot.Size = new Size(840, 495);
            this.richTextBoxNot.BackColor = Color.FromArgb(52, 73, 94);
            this.richTextBoxNot.ForeColor = Color.FromArgb(236, 240, 241);
            this.richTextBoxNot.BorderStyle = BorderStyle.FixedSingle;
            this.richTextBoxNot.Font = new Font("Segoe UI", 10F);

            // Label - Durum
            this.lblDurum = new Label();
            this.lblDurum.Text = "Hazır";
            this.lblDurum.ForeColor = Color.FromArgb(149, 165, 166);
            this.lblDurum.Font = new Font("Segoe UI", 9F);
            this.lblDurum.Location = new Point(10, 10);
            this.lblDurum.Size = new Size(1160, 20);

            // Kontrolleri panellere ekle
            this.panelSol.Controls.Add(this.listBoxNotlar);
            this.panelSol.Controls.Add(this.lblNotListesi);
            this.panelSol.Controls.Add(this.lblArama);
            this.panelSol.Controls.Add(this.txtArama);
            this.panelSol.Controls.Add(this.btnAra);

            this.panelSag.Controls.Add(this.lblNotBaslik);
            this.panelSag.Controls.Add(this.txtNotBaslik);
            this.panelSag.Controls.Add(this.richTextBoxNot);

            this.panelUst.Controls.Add(this.btnYeniNot);
            this.panelUst.Controls.Add(this.btnKaydet);
            this.panelUst.Controls.Add(this.btnSil);

            this.panelAlt.Controls.Add(this.lblDurum);

            // Panelleri form'a ekle
            this.Controls.Add(this.panelSag);
            this.Controls.Add(this.panelSol);
            this.Controls.Add(this.panelUst);
            this.Controls.Add(this.panelAlt);

            this.ResumeLayout(false);
        }
    }
}

