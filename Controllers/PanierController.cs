using Microsoft.AspNetCore.Mvc;
using tp1.Data;
using tp1.Models;
using Microsoft.EntityFrameworkCore;

public class PanierController : Controller
{
    private readonly ApplicationDbContext _context;

    public PanierController(ApplicationDbContext context) => _context = context;

    // Affiche le contenu du panier
    public async Task<IActionResult> Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        var panier = await _context.Paniers
            .Include(p => p.Items)
            .ThenInclude(i => i.Produit)
            .FirstOrDefaultAsync(p => p.UtilisateurId == userId);
        return View(panier?.Items ?? new List<PanierItem>());
    }

    // Ajoute un produit au panier
    [HttpPost]
    public async Task<IActionResult> Ajouter(int id, int quantite = 1)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");
    
        var panier = await _context.Paniers
            .Include(p => p.Items)
            .FirstOrDefaultAsync(p => p.UtilisateurId == userId);
        
        if (panier == null)
        {
            panier = new Panier { UtilisateurId = userId.Value };
            _context.Paniers.Add(panier);
            await _context.SaveChangesAsync();
        }

        var item = panier.Items.FirstOrDefault(i => i.ProduitId == id);
        if (item != null)
        {
            item.Quantite += quantite;
        }
        else
        {
            panier.Items.Add(new PanierItem { ProduitId = id, Quantite = quantite, PanierId = panier.Id });
        }
        await _context.SaveChangesAsync();
        var returnUrl = Request.Headers["Referer"].ToString();
        if (!string.IsNullOrEmpty(returnUrl))
            return Redirect(returnUrl);
        return RedirectToAction("Index", "Home");
    }
}