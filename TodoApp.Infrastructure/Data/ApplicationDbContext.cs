using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;
using Task = TodoApp.Core.Entities.Task;

namespace TodoApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Task entity
            modelBuilder.Entity<Task>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Task>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Task>()
                .Property(t => t.Description)
                .HasMaxLength(500);
        }
    }
} 