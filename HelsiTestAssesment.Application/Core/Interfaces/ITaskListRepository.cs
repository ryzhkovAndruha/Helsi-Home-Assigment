using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Core.Interfaces;

public interface ITaskListRepository
{
    Task<IEnumerable<TaskList>?> GetAllAsync(string userId, CancellationToken cancellationToken = default);
    Task<TaskList?> GetByIdAsync(string taskListId, string userId, CancellationToken cancellationToken = default);
    Task AddAsync(TaskList taskList, CancellationToken cancellationToken = default);
    Task<TaskList?> UpdateAsync(TaskList taskList, string userId, CancellationToken cancellationToken = default);
    Task DeleteAsync(string taskListId, string userId, CancellationToken cancellationToken = default);
    Task<TaskList?> AddUserToTaskListAsync(string taskListId, string userToAddId, string ownerId, CancellationToken cancellationToken = default);
    Task<TaskList?> RemoveUserFromTaskListAsync(string taskListId, string userToDeleteId, string ownerId, CancellationToken cancellationToken = default);
    Task<IEnumerable<string>?> GetAccesibleUsers(string taskListId, string userId, CancellationToken cancellationToken = default);
}
