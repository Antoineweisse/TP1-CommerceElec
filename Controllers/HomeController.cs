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

    public async Task<IActionResult> Index()
    {
        var produits = await _context.Produits.ToListAsync();
        return View(produits);
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
