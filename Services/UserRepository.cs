using CelsiaAssetsment.Data;
using CelsiaAssetsment.Models;

namespace CelsiaAssetsment.Services
{
    public class UserRepository
    {
        private readonly CelsiaAssetsmentContext _context;

        public UserRepository (CelsiaAssetsmentContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            try
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    Console.WriteLine("Un usuario ya está usando este correo");
                } else
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
            } catch (Exception e)
            {
                Console.WriteLine($"Error al crear el usuario: {e.Message}");
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    Console.WriteLine("Un usuario ya está usando este correo");
                } else
                {
                    _context.Users.Update(user);
                    _context.SaveChanges();
                }
            } catch (Exception e)
            {
                Console.WriteLine($"Error al actualizar el usuario: {e.Message}");
            }
        }
    }
}