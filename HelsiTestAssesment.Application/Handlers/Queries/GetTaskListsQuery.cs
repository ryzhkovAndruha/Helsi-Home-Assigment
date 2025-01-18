using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Handlers.Queries;

public record class GetTaskListsQuery(string UserId): IQuery<IEnumerable<TaskList>?>;
