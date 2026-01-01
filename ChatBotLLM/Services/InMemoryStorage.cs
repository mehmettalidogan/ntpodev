using System.Collections.Generic;
using ChatBotLLM.Models;

namespace ChatBotLLM.Services
{
    public class InMemoryStorage : IMessageStorage
    {
        private Dictionary<string, Conversation> conversations;
        
        public InMemoryStorage()
        {
            conversations = new Dictionary<string, Conversation>();
        }
        
        public void SaveConversation(Conversation conversation)
        {
            if (conversation != null)
                conversations[conversation.Id] = conversation;
        }
        
        public Conversation LoadConversation(string conversationId)
        {
            return conversations.ContainsKey(conversationId) ? conversations[conversationId] : null;
        }
        
        public void DeleteConversation(string conversationId)
        {
            if (conversations.ContainsKey(conversationId))
                conversations.Remove(conversationId);
        }
        
        public bool ConversationExists(string conversationId)
        {
            return conversations.ContainsKey(conversationId);
        }
    }
}

