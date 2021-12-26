using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectRepository.Models;
using ProjectRepository.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ujjwal_Srivastava_SprintIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("/createuser")]
        public ActionResult CreateUser(User user)
        {
            if (_userRepository.GetAllUsers().FirstOrDefault(x => x.ID == user.ID) == null)
            {
                _userRepository.CreateUser(user);
                return Ok("User created successfully");
            }
            else
            {
                return BadRequest("ID must be unique");
            }
        }

        [HttpGet]
        [Route("/getuser")]
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(_userRepository.GetAllUsers());
        }

        [HttpGet]
        [Route("/getuser/{id}")]
        public ActionResult<User> GetUserFromId(int id)
        {
            var user = _userRepository.GetAllUsers().FirstOrDefault(x => x.ID == id);
            if (user == null)
            {
                return BadRequest("user doesn't exist");
            }
            return Ok(_userRepository.GetUserFromId(id));
        }

        [HttpPut]
        [Route("/updateuser")]
        public ActionResult<User> UpdateUser(User user)
        {
            var updateUser = _userRepository.GetAllUsers().FirstOrDefault(x => x.ID == user.ID);
            if (updateUser == null)
            {
                return BadRequest("user doesn't exist");
            }
            _userRepository.UpdateUser(user);
            return Ok();
        }

        [HttpGet]
        [Route("/loginuser/{email}/{pwd}")]
        public ActionResult ValidateUser(string email, string pwd)
        {
            var user = _userRepository.ValidateUser(email, pwd);
            if (user != null && user.Password == pwd)
            {
                return Ok("User validated");
            }
            else return BadRequest("Invalid Operation");
        }
    }
}
