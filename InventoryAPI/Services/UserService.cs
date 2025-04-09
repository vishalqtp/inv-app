using InventoryAPI.Models;
using InventoryTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace InventoryAPI.Services
{
    public class UserService : IUserService
    {
        private readonly InventoryDbContext _context;

        public UserService(InventoryDbContext context)
        {
            _context = context;
        }

        public User? GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

       public void CreateUser(string username, string password)
{
    string salt;
    string passwordHash = HashPassword(password, out salt);
    var user = new User { Username = username, PasswordHash = passwordHash, Salt = salt };
    _context.Users.Add(user);
    _context.SaveChanges();
}
public bool VerifyPassword(User user, string password)
{
    // Get the salt from the stored user record
    string storedSalt = user.Salt ?? throw new ArgumentNullException(nameof(user.Salt), "Salt cannot be null.");

    using (var hmac = new HMACSHA512(Convert.FromBase64String(storedSalt)))
    {
        // Recompute the password hash with the stored salt
        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        // Compare the computed hash with the stored password hash
        return user.PasswordHash == Convert.ToBase64String(computedHash);
    }
}



       private string HashPassword(string password, out string salt)
{
    // Generate a random salt
    byte[] saltBytes = new byte[16];
    RandomNumberGenerator.Fill(saltBytes);
    salt = Convert.ToBase64String(saltBytes);  // Save the salt as a base64 string
    
    using (var hmac = new HMACSHA512(saltBytes))
    {
        // Hash the password with the salt
        byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(passwordHash);  // Return the password hash
    }
}


    }


 }

