using Microsoft.SemanticKernel.ChatCompletion;

public class Pet
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string PetImageUrl { get; private set; }
    public ChatHistory History { get; private set; } = new();

    public Pet(string name, string description, string petImageUrl)
    {
        Name = name;
        Description = description;
        PetImageUrl = petImageUrl;
    }
}