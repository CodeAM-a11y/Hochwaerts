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
    public class VerleihobjektController : Controller
    {
        private readonly HochwaertsDBContext _context;

        public VerleihobjektController(HochwaertsDBContext context)
        {
            _context = context;
        }

        // GET: Verleihobjekt
        public async Task<IActionResult> Index()
        {
            var hochwaertsDBContext = _context.Verleihobjekt.Include(v => v.Bibliothek).Include(v => v.Buch).Include(v => v.Status).Include(v => v.Zauberer);
            return View(await hochwaertsDBContext.ToListAsync());
        }

        // GET: Verleihobjekt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verleihobjekt = await _context.Verleihobjekt
                .Include(v => v.Bibliothek)
                .Include(v => v.Buch)
                .Include(v => v.Status)
                .Include(v => v.Zauberer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verleihobjekt == null)
            {
                return NotFound();
            }

            return View(verleihobjekt);
        }

        // GET: Verleihobjekt/Create
        public IActionResult Create()
        {
            ViewData["BibliothekId"] = new SelectList(_context.Bibliothek, "Id", "Id");
            ViewData["BuchID"] = new SelectList(_context.Buch, "Id", "Id");
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id");
            ViewData["ZaubererId"] = new SelectList(_context.Set<Zauberer>(), "Id", "Name");
            return View();
        }

        // POST: Verleihobjekt/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusId,BibliothekId,BuchID,ZaubererId")] Verleihobjekt verleihobjekt)
        {
            verleihobjekt.StatusId = 1;
            if (ModelState.IsValid)
            {
                _context.Add(verleihobjekt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BibliothekId"] = new SelectList(_context.Bibliothek, "Id", "Id", verleihobjekt.BibliothekId);
            ViewData["BuchID"] = new SelectList(_context.Buch, "Id", "Id", verleihobjekt.BuchID);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", verleihobjekt.StatusId);
            ViewData["ZaubererId"] = new SelectList(_context.Set<Zauberer>(), "Id", "Name", verleihobjekt.ZaubererId);
            return View(verleihobjekt);
        }

        // GET: Verleihobjekt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verleihobjekt = await _context.Verleihobjekt.FindAsync(id);
            if (verleihobjekt == null)
            {
                return NotFound();
            }
            ViewData["BibliothekId"] = new SelectList(_context.Bibliothek, "Id", "Id", verleihobjekt.BibliothekId);
            ViewData["BuchID"] = new SelectList(_context.Buch, "Id", "Id", verleihobjekt.BuchID);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", verleihobjekt.StatusId);
            ViewData["ZaubererId"] = new SelectList(_context.Set<Zauberer>(), "Id", "Name", verleihobjekt.ZaubererId);
            return View(verleihobjekt);
        }

        // POST: Verleihobjekt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusId,BibliothekId,BuchID,ZaubererId")] Verleihobjekt verleihobjekt)
        {
            if (id != verleihobjekt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verleihobjekt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerleihobjektExists(verleihobjekt.Id))
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
            ViewData["BibliothekId"] = new SelectList(_context.Bibliothek, "Id", "Id", verleihobjekt.BibliothekId);
            ViewData["BuchID"] = new SelectList(_context.Buch, "Id", "Id", verleihobjekt.BuchID);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", verleihobjekt.StatusId);
            ViewData["ZaubererId"] = new SelectList(_context.Set<Zauberer>(), "Id", "Name", verleihobjekt.ZaubererId);
            return View(verleihobjekt);
        }

        // GET: Verleihobjekt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verleihobjekt = await _context.Verleihobjekt
                .Include(v => v.Bibliothek)
                .Include(v => v.Buch)
                .Include(v => v.Status)
                .Include(v => v.Zauberer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verleihobjekt == null)
            {
                return NotFound();
            }

            return View(verleihobjekt);
        }

        // POST: Verleihobjekt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var verleihobjekt = await _context.Verleihobjekt.FindAsync(id);
            if (verleihobjekt != null)
            {
                _context.Verleihobjekt.Remove(verleihobjekt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Ausleihen(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verleihobjekt = await _context.Verleihobjekt.FindAsync(id);
            if (verleihobjekt == null)
            {
                return NotFound();
            }
            ViewData["BibliothekId"] = new SelectList(_context.Bibliothek, "Id", "Id", verleihobjekt.BibliothekId);
            ViewData["BuchID"] = new SelectList(_context.Buch, "Id", "Id", verleihobjekt.BuchID);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", verleihobjekt.StatusId);
            ViewData["ZaubererId"] = new SelectList(_context.Set<Zauberer>(), "Id", "Name", verleihobjekt.ZaubererId);
            return View(verleihobjekt);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ausleihen(int id, [Bind("ZaubererId")] Verleihobjekt input)
        {
            var verleihobjekt = await _context.Verleihobjekt.FindAsync(id);
            
            if (id != verleihobjekt.Id)
            {
                return NotFound();
            }
            verleihobjekt.ZaubererId = input.ZaubererId;
            verleihobjekt.StatusId = 2; // Status in der Datenbank prüfen
            verleihobjekt.Verleihdatum = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verleihobjekt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerleihobjektExists(verleihobjekt.Id))
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
            ViewData["BibliothekId"] = new SelectList(_context.Bibliothek, "Id", "Id", verleihobjekt.BibliothekId);
            ViewData["BuchID"] = new SelectList(_context.Buch, "Id", "Id", verleihobjekt.BuchID);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", verleihobjekt.StatusId);
            ViewData["ZaubererId"] = new SelectList(_context.Set<Zauberer>(), "Id", "Name", verleihobjekt.ZaubererId);
            return View(verleihobjekt);
        }
        public async Task<IActionResult> Zurückgeben(int id, [Bind("ZaubererId")] Verleihobjekt input)
        {
            var verleihobjekt = await _context.Verleihobjekt.FindAsync(id);
            
            if (id != verleihobjekt.Id)
            {
                return NotFound();
            }
            verleihobjekt.StatusId = 3; // Status in der Datenbank prüfen
            verleihobjekt.Verleihdatum = null;
            verleihobjekt.ZaubererId = null;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verleihobjekt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerleihobjektExists(verleihobjekt.Id))
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
            ViewData["BibliothekId"] = new SelectList(_context.Bibliothek, "Id", "Id", verleihobjekt.BibliothekId);
            ViewData["BuchID"] = new SelectList(_context.Buch, "Id", "Id", verleihobjekt.BuchID);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", verleihobjekt.StatusId);
            ViewData["ZaubererId"] = new SelectList(_context.Set<Zauberer>(), "Id", "Name", verleihobjekt.ZaubererId);
            return View(verleihobjekt);
        }
        public async Task<IActionResult> Beschädigt(int id, [Bind("ZaubererId")] Verleihobjekt input)
        {
            var verleihobjekt = await _context.Verleihobjekt.FindAsync(id);
            
            if (id != verleihobjekt.Id)
            {
                return NotFound();
            }

            var buch = await _context.Buch.FindAsync(verleihobjekt.BuchID);
            buch.Beschaedigungsgrad = true;
            verleihobjekt.StatusId = 4;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verleihobjekt);
                    _context.Update(buch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerleihobjektExists(verleihobjekt.Id))
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
            ViewData["BibliothekId"] = new SelectList(_context.Bibliothek, "Id", "Id", verleihobjekt.BibliothekId);
            ViewData["BuchID"] = new SelectList(_context.Buch, "Id", "Id", verleihobjekt.BuchID);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", verleihobjekt.StatusId);
            ViewData["ZaubererId"] = new SelectList(_context.Set<Zauberer>(), "Id", "Name", verleihobjekt.ZaubererId);
            return View(verleihobjekt);
        }
        public async Task<IActionResult> nichtBeschädigt(int id, [Bind("ZaubererId")] Verleihobjekt input)
        {
            var verleihobjekt = await _context.Verleihobjekt.FindAsync(id);
            
            if (id != verleihobjekt.Id)
            {
                return NotFound();
            }

            var buch = await _context.Buch.FindAsync(verleihobjekt.BuchID);
            buch.Beschaedigungsgrad = false;
            verleihobjekt.StatusId = 1;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verleihobjekt);
                    _context.Update(buch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerleihobjektExists(verleihobjekt.Id))
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
            ViewData["BibliothekId"] = new SelectList(_context.Bibliothek, "Id", "Id", verleihobjekt.BibliothekId);
            ViewData["BuchID"] = new SelectList(_context.Buch, "Id", "Id", verleihobjekt.BuchID);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", verleihobjekt.StatusId);
            ViewData["ZaubererId"] = new SelectList(_context.Set<Zauberer>(), "Id", "Name", verleihobjekt.ZaubererId);
            return View(verleihobjekt);
        }
        public async Task<IActionResult> verschollen(int id, [Bind("ZaubererId")] Verleihobjekt input)
        {
            var verleihobjekt = await _context.Verleihobjekt.FindAsync(id);
            
            if (id != verleihobjekt.Id)
            {
                return NotFound();
            }
            
            verleihobjekt.StatusId = 5;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verleihobjekt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerleihobjektExists(verleihobjekt.Id))
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
            ViewData["BibliothekId"] = new SelectList(_context.Bibliothek, "Id", "Id", verleihobjekt.BibliothekId);
            ViewData["BuchID"] = new SelectList(_context.Buch, "Id", "Id", verleihobjekt.BuchID);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", verleihobjekt.StatusId);
            ViewData["ZaubererId"] = new SelectList(_context.Set<Zauberer>(), "Id", "Name", verleihobjekt.ZaubererId);
            return View(verleihobjekt);
        }

        private bool VerleihobjektExists(int id)
        {
            return _context.Verleihobjekt.Any(e => e.Id == id);
        }
    }
}
