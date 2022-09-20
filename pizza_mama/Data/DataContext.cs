using System;
using Microsoft.EntityFrameworkCore;
using pizza_mama.Models;

namespace pizza_mama.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Pizza> Pizzas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>().ToTable("Pizza");
        }

    }
}

