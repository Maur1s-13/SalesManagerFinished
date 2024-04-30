using Microsoft.EntityFrameworkCore;
using SalesManagerNew.Lib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerNew.Lib.Services
{
    public class MyDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public string DbPath { get; set; }

        public MyDbContext(string path)
        {
            DbPath = path;

            System.Diagnostics.Debug.WriteLine("Path: " + DbPath);

            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
            .HasKey(e => e.OrderID);

            modelBuilder.Entity<Order>()
                .HasOne(e => e.Customer)
                .WithMany(t => t.Ordered)
                .HasForeignKey(e => e.CustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(e => e.Product)
                .WithMany(t => t.Ordered)
                .HasForeignKey(e => e.ProductId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite($"Data Source={DbPath}");

    }
}
