using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SemanticKernelService;

public class TextToImageService
{
    private readonly IConfiguration _configuration;
    private readonly string? _apiKey;

    public TextToImageService(IConfiguration configuration)
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

        // ==================================================================================================
        // 중요: 아래 API URL과 요청 본문(request body)은 일반적인 예시입니다.
        // 실제 Gemini 이미지 생성 API의 공식 문서를 확인하고, 그에 맞게 반드시 수정해야 합니다.
        // 모델 이름(예: "imagen-2") 또한 사용 가능한 모델 ID로 변경해야 합니다.
        // ==================================================================================================
        // 권장: v1beta + 헤더로 API 키 전달
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
                    // role 생략 가능 (서버가 기본 user로 처리)
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
            // 실제 앱에서는 더 자세한 오류 로깅이 필요합니다.
            throw new HttpRequestException($"Google AI API request failed with status code {response.StatusCode}: {errorContent}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync(ct);

        // ==================================================================================================
        // 중요: 아래 응답 데이터 구조 또한 실제 API의 JSON 응답 형식에 맞게 수정해야 합니다.
        // 여기서는 응답에 base64-encoded 이미지가 포함된 "images" 배열이 있다고 가정합니다.
        // ==================================================================================================
        using var doc = JsonDocument.Parse(jsonResponse);
        
        // 예시: root.images[0].b64_json
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
            throw new Exception("No image data found in response."); // 여기로 와! 이미지가 안왔어

        return $"data:image/png;base64,{b64}";
    }
}