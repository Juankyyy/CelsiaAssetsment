using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CelsiaAssetsment.Models;
using System.Security.Claims;

namespace CelsiaAssetsment.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Auth");
        } else
        {
            ViewBag.Username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
        }
        return View();
    }

    public IActionResult Entities()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Auth");
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
