
using Car2Go.Models;
using Car2Go.Data;
using Car2Go.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Car2Go.Repository
{
    public class LoginRepository : ILoginService
    {
        private readonly Car2GoDBContext _dbContext;
        public LoginRepository(Car2GoDBContext dbContext)
        {
            _dbContext = dbContext;

        }

        public bool LoginUser(string email, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                // Log or return false if no user is found with that email
                Console.WriteLine($"No user found with email: {email}");
                return false;
            }

            // Compare passwords directly (case-sensitive)
            Console.WriteLine($"Comparing password: {password} with stored password: {user.Password}");

            if (user.Password == password)
            {
                return true; // Passwords match
            }
            else
            {
                // Log mismatch
                Console.WriteLine($"Password mismatch: {password} != {user.Password}");
                return false; // Password mismatch
            }
        }

    }
}
