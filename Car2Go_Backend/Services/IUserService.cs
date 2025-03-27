using Car2Go.DTOs;
using Car2Go.Models;

namespace Car2Go.Services
{
    public interface IUserService
    {
        public List<UserDto> GetUsers();
        public UserDto GetUserByEmail(string email);
        public UserDto CreateUser(UserDto userDto);
        public bool UpdateUser(UpdateUserDto userDto, string email);
        public bool DeleteUser(string email);
        public bool DeleteUserAccount(string email);
        public bool resetUserPassword(resetPasswordDto resetPasswordDto, string email);

        public List<UserDto> GetUsersbyRoleUser();
        public List<UserDto> GetUsersByRoleAgent();

    }
}
