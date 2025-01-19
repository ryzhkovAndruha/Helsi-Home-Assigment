using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.DTOs;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public class DeleteTaskListCommandHandler(ITaskListRepository taskListRepository) : ICommandHandler<DeleteTaskListCommand>
{
    public async Task<CommandOperationResultDto> Handle(DeleteTaskListCommand command, CancellationToken cancellationToken)
    {
        var result = await taskListRepository.DeleteAsync(command.TaskListId, command.UserId, cancellationToken);

        return new CommandOperationResultDto
        {
            Success = result,
            ErrorMessage = result ? null : $"Failed to delete task list {command.TaskListId} by user {command.UserId}"
        };
    }
}
