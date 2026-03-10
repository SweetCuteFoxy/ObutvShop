namespace ObutvShop.Models;

public class Product
{
    public int Id { get; set; }
    public string Article { get; set; } = null!;
    public int TypeId { get; set; }
    public int MeasureId { get; set; }
    public decimal Price { get; set; }
    public int SupplierId { get; set; }
    public int ManufacturerId { get; set; }
    public int CategoryId { get; set; }
    public int Discount { get; set; }
    public int StockQty { get; set; }
    public string Description { get; set; } = null!;
    public string? Photo { get; set; }

    public ProductType Type { get; set; } = null!;
    public Measure Measure { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public Supplier Supplier { get; set; } = null!;
    public Manufacturer Manufacturer { get; set; } = null!;
    public ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    public decimal PriceDiscounted => Math.Round(Price * (1 - Discount / 100m), 2);
}
