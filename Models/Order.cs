namespace ObutvShop.Models;

public class Order
{
    public int Id { get; set; }
    public DateOnly OrderDate { get; set; }
    public DateOnly DeliveryDate { get; set; }
    public int DeliveryPointId { get; set; }
    public int UserId { get; set; }
    public int Code { get; set; }
    public int StatusId { get; set; }

    public DeliveryPoint DeliveryPoint { get; set; } = null!;
    public User User { get; set; } = null!;
    public OrderStatus Status { get; set; } = null!;
    public ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
}
