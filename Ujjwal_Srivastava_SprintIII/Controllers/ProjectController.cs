using System;
using System.Collections.Generic;
using System.Linq;
using ProjectRepository.Models;
using ProjectRepository.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ujjwal_Srivastava_SprintIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


        [HttpPost]
        [Route("/createproject")]
        public ActionResult CreateProject(Project Project)
        {
            if (_projectRepository.GetAllProject().FirstOrDefault(x => x.ID == Project.ID) == null)
            {
                _projectRepository.CreateProject(Project);
                return Ok("Project created successfully");
            }
            else
            {
                return BadRequest("ID must be unique");
            }
        }

        [HttpGet]
        [Route("/getproject")]
        public ActionResult<List<Project>> GetAllProjects()
        {
            return Ok(_projectRepository.GetAllProject());
        }

        [HttpGet]
        [Route("/getproject/{id}")]
        public ActionResult<Project> GetProjectFromId(int id)
        {
            var Project = _projectRepository.GetAllProject().FirstOrDefault(x => x.ID == id);
            if (Project == null)
            {
                return BadRequest("Project doesn't exist");
            }
            return Ok(_projectRepository.GetProjectFromId(id));
        }

        [HttpPut]
        [Route("/updateproject")]
        public ActionResult<Project> UpdateProject(Project Project)
        {
            var updateProject = _projectRepository.GetAllProject().FirstOrDefault(x => x.ID == Project.ID);
            if (updateProject == null)
            {
                return BadRequest("Project doesn't exist");
            }
            _projectRepository.UpdateProject(Project);
            return Ok();
        }
    }
}
