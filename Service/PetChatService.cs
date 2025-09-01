using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using PetService.Plugins;


public class PetChatService
{
    private readonly Kernel _kernel;
    private readonly IChatCompletionService _chat;
    private readonly ChatHistory _history = new();

    private PromptExecutionSettings _settings = new()
    {
        ExtensionData = new Dictionary<string, object>
        {
            ["temperature"] = 0.7,
            ["maxOutputTokens"] = 512
        }
    };

    public PetChatService(IConfiguration configuration)
    {
        var apiKey = configuration["OpenAi:ApiKey"];
        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion(
            modelId: "gpt-4o-mini",  // 저렴한 모델
            apiKey: apiKey ?? throw new Exception("OpenAi:ApiKey is not configured in appsettings.json")
        );

        _kernel = builder.Build();
        _kernel.Plugins.AddFromType<PetPlugin>("Pet");

        _settings = new PromptExecutionSettings
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Required()
        };

        // “툴을 반드시 써라”는 강한 유도(System/User 프롬프트)
        _chat = _kernel.GetRequiredService<IChatCompletionService>();

        _history = new ChatHistory();
        _history.AddSystemMessage("You are a pet simulator. Act like a pet. answer in Korean.");
    }

    public async Task<ChatMessageContent> SendAsync(string userInput, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(userInput))
            throw new ArgumentException("userInput must not be empty.", nameof(userInput));

        _history.AddUserMessage(userInput);

        try
        {
            var reply = await _chat.GetChatMessageContentAsync(
            _history,
            executionSettings: _settings,
            kernel: _kernel,
            cancellationToken: ct);

            if (reply is not null)
            {
                _history.Add(reply);
            }

            return reply!;
        }
        catch
        {
            throw;
        }
    }

    public IReadOnlyList<ChatMessageContent> GetHistory() => _history;
}

