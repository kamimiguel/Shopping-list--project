using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Data;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;

namespace ShoppingList.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemlistDbContext _context;

        public ItemController(ItemlistDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            int userId = HttpContext.Session.GetInt32("UserId").Value;
            var data = await _context.Items.Where(i => i.UserId == userId).ToListAsync();
            if (data.Count == 0)
            {
                ViewBag.Message = "No items to show";
            }
            return View(data);

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name", "Quantity", "Bought")] Item item)
        {
            if (!ModelState.IsValid)
            {
                int userId = HttpContext.Session.GetInt32("UserId").Value;
                using (var context = new AccountDbContext())
                {
                    var currentUser = context.userAccount.FirstOrDefault(u => u.UserId == userId);

                    if (currentUser == null)
                    {
                        ModelState.AddModelError("", "No user found");
                    }

                    var existingItem = _context.Items.FirstOrDefault(i => i.Name == item.Name && i.UserId == userId);
                    if (existingItem != null)
                    {
                        ModelState.AddModelError("", "Item already exists on your list");
                    }
                }
                item.UserId = userId;

                _context.Add(item);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
                //return CreatedAtAction("GetItem", new { id = item.Itemid }, item);
            }
            return View(item);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _context.Items.FirstOrDefault(i => i.Itemid == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name", "Quantity")] Item item)
        {
            var existingItem = _context.Items.FirstOrDefault(i => i.Itemid == id);

            if (existingItem == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                existingItem.Name = item.Name;
                existingItem.Quantity = item.Quantity;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkPurchased(int id)
        {
            var item = _context.Items.FirstOrDefault(i => i.Itemid == id);

            if (item == null)
            {
                return NotFound();
            }

            item.Bought = true;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
       {
           HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}

