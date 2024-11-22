using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoServerApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<TaskItem> TaskItems { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Настройка приоритета
            builder.Entity<Priority>().HasData(
                new Priority { Id = 1, Name = "Низкий", Weight = 1 },
                new Priority { Id = 2, Name = "Средний", Weight = 2 },
                new Priority { Id = 3, Name = "Высокий", Weight = 3 }
            );

            // Настройка задачи
            builder.Entity<TaskItem>()
                .Property(t => t.PriorityId)
                .HasDefaultValue(1); // Значение по умолчанию для PriorityId

            builder.Entity<TaskItem>()
                .HasOne(t => t.Priority)
                .WithMany()
                .HasForeignKey(t => t.PriorityId)
                .OnDelete(DeleteBehavior.Cascade); // Удаление связанных задач при удалении приоритета

            builder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, Title = "Задача 1", Description = "Описание 1", CreatedDate = DateTime.UtcNow, PriorityId = 1 },
                new TaskItem { Id = 2, Title = "Задача 2", Description = "Описание 2", CreatedDate = DateTime.UtcNow, PriorityId = 2 },
                new TaskItem { Id = 3, Title = "Задача 3", Description = "Описание 3", CreatedDate = DateTime.UtcNow, PriorityId = 3 }
            );
        }
    }
}
