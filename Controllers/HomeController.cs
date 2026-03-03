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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}