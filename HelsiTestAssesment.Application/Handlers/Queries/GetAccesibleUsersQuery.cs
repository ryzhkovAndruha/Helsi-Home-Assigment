using HelsiTestAssesment.Application.Core.Interfaces.CQRS;

namespace HelsiTestAssesment.Application.Handlers.Queries;

public record class GetAccesibleUsersQuery(string TaskListId, string UserId): IQuery<IEnumerable<string>?>;