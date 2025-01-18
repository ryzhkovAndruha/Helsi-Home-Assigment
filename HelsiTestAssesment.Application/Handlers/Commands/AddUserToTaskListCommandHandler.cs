using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public class AddUserToTaskListCommandHandler(ITaskListRepository taskListRepository) : ICommandHandler<AddUserToTaskListCommand>
{
    public async Task Handle(AddUserToTaskListCommand command, CancellationToken cancellationToken)
    {
        await taskListRepository.AddUserToTaskListAsync(command.TaskListId, 
            command.UserToAdd, command.UserId, cancellationToken);
    }
}
