using Microsoft.EntityFrameworkCore;
using StockApp.Core.Domain.Common;
using StockApp.Core.Domain.Entities;

namespace StockApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> optiones) : base(optiones) { }
        public DbSet<Product> Productos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellation = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            { 
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreateBy = "Funciona";
                        entry.Entity.LastModifiedBy = "Funciona";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "Yahinniel-2005-RD_STD";
                        entry.Property("CreateBy").IsModified = false;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellation);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region tables
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Category>().ToTable("Categories");
            #endregion

            #region "primary keys"
            modelBuilder.Entity<Product>().HasKey(product => product.Id); //Lambda
            modelBuilder.Entity<Category>().HasKey(category => category.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<Category>()
                .HasMany<Product>(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.categoryId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "property configuration"

            #region product
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);
            #endregion

            #region Category
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);
            #endregion

            #endregion

            base.OnModelCreating(modelBuilder);
        }


    }
}
