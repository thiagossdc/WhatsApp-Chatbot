namespace WhatsApp_Chatbot.Error
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Erro no chatbot");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { error = "Ocorreu um erro inesperado." });
            }
        }
    }
}