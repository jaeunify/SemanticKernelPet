using Microsoft.SemanticKernel;

namespace SemanticKernelService;

public interface IGeminiSemanticKernelService
{
    Task<ChatMessageContent> SendAsync(string userInput, CancellationToken ct = default);
    void Reset();
    IReadOnlyList<ChatMessageContent> GetHistory();

    void SetSystemPrompt(string? systemPrompt);
    void UpdateSettings(double? temperature = null, int? maxTokens = null);

    // 필요 시 플러그인 추가
    void AddPlugin<T>(string pluginName) where T : class, new();
}
