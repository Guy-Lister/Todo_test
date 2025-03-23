using HotChocolate.Types;
using TodoApp.Core.Entities;
using Task = TodoApp.Core.Entities.Task;

namespace TodoApp.Api.GraphQL.Types
{
    public class TaskType : ObjectType<Task>
    {
        protected override void Configure(IObjectTypeDescriptor<Task> descriptor)
        {
            descriptor.Description("Represents a task item");

            descriptor.Field(t => t.Id)
                .Description("The unique identifier of the task");

            descriptor.Field(t => t.Title)
                .Description("The title of the task");

            descriptor.Field(t => t.Description)
                .Description("The description of the task");

            descriptor.Field(t => t.IsCompleted)
                .Description("Whether the task is completed");

            descriptor.Field(t => t.CreatedAt)
                .Description("When the task was created");

            descriptor.Field(t => t.CompletedAt)
                .Description("When the task was completed, if completed");
        }
    }
} 