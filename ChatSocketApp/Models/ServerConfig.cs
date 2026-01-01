using System;

namespace ChatSocketApp.Models
{
    public class ServerConfig
    {
        public string Host { get; private set; }
        public int Port { get; private set; }
        public int MaxClients { get; private set; }
        public int BufferSize { get; set; }
        
        public ServerConfig(string host, int port, int maxClients = 10)
        {
            if (string.IsNullOrWhiteSpace(host))
                throw new ArgumentException("Host boş olamaz.");
            if (port < 1024 || port > 65535)
                throw new ArgumentException("Port 1024-65535 arasında olmalıdır.");
            if (maxClients < 1)
                throw new ArgumentException("Maksimum client sayısı pozitif olmalıdır.");
                
            Host = host;
            Port = port;
            MaxClients = maxClients;
            BufferSize = 4096;
        }
    }
}

