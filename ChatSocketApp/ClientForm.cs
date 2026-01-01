using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatSocketApp
{
    public partial class ClientForm : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool isConnected;
        private string username;

        public ClientForm()
        {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string host = txtHost.Text.Trim();
                int port = (int)nudPort.Value;
                username = txtUsername.Text.Trim();

                if (string.IsNullOrWhiteSpace(host))
                {
                    MessageBox.Show("Host adresi boş olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Kullanıcı adı boş olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AppendMessage("Bağlanılıyor...", Color.Gray);

                client = new TcpClient();
                await client.ConnectAsync(host, port);
                stream = client.GetStream();
                isConnected = true;

                AppendMessage("Bağlantı başarılı!", Color.Green);

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                txtHost.Enabled = false;
                nudPort.Enabled = false;
                txtUsername.Enabled = false;
                txtMessage.Enabled = true;
                btnSend.Enabled = true;

                _ = Task.Run(() => ReceiveMessagesAsync());

                await Task.Delay(500);
                byte[] buffer = new byte[4096];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string welcome = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                byte[] usernameData = Encoding.UTF8.GetBytes(username);
                await stream.WriteAsync(usernameData, 0, usernameData.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isConnected = false;
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void Disconnect()
        {
            isConnected = false;
            stream?.Close();
            client?.Close();

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            txtHost.Enabled = true;
            nudPort.Enabled = true;
            txtUsername.Enabled = true;
            txtMessage.Enabled = false;
            btnSend.Enabled = false;

            AppendMessage("Bağlantı kesildi.", Color.Red);
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendMessageAsync();
        }

        private async void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                await SendMessageAsync();
            }
        }
        
        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            // Kullanıcı yazarken otomatik komut önerisi göster
            string text = txtMessage.Text;
            if (text.StartsWith("/") && text.Length > 1 && !text.Contains(" "))
            {
                // Komut yazılıyor, tooltip gösterilebilir (isteğe bağlı)
            }
        }

        private async Task SendMessageAsync()
        {
            string message = txtMessage.Text.Trim();

            if (string.IsNullOrWhiteSpace(message))
                return;

            if (!isConnected)
            {
                MessageBox.Show("Bağlı değilsiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);

                txtMessage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mesaj gönderme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Disconnect();
            }
        }

        private async Task ReceiveMessagesAsync()
        {
            byte[] buffer = new byte[4096];

            while (isConnected && client != null && client.Connected)
            {
                try
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        isConnected = false;
                        Invoke(new Action(() => {
                            AppendMessage("Server bağlantısı kesildi.", Color.Red);
                            Disconnect();
                        }));
                        break;
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Invoke(new Action(() => AppendMessage(message, Color.Black)));
                }
                catch
                {
                    if (isConnected)
                    {
                        isConnected = false;
                        Invoke(new Action(() => {
                            AppendMessage("Bağlantı hatası.", Color.Red);
                            Disconnect();
                        }));
                    }
                    break;
                }
            }
        }

        private void AppendMessage(string message, Color color)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, Color>(AppendMessage), message, color);
                return;
            }

            rtbChat.SelectionColor = color;
            rtbChat.AppendText(message.TrimEnd() + "\r\n");
            rtbChat.ScrollToCaret();
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isConnected)
            {
                Disconnect();
            }
        }
    }
}

