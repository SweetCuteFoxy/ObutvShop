namespace ObutvShop.Models;

public class Product
{
    public int Id { get; set; }
    public string Article { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Unit { get; set; } = "шт.";
    public decimal Price { get; set; }
    public int SupplierId { get; set; }
    public int ManufacturerId { get; set; }
    public int CategoryId { get; set; }
    public decimal DiscountPct { get; set; }
    public int StockQty { get; set; }
    public string? Description { get; set; }
    public string? Photo { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Category Category { get; set; } = null!;
    public Supplier Supplier { get; set; } = null!;
    public Manufacturer Manufacturer { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public decimal PriceDiscounted => Math.Round(Price * (1 - DiscountPct / 100m), 2);
}
