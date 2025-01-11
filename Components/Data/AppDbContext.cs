using BanSach.Components.Model;
using Microsoft.EntityFrameworkCore;
namespace BanSach.Components.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<LogAnhNoiBat> Logs { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<QNA> QNA { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Feature_Products> FeatureProducts { get; set; }
        public DbSet<Img> Imgs { get; set; }
        public DbSet<Personal_access_token> Personal_access_tokens { get; set; }
        public DbSet<Product_bill> Product_bills { get; set; }
        public DbSet<Product_cart> Product_carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
             .HasKey(l => l.ReviewId);
            modelBuilder.Entity<LogAnhNoiBat>()
            .HasKey(l => l.LogId);
            modelBuilder.Entity<Address>()
                .HasKey(u => u.AddressId);
            modelBuilder.Entity<Address>()
                .Property(u => u.AddressId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Bill>()
                .HasKey(o => o.BillId);
            modelBuilder.Entity<Bill>()
                .Property(o => o.BillId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Bill>()
                .Property(b => b.TotalPrice)
                .HasColumnType("decimal(18, 0)");


            modelBuilder.Entity<Discount>()
                    .HasKey(o => o.DiscountId);
            modelBuilder.Entity<Discount>()
                .Property(o => o.DiscountId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Category>()
                .HasKey(o => o.CategoryId);
            modelBuilder.Entity<Category>()
                .Property(o => o.CategoryId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Delivery>()
                .HasKey(m => m.DeliveryId);
            modelBuilder.Entity<Delivery>()
                .Property(m => m.DeliveryId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Feature_Products>()
                .HasKey(m => m.FeaturePId);
            modelBuilder.Entity<Feature_Products>()
                .Property(m => m.FeaturePId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Img>()
                .HasKey(m => m.ImgId);
            modelBuilder.Entity<Img>()
                .Property(m => m.ImgId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Personal_access_token>()
                .HasKey(m => m.PersonalId);
            modelBuilder.Entity<Personal_access_token>()
                .Property(m => m.PersonalId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product_bill>()
                .HasKey(m => m.ProductBillId);
            modelBuilder.Entity<Product_bill>()
                .Property(m => m.ProductBillId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product_bill>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product_cart>()
           .HasKey(m => new { m.UserId, m.ProductId });  // Sử dụng khóa chính kết hợp UserId và ProductId
            modelBuilder.Entity<Product_cart>()
                .HasOne(pc => pc.Product)   // Mối quan hệ với Product
                .WithMany()                  // Một sản phẩm có thể có nhiều giỏ hàng
                .HasForeignKey(pc => pc.ProductId)  // Khóa ngoại tham chiếu đến bảng Products
                .OnDelete(DeleteBehavior.Cascade);  // Xóa giỏ hàng nếu sản phẩm bị xóa
            modelBuilder.Entity<Product_cart>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
                .HasKey(m => m.ProductId);
            modelBuilder.Entity<Product>()
                .Property(m => m.ProductId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Role>()
                .HasKey(m => m.RoleId);
            modelBuilder.Entity<Role>()
                .Property(m => m.RoleId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Sell>()
                .HasKey(m => m.SellId);
            modelBuilder.Entity<Sell>()
                .Property(m => m.SellId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasKey(m => m.UserId);
            modelBuilder.Entity<User>()
                .Property(m => m.UserId)
                .ValueGeneratedOnAdd();

        }
    }
}

