using System;
using System.Collections.Generic;
using System.Linq;
using ChatSocketApp.Models;

namespace ChatSocketApp.Services
{
    public class ClientManager : IClientManager
    {
        private Dictionary<string, ClientInfo> clients;
        private object lockObject;
        
        public ClientManager()
        {
            clients = new Dictionary<string, ClientInfo>();
            lockObject = new object();
        }
        
        public void AddClient(ClientInfo client)
        {
            if (client == null)
                throw new ArgumentNullException("client");
                
            lock (lockObject)
            {
                if (clients.ContainsKey(client.Id))
                    throw new InvalidOperationException("Client zaten ekli.");
                clients[client.Id] = client;
            }
        }
        
        public void RemoveClient(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                return;
                
            lock (lockObject)
            {
                if (clients.ContainsKey(clientId))
                    clients.Remove(clientId);
            }
        }
        
        public ClientInfo GetClient(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                return null;
                
            lock (lockObject)
            {
                return clients.ContainsKey(clientId) ? clients[clientId] : null;
            }
        }
        
        public ClientInfo GetClientByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;
                
            lock (lockObject)
            {
                return clients.Values.FirstOrDefault(c => c.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            }
        }
        
        public List<ClientInfo> GetAllClients()
        {
            lock (lockObject)
            {
                return new List<ClientInfo>(clients.Values);
            }
        }
        
        public int GetClientCount()
        {
            lock (lockObject)
            {
                return clients.Count;
            }
        }
        
        public string GetUserListString()
        {
            lock (lockObject)
            {
                if (clients.Count == 0)
                    return "Kimse yok";
                    
                var usernames = clients.Values.Select(c => c.Username);
                return string.Join(", ", usernames.ToArray());
            }
        }
    }
}

