using Microsoft.AspNetCore.Mvc;
using CelsiaAssetsment.Data;
using CelsiaAssetsment.Models;
namespace CelsiaAssetsment.Controllers
{
    public class AuthController : Controller
    {
        private readonly CelsiaAssetsmentContext _context;

        public AuthController (CelsiaAssetsmentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                // Verificar si los campos no son nulos
                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                {
                    Console.WriteLine("Email and Password are required");
                    return RedirectToAction("Index");
                } else
                {
                    // Buscar el correo que se ingresa
                    var userFound = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                    Console.WriteLine(user.Email);
                    Console.WriteLine(user.Password);

                    // Si no se encuentra el usuario
                    if (userFound == null)
                    {
                        Console.WriteLine("Email not found");
                        return RedirectToAction("Index");
                    } else
                    {
                        // Si la contraseña no coincide
                        if (userFound.Password != user.Password)
                        {
                            Console.WriteLine("Password is incorrect");
                            return RedirectToAction("Index");
                        } else
                        {
                            // Si la contraseña es correcta, ir al home
                            Console.WriteLine("Password is CORRECT");
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Index");
            }
        }
    }
}