using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;

namespace HelsiTestAssesment.Application.Handlers.Queries;

public class GetAccesibleUsersQueryHandler(ITaskListRepository taskListRepository) : IQueryHandler<GetAccesibleUsersQuery, IEnumerable<string>?>
{
    public async Task<IEnumerable<string>?> Handle(GetAccesibleUsersQuery query, CancellationToken cancellationToken)
    {
        return await taskListRepository.GetAccesibleUsers(query.TaskListId, query.UserId, cancellationToken);
    }
}
