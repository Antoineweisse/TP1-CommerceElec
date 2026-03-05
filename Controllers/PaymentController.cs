using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp1.Settings;
using tp1.Data;
using tp1.Models;
using Stripe;

public class PaymentController : Controller
{
    private readonly StripeSettings _stripeSettings;
    private readonly ApplicationDbContext _context;

    public PaymentController(IOptions<StripeSettings> stripeSettings, ApplicationDbContext context)
    {
        _stripeSettings = stripeSettings.Value;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Title = "Page de paiement";
        ViewBag.StripePublishableKey = _stripeSettings.PublishableKey;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Charge(string stripeToken)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Auth");

        var panier = _context.Paniers
            .Include(p => p.Items)
            .ThenInclude(i => i.Produit)
            .FirstOrDefault(p => p.UtilisateurId == userId);

        if (panier == null || !panier.Items.Any())
        {
            TempData["ErrorMessage"] = "Votre panier est vide.";
            return RedirectToAction("Failed");
        }

        var totalDecimal = panier.Items.Sum(i => i.Produit.Prix * i.Quantite);
        var totalCents = (long)(totalDecimal * 100);

        var chargeOptions = new ChargeCreateOptions
        {
            Amount = totalCents,
            Currency = "cad",
            Description = $"Commande - Boutique TP1",
            Source = stripeToken,
        };

        try
        {
            var chargeService = new ChargeService();
            Charge charge = chargeService.Create(chargeOptions);

            if (charge.Status == "succeeded")
            {
                var commande = new Commande
                {
                    UtilisateurId = userId.Value,
                    Date = DateTime.Now,
                    Total = totalDecimal,
                    Statut = StatutCommande.Terminee,
                    PaiementId = charge.Id,
                };

                foreach (var item in panier.Items)
                {
                    commande.Lignes.Add(new LigneCommande
                    {
                        ProduitId = item.ProduitId,
                        Quantite = item.Quantite,
                        PrixUnitaire = item.Produit.Prix,
                    });
                }

                _context.Commandes.Add(commande);
                _context.SaveChanges();

                var facture = new Facture
                {
                    ClientId = userId.Value,
                    CommandeId = commande.Id,
                    MontantTotal = totalDecimal,
                    PaiementId = charge.Id,
                    DateFacture = DateTime.Now,
                };

                _context.Factures.Add(facture);

                _context.PanierItems.RemoveRange(panier.Items);
                _context.SaveChanges();

                return RedirectToAction("Success", new { factureId = facture.Id });
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }
        catch (StripeException e)
        {
            TempData["ErrorMessage"] = e.StripeError?.Message ?? "Paiement refusé.";
            return RedirectToAction("Failed");
        }
    }

    public IActionResult Failed()
    {
        return View();
    }

    public IActionResult Success(int? factureId)
    {
        ViewBag.FactureId = factureId;
        return View();
    }
}