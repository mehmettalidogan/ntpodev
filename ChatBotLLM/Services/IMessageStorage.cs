using ChatBotLLM.Models;

namespace ChatBotLLM.Services
{
    public interface IMessageStorage
    {
        void SaveConversation(Conversation conversation);
        Conversation LoadConversation(string conversationId);
        void DeleteConversation(string conversationId);
        bool ConversationExists(string conversationId);
    }
}

