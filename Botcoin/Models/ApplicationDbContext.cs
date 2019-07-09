using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Botcoin.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuyOrderModel>()
                        .Property(p => p.Amount)
                        .HasPrecision(18, 10);
            modelBuilder.Entity<BuyOrderModel>()
                        .Property(p => p.Price)
                        .HasPrecision(18, 10);
            modelBuilder.Entity<SellOrderModel>()
                        .Property(p => p.Amount)
                        .HasPrecision(18, 10);
            modelBuilder.Entity<SellOrderModel>()
                        .Property(p => p.Price)
                        .HasPrecision(18, 10);
        }

        public DbSet<BuyOrderModel> BuyOrders { get; set; }
        public DbSet<SellOrderModel> SellOrders { get; set; }
    }
}