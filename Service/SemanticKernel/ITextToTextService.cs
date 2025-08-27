using Microsoft.SemanticKernel;

namespace SemanticKernelService;

public interface ITextToTextService
{
    Task<ChatMessageContent> SendAsync(string userInput, CancellationToken ct = default);
    void Reset();
    IReadOnlyList<ChatMessageContent> GetHistory();
    void SetSystemPrompt(string? systemPrompt);
    void UpdateSettings(double? temperature = null, int? maxTokens = null);
    void AddPlugin<T>(string pluginName) where T : class, new();
}
