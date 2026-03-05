using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using tp1.Settings;
using Stripe;

public class PaymentController : Controller
{
    private readonly StripeSettings _stripeSettings;

    public PaymentController(IOptions<StripeSettings> stripeSettings)
    {
        _stripeSettings = stripeSettings.Value;
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
        var chargeOptions = new ChargeCreateOptions
        {
            Amount = 5000,
            Currency = "cad",
            Description = "Exemple de paiement",
            Source = stripeToken,
            // Metadata = new Dictionary<string, string>
            // {
            //     { "customer_name", "John Doe" },
            //     { "customer_email", "john.doe@example.com" },
            //     { "customer_address", "123 Main St, Anytown, USA" },
            //     { "customer_city", "Lévis" },
            //     { "customer_state", "CA" },
            //     { "customer_postal_code", "G5L A4L" },
            // },
        };
        try
        {
            var chargeService = new ChargeService();
            Charge charge = chargeService.Create(chargeOptions);

            if (charge.Status == "succeeded")
            {
                return RedirectToAction("Success");
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

    public IActionResult Success()
    {
        return View();
    }
}