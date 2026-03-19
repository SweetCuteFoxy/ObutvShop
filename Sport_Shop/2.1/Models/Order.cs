namespace SportShopV1.Models;

public class Order
{
    public int OrderNum { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int? PickupPointId { get; set; }
    public int? UserId { get; set; }
    public int? PickupCode { get; set; }
    public int? StatusId { get; set; }

    public PickupPoint? PickupPoint { get; set; }
    public User? User { get; set; }
    public OrderStatus? Status { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
