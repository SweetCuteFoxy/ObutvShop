namespace SportShopV2.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string PasswordText { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public int RoleId { get; set; }

    public Role Role { get; set; } = null!;
}
