using System;
using System.Linq;
using System.Collections.Generic;
using ProjectRepository.Models;
using ProjectRepository.DBAccess;

namespace ProjectRepository.Repository
{
    public class TasksRepository : ITasksRepository
    {
        private readonly DBContext _context;
        public TasksRepository(DBContext context)
        {
            _context = context;
        }

        public void CreateTasks(Tasks tasks)
        {
            _context.Tasks.Add(tasks);
            _context.SaveChanges();
        }

        public List<Tasks> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public Tasks GetTasksFromId(int id)
        {
            return _context.Tasks.FirstOrDefault(x => x.ID == id);
        }

        public void UpdateTasks(Tasks tasks)
        {
            var updateTask = _context.Tasks.FirstOrDefault(x => x.ID == tasks.ID);
            updateTask.ProjectID = tasks.ProjectID;
            updateTask.Status = tasks.Status;
            updateTask.AssiignedToUserID = tasks.AssiignedToUserID;
            updateTask.Detail = tasks.Detail;
            updateTask.CreatedOn = tasks.CreatedOn;
            _context.SaveChanges();
        }
    }
}
