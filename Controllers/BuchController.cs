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
    public class BuchController : Controller
    {
        private readonly HochwaertsDBContext _context;

        public BuchController(HochwaertsDBContext context)
        {
            _context = context;
        }

        // GET: Buch
        public async Task<IActionResult> Index()
        {
            return View(await _context.Buch.ToListAsync());
        }

        // GET: Buch/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buch = await _context.Buch
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buch == null)
            {
                return NotFound();
            }

            return View(buch);
        }

        // GET: Buch/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buch/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titel,Inhalt,Erscheinungsjahr,ISBN,Beschaedigungsgrad")] Buch buch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buch);
        }

        // GET: Buch/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buch = await _context.Buch.FindAsync(id);
            if (buch == null)
            {
                return NotFound();
            }
            return View(buch);
        }

        // POST: Buch/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titel,Inhalt,Erscheinungsjahr,ISBN,Beschaedigungsgrad")] Buch buch)
        {
            if (id != buch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuchExists(buch.Id))
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
            return View(buch);
        }

        // GET: Buch/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buch = await _context.Buch
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buch == null)
            {
                return NotFound();
            }

            return View(buch);
        }

        // POST: Buch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buch = await _context.Buch.FindAsync(id);
            if (buch != null)
            {
                _context.Buch.Remove(buch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuchExists(int id)
        {
            return _context.Buch.Any(e => e.Id == id);
        }
    }
}
