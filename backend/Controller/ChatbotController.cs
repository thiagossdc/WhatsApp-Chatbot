using Microsoft.AspNetCore.Mvc;

namespace WhatsApp_Chatbot.Controller
{
    [ApiController]
    [Route("api/chatbot")]
    public class ChatbotController : ControllerBase
    {
        private readonly IChatbotAppService _chatbotAppService;
        private readonly ILogger<ChatbotController> _logger;

        public ChatbotController(IChatbotAppService chatService, ILogger<ChatbotController> logger)
        {
            _chatbotAppService = chatService;
            _logger = logger;
        }
        public class ChatMessage
        {
            public string UserId { get; set; } // usuários diferentes
            public string Text { get; set; }
        }

        [HttpPost("message")]
        public async Task<IActionResult> ReceiveMessage([FromBody] ChatMessage message)
        {
            if (string.IsNullOrWhiteSpace(message?.Text) || string.IsNullOrWhiteSpace(message?.UserId))
            {
                return BadRequest(new { error = "Mensagem inválida." });
            }

            _logger.LogInformation($"Mensagem recebida de {message.UserId}: {message.Text}");

            string response = await _chatbotAppService.GetResponseAsync(message.UserId, message.Text);
            return Ok(new { reply = response });
        }
    }
}