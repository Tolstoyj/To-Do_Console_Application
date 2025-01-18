using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using HelloRider.Models;

namespace HelloRider.Services
{
    public class TaskManager
    {
        private List<TodoTask> _tasks;
        private readonly string _filePath;

        public TaskManager()
        {
            _tasks = new List<TodoTask>();
            _filePath = "tasks.json";
            LoadTasks();
        }

        public void AddTask(string description, DateTime? dueDate = null)
        {
            _tasks.Add(new TodoTask(description, dueDate));
            SaveTasks();
        }

        public List<TodoTask> GetAllTasks(bool sortByCompletion = true)
        {
            if (sortByCompletion)
            {
                return _tasks
                    .OrderBy(t => t.IsCompleted)
                    .ThenBy(t => t.DueDate ?? DateTime.MaxValue)
                    .ThenBy(t => t.CreatedAt)
                    .ToList();
            }
            return _tasks;
        }

        public void DeleteAllTasks()
        {
            _tasks.Clear();
            SaveTasks();
        }

        public bool UpdateTask(int index, string newDescription, DateTime? newDueDate = null)
        {
            if (index >= 0 && index < _tasks.Count)
            {
                _tasks[index].Description = newDescription;
                _tasks[index].DueDate = newDueDate;
                SaveTasks();
                return true;
            }
            return false;
        }

        public bool UpdateTaskDueDate(int index, DateTime? newDueDate)
        {
            if (index >= 0 && index < _tasks.Count)
            {
                _tasks[index].DueDate = newDueDate;
                SaveTasks();
                return true;
            }
            return false;
        }

        public bool MarkTaskAsCompleted(int index)
        {
            if (index >= 0 && index < _tasks.Count)
            {
                _tasks[index].IsCompleted = true;
                SaveTasks();
                return true;
            }
            return false;
        }

        public bool DeleteTask(int index)
        {
            if (index >= 0 && index < _tasks.Count)
            {
                _tasks.RemoveAt(index);
                SaveTasks();
                return true;
            }
            return false;
        }

        private void SaveTasks()
        {
            string jsonString = JsonSerializer.Serialize(_tasks);
            File.WriteAllText(_filePath, jsonString);
        }

        private void LoadTasks()
        {
            if (File.Exists(_filePath))
            {
                string jsonString = File.ReadAllText(_filePath);
                _tasks = JsonSerializer.Deserialize<List<TodoTask>>(jsonString) ?? new List<TodoTask>();
            }
        }
    }
} 