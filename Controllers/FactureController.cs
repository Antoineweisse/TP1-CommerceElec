using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp1.Data;
using tp1.Models;

public class FactureController : Controller
{
    private readonly ApplicationDbContext _context;

    public FactureController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Details(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Auth");

        var facture = _context.Factures
            .Include(f => f.Client)
            .Include(f => f.Commande)
                .ThenInclude(c => c.Lignes)
                    .ThenInclude(l => l.Produit)
                        .ThenInclude(p => p.Vendeur)
            .FirstOrDefault(f => f.Id == id);

        if (facture == null)
            return NotFound();

        var userRole = HttpContext.Session.GetString("UserRole");
        if (facture.ClientId != userId && userRole != "Vendeur")
            return Forbid();

        return View(facture);
    }

    public IActionResult MesAchats()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Auth");

        var factures = _context.Factures
            .Include(f => f.Commande)
                .ThenInclude(c => c.Lignes)
                    .ThenInclude(l => l.Produit)
            .Where(f => f.ClientId == userId)
            .OrderByDescending(f => f.DateFacture)
            .ToList();

        ViewBag.TotalDepense = factures.Sum(f => f.MontantTotal);

        return View(factures);
    }

    public IActionResult MesVentes()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Auth");

        var lignesVendeur = _context.LigneCommandes
            .Include(l => l.Produit)
            .Include(l => l.Commande)
                .ThenInclude(c => c.Utilisateur)
            .Where(l => l.Produit.VendeurId == userId)
            .OrderByDescending(l => l.Commande.Date)
            .ToList();

        ViewBag.TotalGagne = lignesVendeur.Sum(l => l.PrixUnitaire * l.Quantite);

        return View(lignesVendeur);
    }
}
