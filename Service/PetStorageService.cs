public class PetStorageService
{
    private readonly Dictionary<string, Pet> _petData = new(); // petName - Pet

    public event Action? OnChange;

    public ErrorCode AddPet(string petName, string description, string petImageUrl)
    {
        if (_petData.ContainsKey(petName))
        {
            return ErrorCode.DuplicatePetName;
        }

        var pet = new Pet(petName, description, petImageUrl);
        _petData[petName] = pet;

        OnChange?.Invoke();
        return ErrorCode.OK;
    }

    public Pet? GetPet(string petName)
    {
        return _petData.TryGetValue(petName, out var data) ? data : null;
    }

    public bool IsAnyPet()
    {
        return _petData.Count > 0;
    }

    public List<string> GetAllPetNames()
    {
        return _petData.Keys.ToList(); // 여기서 아무것도 없는 걸로 나온다 ..
    }
}
