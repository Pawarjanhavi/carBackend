using Car2Go.Models;

namespace Car2Go.Service
{
    public interface ILoginService
    {
        public bool LoginUser(string Email, string Password);
    }
}
