using System.Collections.Concurrent;

public interface IChatbotAppService
{
    Task<string> GetResponseAsync(string userId, string userMessage);
}

public class ChatbotAppService : IChatbotAppService
{
    private static readonly ConcurrentDictionary<string, List<string>> UserChatHistory = new();
    private static readonly Dictionary<string, string> PredefinedResponses = new()
    {
        { "oi", "Olá! Como posso ajudar você hoje?" },
        { "horário", "Nosso horário de atendimento é das 9h às 18h, de segunda a sexta." },
        { "preço", "Podemos fornecer preços específicos. Qual produto ou serviço você deseja saber mais?" }
    };

    private readonly IHttpClientFactory _httpClientFactory;

    public ChatbotAppService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetResponseAsync(string userId, string userMessage)
    {
        userMessage = userMessage.ToLower();

        if (!UserChatHistory.ContainsKey(userId))
            UserChatHistory[userId] = new List<string>();

        UserChatHistory[userId].Add(userMessage);

        if (PredefinedResponses.TryGetValue(userMessage, out var predefinedResponse))
        {
            return predefinedResponse;
        }

        return await GetAIResponseAsync(userMessage);
    }

    private async Task<string> GetAIResponseAsync(string userMessage)
    {
        await Task.Delay(500);
        return $"Não encontrei uma resposta exata para '{userMessage}', mas estou aprendendo!";
    }
}
