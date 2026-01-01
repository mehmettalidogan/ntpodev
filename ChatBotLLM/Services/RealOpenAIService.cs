using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChatBotLLM.Models;

namespace ChatBotLLM.Services
{
    /// <summary>
    /// Gerçek OpenAI API entegrasyonu
    /// </summary>
    public class RealOpenAIService : ILLMService
    {
        private readonly string apiKey;
        private readonly HttpClient httpClient;
        private const string API_URL = "https://api.openai.com/v1/chat/completions";
        
        public string ModelName { get; private set; }
        public int MaxTokens { get; set; }
        public string SystemPrompt { get; set; }
        public double Temperature { get; set; }
        
        public RealOpenAIService(string apiKey, string modelName = "gpt-3.5-turbo")
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("API Key boş olamaz.");
            
            this.apiKey = apiKey;
            ModelName = modelName;
            MaxTokens = 1000;
            Temperature = 0.7;
            SystemPrompt = "Sen yardımsever bir AI asistanısın. Türkçe konuşuyorsun ve programlama, teknoloji konularında uzmansın.";
            
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
        }
        
        public async Task<string> GenerateResponseAsync(string prompt, List<Message> context = null)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                throw new ArgumentException("Prompt boş olamaz.");
            
            try
            {
                // Mesaj listesi oluştur
                var messages = new List<object>();
                
                // Sistem promptu ekle
                messages.Add(new
                {
                    role = "system",
                    content = SystemPrompt
                });
                
                // Context'teki son 5 mesajı ekle
                if (context != null && context.Count > 0)
                {
                    var recentMessages = context.Skip(Math.Max(0, context.Count - 5)).ToList();
                    foreach (var msg in recentMessages)
                    {
                        messages.Add(new
                        {
                            role = msg.IsUserMessage ? "user" : "assistant",
                            content = msg.Content
                        });
                    }
                }
                
                // Mevcut kullanıcı mesajı
                messages.Add(new
                {
                    role = "user",
                    content = prompt
                });
                
                // API request body
                var requestBody = new
                {
                    model = ModelName,
                    messages = messages,
                    max_tokens = MaxTokens,
                    temperature = Temperature
                };
                
                string jsonBody = SimpleJsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                
                var response = await httpClient.PostAsync(API_URL, content);
                string responseBody = await response.Content.ReadAsStringAsync();
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("API Hatası: " + response.StatusCode + " - " + responseBody);
                }
                
                // Response'u parse et
                string assistantMessage = SimpleJsonSerializer.ExtractAssistantMessage(responseBody);
                return assistantMessage;
            }
            catch (Exception ex)
            {
                throw new Exception("OpenAI API isteği başarısız: " + ex.Message);
            }
        }
    }
    
    /// <summary>
    /// Basit JSON serializer (Newtonsoft.Json olmadan)
    /// </summary>
    internal static class SimpleJsonSerializer
    {
        public static string Serialize(object obj)
        {
            var sb = new StringBuilder();
            SerializeObject(obj, sb);
            return sb.ToString();
        }
        
        private static void SerializeObject(object obj, StringBuilder sb)
        {
            if (obj == null)
            {
                sb.Append("null");
                return;
            }
            
            var type = obj.GetType();
            
            if (obj is string str)
            {
                sb.Append('"').Append(EscapeString(str)).Append('"');
            }
            else if (obj is int || obj is long || obj is double || obj is float)
            {
                sb.Append(obj.ToString().Replace(',', '.'));
            }
            else if (obj is bool b)
            {
                sb.Append(b ? "true" : "false");
            }
            else if (obj is System.Collections.IEnumerable enumerable && !(obj is string))
            {
                sb.Append('[');
                bool first = true;
                foreach (var item in enumerable)
                {
                    if (!first) sb.Append(',');
                    SerializeObject(item, sb);
                    first = false;
                }
                sb.Append(']');
            }
            else
            {
                sb.Append('{');
                bool first = true;
                foreach (var prop in type.GetProperties())
                {
                    if (!first) sb.Append(',');
                    sb.Append('"').Append(prop.Name).Append("\":");
                    SerializeObject(prop.GetValue(obj), sb);
                    first = false;
                }
                sb.Append('}');
            }
        }
        
        private static string EscapeString(string str)
        {
            return str.Replace("\\", "\\\\")
                      .Replace("\"", "\\\"")
                      .Replace("\n", "\\n")
                      .Replace("\r", "\\r")
                      .Replace("\t", "\\t");
        }
        
        public static string ExtractAssistantMessage(string json)
        {
            // Basit JSON parsing - "content": "..." değerini bul
            int contentIndex = json.IndexOf("\"content\"");
            if (contentIndex == -1) return "Cevap alınamadı.";
            
            int startQuote = json.IndexOf('"', contentIndex + 10);
            if (startQuote == -1) return "Cevap alınamadı.";
            
            int endQuote = startQuote + 1;
            while (endQuote < json.Length)
            {
                if (json[endQuote] == '"' && json[endQuote - 1] != '\\')
                    break;
                endQuote++;
            }
            
            if (endQuote >= json.Length) return "Cevap alınamadı.";
            
            string content = json.Substring(startQuote + 1, endQuote - startQuote - 1);
            return UnescapeString(content);
        }
        
        private static string UnescapeString(string str)
        {
            return str.Replace("\\n", "\n")
                      .Replace("\\r", "\r")
                      .Replace("\\t", "\t")
                      .Replace("\\\"", "\"")
                      .Replace("\\\\", "\\");
        }
    }
}


