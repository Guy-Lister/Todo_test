using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using TodoApp.Infrastructure.Data;
using Task = TodoApp.Core.Entities.Task;

namespace TodoApp.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Task>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Task> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<Task> CreateAsync(Task task)
        {
            task.Id = Guid.NewGuid();
            task.CreatedAt = DateTime.UtcNow;
            
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            
            return task;
        }

        public async Task<Task> UpdateAsync(Task task)
        {
            if (task.IsCompleted && !task.CompletedAt.HasValue)
            {
                task.CompletedAt = DateTime.UtcNow;
            }
            else if (!task.IsCompleted)
            {
                task.CompletedAt = null;
            }
            
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return task;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}
