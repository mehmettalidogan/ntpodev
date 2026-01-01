using System.Threading.Tasks;
using ChatSocketApp.Models;

namespace ChatSocketApp.Services
{
    public interface IMessageHandler
    {
        Task BroadcastMessageAsync(ChatMessage message, ClientInfo excludeClient = null);
        Task SendPrivateMessageAsync(ChatMessage message, ClientInfo targetClient);
        Task SendPrivateMessageAsync(string fromUsername, string toUsername, string content);
        Task SendToClientAsync(string message, ClientInfo client);
        Task BroadcastStatusChangeAsync(string username, UserStatus newStatus);
    }
}

