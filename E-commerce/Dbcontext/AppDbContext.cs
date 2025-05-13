using System.Collections.Generic;
using CloudinaryDotNet.Actions;
using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Dbcontext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> users{ get; set; }
        public DbSet<Product> Product { get; set; }
       public DbSet<Category> Category { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OderItems> OrderItems { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Product>()
                .Property(pr => pr.Price).
                HasPrecision(18, 2);


            modelBuilder.Entity<OderItems>()
       .Property(o => o.TotalPrice)
       .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);
        }
    }
}
