using Microsoft.EntityFrameworkCore;

namespace ObutvShop.Models;

public class ObutvShopContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Measure> Measures { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderStatus> Statuses { get; set; }
    public DbSet<DeliveryPoint> DeliveryPoints { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ProductOrder> ProductOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=shop_dp;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(e =>
        {
            e.ToTable("roles");
            e.Property(r => r.Id).HasColumnName("id");
            e.Property(r => r.Name).HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("users");
            e.Property(u => u.Id).HasColumnName("id");
            e.Property(u => u.RoleId).HasColumnName("id_role");
            e.Property(u => u.LastName).HasColumnName("last_name");
            e.Property(u => u.FirstName).HasColumnName("first_name");
            e.Property(u => u.MiddleName).HasColumnName("middle_name");
            e.Property(u => u.Login).HasColumnName("login");
            e.Property(u => u.Password).HasColumnName("pass");
            e.Ignore(u => u.FullName);
            e.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
        });

        modelBuilder.Entity<Category>(e =>
        {
            e.ToTable("categories");
            e.Property(c => c.Id).HasColumnName("id");
            e.Property(c => c.Name).HasColumnName("category_name");
        });

        modelBuilder.Entity<Supplier>(e =>
        {
            e.ToTable("suppliers");
            e.Property(s => s.Id).HasColumnName("id");
            e.Property(s => s.Name).HasColumnName("supplier_name");
        });

        modelBuilder.Entity<Manufacturer>(e =>
        {
            e.ToTable("manufacturers");
            e.Property(m => m.Id).HasColumnName("id");
            e.Property(m => m.Name).HasColumnName("manufacturer_name");
        });

        modelBuilder.Entity<Measure>(e =>
        {
            e.ToTable("measures");
            e.Property(m => m.Id).HasColumnName("id");
            e.Property(m => m.Name).HasColumnName("measure_name");
        });

        modelBuilder.Entity<ProductType>(e =>
        {
            e.ToTable("product_types");
            e.Property(pt => pt.Id).HasColumnName("id");
            e.Property(pt => pt.Name).HasColumnName("prod_type");
        });

        modelBuilder.Entity<Product>(e =>
        {
            e.ToTable("products");
            e.Property(p => p.Id).HasColumnName("id");
            e.Property(p => p.Article).HasColumnName("art");
            e.Property(p => p.TypeId).HasColumnName("id_type");
            e.Property(p => p.MeasureId).HasColumnName("id_measure");
            e.Property(p => p.Price).HasColumnName("price").HasColumnType("money");
            e.Property(p => p.SupplierId).HasColumnName("id_supplier");
            e.Property(p => p.ManufacturerId).HasColumnName("id_manufacturer");
            e.Property(p => p.CategoryId).HasColumnName("id_category");
            e.Property(p => p.Discount).HasColumnName("discount");
            e.Property(p => p.StockQty).HasColumnName("coint_in_stock");
            e.Property(p => p.Description).HasColumnName("description");
            e.Property(p => p.Photo).HasColumnName("photo_url");
            e.Ignore(p => p.PriceDiscounted);
            e.HasOne(p => p.Type).WithMany(pt => pt.Products).HasForeignKey(p => p.TypeId);
            e.HasOne(p => p.Measure).WithMany(m => m.Products).HasForeignKey(p => p.MeasureId);
            e.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
            e.HasOne(p => p.Supplier).WithMany(s => s.Products).HasForeignKey(p => p.SupplierId);
            e.HasOne(p => p.Manufacturer).WithMany(m => m.Products).HasForeignKey(p => p.ManufacturerId);
        });

        modelBuilder.Entity<OrderStatus>(e =>
        {
            e.ToTable("statuses");
            e.Property(s => s.Id).HasColumnName("id");
            e.Property(s => s.Name).HasColumnName("status_name");
        });

        modelBuilder.Entity<DeliveryPoint>(e =>
        {
            e.ToTable("delivery_points");
            e.Property(dp => dp.Id).HasColumnName("id");
            e.Property(dp => dp.Address).HasColumnName("delivery_address");
        });

        modelBuilder.Entity<Order>(e =>
        {
            e.ToTable("orders");
            e.Property(o => o.Id).HasColumnName("id");
            e.Property(o => o.OrderDate).HasColumnName("order_date");
            e.Property(o => o.DeliveryDate).HasColumnName("delivery_date");
            e.Property(o => o.DeliveryPointId).HasColumnName("id_delivery_point");
            e.Property(o => o.UserId).HasColumnName("id_user");
            e.Property(o => o.Code).HasColumnName("code");
            e.Property(o => o.StatusId).HasColumnName("id_statuses");
            e.HasOne(o => o.DeliveryPoint).WithMany(dp => dp.Orders).HasForeignKey(o => o.DeliveryPointId);
            e.HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId);
            e.HasOne(o => o.Status).WithMany(s => s.Orders).HasForeignKey(o => o.StatusId);
        });

        modelBuilder.Entity<ProductOrder>(e =>
        {
            e.ToTable("products_orders");
            e.Property(po => po.Id).HasColumnName("id");
            e.Property(po => po.OrderId).HasColumnName("id_order");
            e.Property(po => po.ProductId).HasColumnName("id_product");
            e.Property(po => po.Quantity).HasColumnName("quantity");
            e.HasOne(po => po.Order).WithMany(o => o.ProductOrders).HasForeignKey(po => po.OrderId);
            e.HasOne(po => po.Product).WithMany(p => p.ProductOrders).HasForeignKey(po => po.ProductId);
        });
    }
}
