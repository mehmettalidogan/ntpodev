using System;
using System.Drawing;
using System.Windows.Forms;
using SocketIOClient;
using Newtonsoft.Json;

namespace ChatApp
{
    public partial class MainForm : Form
    {
        private SocketIO socket;
        private TextBox messageTextBox;
        private ListBox messageListBox;
        private Button sendButton;
        private Button connectButton;
        private TextBox usernameTextBox;
        private Label statusLabel;
        private string currentUsername = "";

        public MainForm()
        {
            InitializeComponent();
            SetupSocket();
        }

        private void InitializeComponent()
        {
            // Form ayarları - Modern tasarım
            this.Text = "💬 Modern Chat";
            this.Size = new Size(700, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 245, 245); // Modern gri arka plan
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Modern font

            // Üst panel - Modern header
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(33, 150, 243), // Modern mavi
                Padding = new Padding(20, 15, 20, 15)
            };

            // Kullanıcı adı - Modern tasarım
            var usernameLabel = new Label
            {
                Text = "👤 Kullanıcı Adı:",
                Location = new Point(20, 15),
                Size = new Size(100, 25),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.Transparent
            };

            usernameTextBox = new TextBox
            {
                Location = new Point(130, 12),
                Size = new Size(180, 28),
                Text = "User" + new Random().Next(1000, 9999),
                Font = new Font("Segoe UI", 9F),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Bağlan butonu - Modern tasarım
            connectButton = new Button
            {
                Text = "🔗 Bağlan",
                Location = new Point(325, 10),
                Size = new Size(90, 32),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(76, 175, 80), // Yeşil
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            connectButton.FlatAppearance.BorderSize = 0;
            connectButton.Click += ConnectButton_Click;

            // Durum etiketi - Modern tasarım
            statusLabel = new Label
            {
                Text = "🔴 Bağlantı Yok",
                Location = new Point(430, 15),
                Size = new Size(120, 25),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.Transparent
            };

            headerPanel.Controls.AddRange(new Control[] {
                usernameLabel, usernameTextBox, connectButton, statusLabel
            });

            // Ana mesaj paneli - Modern tasarım
            var messagePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(250, 250, 250),
                Padding = new Padding(20, 20, 20, 20)
            };

            // Mesaj listesi başlığı - Modern tasarım
            var messageListLabel = new Label
            {
                Text = "💬 Mesajlar",
                Location = new Point(20, 10),
                Size = new Size(100, 30),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                BackColor = Color.Transparent
            };

            messageListBox = new ListBox
            {
                Location = new Point(20, 45),
                Size = new Size(620, 350),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                ScrollAlwaysVisible = true,
                SelectionMode = SelectionMode.One
            };

            // Mesaj gönderme paneli - Modern tasarım
            var sendPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(33, 150, 243),
                Padding = new Padding(20, 15, 20, 15)
            };

            var messageLabel = new Label
            {
                Text = "✏️ Mesaj:",
                Location = new Point(20, 20),
                Size = new Size(80, 25),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.Transparent
            };

            messageTextBox = new TextBox
            {
                Location = new Point(110, 17),
                Size = new Size(450, 30),
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                PlaceholderText = "Mesajınızı yazın..."
            };
            messageTextBox.KeyPress += MessageTextBox_KeyPress;

            sendButton = new Button
            {
                Text = "🚀 Gönder",
                Location = new Point(570, 15),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 87, 34), // Turuncu
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            sendButton.FlatAppearance.BorderSize = 0;
            sendButton.Click += SendButton_Click;

            sendPanel.Controls.AddRange(new Control[] {
                messageLabel, messageTextBox, sendButton
            });

            messagePanel.Controls.AddRange(new Control[] {
                messageListLabel, messageListBox
            });

            // Kontrolleri forma ekle
            this.Controls.AddRange(new Control[] {
                headerPanel, messagePanel, sendPanel
            });

            // Form kapatılırken temizlik
            this.FormClosing += (s, e) =>
            {
                if (socket != null)
                {
                    socket.DisconnectAsync();
                    socket.Dispose();
                }
            };
        }

        private void SetupSocket()
        {
            socket = new SocketIO("http://localhost:3000");
            
            socket.OnConnected += (sender, e) =>
            {
                this.Invoke(new Action(() =>
                {
                    statusLabel.Text = "🟢 Bağlı";
                    statusLabel.ForeColor = Color.White;
                    connectButton.Enabled = false;
                    connectButton.BackColor = Color.FromArgb(100, 100, 100); // Gri
                    connectButton.Text = "🔗 Bağlandı";
                    sendButton.Enabled = true;
                    AddMessage("Sistem", "🎉 Sunucuya başarıyla bağlandı!");
                }));
            };

            socket.OnDisconnected += (sender, e) =>
            {
                this.Invoke(new Action(() =>
                {
                    statusLabel.Text = "🔴 Bağlantı Yok";
                    statusLabel.ForeColor = Color.White;
                    connectButton.Enabled = true;
                    connectButton.BackColor = Color.FromArgb(76, 175, 80); // Yeşil
                    connectButton.Text = "🔗 Bağlan";
                    sendButton.Enabled = false;
                    AddMessage("Sistem", "❌ Bağlantı kesildi!");
                }));
            };

            socket.On("message", (response) =>
            {
                try
                {
                    // Basit string parsing kullan
                    var responseString = response.ToString();
                    
                    // JSON'dan manuel parsing
                    string username = "Bilinmeyen";
                    string message = "Mesaj alınamadı";
                    string timestamp = DateTime.Now.ToString("HH:mm:ss");
                    
                    // Basit regex ile username ve message çıkar
                    var usernameMatch = System.Text.RegularExpressions.Regex.Match(responseString, @"""username"":""([^""]+)""");
                    var messageMatch = System.Text.RegularExpressions.Regex.Match(responseString, @"""message"":""([^""]+)""");
                    var timestampMatch = System.Text.RegularExpressions.Regex.Match(responseString, @"""timestamp"":""([^""]+)""");
                    
                    if (usernameMatch.Success)
                        username = usernameMatch.Groups[1].Value;
                    if (messageMatch.Success)
                        message = messageMatch.Groups[1].Value;
                    if (timestampMatch.Success)
                        timestamp = timestampMatch.Groups[1].Value;

                    this.Invoke(new Action(() =>
                    {
                        AddMessage(username, message, timestamp);
                    }));
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        AddMessage("Hata", $"Mesaj alınırken hata: {ex.Message}");
                    }));
                }
            });
        }

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                MessageBox.Show("Lütfen bir kullanıcı adı girin!", "Hata");
                return;
            }

            currentUsername = usernameTextBox.Text.Trim();
            usernameTextBox.Enabled = false;

            try
            {
                await socket.ConnectAsync();
                await socket.EmitAsync("join", currentUsername);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata");
                usernameTextBox.Enabled = true;
            }
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            await SendMessage();
        }

        private async void MessageTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                await SendMessage();
            }
        }

        private async Task SendMessage()
        {
            if (string.IsNullOrWhiteSpace(messageTextBox.Text))
                return;

            string message = messageTextBox.Text.Trim();
            messageTextBox.Clear();

            try
            {
                var messageData = new
                {
                    username = currentUsername,
                    message = message,
                    timestamp = DateTime.Now.ToString("HH:mm:ss")
                };

                await socket.EmitAsync("message", messageData);
            }
            catch (Exception ex)
            {
                AddMessage("Hata", $"Mesaj gönderilirken hata: {ex.Message}");
                messageTextBox.Text = message;
            }
        }

        private void AddMessage(string username, string message, string timestamp = null)
        {
            if (string.IsNullOrEmpty(timestamp))
                timestamp = DateTime.Now.ToString("HH:mm:ss");

            // Modern mesaj formatı
            string formattedMessage;
            if (username == "Sistem")
            {
                formattedMessage = $"🔔 [{timestamp}] {username}: {message}";
            }
            else if (username == currentUsername)
            {
                formattedMessage = $"💬 [{timestamp}] Sen: {message}";
            }
            else
            {
                formattedMessage = $"👤 [{timestamp}] {username}: {message}";
            }
            
            messageListBox.Items.Add(formattedMessage);
            
            // Otomatik scroll - son mesajı göster
            if (messageListBox.Items.Count > 0)
            {
                messageListBox.TopIndex = messageListBox.Items.Count - 1;
            }
        }
    }
}