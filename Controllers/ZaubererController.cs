using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hochwaerts.Models;

namespace Hochwaerts.Controllers
{
    public class ZaubererController : Controller
    {
        private readonly HochwaertsDBContext _context;

        public ZaubererController(HochwaertsDBContext context)
        {
            _context = context;
        }

        // GET: Zauberer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zauberer.ToListAsync());
        }

        // GET: Zauberer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zauberer = await _context.Zauberer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zauberer == null)
            {
                return NotFound();
            }

            return View(zauberer);
        }

        // GET: Zauberer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zauberer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Haus")] Zauberer zauberer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zauberer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zauberer);
        }

        // GET: Zauberer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zauberer = await _context.Zauberer.FindAsync(id);
            if (zauberer == null)
            {
                return NotFound();
            }
            return View(zauberer);
        }

        // POST: Zauberer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Haus")] Zauberer zauberer)
        {
            if (id != zauberer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zauberer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaubererExists(zauberer.Id))
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
            return View(zauberer);
        }

        // GET: Zauberer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zauberer = await _context.Zauberer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zauberer == null)
            {
                return NotFound();
            }

            return View(zauberer);
        }

        // POST: Zauberer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zauberer = await _context.Zauberer.FindAsync(id);
            if (zauberer != null)
            {
                _context.Zauberer.Remove(zauberer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZaubererExists(int id)
        {
            return _context.Zauberer.Any(e => e.Id == id);
        }
    }
}
