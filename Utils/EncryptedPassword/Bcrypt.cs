namespace CelsiaAssetsment.Utils
{
    public class Bcrypt
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string encryptedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, encryptedPassword);
        }
    }
}