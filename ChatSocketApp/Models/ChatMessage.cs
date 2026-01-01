using System;

namespace ChatSocketApp.Models
{
    public class ChatMessage
    {
        public string Id { get; private set; }
        public string Sender { get; private set; }
        public string Content { get; private set; }
        public ChatMessageType Type { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Target { get; set; }  // Özel mesaj için hedef kullanıcı
        
        public ChatMessage(string sender, string content, ChatMessageType type, string target = null)
        {
            if (string.IsNullOrWhiteSpace(sender))
                throw new ArgumentException("Gönderici adı boş olamaz.");
            if (string.IsNullOrWhiteSpace(content) && type != ChatMessageType.Typing)
                throw new ArgumentException("Mesaj içeriği boş olamaz.");
                
            Id = Guid.NewGuid().ToString();
            Sender = sender;
            Content = content;
            Type = type;
            Timestamp = DateTime.Now;
            Target = target;
        }
        
        public string FormatMessage()
        {
            switch (Type)
            {
                case ChatMessageType.Public:
                    return string.Format("[{0}] {1}: {2}", Timestamp.ToString("HH:mm:ss"), Sender, Content);
                case ChatMessageType.Private:
                    return string.Format("[{0}] 💬 [Özel - {1}]: {2}", Timestamp.ToString("HH:mm:ss"), Sender, Content);
                case ChatMessageType.System:
                    return string.Format("[{0}] ℹ️ [Sistem] {1}", Timestamp.ToString("HH:mm:ss"), Content);
                case ChatMessageType.Typing:
                    return string.Format("✍️ {0} yazıyor...", Sender);
                case ChatMessageType.StatusChange:
                    return string.Format("[{0}] 🔔 {1}", Timestamp.ToString("HH:mm:ss"), Content);
                default:
                    return Content;
            }
        }
    }
    
    public enum ChatMessageType
    {
        Public,         // Herkese açık mesaj
        Private,        // Özel mesaj (1'e 1)
        System,         // Sistem mesajı
        Typing,         // Yazıyor göstergesi
        StatusChange    // Durum değişikliği (Online, Away, vb.)
    }
}

