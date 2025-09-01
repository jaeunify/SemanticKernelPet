using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using PetService.Entity;

public class PetChatService
{
    private readonly Kernel _kernel;
    private readonly IChatCompletionService _chat;
    private readonly PetStorageService _petStorageService;

    private PromptExecutionSettings _settings = new()
    {
        ExtensionData = new Dictionary<string, object>
        {
            ["temperature"] = 0.7,
            ["maxOutputTokens"] = 512
        }
    };

    public PetChatService(IConfiguration configuration, PetStorageService petStorageService, ItemStorageService itemStorageService)
    {
        _petStorageService = petStorageService;
        var apiKey = configuration["OpenAi:ApiKey"];
        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion(
            modelId: "gpt-4o-mini",  // 저렴한 모델
            apiKey: apiKey ?? throw new Exception("OpenAi:ApiKey is not configured in appsettings.json")
        );

        _kernel = builder.Build();
        _kernel.Plugins.AddFromObject(new PetPlugin(itemStorageService), "Pet");

        _settings = new PromptExecutionSettings
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Required()
        };

        // “툴을 반드시 써라”는 강한 유도(System/User 프롬프트)
        _chat = _kernel.GetRequiredService<IChatCompletionService>();
    }

    public async Task<ChatMessageContent> SendAsync(Pet pet, string userInput, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(userInput))
            throw new ArgumentException("userInput must not be empty.", nameof(userInput));

        pet.History.AddUserMessage(userInput);

        try
        {
            var reply = await _chat.GetChatMessageContentAsync(
            pet.History,
            executionSettings: _settings,
            kernel: _kernel,
            cancellationToken: ct);

            if (reply is not null)
            {
                // Check the history for the result of the tool call
                var ranAway = pet.History.Any(m =>
                    m.Role == AuthorRole.Tool &&
                    m.Content?.Contains("running away from home", StringComparison.OrdinalIgnoreCase) == true);

                if (ranAway)
                {
                    var errorCode = _petStorageService.DeletePet(pet.Name);
                    if (errorCode != ErrorCode.OK)
                    {
                        throw new Exception($"Failed to delete pet {pet.Name}. ErrorCode: {errorCode}");
                    }
                }

                pet.History.Add(reply);
            }

            return reply!;
        }
        catch
        {
            throw;
        }
    }
}

