public class Item
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ImageUrl { get; private set; }

    public Item(int id, string name, string description, string imageUrl)
    {
        Id = id;
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
    }
}