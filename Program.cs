
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add HttpClientFactory for direct API calls
builder.Services.AddHttpClient();

// Register my wrapper services
builder.Services.AddScoped<PetChatService>();
builder.Services.AddTransient<PetImageService>();
builder.Services.AddScoped<PetStorageService>();
builder.Services.AddRadzenComponents();

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
app.MapRazorComponents<SemanticKernelPet.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
