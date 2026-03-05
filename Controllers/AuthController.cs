using Microsoft.AspNetCore.Mvc;
using tp1.Data;
using tp1.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Auth/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Auth/Login
    [HttpPost]
    public IActionResult Login(string email, string motDePasse)
    {
        string hashedPassword = HashPassword(motDePasse);
        var utilisateur = _context.Utilisateurs.FirstOrDefault(u => u.Email == email && u.MotDePasse == hashedPassword);
        if (utilisateur == null)
        {
            ViewBag.Error = "Email ou mot de passe incorrect.";
            return View();
        }
        HttpContext.Session.SetInt32("UserId", utilisateur.Id);
        HttpContext.Session.SetString("UserRole", utilisateur.Role.ToString());
        HttpContext.Session.SetString("UserName", utilisateur.Prenom);
        return RedirectToAction("Index", "Home");
    }

    // GET: /Auth/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Auth/Register
    [HttpPost]
    public IActionResult Register(Utilisateur utilisateur,string motDePasse)
    {
        if (!ModelState.IsValid)
            return View(utilisateur);
        utilisateur.MotDePasse = HashPassword(motDePasse);
        // utilisateur.Role = TypeUtilisateur.Client;

        _context.Utilisateurs.Add(utilisateur);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    public async Task<IActionResult> Profil()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login");

        var utilisateur = await _context.Utilisateurs
            .FirstOrDefaultAsync(u => u.Id == userId.Value);
        
        if (utilisateur == null) return RedirectToAction("Login");

        return View(utilisateur);
    }
}