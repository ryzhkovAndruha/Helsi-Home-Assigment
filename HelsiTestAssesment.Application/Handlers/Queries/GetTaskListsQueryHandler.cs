using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Handlers.Queries;

public class GetTaskListsQueryHandler(ITaskListRepository taskListRepository) : IQueryHandler<GetTaskListsQuery, IEnumerable<TaskList>?>
{
    public async Task<IEnumerable<TaskList>?> Handle(GetTaskListsQuery query, CancellationToken cancellationToken)
    {
        return await taskListRepository.GetAllAsync(query.UserId, cancellationToken);
    }
}
