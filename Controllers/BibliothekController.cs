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
    public class BibliothekController : Controller
    {
        private readonly HochwaertsDBContext _context;

        public BibliothekController(HochwaertsDBContext context)
        {
            _context = context;
        }

        // GET: Bibliothek
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bibliothek.ToListAsync());
        }

        // GET: Bibliothek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bibliothek = await _context.Bibliothek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bibliothek == null)
            {
                return NotFound();
            }

            return View(bibliothek);
        }

        // GET: Bibliothek/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bibliothek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Bibliothek bibliothek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bibliothek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bibliothek);
        }

        // GET: Bibliothek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bibliothek = await _context.Bibliothek.FindAsync(id);
            if (bibliothek == null)
            {
                return NotFound();
            }
            return View(bibliothek);
        }

        // POST: Bibliothek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Bibliothek bibliothek)
        {
            if (id != bibliothek.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bibliothek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BibliothekExists(bibliothek.Id))
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
            return View(bibliothek);
        }

        // GET: Bibliothek/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bibliothek = await _context.Bibliothek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bibliothek == null)
            {
                return NotFound();
            }

            return View(bibliothek);
        }

        // POST: Bibliothek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bibliothek = await _context.Bibliothek.FindAsync(id);
            if (bibliothek != null)
            {
                _context.Bibliothek.Remove(bibliothek);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BibliothekExists(int id)
        {
            return _context.Bibliothek.Any(e => e.Id == id);
        }
    }
}
