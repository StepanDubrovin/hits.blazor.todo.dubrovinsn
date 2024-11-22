using Microsoft.EntityFrameworkCore;
using TodoServerApp.Data.Interfaces;

namespace TodoServerApp.Data.Services
{
	public class MSSQLDataService(ApplicationDbContext context) : IDataService
	{
        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await context.TaskItems
            .Include(t => t.Priority) // Подключаем приоритет
            .ToListAsync();
        }
        public async Task SaveAsync(TaskItem taskItem)
        {
            if (taskItem.Id == 0)
            {
                taskItem.CreatedDate = DateTime.Now;
                await context.TaskItems.AddAsync(taskItem);
            }
            else
            {
                context.TaskItems.Update(taskItem);
            }
            await context.SaveChangesAsync();
        }
        public async Task<TaskItem> GetTaskAsync(int id)
        {
           return await context.TaskItems
                .Include(t => t.Priority) // Подключаем приоритет
                .FirstOrDefaultAsync(x => x.Id == id); 
        }
        public async Task DeleteAsync(int id)
        {
            var taskItem = await context.TaskItems.FirstAsync(x => x.Id == id);
            context.TaskItems.Remove(taskItem);
            await context.SaveChangesAsync();
        }

        public Task<IEnumerable<TaskItem>> GetTaskItemsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Priority>> GetPrioritiesAsync()
        {
            return await context.Priorities.ToListAsync();
        }

        public async Task SavePriorityAsync(Priority priority)
        {
            if (priority.Id == 0)
            {
                await context.Priorities.AddAsync(priority);
            }
            else
            {
                context.Priorities.Update(priority);
            }
            await context.SaveChangesAsync();
        }

        public async Task DeletePriorityAsync(int id)
        {
            var priority = await context.Priorities.FirstAsync(x => x.Id == id);
            context.Priorities.Remove(priority);
            await context.SaveChangesAsync();
        }

    }
}
