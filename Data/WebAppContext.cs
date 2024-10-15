using Microsoft.EntityFrameworkCore;
using WebApp_vSem1.Models;

namespace WebApp_vSem1.Data
{
    public class WebAppContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conStrg = "Server=localHost;Database=WebAppDB;Uid=root;Pwd=12345;";
            optionsBuilder.LogTo(Console.WriteLine)
                          .UseLazyLoadingProxies()
                          .UseMySql(conStrg, ServerVersion.AutoDetect(conStrg));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.HasKey(pq => pq.Id)
                      .HasName("product_group_pk");

                entity.ToTable("category");

                entity.Property(pq => pq.Name)
                      .HasColumnName("name")
                      .HasMaxLength(255);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id)
                       .HasName("product_pk");

                entity.Property(p => p.Name)
                      .HasColumnName("name")
                      .HasMaxLength(255);

                entity.HasOne(p => p.ProductGroup)
                      .WithMany(p => p.Products)
                      .HasForeignKey(p => p.ProductGroupId);
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(s => s.Id)
                      .HasName("storage_pk");

                entity.HasOne(s => s.Product)
                      .WithMany(p => p.Storages)
                      .HasForeignKey(p => p.ProductId);
            });
        }
    }
}
