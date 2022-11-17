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
    public class StorageLocationsController : Controller
    {
        private readonly WMS_Inventory_API_ClientContext _context;

        public StorageLocationsController(WMS_Inventory_API_ClientContext context)
        {
            _context = context;
        }

        // GET: StorageLocations
        public async Task<IActionResult> Index()
        {
              return View(await _context.StorageLocation.ToListAsync());
        }

        // GET: StorageLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StorageLocation == null)
            {
                return NotFound();
            }

            var storageLocation = await _context.StorageLocation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storageLocation == null)
            {
                return NotFound();
            }

            return View(storageLocation);
        }

        // GET: StorageLocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StorageLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LocationName,Address1,Address2,City,State,ZipCode,Longitude,Latitude,AccountId")] StorageLocation storageLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storageLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storageLocation);
        }

        // GET: StorageLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StorageLocation == null)
            {
                return NotFound();
            }

            var storageLocation = await _context.StorageLocation.FindAsync(id);
            if (storageLocation == null)
            {
                return NotFound();
            }
            return View(storageLocation);
        }

        // POST: StorageLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LocationName,Address1,Address2,City,State,ZipCode,Longitude,Latitude,AccountId")] StorageLocation storageLocation)
        {
            if (id != storageLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storageLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorageLocationExists(storageLocation.Id))
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
            return View(storageLocation);
        }

        // GET: StorageLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StorageLocation == null)
            {
                return NotFound();
            }

            var storageLocation = await _context.StorageLocation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storageLocation == null)
            {
                return NotFound();
            }

            return View(storageLocation);
        }

        // POST: StorageLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StorageLocation == null)
            {
                return Problem("Entity set 'WMS_Inventory_API_ClientContext.StorageLocation'  is null.");
            }
            var storageLocation = await _context.StorageLocation.FindAsync(id);
            if (storageLocation != null)
            {
                _context.StorageLocation.Remove(storageLocation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StorageLocationExists(int id)
        {
          return _context.StorageLocation.Any(e => e.Id == id);
        }
    }
}
