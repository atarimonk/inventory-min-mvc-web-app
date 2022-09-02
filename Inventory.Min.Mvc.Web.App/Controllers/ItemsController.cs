using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.Min.Mvc.Web.App.Models;

namespace Inventory.Min.Mvc.Web.App.Controllers;

public class ItemsController
    : Controller
{
    private readonly IApiClient api;

    public ItemsController(IApiClient api)
    {
        this.api = api;
    }

    // GET: Items
    public async Task<IActionResult> Index()
    {
        var client = api.GetClinet();
        var items = await api.GetItemsAsync(client);
        return View(items);
    }

    // GET: Items/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var client = api.GetClinet();
        var item = await api.GetItemAsync(client, id);
        if (id == null || item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    // GET: Items/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Items/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description")] ItemVM itemVM)
    {
        if (ModelState.IsValid == false)
            return View(itemVM);
        var client = api.GetClinet();
        await api.CreateItemAsync(client, itemVM);
        return RedirectToAction(nameof(Index));
    }

    // GET: Items/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var client = api.GetClinet();
        var item = await api.GetItemAsync(client, id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    // POST: Items/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] ItemVM itemVM)
    {
        if (id != itemVM.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var client = api.GetClinet();
                await api.UpdateItemAsync(client, itemVM);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemVMExists(itemVM.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(itemVM);
    }

    // GET: Items/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        // if (id == null || _context.ItemVM == null)
        // {
        //     return NotFound();
        // }

        // var itemVM = await _context.ItemVM
        //     .FirstOrDefaultAsync(m => m.Id == id);
        // if (itemVM == null)
        // {
        //     return NotFound();
        // }

        return View(new ItemVM());
    }

    // POST: Items/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // if (_context.ItemVM == null)
        // {
        //     return Problem("Entity set 'InventoryDbContext.ItemVM'  is null.");
        // }
        // var itemVM = await _context.ItemVM.FindAsync(id);
        // if (itemVM != null)
        // {
        //     _context.ItemVM.Remove(itemVM);
        // }
        
        // await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ItemVMExists(int id)
    {
      return false;//(_context.ItemVM?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}