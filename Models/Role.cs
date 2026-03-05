namespace ObutvShop.Models;

public class Role
{
    public short Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    public ICollection<User> Users { get; set; } = new List<User>();
}
