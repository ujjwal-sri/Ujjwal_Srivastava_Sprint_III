using System;
using System.Collections.Generic;
using ProjectRepository.Models;
namespace ProjectRepository.Repository
{
    public interface IProjectRepository
    {
        void CreateProject(Project project);
        List<Project> GetAllProject();
        Project GetProjectFromId(int id);
        void UpdateProject(Project project);
    }
}
