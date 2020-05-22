using System;
using eProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<SlideShow> SlideShows { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<About> About { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<OrderDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Porfolio> Porfolios { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }
        public virtual DbSet<Contact> Contacts {get;set; }

        //public virtual DbSet<Delivery> Delivery { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Override default AspNet Identity table names
            modelBuilder.Entity<User>(entity => { entity.ToTable(name: "Users"); });
            modelBuilder.Entity<Address>(entity => { entity.ToTable(name: "Addresses"); });
            modelBuilder.Entity<SlideShow>(entity => { entity.ToTable(name: "SlideShows"); });
            modelBuilder.Entity<Category>(entity => { entity.ToTable(name: "Categories"); });
            modelBuilder.Entity<About>(entity => { entity.ToTable(name: "Abouts"); });
            modelBuilder.Entity<Blog>(entity => { entity.ToTable(name: "Blogs"); });
            modelBuilder.Entity<Review>(entity => { entity.ToTable(name: "Reviews"); });
            modelBuilder.Entity<Porfolio>(entity => { entity.ToTable(name: "Porfolios"); });
            modelBuilder.Entity<WishList>(entity => { entity.ToTable(name: "WishLists"); });
            modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.Category)
                      .WithMany(p => p.Products)
                      .HasForeignKey(d => d.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Category_Product");

                entity.ToTable("Products");
            });


            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasOne(d => d.Product)
                      .WithMany(p => p.Photos)
                      .HasForeignKey(d => d.ProductId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Product_Photo");

                entity.ToTable("Photos");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {

                entity.ToTable("Invoices");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceId, e.ProductId });
                entity.HasOne(d => d.Invoice)
                      .WithMany(p => p.OrderDetails)
                      .HasForeignKey(d => d.InvoiceId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_InvoiceDetail_Invoice");

                entity.ToTable("InvoiceDetails");
            });
        }
    }
}
