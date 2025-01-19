using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.DTOs;

namespace HelsiTestAssesment.Application.Handlers.Commands;

internal class RemoveUserFromTaskListCommandHandler(ITaskListRepository taskListRepository) : ICommandHandler<RemoveUserFromTaskListCommand>
{
    public async Task<CommandOperationResultDto> Handle(RemoveUserFromTaskListCommand command, CancellationToken cancellationToken)
    {
        var taskList = await taskListRepository.RemoveUserFromTaskListAsync(command.TaskListId,
            command.UserToRemove, command.UserId, cancellationToken);

        if (taskList == null)
        {
            return new CommandOperationResultDto
            {
                Success = false,
                ErrorMessage = $"Failed to remove user {command.UserToRemove} to task list {command.TaskListId}"
            };
        }

        return new CommandOperationResultDto
        {
            Success = true,
            Data = taskList
        };
    }
}
