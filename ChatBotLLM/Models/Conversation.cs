using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatBotLLM.Models
{
    public class Conversation
    {
        private List<Message> messages;
        
        public string Id { get; private set; }
        public User User { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public IReadOnlyList<Message> Messages { get { return messages.AsReadOnly(); } }
        
        public Conversation(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
                
            Id = Guid.NewGuid().ToString();
            User = user;
            StartTime = DateTime.Now;
            LastUpdated = DateTime.Now;
            messages = new List<Message>();
        }
        
        public void AddMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("message");
                
            messages.Add(message);
            LastUpdated = DateTime.Now;
        }
        
        public void ClearMessages()
        {
            messages.Clear();
            LastUpdated = DateTime.Now;
        }
        
        public List<Message> GetRecentMessages(int count)
        {
            return messages.Skip(Math.Max(0, messages.Count - count)).ToList();
        }
    }
}

