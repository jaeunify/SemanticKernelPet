using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelPet.Components;
using SemanticKernelService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add HttpClientFactory for direct API calls
builder.Services.AddHttpClient();

// semantic kernel
var geminiApiKey = builder.Configuration["Gemini:ApiKey"] ?? throw new Exception("Gemini:ApiKey is not set in configuration");

var kernelBuilder = builder.Services.AddKernel()
    .AddGoogleAIGeminiChatCompletion(
        modelId: "gemini-1.5-pro-002",   // 또는 "gemini-1.5-flash-002"
        apiKey: geminiApiKey
    );

// Register my wrapper services
builder.Services.AddTransient<TextToTextService>();
builder.Services.AddTransient<TextToImageService>();

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
