using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hochwaerts.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hochwaerts.Controllers;

public class HomeController : Controller
{
    private readonly HochwaertsDBContext _context;

    public HomeController(HochwaertsDBContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var verkaufbareBücher = _context.Verleihobjekt
            .Include( verleihobjekt=>verleihobjekt.Buch)
            .ThenInclude(buch =>buch.Autoren).Where(y=>y.StatusId==4);
        ViewBag.Autorenliste = new SelectList(_context.Autor, "Name", "Name");
        return View(await verkaufbareBücher.ToListAsync());
    }

    public IActionResult Privacy()
    {
        ViewBag.Autorenliste = new SelectList(_context.Autor, "Name", "Name");
        return View();
    }
    public async Task<IActionResult> kaufen(int? id)
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

        ViewBag.Autorenliste = new SelectList(_context.Autor, "Name", "Name");
        return View(verleihobjekt);
    }
    [HttpPost, ActionName("kaufen")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var verleihobjekt = await _context.Verleihobjekt.FindAsync(id);
        if (verleihobjekt != null)
        {
            _context.Verleihobjekt.Remove(verleihobjekt);
        }

        ViewBag.Autorenliste = new SelectList(_context.Autor, "Name", "Name");
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> SortTitel()
    {
        var verkaufbareBücher = _context.Verleihobjekt
            .Include(x=>x.Buch).ThenInclude(buch =>buch.Autoren ).Where(y=>y.StatusId==4)
            .OrderBy(x=>x.Buch.Titel);
        ViewBag.Autorenliste = new SelectList(_context.Autor, "Name", "Name");
        return View(nameof(Index),await verkaufbareBücher.ToListAsync());
    }
    public async Task<IActionResult> SortAutor()
    {
        var verkaufbareBücher = _context.Verleihobjekt
            .Include(verleihobjekt => verleihobjekt.Buch)
            .ThenInclude(buch =>buch.Autoren )
            .Where(y=>y.StatusId==4)
            .OrderBy(x=>x.Buch.Autoren.OrderBy(a=>a.Name).FirstOrDefault().Name);
        ViewBag.Autorenliste = new SelectList(_context.Autor, "Name", "Name");
        return View(nameof(Index),await verkaufbareBücher.ToListAsync());
    }
    public async Task<IActionResult> SortDate()
    {
        var verkaufbareBücher = _context.Verleihobjekt
            .Include(x=>x.Buch).ThenInclude(buch =>buch.Autoren ).Where(y=>y.StatusId==4)
            .OrderBy(x=>x.Buch.Erscheinungsjahr);
        ViewBag.Autorenliste = new SelectList(_context.Autor, "Name", "Name");
        return View(nameof(Index),await verkaufbareBücher.ToListAsync());
    }

    public async Task<IActionResult> FilterAutor(string autor)
    {
        var verkaufbareBücher = _context.Verleihobjekt.Include(verleihobjekt =>  verleihobjekt.Buch).
            ThenInclude(buch => buch.Autoren).Where(verleihobjekt => verleihobjekt.StatusId == 4);
        if (!string.IsNullOrEmpty(autor)) { 
            verkaufbareBücher = verkaufbareBücher
            .Where(v => v.Buch.Autoren.Any(a => a.Name.Equals(autor))); 
        } 
        ViewBag.Autorenliste = new SelectList(_context.Autor, "Name", "Name");
        var result = await verkaufbareBücher.ToListAsync(); 
        return View(nameof(Index), result);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}