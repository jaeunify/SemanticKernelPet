public class PetStorageService
{
    private readonly Dictionary<string, Pet> _petData = new(); // petName - Pet

    public Pet? GetPet(string petName)
    {
        return _petData.TryGetValue(petName, out var data) ? data : null;
    }

    public bool IsAnyPet()
    {
        return _petData.Count > 0;
    }

    public event Action<Pet>? OnPetAdded;
    public event Action<string>? OnPetRemoved;

    public ErrorCode AddPet(string petName, string description, string petImageUrl)
    {
        if (_petData.ContainsKey(petName))
        {
            return ErrorCode.DuplicatePetName;
        }

        var pet = new Pet(petName, description, petImageUrl);
        _petData[petName] = pet;

        OnPetAdded?.Invoke(pet);
        return ErrorCode.OK;
    }

    public List<string> GetAllPetNames()
    {
        return _petData.Keys.ToList();
    }

    public ErrorCode DeletePet(string petName)
    {
        if (_petData.Remove(petName))
        {
            OnPetRemoved?.Invoke(petName);
            return ErrorCode.OK;
        }
        return ErrorCode.PetNotFound;
    }
}
