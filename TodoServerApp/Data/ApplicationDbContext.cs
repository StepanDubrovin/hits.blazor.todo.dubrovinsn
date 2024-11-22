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

            // ��������� ����������
            builder.Entity<Priority>().HasData(
                new Priority { Id = 1, Name = "������", Weight = 1 },
                new Priority { Id = 2, Name = "�������", Weight = 2 },
                new Priority { Id = 3, Name = "�������", Weight = 3 }
            );

            // ��������� ������
            builder.Entity<TaskItem>()
                .Property(t => t.PriorityId)
                .HasDefaultValue(1); // �������� �� ��������� ��� PriorityId

            builder.Entity<TaskItem>()
                .HasOne(t => t.Priority)
                .WithMany()
                .HasForeignKey(t => t.PriorityId)
                .OnDelete(DeleteBehavior.Cascade); // �������� ��������� ����� ��� �������� ����������

            builder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, Title = "������ 1", Description = "�������� 1", CreatedDate = DateTime.UtcNow, PriorityId = 1 },
                new TaskItem { Id = 2, Title = "������ 2", Description = "�������� 2", CreatedDate = DateTime.UtcNow, PriorityId = 2 },
                new TaskItem { Id = 3, Title = "������ 3", Description = "�������� 3", CreatedDate = DateTime.UtcNow, PriorityId = 3 }
            );
        }
    }
}
