using Microsoft.AspNetCore.Mvc;
using CelsiaAssetsment.Data;
using CelsiaAssetsment.Models;
using CelsiaAssetsment.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CelsiaAssetsment.Controllers
{
    public class AuthController : Controller
    {
        private readonly CelsiaAssetsmentContext _context;
        private readonly Bcrypt _bcrypt;

        public AuthController (CelsiaAssetsmentContext context, Bcrypt bcrypt)
        {
            _context = context;
            _bcrypt = bcrypt;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Signup()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {
                // Datos nulos
                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                {
                    Console.WriteLine("Email and Password are required");
                    return RedirectToAction("Signup");
                } else
                {
                    var userFound = _context.Users.FirstOrDefault(u => u.Email == user.Email);

                    // Correo ya existe
                    if (userFound != null)
                    {
                        Console.WriteLine("Email already exists");
                        return RedirectToAction("Signup");
                    } else
                    {
                        // Encriptar contraseña
                        user.Password = _bcrypt.HashPassword(user.Password);

                        _context.Users.Add(user);
                        _context.SaveChanges();
                        Console.WriteLine("User created");
                        return RedirectToAction("Index");
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Signup");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(User user)
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

                    // Si no se encuentra el usuario
                    if (userFound == null)
                    {
                        Console.WriteLine("Email not found");
                        return RedirectToAction("Index");
                    } else
                    {
                        if (_bcrypt.VerifyPassword(user.Password, userFound.Password))
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, userFound.Name),
                                new Claim(ClaimTypes.Email, userFound.Email)
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                            // Si la contraseña es correcta, ir al home
                            Console.WriteLine($"Password is correct: {userFound.Password}");
                            return RedirectToAction("Index", "Home");
                        } else
                        {
                            Console.WriteLine("Password is incorrect");
                            return RedirectToAction("Index");
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