using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Core.Entities;
using Task = TodoApp.Core.Entities.Task;

namespace TodoApp.Core.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetAllAsync();
        Task<Task> GetByIdAsync(Guid id);
        Task<Task> CreateAsync(Task task);
        Task<Task> UpdateAsync(Task task);
        Task<bool> DeleteAsync(Guid id);
    }
} 