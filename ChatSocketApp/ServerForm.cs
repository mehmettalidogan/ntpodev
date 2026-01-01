using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatSocketApp.Models;
using ChatSocketApp.Services;

namespace ChatSocketApp
{
    public partial class ServerForm : Form
    {
        private TcpListener listener;
        private IClientManager clientManager;
        private IMessageHandler messageHandler;
        private ServerConfig config;
        private bool isRunning;

        public ServerForm()
        {
            InitializeComponent();
            clientManager = new ClientManager();
            messageHandler = new MessageHandler(clientManager);
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                int port = (int)nudPort.Value;
                int maxClients = (int)nudMaxClients.Value;
                
                config = new ServerConfig("0.0.0.0", port, maxClients);
                
                listener = new TcpListener(IPAddress.Any, config.Port);
                listener.Start();
                isRunning = true;
                
                LogMessage("Server başlatıldı: Port " + config.Port);
                LogMessage("Maksimum client: " + config.MaxClients);
                
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                nudPort.Enabled = false;
                nudMaxClients.Enabled = false;
                
                await AcceptClientsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server başlatma hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        private void StopServer()
        {
            isRunning = false;
            listener?.Stop();
            
            LogMessage("Server durduruldu.");
            
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            nudPort.Enabled = true;
            nudMaxClients.Enabled = true;
        }

        private async Task AcceptClientsAsync()
        {
            while (isRunning)
            {
                try
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    
                    if (clientManager.GetClientCount() < config.MaxClients)
                    {
                        _ = Task.Run(() => HandleClientAsync(client));
                    }
                    else
                    {
                        SendMessage(client, "Server dolu.");
                        client.Close();
                    }
                }
                catch
                {
                    if (isRunning)
                        break;
                }
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            ClientInfo clientInfo = null;
            NetworkStream stream = client.GetStream();
            
            try
            {
                SendMessage(client, "Kullanıcı adınızı girin:");
                byte[] buffer = new byte[4096];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string username = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                
                if (string.IsNullOrWhiteSpace(username))
                {
                    SendMessage(client, "Geçersiz kullanıcı adı.");
                    client.Close();
                    return;
                }
                
                clientInfo = new ClientInfo(username, client);
                clientManager.AddClient(clientInfo);
                
                LogMessage(username + " bağlandı.");
                UpdateClientList();
                
                SendMessage(client, "Hoş geldiniz " + username + "!");
                
                var joinMessage = new ChatMessage("System", username + " sohbete katıldı.", ChatMessageType.System);
                await messageHandler.BroadcastMessageAsync(joinMessage, clientInfo);
                
                while (isRunning && client.Connected)
                {
                    bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;
                        
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    if (string.IsNullOrWhiteSpace(message))
                        continue;
                    
                    // Komut kontrolü
                    if (message.StartsWith("/"))
                    {
                        await HandleCommandAsync(clientInfo, message);
                    }
                    else
                    {
                        LogMessage(username + ": " + message);
                        var chatMessage = new ChatMessage(username, message, ChatMessageType.Public);
                        await messageHandler.BroadcastMessageAsync(chatMessage);
                    }
                }
            }
            catch
            {
                // Client disconnected
            }
            finally
            {
                if (clientInfo != null)
                {
                    clientManager.RemoveClient(clientInfo.Id);
                    LogMessage(clientInfo.Username + " ayrıldı.");
                    UpdateClientList();
                    
                    var leaveMessage = new ChatMessage("System", clientInfo.Username + " ayrıldı.", ChatMessageType.System);
                    await messageHandler.BroadcastMessageAsync(leaveMessage);
                }
                
                client.Close();
            }
        }

        private void SendMessage(TcpClient client, string message)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(message + "\r\n");
                stream.Write(data, 0, data.Length);
            }
            catch
            {
                // Ignore
            }
        }

        private void LogMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(LogMessage), message);
                return;
            }
            
            rtbLog.AppendText(string.Format("[{0}] {1}\r\n", DateTime.Now.ToString("HH:mm:ss"), message));
            rtbLog.ScrollToCaret();
        }

        private void UpdateClientList()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateClientList));
                return;
            }
            
            lstClients.Items.Clear();
            var clients = clientManager.GetAllClients();
            foreach (var client in clients)
            {
                lstClients.Items.Add(client.GetDisplayName());
            }
            
            lblClientCount.Text = "Bağlı Client: " + clientManager.GetClientCount();
        }

        private async Task HandleCommandAsync(ClientInfo client, string command)
        {
            string[] parts = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                return;
            
            string cmd = parts[0].ToLower();
            
            switch (cmd)
            {
                case "/pm":
                case "/msg":
                case "/w":
                    // Format: /pm username mesaj
                    if (parts.Length < 3)
                    {
                        SendMessage(client.TcpClient, "[Sistem] Kullanım: /pm <kullanıcı_adı> <mesaj>");
                        return;
                    }
                    string targetUser = parts[1].TrimStart('@');
                    string privateMessage = string.Join(" ", parts.Skip(2));
                    
                    LogMessage(string.Format("[PM] {0} -> {1}: {2}", client.Username, targetUser, privateMessage));
                    await messageHandler.SendPrivateMessageAsync(client.Username, targetUser, privateMessage);
                    break;
                
                case "/status":
                    // Format: /status [online|away|busy]
                    if (parts.Length < 2)
                    {
                        SendMessage(client.TcpClient, string.Format("[Sistem] Mevcut durumunuz: {0}", client.Status));
                        SendMessage(client.TcpClient, "[Sistem] Kullanım: /status [online|away|busy]");
                        return;
                    }
                    
                    UserStatus newStatus;
                    switch (parts[1].ToLower())
                    {
                        case "online":
                            newStatus = UserStatus.Online;
                            break;
                        case "away":
                            newStatus = UserStatus.Away;
                            break;
                        case "busy":
                            newStatus = UserStatus.Busy;
                            break;
                        default:
                            SendMessage(client.TcpClient, "[Sistem] Geçersiz durum. Kullanılabilir: online, away, busy");
                            return;
                    }
                    
                    await messageHandler.BroadcastStatusChangeAsync(client.Username, newStatus);
                    LogMessage(string.Format("{0} durumunu değiştirdi: {1}", client.Username, newStatus));
                    break;
                
                case "/list":
                case "/users":
                    // Online kullanıcıları listele
                    var clients = clientManager.GetAllClients();
                    string userList = "\r\n=== Çevrimiçi Kullanıcılar ===\r\n";
                    foreach (var c in clients)
                    {
                        userList += string.Format("{0} {1}\r\n", c.GetStatusIcon(), c.Username);
                    }
                    userList += string.Format("\r\nToplam: {0} kullanıcı", clients.Count);
                    SendMessage(client.TcpClient, userList);
                    break;
                
                case "/help":
                    string helpText = "\r\n=== Komutlar ===\r\n";
                    helpText += "/pm <kullanıcı> <mesaj> - Özel mesaj gönder\r\n";
                    helpText += "/status [online|away|busy] - Durumunu değiştir\r\n";
                    helpText += "/list - Çevrimiçi kullanıcıları listele\r\n";
                    helpText += "/help - Bu yardım mesajını göster\r\n";
                    SendMessage(client.TcpClient, helpText);
                    break;
                
                default:
                    SendMessage(client.TcpClient, string.Format("[Sistem] Bilinmeyen komut: {0}. /help yazarak komutları görebilirsiniz.", cmd));
                    break;
            }
        }
        
        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRunning)
            {
                StopServer();
            }
        }
    }
}

