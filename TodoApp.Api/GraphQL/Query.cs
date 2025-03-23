using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using Task = TodoApp.Core.Entities.Task;

namespace TodoApp.Api.GraphQL
{
    public class Query
    {
        [GraphQLName("tasks")]
        public async Task<IEnumerable<Task>> GetAllTasks([Service] ITaskRepository repository)
        {
            return await repository.GetAllAsync();
        }

        [GraphQLName("task")]
        public async Task<Task> GetTaskById([Service] ITaskRepository repository, Guid id)
        {
            return await repository.GetByIdAsync(id);
        }
    }
}
