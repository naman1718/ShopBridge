using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShopBridge.Controllers
{
    public class InventoryController : ApiController
    {
        public async Task<IHttpActionResult> GetAllItems()
        {
            IList<Inventory> inventoryViews = null;

            try
            {
                using (var ctx = new InventoryDBEntities())
                {
                    inventoryViews = await ctx.Inventory.ToListAsync();
                }

                if (inventoryViews.Count == 0)
                {
                    return NotFound();
                }

                return Ok(inventoryViews);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddNewItem(InventoryViewModel inventory)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            try
            {
                using (var ctx = new InventoryDBEntities())
                {
                    Inventory viewModel = new Inventory
                    {
                        ItemSKUNumber = Guid.NewGuid().ToString(),
                        ItemName = inventory.ItemName,
                        description = inventory.description,
                        ItemPrice = inventory.ItemPrice,
                        SupplierName = inventory.SupplierName,
                        AvailableQuantity = inventory.AvailableQuantity
                    };
                    ctx.Inventory.Add(viewModel);

                    await ctx.SaveChangesAsync();
                }

                return Ok("Item Saved Successfully!!");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> ModifyItemDetails(InventoryViewModel inventory, string itemID)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            try
            {
                using (var ctx = new InventoryDBEntities())
                {
                    var existingItem = await ctx.Inventory.Where(s => s.ItemSKUNumber == itemID).FirstOrDefaultAsync();

                    if (existingItem != null)
                    {
                        existingItem.ItemPrice = inventory.ItemPrice;
                        existingItem.SupplierName = inventory.SupplierName;
                        existingItem.AvailableQuantity = inventory.AvailableQuantity;
                        existingItem.description = inventory.description;

                        await ctx.SaveChangesAsync();
                    }
                    else
                    {
                        return NotFound();
                    }
                }

                return Ok("Item Modified SuccessFully!!");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IHttpActionResult> DeleteItem(string itemNumber)
        {
            if (string.IsNullOrEmpty(itemNumber))
            {
                return BadRequest("Not a valid Item Number");
            }

            try
            {
                using (var ctx = new InventoryDBEntities())
                {
                    var inventoryDetails = await ctx.Inventory.Where(s => s.ItemSKUNumber == itemNumber).FirstOrDefaultAsync();

                    ctx.Entry(inventoryDetails).State = EntityState.Deleted;
                    ctx.SaveChanges();
                }

                return Ok("Item Deleted SuccessFully!!");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
