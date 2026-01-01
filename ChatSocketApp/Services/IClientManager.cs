using System.Collections.Generic;
using ChatSocketApp.Models;

namespace ChatSocketApp.Services
{
    public interface IClientManager
    {
        void AddClient(ClientInfo client);
        void RemoveClient(string clientId);
        ClientInfo GetClient(string clientId);
        ClientInfo GetClientByUsername(string username);
        List<ClientInfo> GetAllClients();
        int GetClientCount();
        string GetUserListString();
    }
}

