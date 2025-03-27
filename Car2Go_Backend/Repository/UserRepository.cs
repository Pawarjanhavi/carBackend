using Car2Go.Data;
using Car2Go.DTOs;
using Car2Go.Models;
using Car2Go.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Car2Go.Repository
{
    public class UserRepository : IUserService
    {
        private Car2GoDBContext _dbContext;
        public UserRepository(Car2GoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserDto> GetUsers()
        {
            List<User> users;

            List<UserDto> userList = new List<UserDto>();

            try
            {
                users = _dbContext.Users.ToList();
               
                foreach (User u  in users)
                {
                    userList.Add(new UserDto()
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        Password = u.Password,
                        PhoneNumber = u.PhoneNumber,
                        RoleType = u.RoleType
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return userList;
        }

        public UserDto CreateUser(UserDto userDto)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => 
            u.Email.Replace(" ", "").ToLower() == userDto.Email.Replace(" ", "").ToLower());
            
            UserDto result = new UserDto();

            try
            {
                //if (existingUser != null && userDto != null)
                //{
                //    UserDto check = new()
                //    {
                //        FirstName = existingUser.FirstName,
                //        LastName = existingUser.LastName,
                //        Email = existingUser.Email,
                //        Password = existingUser.Password,
                //        PhoneNumber = existingUser.PhoneNumber,
                //        RoleType = existingUser.RoleType
                //    };

                //    if (userDto == check)
                //    {
                //        result = new() { };
                //        return result;
                //    }
                //}

                //if (userDto == null)
                //{
                //    result = new() { };
                //    return result;
                //}

                if (existingUser != null)
                {
                    if (existingUser.Email == userDto.Email && existingUser.IsDeleted == false)
                    {
                        result = new() { };
                        return result;
                    }
                }
                if (existingUser != null && existingUser.IsDeleted == true)
                {
                    existingUser.FirstName = userDto.FirstName;
                    existingUser.LastName = userDto.LastName;
                    existingUser.Email = userDto.Email;
                    existingUser.Password = userDto.Password;
                    existingUser.PhoneNumber = userDto.PhoneNumber;
                    existingUser.RoleType = userDto.RoleType;
                    existingUser.IsDeleted = false;

                    _dbContext.Users.Update(existingUser);
                    _dbContext.SaveChanges();

                    result = new UserDto()
                    {
                        FirstName = existingUser.FirstName,
                        LastName = existingUser.LastName,
                        Email = existingUser.Email,
                        Password = existingUser.Password,
                        PhoneNumber = existingUser.PhoneNumber,
                        RoleType = existingUser.RoleType
                    };

                    return result;
                }
                if(existingUser == null) 
                {
                    User newUser = new()
                    {
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        Email = userDto.Email,
                        Password = userDto.Password,
                        PhoneNumber = userDto.PhoneNumber,
                        RoleType = userDto.RoleType
                    };

                    result = new UserDto()
                    {
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        Email = newUser.Email,
                        Password = newUser.Password,
                        PhoneNumber = newUser.PhoneNumber,
                        RoleType = newUser.RoleType
                    };

                    _dbContext.Users.Add(newUser);
                    _dbContext.SaveChanges();

                }
                    return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public bool UpdateUser(UpdateUserDto userDto, string email)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u=>u.Email.Replace(" ","").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());

            try
            {
                if (userDto == null || existingUser == null)
                {
                    return false;
                }

                existingUser.FirstName = userDto.FirstName;
                existingUser.LastName = userDto.LastName;
                existingUser.Email = userDto.Email;
                //existingUser.Password = userDto.Password;
                existingUser.PhoneNumber = userDto.PhoneNumber;
                //existingUser.RoleType = userDto.RoleType;

                _dbContext.Users.Update(existingUser);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteUser(string email)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u =>
            u.Email.Replace(" ", "").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());

            try
            {
                if (existingUser == null)
                {
                    return false;
                }
                _dbContext.Remove(existingUser);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public UserDto GetUserByEmail(string email)
        {
            UserDto result;

            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Email.Replace(" ", "").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());

            try
            {
                if (existingUser == null)
                {
                    result = new() { };
                    return result;
                }

                result = new()
                {
                    FirstName = existingUser.FirstName,
                    LastName = existingUser.LastName,
                    Email = existingUser.Email,
                    Password = existingUser.Password,
                    PhoneNumber = existingUser.PhoneNumber,
                    RoleType = existingUser.RoleType,
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool DeleteUserAccount(string email)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Email.Replace(" ", "").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());

            try
            {
                if (existingUser == null)
                {
                    return false;
                }
                existingUser.IsDeleted = true;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool resetUserPassword(resetPasswordDto resetPasswordDto, string email)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u =>
            u.Email.Replace(" ", "").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());


            try
            {
                if (existingUser == null)
                {
                    return false;
                }
                if (existingUser.Password == resetPasswordDto.oldPassword)
                {
                    existingUser.Password = resetPasswordDto.newPassword;
                     _dbContext.Users.Update(existingUser);
                    _dbContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        //*******************************************************************************************************************************
        public List<UserDto> GetUsersbyRoleUser()
        {
            List<UserDto> userList = new List<UserDto>();
            try
            {
                var users = _dbContext.Users
                    .Where(u => u.RoleType.Contains("User"));

                string userStatus;
                //UserDto result;

                foreach (var u in users)
                {
                    if (u.IsDeleted == false)
                    {
                        userStatus = "Active";
                    }
                    else
                    {
                        userStatus = "Deactivate";
                    }

                    userList.Add(new UserDto()
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        Password = u.Password,
                        PhoneNumber = u.PhoneNumber,
                        RoleType = u.RoleType,
                        AccountStatus = userStatus
                    });
                }
                return userList;


                //return users;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching users", ex);
            }
        }


        public List<UserDto> GetUsersByRoleAgent()
        {
            List<UserDto> agentList = new List<UserDto>();
            try
            {
                var agents = _dbContext.Users
                    .Where(u => u.RoleType.Contains("Agent")); // Filter by role type "Agent"

                string agentStatus;

                foreach (var a in agents)
                {
                    if (a.IsDeleted == false)
                    {
                        agentStatus = "Active";
                    }
                    else
                    {
                        agentStatus = "Deactivate";
                    }

                    agentList.Add(new UserDto()
                    {
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Email = a.Email,
                        Password = a.Password,
                        PhoneNumber = a.PhoneNumber,
                        RoleType = a.RoleType,
                        AccountStatus = agentStatus
                    });
                }
                return agentList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching agents", ex);
            }
        }

    }
}
