using InventoryAPI.Models;

namespace InventoryAPI.Services
{
    public interface IUserService
    {
        User? GetUser(string username);
        void CreateUser(string username, string password);
        bool VerifyPassword(User user, string password);
    }
}
