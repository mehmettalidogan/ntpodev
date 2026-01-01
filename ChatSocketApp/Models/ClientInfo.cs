using System;
using System.Net.Sockets;

namespace ChatSocketApp.Models
{
    public class ClientInfo
    {
        public string Id { get; private set; }
        public string Username { get; private set; }
        public TcpClient TcpClient { get; private set; }
        public DateTime ConnectedAt { get; private set; }
        public DateTime LastActivity { get; private set; }
        public UserStatus Status { get; set; }
        public bool IsTyping { get; set; }
        
        public ClientInfo(string username, TcpClient tcpClient)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Kullanıcı adı boş olamaz.");
            if (tcpClient == null)
                throw new ArgumentNullException("tcpClient");
                
            Id = Guid.NewGuid().ToString();
            Username = username;
            TcpClient = tcpClient;
            ConnectedAt = DateTime.Now;
            LastActivity = DateTime.Now;
            Status = UserStatus.Online;
            IsTyping = false;
        }
        
        public void UpdateActivity()
        {
            LastActivity = DateTime.Now;
        }
        
        public bool IsConnected()
        {
            try
            {
                return TcpClient.Connected;
            }
            catch
            {
                return false;
            }
        }
        
        public string GetStatusIcon()
        {
            switch (Status)
            {
                case UserStatus.Online:
                    return "🟢";
                case UserStatus.Away:
                    return "🟡";
                case UserStatus.Busy:
                    return "🔴";
                case UserStatus.Offline:
                    return "⚫";
                default:
                    return "";
            }
        }
        
        public string GetDisplayName()
        {
            string typingIndicator = IsTyping ? " ✍️" : "";
            return string.Format("{0} {1}{2}", GetStatusIcon(), Username, typingIndicator);
        }
    }
    
    public enum UserStatus
    {
        Online,
        Away,
        Busy,
        Offline
    }
}

