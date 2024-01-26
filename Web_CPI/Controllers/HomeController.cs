using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_CPI.Data;
using Web_CPI.Models;
using Web_CPI.SD;

namespace Web_CPI.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        if (SD_Login.Role == null)
        {
            TempData["Error"] = "Silahkan login untuk melanjutkan !!";

            return Redirect("Login");
        }

        return View();
    }

    public IActionResult Login()
    {
        Pengguna pg = new Pengguna();
        return View(pg);
    }

    public IActionResult Logout()
    {
        SD_Login.Role = null;

        TempData["Success"] = "Anda Telah Logout !";
        return Redirect("Login");
    }

    [HttpPost]
    public IActionResult Login(Pengguna png)
    {
        var check = _db.Penggunas.Where(x => x.Username == png.Username && x.Password == png.Password).FirstOrDefault();

        if (check != null)
        {
            var role = check.Role;

            SD_Login.Username = check.Username;
            SD_Login.Role = role;

            return RedirectToAction("Index");
        }
        else if (png.Username == null || png.Password == null)
        {

            return View();
        }

        TempData["error"] = "Password atau Username Salah !!";

        return View();
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

