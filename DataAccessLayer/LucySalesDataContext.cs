using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public partial class LucySalesDataContext : DbContext
{
    public LucySalesDataContext()
    {
    }

    public LucySalesDataContext(DbContextOptions<LucySalesDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DataTrainTest> DataTrainTests { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=LAPTOP-K7C91U87\\MSSQLSERVER02;database=Lucy_SalesData;uid=sa;pwd=1234567890;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.CategoryName, "CategoryName");

            entity.Property(e => e.CategoryID).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(15);
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Picture).HasColumnType("image");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(e => e.CompanyName, "CompanyName");

            entity.Property(e => e.CustomerID).HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.ContactTitle).HasMaxLength(30);
            entity.Property(e => e.Phone).HasMaxLength(24);
        });

        modelBuilder.Entity<DataTrainTest>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("DataTrainTest");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.Name, "LastName");

            entity.Property(e => e.EmployeeID).HasColumnName("EmployeeID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.JobTitle).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(25);
            entity.Property(e => e.UserName).HasMaxLength(10);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderID)
                .ValueGeneratedNever()
                .HasColumnName("OrderID");
            entity.Property(e => e.CustomerID).HasColumnName("CustomerID");
            entity.Property(e => e.EmployeeID).HasColumnName("EmployeeID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Employees");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderID, e.ProductID }).HasName("PK_Order_Details");

            entity.ToTable("Order Details");

            entity.HasIndex(e => e.OrderID, "OrderID");

            entity.HasIndex(e => e.OrderID, "OrdersOrder_Details");

            entity.HasIndex(e => e.ProductID, "ProductID");

            entity.HasIndex(e => e.ProductID, "ProductsOrder_Details");

            entity.Property(e => e.OrderID).HasColumnName("OrderID");
            entity.Property(e => e.ProductID).HasColumnName("ProductID");
            entity.Property(e => e.Quantity).HasDefaultValue((short)1);
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order Details_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryID, "CategoriesProducts");

            entity.HasIndex(e => e.CategoryID, "CategoryID");

            entity.HasIndex(e => e.ProductName, "ProductName");

            entity.HasIndex(e => e.SupplierID, "SupplierID");

            entity.HasIndex(e => e.SupplierID, "SuppliersProducts");

            entity.Property(e => e.ProductID).HasColumnName("ProductID");
            entity.Property(e => e.CategoryID).HasColumnName("CategoryID");
            entity.Property(e => e.ProductName).HasMaxLength(40);
            entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);
            entity.Property(e => e.ReorderLevel).HasDefaultValue(0);
            entity.Property(e => e.SupplierID).HasColumnName("SupplierID");
            entity.Property(e => e.UnitPrice)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.UnitsInStock).HasDefaultValue(0);
            entity.Property(e => e.UnitsOnOrder).HasDefaultValue(0);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryID)
                .HasConstraintName("FK_Products_Categories");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
