using System;

namespace HelloRider.Models
{
    public class TodoTask
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }

        public TodoTask(string description, DateTime? dueDate = null)
        {
            Description = description;
            IsCompleted = false;
            CreatedAt = DateTime.Now;
            DueDate = dueDate;
        }
    }
} 