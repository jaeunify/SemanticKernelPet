namespace SemanticKernelService;

public interface ITextToImageService
{
    Task<string> GenerateImageAsync(string description, CancellationToken ct = default);
}
