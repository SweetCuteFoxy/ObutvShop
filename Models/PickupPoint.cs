namespace ObutvShop.Models;

public class DeliveryPoint
{
    public int Id { get; set; }
    public string Address { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
