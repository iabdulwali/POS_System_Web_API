using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS_System_API.Entities.Models;

namespace POS_System_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillProduct> BillProducts { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseProduct> PurchaseProducts {  get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roleList = new List<IdentityRole>() 
            { 
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roleList);
            // Bill to Product - Many to Many Relationship
            modelBuilder.Entity<BillProduct>()
                .HasKey(bp => new { bp.BillId, bp.ProductId });

            modelBuilder.Entity<BillProduct>()
                .HasOne(bp => bp.Bill)
                .WithMany(bp => bp.BillProducts)
                .HasForeignKey(bp => bp.BillId);

            modelBuilder.Entity<BillProduct>()
                .HasOne(bp => bp.Product)
                .WithMany(bp => bp.BillProducts)
                .HasForeignKey(bp => bp.ProductId);

            // Purchase to Product - Many to Many Relationship
            modelBuilder.Entity<PurchaseProduct>()
                .HasKey(bp => new { bp.PurchaseId, bp.ProductId });

            modelBuilder.Entity<PurchaseProduct>()
                .HasOne(bp => bp.Purchase)
                .WithMany(bp => bp.PurchaseProducts)
                .HasForeignKey(bp => bp.PurchaseId);

            modelBuilder.Entity<PurchaseProduct>()
                .HasOne(bp => bp.Product)
                .WithMany(bp => bp.PurchaseProducts)
                .HasForeignKey(bp => bp.ProductId);
        }
    }
}
