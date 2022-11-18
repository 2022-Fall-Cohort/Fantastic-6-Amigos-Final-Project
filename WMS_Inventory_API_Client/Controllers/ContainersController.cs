using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMS_Inventory_API_Client.Data;
using WMS_Inventory_API_Client.Models;

namespace WMS_Inventory_API_Client.Controllers
{
    public class ContainersController : Controller
    {
        private readonly WMS_Inventory_API_ClientContext _context;

        public ContainersController(WMS_Inventory_API_ClientContext context)
        {
            _context = context;
        }

        // GET: Containers
        public async Task<IActionResult> Index()
        {
            var wMS_Inventory_API_ClientContext = _context.Container.Include(c => c.StorageLocation);
            return View(await wMS_Inventory_API_ClientContext.ToListAsync());
        }

        // GET: Containers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Container == null)
            {
                return NotFound();
            }

            var container = await _context.Container
                .Include(c => c.StorageLocation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        // GET: Containers/Create
        public IActionResult Create()
        {
            ViewData["StorageLocationId"] = new SelectList(_context.StorageLocation, "Id", "Id");
            return View();
        }

        // POST: Containers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Description,StorageLocationId")] Container container)
        {
            if (ModelState.IsValid)
            {
                _context.Add(container);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StorageLocationId"] = new SelectList(_context.StorageLocation, "Id", "Id", container.StorageLocationId);
            return View(container);
        }

        // GET: Containers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Container == null)
            {
                return NotFound();
            }

            var container = await _context.Container.FindAsync(id);
            if (container == null)
            {
                return NotFound();
            }
            ViewData["StorageLocationId"] = new SelectList(_context.StorageLocation, "Id", "Id", container.StorageLocationId);
            return View(container);
        }

        // POST: Containers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Type,Description,StorageLocationId")] Container container)
        {
            if (id != container.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(container);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContainerExists(container.Id))
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
            ViewData["StorageLocationId"] = new SelectList(_context.StorageLocation, "Id", "Id", container.StorageLocationId);
            return View(container);
        }

        // GET: Containers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Container == null)
            {
                return NotFound();
            }

            var container = await _context.Container
                .Include(c => c.StorageLocation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        // POST: Containers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Container == null)
            {
                return Problem("Entity set 'WMS_Inventory_API_ClientContext.Container'  is null.");
            }
            var container = await _context.Container.FindAsync(id);
            if (container != null)
            {
                _context.Container.Remove(container);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContainerExists(int? id)
        {
            return _context.Container.Any(e => e.Id == id);
        }
    }
}
