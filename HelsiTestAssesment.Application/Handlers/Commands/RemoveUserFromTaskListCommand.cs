using HelsiTestAssesment.Application.Core.Interfaces.CQRS;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public record class RemoveUserFromTaskListCommand(string TaskListId, string UserToRemove, string UserId) : ICommand;