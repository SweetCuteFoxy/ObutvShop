namespace ObutvShop.Models;

public class OrderStatus
{
    public short Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
