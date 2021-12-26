using System;
using System.Collections.Generic;
using ProjectRepository.Models;
namespace ProjectRepository.Repository
{
    public interface ITasksRepository
    {
        void CreateTasks(Tasks tasks);
        List<Tasks> GetAllTasks();
        Tasks GetTasksFromId(int id);
        void UpdateTasks(Tasks tasks);
    }
}
