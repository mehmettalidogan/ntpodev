using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatSocketApp.Services
{
    public static class SocketHelper
    {
        public static async Task<string> ReadStringAsync(NetworkStream stream, int bufferSize = 4096)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
                
            byte[] buffer = new byte[bufferSize];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            
            if (bytesRead == 0)
                return null;
                
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }
        
        public static async Task WriteStringAsync(NetworkStream stream, string message)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (string.IsNullOrEmpty(message))
                return;
                
            byte[] data = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length);
        }
        
        public static bool IsConnected(TcpClient client)
        {
            if (client == null)
                return false;
                
            try
            {
                if (!client.Connected)
                    return false;
                    
                Socket socket = client.Client;
                bool part1 = socket.Poll(1000, SelectMode.SelectRead);
                bool part2 = (socket.Available == 0);
                
                return !(part1 && part2);
            }
            catch
            {
                return false;
            }
        }
    }
}

