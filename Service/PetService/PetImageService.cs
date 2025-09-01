using System.Text;
using System.Text.Json;

namespace PetService;

public class PetImageService
{
    private readonly IConfiguration _configuration;
    private readonly string? _apiKey;

    public PetImageService(IConfiguration configuration)
    {
        _configuration = configuration;
        _apiKey = _configuration["Gemini:ApiKey"];

        if (string.IsNullOrWhiteSpace(_apiKey))
        {
            throw new InvalidOperationException("Gemini:ApiKey is not configured in appsettings.json");
        }
    }

    public async Task<string> GenerateImageAsync(string description, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description must not be empty.", nameof(description));

        var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-image-preview:generateContent";

        using var http = new HttpClient();
        http.DefaultRequestHeaders.Add("x-goog-api-key", _apiKey);

        var prompt = $@"Generate a single PNG image that matches the following description. 
                Return only an image (no text). 
                Style: 2D pixel art.
                Subject: a cute pet.
                Description: {description}";


        var payload = new
        {
            contents = new[]
            {
                new
                {
                    parts = new object[]
                    {
                        new { text = prompt }  // 예: "수채화 느낌의 책상 위 고양이 그림"
                    }
                }
            }
        };

        var json = JsonSerializer.Serialize(payload);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var response = await http.PostAsync(url, content, ct);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(ct);
            throw new HttpRequestException($"Google AI API request failed with status code {response.StatusCode}: {errorContent}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync(ct);

        using var doc = JsonDocument.Parse(jsonResponse);

        var candidates = doc.RootElement.GetProperty("candidates");
        if (candidates.GetArrayLength() == 0)
            throw new Exception("No candidates returned.");

        var parts = candidates[0].GetProperty("content").GetProperty("parts");

        string? b64 = null;
        foreach (var p in parts.EnumerateArray())
        {
            if (p.TryGetProperty("inlineData", out var inlineData))
            {
                // base64 PNG
                b64 = inlineData.GetProperty("data").GetString();
                break;
            }
        }

        if (string.IsNullOrEmpty(b64))
            throw new Exception("No image data found in response.");

        return $"data:image/png;base64,{b64}";
    }
}