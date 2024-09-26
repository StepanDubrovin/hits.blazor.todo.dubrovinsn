using System.Collections.Generic;
using TodoServerApp.Data.Interfaces;

namespace TodoServerApp.Data.Services
{
    public class MemoryDataService : IDataService
    {
        private static IEnumerable<TaskItem> Tasks { get; } = [
            new() {Id = 1, Title = "Задача 1", Description = "Описание", CreatedDate = DateTime.Now },
            new() {Id = 1, Title = "Задача 2", Description = "Описание 2", CreatedDate = DateTime.Now },
            new() {Id = 1, Title = "Задача 3", Description = "Описание 3", CreatedDate = DateTime.Now },
        ];
        public async Task<IEnumerable<TaskItem>>GetTaskItemsAsync()
        {   
            await Task.Delay(1000);
            return await Task.FromResult(Tasks);
        }
    }
}
