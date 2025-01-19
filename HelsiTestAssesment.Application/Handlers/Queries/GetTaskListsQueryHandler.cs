using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.DTOs;
using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Handlers.Queries;

public class GetTaskListsQueryHandler(ITaskListRepository taskListRepository) : IQueryHandler<GetTaskListsQuery, PaginatedResult<TaskList>?>
{
    public async Task<PaginatedResult<TaskList>?> Handle(GetTaskListsQuery query, CancellationToken cancellationToken)
    {
        var taskLists = await taskListRepository.GetAllAsync(query.UserId, cancellationToken);

        if (taskLists == null || !taskLists.Any())
        {
            return null;
        }

        var totalCount = taskLists.Count();

        var pagedItems = taskLists
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return new PaginatedResult<TaskList>
        {
            Items = pagedItems,
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}
