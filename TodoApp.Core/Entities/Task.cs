using System;

namespace TodoApp.Core.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Title { get; private set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public Task(string title, string? description = null)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title), "Title cannot be null or empty");
            }
            
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            IsCompleted = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void Complete()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                CompletedAt = DateTime.UtcNow;
            }
        }
    }
} 