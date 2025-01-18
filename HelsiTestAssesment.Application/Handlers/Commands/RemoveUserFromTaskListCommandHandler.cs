using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;

namespace HelsiTestAssesment.Application.Handlers.Commands;

internal class RemoveUserFromTaskListCommandHandler(ITaskListRepository taskListRepository) : ICommandHandler<RemoveUserFromTaskListCommand>
{
    public async Task Handle(RemoveUserFromTaskListCommand command, CancellationToken cancellationToken)
    {
        await taskListRepository.RemoveUserFromTaskListAsync(command.TaskListId,
            command.UserToAdd, command.UserId, cancellationToken);
    }
}
