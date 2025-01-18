using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Core.Interfaces;

public interface ITaskListRepository
{
    Task<IEnumerable<TaskList>?> GetAllAsync(string userId, CancellationToken cancellationToken);
    Task<TaskList?> GetByIdAsync(string taskListId, string userId, CancellationToken cancellationToken);
    Task AddAsync(TaskList taskList, CancellationToken cancellationToken);
    Task<TaskList?> UpdateAsync(TaskList taskList, string userId, CancellationToken cancellationToken);
    Task DeleteAsync(string taskListId, string userId, CancellationToken cancellationToken);
}
