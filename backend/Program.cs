using WhatsApp_Chatbot.Error;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IChatbotAppService, ChatbotAppService>();
builder.Services.AddHttpClient(); 

var app = builder.Build();

app.UseRouting();
app.UseMiddleware<ExceptionHandlingMiddleware>();

IApplicationBuilder applicationBuilder = app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

builder.Services.AddControllers();
builder.Services.AddSingleton<IChatbotAppService, ChatbotAppService>();
builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

app.UseCors("AllowAngular");
app.UseRouting();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
