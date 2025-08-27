using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernelPet.Components;
using SemanticKernelService;

#pragma warning disable SKEXP0010

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// semantic kernel
// Gemini for Chat
var geminiApiKey = builder.Configuration["Gemini:ApiKey"] ?? throw new Exception("Gemini:ApiKey is not set in configuration");

builder.Services.AddKernel()
    .AddGoogleAIGeminiChatCompletion(
        modelId: "gemini-1.5-pro-002",   // 또는 "gemini-1.5-flash-002"
        apiKey: geminiApiKey
    );

builder.Services.AddSingleton<ITextToTextService, TextToTextService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
