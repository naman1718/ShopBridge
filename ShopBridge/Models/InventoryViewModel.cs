using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBridge.Models
{
    public class InventoryViewModel
    {
        public string ItemSKUNumber { get; set; }
        public string ItemName { get; set; }
        public string description { get; set; }
        public decimal ItemPrice { get; set; }
        public string SupplierName { get; set; }
        public string AvailableQuantity { get; set; }

    }

    public class Inventory
    {
        public string ItemSKUNumber { get; set; }
        public string ItemName { get; set; }
        public string description { get; set; }
        public decimal ItemPrice { get; set; }
        public string SupplierName { get; set; }
        public string AvailableQuantity { get; set; }

    }
}