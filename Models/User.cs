namespace ObutvShop.Models;

public class User
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

    public Role Role { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public string FullName => $"{LastName} {FirstName} {MiddleName}";
}
