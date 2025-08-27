using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SemanticKernelService
{
    public class TextToTextService : ITextToTextService
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

        private string? _systemPrompt;

        public TextToTextService(Kernel kernel)
        {
            _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
            _chat = _kernel.GetRequiredService<IChatCompletionService>();
        }

        public void SetSystemPrompt(string? systemPrompt)
        {
            _systemPrompt = string.IsNullOrWhiteSpace(systemPrompt) ? null : systemPrompt!.Trim();
            RemoveExistingSystemMessages();
            EnsureSystemPrompt();
        }

        public void UpdateSettings(double? temperature = null, int? maxTokens = null)
        {
            if (_settings.ExtensionData == null)
            {
                _settings.ExtensionData = new Dictionary<string, object>();
            }

            if (temperature.HasValue)
            {
                _settings.ExtensionData["temperature"] = temperature.Value;
            }

            if (maxTokens.HasValue)
            {
                _settings.ExtensionData["maxOutputTokens"] = maxTokens.Value;
            }
        }
        
        public void AddPlugin<T>(string pluginName) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(pluginName))
                throw new ArgumentException("pluginName must not be empty.", nameof(pluginName));
            _kernel.Plugins.AddFromType<T>(pluginName);
        }

        public async Task<ChatMessageContent> SendAsync(string userInput, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                throw new ArgumentException("userInput must not be empty.", nameof(userInput));

            EnsureSystemPrompt();
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

        public void Reset()
        {
            _history.Clear();
            EnsureSystemPrompt();
        }

        private void EnsureSystemPrompt()
        {
            if (string.IsNullOrEmpty(_systemPrompt)) return;

            bool hasSystem = false;
            foreach (var m in _history)
            {
                if (m.Role == AuthorRole.System)
                {
                    hasSystem = true;
                    break;
                }
            }

            if (!hasSystem)
            {
                _history.AddSystemMessage(_systemPrompt);
            }
        }

        private void RemoveExistingSystemMessages()
        {
            for (int i = _history.Count - 1; i >= 0; i--)
            {
                if (_history[i].Role == AuthorRole.System)
                    _history.RemoveAt(i);
            }
        }
    }
}
