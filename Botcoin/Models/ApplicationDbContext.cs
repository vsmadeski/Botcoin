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

        public DbSet<PriceRecord> PriceRecords { get; set; }
        public DbSet<BuyOrderModel> BuyOrders { get; set; }
        public DbSet<SellOrderModel> SellOrders { get; set; }
    }
}