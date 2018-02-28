using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AmonFinalProect.Models
{
    public partial class AmonTestContext : IdentityDbContext<ApplicationUser>
    {

        public virtual DbSet<Carts> Carts { get; set; }
        public virtual DbSet<CartsProducts> CartsProducts { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        //Add a new Virtual DbSet of your object to the context
        public virtual DbSet<Review> Reviews { get; set; }

        public AmonTestContext() : base()
        {

        }

        public AmonTestContext(DbContextOptions options) : base(options)
        {

        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //This is called Fluent API - it allows for more specific customization of database rules
            modelBuilder.Entity<Order>().HasKey(o => o.ID);

            //This is called Fluent API - it allows for more specific customization of database rules
            modelBuilder.Entity<Order>().HasKey(prop => prop.ID);
            modelBuilder.Entity<Order>()
                .Property(prop => prop.ID)
                .ValueGeneratedOnAdd();

            //Fluent API can almost be translated into a sentance:
            //Order Has Property Tracking Number whose value is generated when added
            modelBuilder.Entity<Order>().Property(prop => prop.TrackingNumber)
               .ValueGeneratedOnAdd();

            //Order has many line items, each line item has an order, which is required
            modelBuilder.Entity<Order>().HasMany(o => o.LineItems).WithOne(l => l.Order).IsRequired();

            //Line items have one order, with many line items.
            modelBuilder.Entity<LineItem>().HasOne(l => l.Order).WithMany(o => o.LineItems);
            modelBuilder.Entity<LineItem>().HasOne(l => l.Product).WithMany(o => o.LineItems);

            modelBuilder.Entity<Products>().HasMany(p => p.LineItems).WithOne(l => l.Product).IsRequired();

            //base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Carts>(entity =>
            {
                entity.HasKey(e => e.CartId);

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateLastModified).HasColumnType("datetime");
            });

            modelBuilder.Entity<CartsProducts>(entity =>
            {
                entity.HasKey(e => new { e.CartId, e.ProductId });

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SpecialInstructions).HasMaxLength(1000);

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartsProducts)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartsProducts_Carts");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartsProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartsProducts_Products");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.ImageUrl).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.Price).HasColumnType("money");
            });
            
        }
    }
}
