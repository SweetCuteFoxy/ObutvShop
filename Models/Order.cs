namespace ObutvShop.Models;

public class Order
{
    public int Id { get; set; }
    public int OrderNum { get; set; }
    public DateOnly? OrderDate { get; set; }
    public DateOnly? DeliveryDate { get; set; }
    public int? PickupPointId { get; set; }
    public int? UserId { get; set; }
    public int? PickupCode { get; set; }
    public short StatusId { get; set; }
    public DateTime CreatedAt { get; set; }

    public PickupPoint? PickupPoint { get; set; }
    public User? User { get; set; }
    public OrderStatus Status { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
