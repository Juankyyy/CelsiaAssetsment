using Microsoft.AspNetCore.Mvc;
using CelsiaAssetsment.Data;
using CelsiaAssetsment.Models;
using CelsiaAssetsment.Services;

namespace CelsiaAssetsment.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly CelsiaAssetsmentContext _context;

        public TransactionsController (CelsiaAssetsmentContext context, UserRepository userRepository)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Auth");
            }

            var transactions = _context.Transactions.ToList();
            return View(transactions);
        }

        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Auth");
            }

            return View();
        }
    }
}