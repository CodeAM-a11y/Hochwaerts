using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hochwaerts.Models;
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
            .Include(x=>x.Buch).Where(y=>y.StatusId==4);
        return View(await verkaufbareBücher.ToListAsync());
    }

    public IActionResult Privacy()
    {
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

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> SortTitel()
    {
        var verkaufbareBücher = _context.Verleihobjekt
            .Include(x=>x.Buch).Where(y=>y.StatusId==4)
            .OrderBy(x=>x.Buch.Titel);
        return View(nameof(Index),await verkaufbareBücher.ToListAsync());
    }
    public async Task<IActionResult> SortAutor()
    {
        var verkaufbareBücher = _context.Verleihobjekt
            .Include(x=>x.Buch).Where(y=>y.StatusId==4)
            .OrderBy(x=>x.Buch.Autoren);
        return View(nameof(Index),await verkaufbareBücher.ToListAsync());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}