using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp1.Models;
using tp1.Data;
using Microsoft.EntityFrameworkCore;

namespace tp1.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string search, string category)
    {
        var categories = await _context.Produits
            .Select(p => p.Categorie)
            .Distinct()
            .ToListAsync();
        ViewBag.Categories = categories;

        var query = _context.Produits.AsQueryable();
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Titre.ToLower().Contains(search.ToLower()));
        }
        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.Categorie == category);
        }
        return View(await query.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produit = await _context.Produits
            .Include(p => p.Vendeur)
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (produit == null)
        {
            return NotFound();
        }

        return View(produit);
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
