using System;
using System.Linq;
using System.Collections.Generic;
using ProjectRepository.Models;
using ProjectRepository.DBAccess;
using Microsoft.EntityFrameworkCore;

namespace ProjectRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _context;

        public UserRepository(DBContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            _context.Entry(user).State = EntityState.Detached;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserFromId(int id)
        {
            return _context.Users.FirstOrDefault(x => x.ID == id);
        }

        public void UpdateUser(User user)
        {
            var updateUser = _context.Users.FirstOrDefault(x => x.ID == user.ID);
            updateUser.FirstName = user.FirstName;
            updateUser.SecondName = user.SecondName;
            updateUser.Email = user.Email;
            updateUser.Password = user.Password;
            _context.SaveChanges();
        }

        public User ValidateUser(string email, string pwd)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }
    }
}
