using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.DTOs;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public class AddUserToTaskListCommandHandler(ITaskListRepository taskListRepository) : ICommandHandler<AddUserToTaskListCommand>
{
    public async Task<CommandOperationResultDto> Handle(AddUserToTaskListCommand command, CancellationToken cancellationToken)
    {
        var taskList = await taskListRepository.AddUserToTaskListAsync(command.TaskListId,
            command.UserToAdd, command.UserId, cancellationToken);

        if (taskList == null)
        {
            return new CommandOperationResultDto
            {
                Success = false,
                ErrorMessage = $"Failed to add user {command.UserToAdd} to task list {command.TaskListId}"
            };
        }

        return new CommandOperationResultDto
        {
            Success = false,
            Data = taskList
        };

    }
}
