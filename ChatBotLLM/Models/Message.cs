using System;

namespace ChatBotLLM.Models
{
    public class Message
    {
        public string Id { get; private set; }
        public string Content { get; private set; }
        public bool IsUserMessage { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string SenderName { get; private set; }
        
        public Message(string content, bool isUserMessage, string senderName = null)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Mesaj içeriği boş olamaz.");
                
            Id = Guid.NewGuid().ToString();
            Content = content;
            IsUserMessage = isUserMessage;
            SenderName = senderName ?? (isUserMessage ? "User" : "Bot");
            Timestamp = DateTime.Now;
        }
    }
}

