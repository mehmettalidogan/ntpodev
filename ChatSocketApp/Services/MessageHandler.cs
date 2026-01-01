using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatSocketApp.Models;

namespace ChatSocketApp.Services
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IClientManager clientManager;
        
        public MessageHandler(IClientManager clientManager)
        {
            this.clientManager = clientManager ?? throw new ArgumentNullException("clientManager");
        }
        
        public async Task BroadcastMessageAsync(ChatMessage message, ClientInfo excludeClient = null)
        {
            if (message == null)
                throw new ArgumentNullException("message");
                
            string formattedMessage = message.FormatMessage();
            var clients = clientManager.GetAllClients();
            
            foreach (var client in clients)
            {
                if (excludeClient != null && client.Id == excludeClient.Id)
                    continue;
                    
                await SendToClientAsync(formattedMessage, client);
            }
        }
        
        public async Task SendPrivateMessageAsync(ChatMessage message, ClientInfo targetClient)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            if (targetClient == null)
                throw new ArgumentNullException("targetClient");
                
            string formattedMessage = message.FormatMessage();
            await SendToClientAsync(formattedMessage, targetClient);
        }
        
        public async Task SendPrivateMessageAsync(string fromUsername, string toUsername, string content)
        {
            var targetClient = clientManager.GetAllClients().FirstOrDefault(c => c.Username.Equals(toUsername, StringComparison.OrdinalIgnoreCase));
            var senderClient = clientManager.GetAllClients().FirstOrDefault(c => c.Username.Equals(fromUsername, StringComparison.OrdinalIgnoreCase));
            
            if (targetClient == null)
            {
                if (senderClient != null)
                {
                    await SendToClientAsync(string.Format("[Sistem] Kullanıcı '{0}' bulunamadı.", toUsername), senderClient);
                }
                return;
            }
            
            // Alıcıya gönder
            var messageToTarget = new ChatMessage(fromUsername, content, ChatMessageType.Private, toUsername);
            await SendToClientAsync(messageToTarget.FormatMessage(), targetClient);
            
            // Gönderene de kopyasını gönder
            if (senderClient != null && senderClient.Id != targetClient.Id)
            {
                await SendToClientAsync(string.Format("[{0}] 💬 [Özel -> {1}]: {2}", 
                    DateTime.Now.ToString("HH:mm:ss"), toUsername, content), senderClient);
            }
        }
        
        public async Task BroadcastTypingIndicatorAsync(string username, bool isTyping)
        {
            var client = clientManager.GetAllClients().FirstOrDefault(c => c.Username == username);
            if (client != null)
            {
                client.IsTyping = isTyping;
            }
            
            if (isTyping)
            {
                var typingMessage = new ChatMessage(username, "", ChatMessageType.Typing);
                await BroadcastMessageAsync(typingMessage);
            }
        }
        
        public async Task BroadcastStatusChangeAsync(string username, UserStatus newStatus)
        {
            var client = clientManager.GetAllClients().FirstOrDefault(c => c.Username == username);
            if (client != null)
            {
                client.Status = newStatus;
                string statusText = GetStatusText(newStatus);
                var statusMessage = new ChatMessage("System", 
                    string.Format("{0} artık {1}", username, statusText), 
                    ChatMessageType.StatusChange);
                await BroadcastMessageAsync(statusMessage);
            }
        }
        
        public async Task SendToClientAsync(string message, ClientInfo client)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;
            if (client == null || !client.IsConnected())
                return;
                
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\r\n");
                var stream = client.TcpClient.GetStream();
                await stream.WriteAsync(data, 0, data.Length);
                client.UpdateActivity();
            }
            catch
            {
                // Client bağlantısı kopmuş
            }
        }
        
        private string GetStatusText(UserStatus status)
        {
            switch (status)
            {
                case UserStatus.Online:
                    return "çevrimiçi 🟢";
                case UserStatus.Away:
                    return "uzakta 🟡";
                case UserStatus.Busy:
                    return "meşgul 🔴";
                case UserStatus.Offline:
                    return "çevrimdışı ⚫";
                default:
                    return "bilinmeyen durum";
            }
        }
    }
}

