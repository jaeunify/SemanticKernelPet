using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SemanticKernelService
{
    public class GeminiSemanticKernelService : IGeminiSemanticKernelService
    {
        private readonly Kernel _kernel;
        private readonly IChatCompletionService _chat;
        private readonly ChatHistory _history = new();

        // 실행 설정 (커넥터 공통 설정 객체)
        private PromptExecutionSettings _settings = new()
        {
            ExtensionData = new Dictionary<string, object>
            {
                ["temperature"] = 0.7,
                ["maxOutputTokens"] = 512
            }
        };

        private string? _systemPrompt;

        public GeminiSemanticKernelService(Kernel kernel)
        {
            _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
            _chat = _kernel.GetRequiredService<IChatCompletionService>();
        }

        public void SetSystemPrompt(string? systemPrompt)
        {
            _systemPrompt = string.IsNullOrWhiteSpace(systemPrompt) ? null : systemPrompt!.Trim();
            // 기존 System 메시지는 하나만 유지하도록 정리 후 재설정
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
                // Vertex API에서는 "maxOutputTokens" 또는 "max_output_tokens"를 씀.
                // 지금 커넥터(GoogleAIGeminiChatCompletion)는 "maxOutputTokens" 키를 기대합니다.
                _settings.ExtensionData["maxOutputTokens"] = maxTokens.Value;
            }
        }
        
        public void AddPlugin<T>(string pluginName) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(pluginName))
                throw new ArgumentException("pluginName must not be empty.", nameof(pluginName));

            // 같은 이름으로 중복 등록되는 것을 방지하려면 필요 시 제거/체크 로직 추가 가능
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
                // SK 1.63 기준: generic PromptExecutionSettings로도 동작
                var reply = await _chat.GetChatMessageContentAsync(
                _history,
                executionSettings: _settings,
                kernel: _kernel,
                cancellationToken: ct);

                // 히스토리에 어시스턴트 응답 추가
                // reply는 ChatMessageContent 형태 (텍스트/툴/이미지 등 포함 가능)
                if (reply is not null)
                {
                    // 텍스트만 쓰고 싶다면 AddAssistantMessage(reply.Content)도 가능
                    _history.Add(reply);
                }

                return reply!;
            }
            catch
            {
                // 필요 시 로깅 추가
                throw; // 상위에서 UI 알림 처리
            }
        }

        public IReadOnlyList<ChatMessageContent> GetHistory() => _history;

        public void Reset()
        {
            _history.Clear();
            // 시스템 프롬프트가 있다면 초기화 후 다시 세팅
            EnsureSystemPrompt();
        }

        // --- 내부 유틸 ---

        private void EnsureSystemPrompt()
        {
            if (string.IsNullOrEmpty(_systemPrompt)) return;

            // 이미 System 메시지가 없으면 하나 추가
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
            // ChatHistory는 List<ChatMessageContent>라서 뒤에서 앞으로 제거
            for (int i = _history.Count - 1; i >= 0; i--)
            {
                if (_history[i].Role == AuthorRole.System)
                    _history.RemoveAt(i);
            }
        }
    }
}
