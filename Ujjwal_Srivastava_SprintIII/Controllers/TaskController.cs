using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectRepository.Models;
using ProjectRepository.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ujjwal_Srivastava_SprintIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITasksRepository _tasksRepository;

        public TaskController(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }


        [HttpPost]
        [Route("/createtask")]
        public ActionResult CreateTasks(Tasks Tasks)
        {
            if (_tasksRepository.GetAllTasks().FirstOrDefault(x => x.ID == Tasks.ID) == null)
            {
                _tasksRepository.CreateTasks(Tasks);
                return Ok("Tasks created successfully");
            }
            else
            {
                return BadRequest("ID must be unique");
            }
        }

        [HttpGet]
        [Route("/gettask")]
        public ActionResult<List<Tasks>> GetAllTaskss()
        {
            return Ok(_tasksRepository.GetAllTasks());
        }

        [HttpGet]
        [Route("/gettask/{id}")]
        public ActionResult<Tasks> GetTasksFromId(int id)
        {
            var Tasks = _tasksRepository.GetAllTasks().FirstOrDefault(x => x.ID == id);
            if (Tasks == null)
            {
                return BadRequest("Tasks doesn't exist");
            }
            return Ok(_tasksRepository.GetTasksFromId(id));
        }

        [HttpPut]
        [Route("/updatetask")]
        public ActionResult<Tasks> UpdateTasks(Tasks Tasks)
        {
            var updateTasks = _tasksRepository.GetAllTasks().FirstOrDefault(x => x.ID == Tasks.ID);
            if (updateTasks == null)
            {
                return BadRequest("Tasks doesn't exist");
            }
            _tasksRepository.UpdateTasks(Tasks);
            return Ok();
        }
    }
}
