using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagementSystem.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Process2
            builder.Entity<ProductListVm>().HasNoKey().ToView(null);
            builder.Entity<ProductInfoVm>().HasNoKey().ToView(null);
            //builder.Entity<ProductListVm>().ToView(null);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductInventorySnap> ProductInventorySnaps { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<ReturnDetail> ReturnDetails { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerProductPrice> CustomerProductPrices { get; set; }


        //Using for store procedure
        // Process1
        //[NotMapped]
        //public DbSet<ProductListVm> ProductListVm { get; set; }

        //Process2
        public DbSet<ProductListVm> ProductListVm { get; set; }
        public DbSet<ProductInfoVm> ProductInfoVm { get; set; }
    }
}