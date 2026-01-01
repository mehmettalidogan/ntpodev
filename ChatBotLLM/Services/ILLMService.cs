using System.Collections.Generic;
using System.Threading.Tasks;
using ChatBotLLM.Models;

namespace ChatBotLLM.Services
{
    public interface ILLMService
    {
        Task<string> GenerateResponseAsync(string prompt, List<Message> context = null);
        string ModelName { get; }
        int MaxTokens { get; set; }
    }
}

