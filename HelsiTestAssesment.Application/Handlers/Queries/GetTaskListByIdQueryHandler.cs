using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Handlers.Queries;

public class GetTaskListByIdQueryHandler(ITaskListRepository taskListRepository) : IQueryHandler<GetTaskListByIdQuery, TaskList?>
{
    public async Task<TaskList?> Handle(GetTaskListByIdQuery query, CancellationToken cancellationToken)
    {
        return await taskListRepository.GetByIdAsync(query.TaskListId, query.UserId, cancellationToken);
    }
}
