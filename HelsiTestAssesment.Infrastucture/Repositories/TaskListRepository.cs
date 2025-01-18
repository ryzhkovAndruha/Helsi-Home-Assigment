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

    public async Task DeleteAsync(string taskListId, string userId, CancellationToken cancellationToken)
    {
        var filterBuilder = Builders<TaskList>.Filter;

        var filter = filterBuilder.Eq(x => x.Id, taskListId) &
                     filterBuilder.Eq(x => x.OwnerId, userId);

        await _tasksList.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<IEnumerable<TaskList>?> GetAllAsync(string userId, CancellationToken cancellationToken)
    {
        var filterBuilder = Builders<TaskList>.Filter;

        var filter = filterBuilder.Eq(x => x.OwnerId, userId) |
                     filterBuilder.AnyEq(x => x.AccessibleUserIds, userId);

        return await _tasksList.Find(filter)
            .SortBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskList?> GetByIdAsync(string taskListId, string userId, CancellationToken cancellationToken)
    {
        var filterBuilder = Builders<TaskList>.Filter;

        var filter = filterBuilder.Eq(x => x.Id, taskListId) &
                     (filterBuilder.Eq(x => x.OwnerId, userId) |
                      filterBuilder.AnyEq(x => x.AccessibleUserIds, userId ));

        return await _tasksList.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TaskList?> UpdateAsync(TaskList taskList, string userId, CancellationToken cancellationToken)
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

    private UpdateDefinition<TaskList> GetUpdateDefinitionForTaskList(UpdateDefinitionBuilder<TaskList> updateBuilder, TaskList taskList)
    {
        var updates = new List<UpdateDefinition<TaskList>>
        {
            updateBuilder.Set(x => x.Name, taskList.Name),
            updateBuilder.Set(x => x.AccessibleUserIds, taskList.AccessibleUserIds)
        };
        return updateBuilder.Combine(updates);
    }
}
