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

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Override default AspNet Identity table names
            modelBuilder.Entity<User>(entity => { entity.ToTable(name: "Users"); });
            modelBuilder.Entity<Address>(entity => { entity.ToTable(name: "Address"); });
            modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });
            modelBuilder.Entity<Category>(entity => {
                entity.HasOne(d => d.Parent)
                       .WithMany(p => p.InverseParents)
                       .HasForeignKey(d => d.ParentId);
                entity.ToTable(name: "Categories");

            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.Category)
                      .WithMany(p => p.Products)
                      .HasForeignKey(d => d.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Category_Product");
            });


            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasOne(d => d.Product)
                      .WithMany(p => p.Photos)
                      .HasForeignKey(d => d.ProductId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Product_Photo");
            });
        }
    }
}
