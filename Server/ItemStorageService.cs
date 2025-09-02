public class ItemStorageService
{
    public int ItemCount = 1;
    private readonly List<Item> _items = new();

    public Action? OnChange;

    public void AddItem(string name, string description, string imageUrl)
    {
        _items.Add(new Item(ItemCount++, name, description, imageUrl));
        OnChange?.Invoke();
    }

    public List<Item> GetItems()
    {
        return _items;
    }
}
