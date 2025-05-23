﻿using FlutterBackEnd.Data;
using FlutterBackEnd.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Drawing;

namespace FlutterBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroceryController : ControllerBase
    {
        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> PostItems([FromBody] GroceryModel value)
        {
            try
            {
                using (MyContext db = new MyContext())
                {
                    GroceryModel model = new GroceryModel();

                    model.name = value.name;
                    model.isBought = value.isBought;
                    model.createdAt = value.createdAt;


                    db.GroceryItem.Add(model);
                    await db.SaveChangesAsync();

                    Console.WriteLine("Received: name=" + value.name + ", isBought=" + value.isBought);
                    return new ObjectResult(model);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error message " + e.Message + ", stack trace" + e.StackTrace);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItems([FromBody] GroceryModel value, Int64 id)
        {
            try
            {
                Console.WriteLine("Updating item id " + id);

                using (MyContext db = new MyContext())
                {
                    GroceryModel item = await db.GroceryItem.FirstOrDefaultAsync(x => x.Id == id);
                    if (item == null)
                    {
                        Console.WriteLine("Item ID " + id + " not found");
                        return NotFound("Item not found");
                    }

                    item.name = value.name;
                    item.isBought = value.isBought;
                    item.createdAt = DateTime.UtcNow;


                    db.GroceryItem.Update(item);
                    await db.SaveChangesAsync();

                    return new ObjectResult(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Put got exception: " + ex.Message + ", Stack = " + ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
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
