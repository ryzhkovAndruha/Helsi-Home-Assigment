using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Handlers.Queries;

public record class GetTaskListByIdQuery(string TaskListId, string UserId): IQuery<TaskList?>;