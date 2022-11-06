using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace testApp.Models
{
    public partial class MarketContext : DbContext
    {
        public MarketContext()
        {
        }

        public MarketContext(DbContextOptions<MarketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Market;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasMany(d => d.Products)
                    .WithMany(p => p.Categories)
                    .UsingEntity<Dictionary<string, object>>(
                        "CategoryProduct",
                        l => l.HasOne<Product>().WithMany().HasForeignKey("ProductsId"),
                        r => r.HasOne<Category>().WithMany().HasForeignKey("CategoriesId"),
                        j =>
                        {
                            j.HasKey("CategoriesId", "ProductsId");

                            j.ToTable("CategoryProduct");

                            j.HasIndex(new[] { "ProductsId" }, "IX_CategoryProduct_ProductsId");
                        });
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_Orders_ProductId");

                entity.HasIndex(e => e.UserId, "IX_Orders_UserId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(N'No description')");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
