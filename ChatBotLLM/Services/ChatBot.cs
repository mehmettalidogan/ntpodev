using System;
using System.Threading.Tasks;
using ChatBotLLM.Models;

namespace ChatBotLLM.Services
{
    public class ChatBot
    {
        private readonly ILLMService llmService;
        private readonly IMessageStorage storage;
        
        public string BotName { get; set; }
        
        public ChatBot(ILLMService llmService, IMessageStorage storage)
        {
            this.llmService = llmService ?? throw new ArgumentNullException("llmService");
            this.storage = storage ?? throw new ArgumentNullException("storage");
            BotName = "AssistantBot";
        }
        
        public string StartConversation(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
                
            Conversation conversation = new Conversation(user);
            storage.SaveConversation(conversation);
            return conversation.Id;
        }
        
        public async Task<string> SendMessageAsync(string conversationId, string message)
        {
            if (string.IsNullOrWhiteSpace(conversationId))
                throw new ArgumentException("Conversation ID boş olamaz.");
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Mesaj boş olamaz.");
                
            Conversation conversation = storage.LoadConversation(conversationId);
            if (conversation == null)
                throw new InvalidOperationException("Konuşma bulunamadı.");
                
            Message userMessage = new Message(message, true, conversation.User.Name);
            conversation.AddMessage(userMessage);
            
            var context = conversation.GetRecentMessages(5);
            string response = await llmService.GenerateResponseAsync(message, context);
            
            Message botMessage = new Message(response, false, BotName);
            conversation.AddMessage(botMessage);
            
            storage.SaveConversation(conversation);
            return response;
        }
        
        public Conversation GetConversation(string conversationId)
        {
            return storage.LoadConversation(conversationId);
        }
        
        public void ClearConversation(string conversationId)
        {
            var conversation = storage.LoadConversation(conversationId);
            if (conversation != null)
            {
                conversation.ClearMessages();
                storage.SaveConversation(conversation);
            }
        }
    }
}

