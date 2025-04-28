using FlutterBackEnd.Data;
using FlutterBackEnd.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Drawing;

namespace FlutterBackEnd.Controllers
{
    public class GroceryController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult GetItems()
        {
            try
            {
                using (MyContext db = new MyContext())
                {
                    List<GroceryModel> name = db.GroceryItem.ToList();
                    return new ObjectResult(name);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error message " + e.Message + ", stack trace" + e.StackTrace);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostItems([FromBody] GroceryModel value)
        {
            try
            {
                using (MyContext db = new MyContext())
                {
                    GroceryModel model = new GroceryModel();

                    model.name = value.name;
                    model.isBought = value.isBought;


                    db.GroceryItem.Add(model);
                    await db.SaveChangesAsync();

                    return new ObjectResult(model);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error message " + e.Message + ", stack trace" + e.StackTrace);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateItems([FromBody] GroceryModel value, Int64 id)
        {
            try
            {
                Console.WriteLine("Updating item id " + id);

                // connect to the database
                using (MyContext db = new MyContext())
                {
                    GroceryModel Item = await db.GroceryItem.FirstOrDefaultAsync(x => x.Id == id);
                    if (Item == null)
                    {
                        Console.WriteLine("Color ID " + id + "not found");
                        return NotFound("Color not found");
                    }

                    Item.name = value.name;
                    Item.isBought = value.isBought;
                    Item.createdAt = value.createdAt;


                    db.GroceryItem.Update(Item);
                    await db.SaveChangesAsync();

                    return new ObjectResult(Item);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Put got exception: " + ex.Message + ", Stack = " + ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteItems(Int64 id)
        {
            try
            {
                using (MyContext db = new MyContext())
                {
                    GroceryModel Item = await db.GroceryItem.FirstOrDefaultAsync(x => x.Id == id);
                    if (Item != null)
                    {
                        Console.WriteLine("deleting item with Id: " + Item.Id);
                        db.GroceryItem.Remove(Item);
                        await db.SaveChangesAsync();
                    }
                    Console.WriteLine("Item didn't exist or was already Deleted");
                    return new OkResult();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Got exception: " + e.Message + "Stack trace: " + e.StackTrace);
                return BadRequest(e);
            }
        }
    }
}
