using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Domain;
using MongoDB.Driver;

namespace HelsiTestAssesment.Infrastucture.Repositories;

public class TaskListRepository(MongoDbContext context) : ITaskListRepository
{
    private const string CollectionName = "tasksList";

    private readonly IMongoCollection<TaskList> _tasksList = context.MongoDatabase.GetCollection<TaskList>(CollectionName);

    public async Task AddAsync(TaskList taskList, CancellationToken cancellationToken)
    {
        await _tasksList.InsertOneAsync(taskList, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(string taskListId, string userId, CancellationToken cancellationToken = default)
    {
        var filterBuilder = Builders<TaskList>.Filter;

        var filter = filterBuilder.Eq(x => x.Id, taskListId) &
                     filterBuilder.Eq(x => x.OwnerId, userId);

        await _tasksList.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<IEnumerable<TaskList>?> GetAllAsync(string userId, CancellationToken cancellationToken = default)
    {
        var filterBuilder = Builders<TaskList>.Filter;

        var filter = filterBuilder.Eq(x => x.OwnerId, userId) |
                     filterBuilder.AnyEq(x => x.AccessibleUserIds, userId);

        return await _tasksList.Find(filter)
            .SortBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>?> GetAccesibleUsers(string taskListId, string userId, CancellationToken cancellationToken = default)
    {
        var filterBuilder = Builders<TaskList>.Filter;

        var filter = filterBuilder.Eq(x => x.Id, taskListId) &
                     (filterBuilder.Eq(x => x.OwnerId, userId) |
                      filterBuilder.AnyEq(x => x.AccessibleUserIds, userId));

        return await _tasksList.Find(filter)
            .Project(x => x.AccessibleUserIds)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TaskList?> GetByIdAsync(string taskListId, string userId, CancellationToken cancellationToken = default)
    {
        var filterBuilder = Builders<TaskList>.Filter;

        var filter = filterBuilder.Eq(x => x.Id, taskListId) &
                     (filterBuilder.Eq(x => x.OwnerId, userId) |
                      filterBuilder.AnyEq(x => x.AccessibleUserIds, userId ));

        return await _tasksList.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TaskList?> UpdateAsync(TaskList taskList, string userId, CancellationToken cancellationToken = default)
    {
        var filterBuilder = Builders<TaskList>.Filter;
        var updateBuilder = Builders<TaskList>.Update;

        var update = GetUpdateDefinitionForTaskList(updateBuilder, taskList);
        var filter = filterBuilder.Eq(x => x.Id, taskList.Id) &
                     (filterBuilder.Eq(x => x.OwnerId, userId) |
                      filterBuilder.AnyEq(x => x.AccessibleUserIds, userId));

        var options = new FindOneAndUpdateOptions<TaskList>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await _tasksList.FindOneAndUpdateAsync(filter, update, options, cancellationToken);
    }

    public async Task<TaskList?> AddUserToTaskListAsync(string taskListId, string userToAddId, string ownerId, CancellationToken cancellationToken = default)
    {
        var update = Builders<TaskList>.Update.AddToSet(x => x.AccessibleUserIds, userToAddId);
        return await UpdateTaskListUsersAsync(taskListId, ownerId, update, cancellationToken);
    }

    public async Task<TaskList?> RemoveUserFromTaskListAsync(string taskListId, string userToDeleteId, string ownerId, CancellationToken cancellationToken = default)
    {
        var update = Builders<TaskList>.Update.Pull(x => x.AccessibleUserIds, userToDeleteId);
        return await UpdateTaskListUsersAsync(taskListId, ownerId, update, cancellationToken);
    }

    private async Task<TaskList?> UpdateTaskListUsersAsync(
    string taskListId,
    string ownerId,
    UpdateDefinition<TaskList> updateDefinition,
    CancellationToken cancellationToken = default)
    {
        var filterBuilder = Builders<TaskList>.Filter;

        var filter = filterBuilder.Eq(x => x.Id, taskListId) &
                     (filterBuilder.Eq(x => x.OwnerId, ownerId) |
                      filterBuilder.AnyEq(x => x.AccessibleUserIds, ownerId));

        var options = new FindOneAndUpdateOptions<TaskList>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await _tasksList.FindOneAndUpdateAsync(filter, updateDefinition, options, cancellationToken);
    }

    private UpdateDefinition<TaskList> GetUpdateDefinitionForTaskList(UpdateDefinitionBuilder<TaskList> updateBuilder, TaskList taskList)
    {
        var updates = new List<UpdateDefinition<TaskList>>
        {
            updateBuilder.Set(x => x.Name, taskList.Name),
            updateBuilder.Set(x => x.Tasks, taskList.Tasks)
        };

        return updateBuilder.Combine(updates);
    }
}
