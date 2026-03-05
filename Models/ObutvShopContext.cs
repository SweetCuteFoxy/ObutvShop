using Microsoft.EntityFrameworkCore;

namespace ObutvShop.Models;

public class ObutvShopContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PickupPoint> PickupPoints { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=obuv_shop;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // roles
        modelBuilder.Entity<Role>(e =>
        {
            e.ToTable("roles");
            e.Property(r => r.Id).HasColumnName("id");
            e.Property(r => r.Code).HasColumnName("code").HasMaxLength(20);
            e.Property(r => r.Name).HasColumnName("name").HasMaxLength(50);
        });

        // users
        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("users");
            e.Property(u => u.Id).HasColumnName("id");
            e.Property(u => u.Login).HasColumnName("login").HasMaxLength(100);
            e.Property(u => u.Password).HasColumnName("password").HasMaxLength(50);
            e.Property(u => u.FullName).HasColumnName("full_name").HasMaxLength(150);
            e.Property(u => u.RoleId).HasColumnName("role_id");
            e.Property(u => u.IsActive).HasColumnName("is_active");
            e.Property(u => u.CreatedAt).HasColumnName("created_at");
            e.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
        });

        // categories
        modelBuilder.Entity<Category>(e =>
        {
            e.ToTable("categories");
            e.Property(c => c.Id).HasColumnName("id");
            e.Property(c => c.Name).HasColumnName("name").HasMaxLength(80);
        });

        // suppliers
        modelBuilder.Entity<Supplier>(e =>
        {
            e.ToTable("suppliers");
            e.Property(s => s.Id).HasColumnName("id");
            e.Property(s => s.Name).HasColumnName("name").HasMaxLength(100);
        });

        // manufacturers
        modelBuilder.Entity<Manufacturer>(e =>
        {
            e.ToTable("manufacturers");
            e.Property(m => m.Id).HasColumnName("id");
            e.Property(m => m.Name).HasColumnName("name").HasMaxLength(100);
        });

        // products
        modelBuilder.Entity<Product>(e =>
        {
            e.ToTable("products");
            e.Property(p => p.Id).HasColumnName("id");
            e.Property(p => p.Article).HasColumnName("article").HasMaxLength(30);
            e.Property(p => p.Name).HasColumnName("name").HasMaxLength(150);
            e.Property(p => p.Unit).HasColumnName("unit").HasMaxLength(20);
            e.Property(p => p.Price).HasColumnName("price").HasColumnType("numeric(10,2)");
            e.Property(p => p.SupplierId).HasColumnName("supplier_id");
            e.Property(p => p.ManufacturerId).HasColumnName("manufacturer_id");
            e.Property(p => p.CategoryId).HasColumnName("category_id");
            e.Property(p => p.DiscountPct).HasColumnName("discount_pct").HasColumnType("numeric(5,2)");
            e.Property(p => p.StockQty).HasColumnName("stock_qty");
            e.Property(p => p.Description).HasColumnName("description");
            e.Property(p => p.Photo).HasColumnName("photo").HasMaxLength(100);
            e.Property(p => p.IsActive).HasColumnName("is_active");
            e.Property(p => p.CreatedAt).HasColumnName("created_at");
            e.Property(p => p.UpdatedAt).HasColumnName("updated_at");
            e.Ignore(p => p.PriceDiscounted);
            e.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
            e.HasOne(p => p.Supplier).WithMany(s => s.Products).HasForeignKey(p => p.SupplierId);
            e.HasOne(p => p.Manufacturer).WithMany(m => m.Products).HasForeignKey(p => p.ManufacturerId);
        });

        // pickup_points
        modelBuilder.Entity<PickupPoint>(e =>
        {
            e.ToTable("pickup_points");
            e.Property(pp => pp.Id).HasColumnName("id");
            e.Property(pp => pp.Address).HasColumnName("address").HasMaxLength(200);
        });

        // order_statuses
        modelBuilder.Entity<OrderStatus>(e =>
        {
            e.ToTable("order_statuses");
            e.Property(os => os.Id).HasColumnName("id");
            e.Property(os => os.Code).HasColumnName("code").HasMaxLength(20);
            e.Property(os => os.Name).HasColumnName("name").HasMaxLength(50);
        });

        // orders
        modelBuilder.Entity<Order>(e =>
        {
            e.ToTable("orders");
            e.Property(o => o.Id).HasColumnName("id");
            e.Property(o => o.OrderNum).HasColumnName("order_num");
            e.Property(o => o.OrderDate).HasColumnName("order_date");
            e.Property(o => o.DeliveryDate).HasColumnName("delivery_date");
            e.Property(o => o.PickupPointId).HasColumnName("pickup_point_id");
            e.Property(o => o.UserId).HasColumnName("user_id");
            e.Property(o => o.PickupCode).HasColumnName("pickup_code");
            e.Property(o => o.StatusId).HasColumnName("status_id");
            e.Property(o => o.CreatedAt).HasColumnName("created_at");
            e.HasIndex(o => o.OrderNum).IsUnique();
            e.HasOne(o => o.PickupPoint).WithMany(pp => pp.Orders).HasForeignKey(o => o.PickupPointId);
            e.HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId);
            e.HasOne(o => o.Status).WithMany(os => os.Orders).HasForeignKey(o => o.StatusId);
        });

        // order_items
        modelBuilder.Entity<OrderItem>(e =>
        {
            e.ToTable("order_items");
            e.Property(oi => oi.Id).HasColumnName("id");
            e.Property(oi => oi.OrderNum).HasColumnName("order_num");
            e.Property(oi => oi.ProductId).HasColumnName("product_id");
            e.Property(oi => oi.Quantity).HasColumnName("quantity");
            e.HasOne(oi => oi.Order).WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderNum)
                .HasPrincipalKey(o => o.OrderNum);
            e.HasOne(oi => oi.Product).WithMany(p => p.OrderItems).HasForeignKey(oi => oi.ProductId);
        });
    }
}
