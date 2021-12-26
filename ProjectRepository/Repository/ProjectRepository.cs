using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ProjectRepository.DBAccess;
using ProjectRepository.Models;

namespace ProjectRepository.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DBContext _context;
        public ProjectRepository(DBContext context)
        {
            _context = context;
        }

        public void CreateProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            _context.Entry(project).State = EntityState.Detached;
        }

        public List<Project> GetAllProject()
        {
            return _context.Projects.ToList();
        }

        public Project GetProjectFromId(int id)
        {
            return _context.Projects.FirstOrDefault(x => x.ID == id);
        }

        public void UpdateProject(Project project)
        {
            var updateProj = _context.Projects.FirstOrDefault(x => x.ID == project.ID);
            updateProj.Name = project.Name;
            updateProj.Detail = project.Detail;
            updateProj.CreatedOn = project.CreatedOn;
            _context.SaveChanges();

        }
    }
}
