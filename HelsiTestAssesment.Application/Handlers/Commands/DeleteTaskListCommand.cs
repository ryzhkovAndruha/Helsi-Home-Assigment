using HelsiTestAssesment.Application.Core.Interfaces.CQRS;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public record class DeleteTaskListCommand(string TaskListId, string UserId): ICommand;