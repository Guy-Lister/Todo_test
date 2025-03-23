using System;
using Xunit;
using Todo = TodoApp.Core.Entities.Task;

namespace TodoApp.Tests;

public class TaskTests
{
    [Fact]
    public void Task_Creation_SetsPropertiesCorrectly()
    {
        // Arrange
        var title = "Test Task";
        var description = "Test Description";

        // Act
        var task = new Todo(title, description);

        // Assert
        Assert.NotEqual(Guid.Empty, task.Id);
        Assert.Equal(title, task.Title);
        Assert.Equal(description, task.Description);
        Assert.False(task.IsCompleted);
        Assert.NotEqual(default, task.CreatedAt);
        Assert.Null(task.CompletedAt);
    }

    [Fact]
    public void Task_Complete_SetsCompletionProperties()
    {
        // Arrange
        var task = new Todo("Test Task");

        // Act
        task.Complete();

        // Assert
        Assert.True(task.IsCompleted);
        Assert.NotNull(task.CompletedAt);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Task_Creation_RequiresTitle(string? invalidTitle)
    {
        // Act & Assert
#pragma warning disable CS8604 // We are intentionally testing null input
        Assert.Throws<ArgumentNullException>(() => new Todo(invalidTitle));
#pragma warning restore CS8604
    }
} 