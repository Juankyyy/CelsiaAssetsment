using Microsoft.AspNetCore.Mvc;
using CelsiaAssetsment.Data;
using CelsiaAssetsment.Models;
using CelsiaAssetsment.Services;

namespace CelsiaAssetsment.Controllers
{
    public class UsersController : Controller
    {
        private readonly CelsiaAssetsmentContext _context;

        private readonly UserRepository _userRepository;

        public UsersController (CelsiaAssetsmentContext context, UserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Auth");
            }

            var users = _context.Users.ToList();
            return View(users);
        }

    }
}