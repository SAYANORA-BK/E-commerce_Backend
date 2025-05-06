using System.Collections.Generic;
using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Dbcontext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> users{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
        }
    }
}
