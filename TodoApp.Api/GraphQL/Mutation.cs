using System;
using System.Threading.Tasks;
using HotChocolate;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using Task = TodoApp.Core.Entities.Task;

namespace TodoApp.Api.GraphQL
{
    public class Mutation
    {
        public async Task<Task> CreateTask(
            [Service] ITaskRepository repository,
            string title,
            string? description)
        {
            var task = new Task
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description ?? "",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            return await repository.CreateAsync(task);
        }

        public async Task<Task> UpdateTask(
            [Service] ITaskRepository repository,
            Guid id,
            string title,
            string description,
            bool isCompleted)
        {
            var task = await repository.GetByIdAsync(id);
            if (task == null)
            {
                throw new GraphQLException($"Task with ID {id} not found.");
            }

            task.Title = title;
            task.Description = description;
            task.IsCompleted = isCompleted;
            
            if (isCompleted && !task.CompletedAt.HasValue)
            {
                task.CompletedAt = DateTime.UtcNow;
            }
            else if (!isCompleted)
            {
                task.CompletedAt = null;
            }

            return await repository.UpdateAsync(task);
        }

        public async Task<Task> UpdateTaskStatus(
            [Service] ITaskRepository repository,
            Guid id,
            bool isCompleted)
        {
            var task = await repository.GetByIdAsync(id);
            if (task == null)
            {
                throw new GraphQLException($"Task with ID {id} not found.");
            }

            task.IsCompleted = isCompleted;
            if (isCompleted && !task.CompletedAt.HasValue)
            {
                task.CompletedAt = DateTime.UtcNow;
            }
            else if (!isCompleted)
            {
                task.CompletedAt = null;
            }

            return await repository.UpdateAsync(task);
        }

        public async Task<bool> DeleteTask(
            [Service] ITaskRepository repository,
            Guid id)
        {
            var task = await repository.GetByIdAsync(id);
            if (task == null)
            {
                throw new GraphQLException($"Task with ID {id} not found.");
            }

            return await repository.DeleteAsync(id);
        }
    }
}
