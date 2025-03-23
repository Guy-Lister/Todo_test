using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Repositories;
using Todo = TodoApp.Core.Entities.Task;

namespace TodoApp.Tests;

public class TaskRepositoryTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;
    private readonly ApplicationDbContext _context;
    private readonly TaskRepository _repository;

    public TaskRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(_options);
        _repository = new TaskRepository(_context);
    }

    [Fact]
    public async Task Create_AddsTaskToDatabase()
    {
        // Arrange
        var task = new Todo("Test Task", "Test Description");

        // Act
        var result = await _repository.CreateAsync(task);

        // Assert
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal("Test Task", result.Title);
        Assert.Equal("Test Description", result.Description);
        Assert.False(result.IsCompleted);
        Assert.NotEqual(default, result.CreatedAt);
        Assert.Null(result.CompletedAt);

        var savedTask = await _context.Tasks.FindAsync(result.Id);
        Assert.NotNull(savedTask);
        Assert.Equal(result.Title, savedTask.Title);
    }

    [Fact]
    public async Task GetById_ReturnsCorrectTask()
    {
        // Arrange
        var task = new Todo("Test Task", "Test Description");
        await _repository.CreateAsync(task);

        // Act
        var result = await _repository.GetByIdAsync(task.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(task.Id, result.Id);
        Assert.Equal(task.Title, result.Title);
        Assert.Equal(task.Description, result.Description);
    }

    [Fact]
    public async Task GetAll_ReturnsAllTasks()
    {
        // Arrange
        var task1 = new Todo("Task 1", "Description 1");
        var task2 = new Todo("Task 2", "Description 2");
        await _repository.CreateAsync(task1);
        await _repository.CreateAsync(task2);

        // Act
        var results = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(2, results.Count());
        Assert.Contains(results, t => t.Id == task1.Id);
        Assert.Contains(results, t => t.Id == task2.Id);
    }

    [Fact]
    public async Task Update_ModifiesTaskProperties()
    {
        // Arrange
        var task = new Todo("Original Title", "Original Description");
        await _repository.CreateAsync(task);

        task.Description = "Updated Description";
        task.IsCompleted = true;
        task.CompletedAt = DateTime.UtcNow;

        // Act
        var result = await _repository.UpdateAsync(task);

        // Assert
        Assert.Equal("Updated Description", result.Description);
        Assert.True(result.IsCompleted);
        Assert.NotNull(result.CompletedAt);
    }

    [Fact]
    public async Task Delete_RemovesTaskFromDatabase()
    {
        // Arrange
        var task = new Todo("Test Task", "Test Description");
        await _repository.CreateAsync(task);

        // Act
        await _repository.DeleteAsync(task.Id);

        // Assert
        var result = await _repository.GetByIdAsync(task.Id);
        Assert.Null(result);
    }
} 