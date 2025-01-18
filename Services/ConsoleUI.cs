using System;
using System.Collections.Generic;
using HelloRider.Models;

namespace HelloRider.Services
{
    public class ConsoleUI
    {
        private readonly TaskManager _taskManager;

        public ConsoleUI(TaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Task Manager ===");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. View tasks");
                Console.WriteLine("3. Mark task as completed");
                Console.WriteLine("4. Update task");
                Console.WriteLine("5. Update due date");
                Console.WriteLine("6. Delete task");
                Console.WriteLine("7. Delete all tasks");
                Console.WriteLine("8. Exit");
                Console.WriteLine("==================");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                ProcessChoice(choice);
            }
        }

        private void ProcessChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    DisplayTasks();
                    break;
                case "3":
                    MarkTaskAsCompleted();
                    break;
                case "4":
                    UpdateTask();
                    break;
                case "5":
                    UpdateDueDate();
                    break;
                case "6":
                    DeleteTask();
                    break;
                case "7":
                    DeleteAllTasks();
                    break;
                case "8":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        private DateTime? ReadDueDate()
        {
            Console.WriteLine("Would you like to add a due date? (Y/N):");
            if (Console.ReadLine()?.Trim().ToUpper() == "Y")
            {
                Console.WriteLine("Enter due date (MM/dd/yyyy):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
                {
                    return dueDate;
                }
                Console.WriteLine("Invalid date format. No due date will be set.");
            }
            return null;
        }

        private void UpdateDueDate()
        {
            DisplayTasks();
            Console.WriteLine("\nEnter the task number to update due date:");
            if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0)
            {
                DateTime? newDueDate = ReadDueDate();
                if (_taskManager.UpdateTaskDueDate(taskNumber - 1, newDueDate))
                {
                    Console.WriteLine("Due date updated successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid task number!");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
            WaitForKey();
        }

        private void UpdateTask()
        {
            DisplayTasks();
            Console.WriteLine("\nEnter the task number to update:");
            if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0)
            {
                Console.WriteLine("Enter new description for the task:");
                string newDescription = Console.ReadLine();
                
                if (!string.IsNullOrWhiteSpace(newDescription))
                {
                    DateTime? newDueDate = ReadDueDate();
                    if (_taskManager.UpdateTask(taskNumber - 1, newDescription, newDueDate))
                    {
                        Console.WriteLine("Task updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid task number!");
                    }
                }
                else
                {
                    Console.WriteLine("Task description cannot be empty!");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
            WaitForKey();
        }

        private void AddTask()
        {
            Console.WriteLine("\nEnter task description:");
            string description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
            {
                DateTime? dueDate = ReadDueDate();
                _taskManager.AddTask(description, dueDate);
                Console.WriteLine("Task added successfully!");
            }
            else
            {
                Console.WriteLine("Task cannot be empty!");
            }
            WaitForKey();
        }

        private void DisplayTasks()
        {
            var tasks = _taskManager.GetAllTasks(true);
            Console.WriteLine("\nCurrent Tasks:");
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
            }
            else
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    string status = tasks[i].IsCompleted ? "[✓]" : "[ ]";
                    string dueDate = tasks[i].DueDate.HasValue 
                        ? $"(Due: {tasks[i].DueDate.Value:MM/dd/yyyy})"
                        : "";
                    Console.WriteLine($"{i + 1}. {status} {tasks[i].Description} {dueDate}");
                }
            }
            WaitForKey();
        }

        private void MarkTaskAsCompleted()
        {
            DisplayTasks();
            Console.WriteLine("\nEnter the task number to mark as completed:");
            if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0)
            {
                if (_taskManager.MarkTaskAsCompleted(taskNumber - 1))
                {
                    Console.WriteLine("Task marked as completed!");
                }
                else
                {
                    Console.WriteLine("Invalid task number!");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
            WaitForKey();
        }

        private void DeleteTask()
        {
            DisplayTasks();
            Console.WriteLine("\nEnter the task number to delete:");
            if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0)
            {
                if (_taskManager.DeleteTask(taskNumber - 1))
                {
                    Console.WriteLine("Task deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid task number!");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
            WaitForKey();
        }

        private void DeleteAllTasks()
        {
            DisplayTasks();
            Console.WriteLine("\nAre you sure you want to delete ALL tasks? This action cannot be undone! (Y/N):");
            if (Console.ReadLine()?.Trim().ToUpper() == "Y")
            {
                _taskManager.DeleteAllTasks();
                Console.WriteLine("All tasks have been deleted successfully!");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
            WaitForKey();
        }

        private void WaitForKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
} 