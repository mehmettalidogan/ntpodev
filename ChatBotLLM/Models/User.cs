using System;

namespace ChatBotLLM.Models
{
    public class User
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; private set; }
        
        public User(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("User ID boş olamaz.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Kullanıcı adı boş olamaz.");
                
            Id = id;
            Name = name;
            CreatedAt = DateTime.Now;
        }
    }
}

