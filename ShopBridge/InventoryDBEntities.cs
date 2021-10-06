using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopBridge
{
    public partial class InventoryDBEntities: DbContext
    {
        public InventoryDBEntities() : base("name=InventoryDBEntities")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>().ToTable("Inventory");
        }

        public DbSet<Inventory> Inventory { get; set; }
    }
}