using System;
using System.Collections.Generic;
using ProjectRepository.Models;
namespace ProjectRepository.Repository
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        List<User> GetAllUsers();
        User GetUserFromId(int id);
        void UpdateUser(User user);
        User ValidateUser(string email, string pwd);
    }
}
