using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp1.Data;
using tp1.Models;

namespace tp1.Controllers;

public class ProduitController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProduitController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> MesProduits()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        var produits = await _context.Produits
            .Where(p => p.VendeurId == userId)
            .ToListAsync();

        return View(produits);
    }

    public IActionResult Create()
    {
        var role = HttpContext.Session.GetString("UserRole");
        if (role != "Vendeur") return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Produit produit)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        produit.VendeurId = userId.Value;
        ModelState.Remove("Vendeur");
        ModelState.Remove("PanierItems");

        if (ModelState.IsValid)
        {
            _context.Add(produit);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Produit mis en vente avec succès !";
            return RedirectToAction("Index", "Home");
        }
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        return View(produit);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var produit = await _context.Produits.FindAsync(id);
        int? userId = HttpContext.Session.GetInt32("UserId");

        if (produit != null && produit.VendeurId == userId)
        {
            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Produit supprimé.";
        }

        return RedirectToAction(nameof(MesProduits));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var produit = await _context.Produits.FindAsync(id);
        int? userId = HttpContext.Session.GetInt32("UserId");

        // Sécurité : Vérifier que le produit existe ET appartient au vendeur connecté
        if (produit == null || produit.VendeurId != userId)
        {
            return Unauthorized();
        }

        return View(produit);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Produit produit)
    {
        if (id != produit.Id) return NotFound();
        
        var produitOriginal = await _context.Produits.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        int? userId = HttpContext.Session.GetInt32("UserId");

        if (produitOriginal == null || produitOriginal.VendeurId != userId)
        {
            return Unauthorized();
        }

        produit.VendeurId = userId.Value;

        ModelState.Remove("Vendeur");
        ModelState.Remove("PanierItems");

        if (ModelState.IsValid)
        {
            _context.Update(produit);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Produit mis à jour !";
            return RedirectToAction(nameof(MesProduits));
        }
        return View(produit);
    }
}