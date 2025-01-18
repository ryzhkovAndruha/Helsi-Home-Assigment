using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public class DeleteTaskListCommandHandler(ITaskListRepository taskListRepository) : ICommandHandler<DeleteTaskListCommand>
{
    public async Task Handle(DeleteTaskListCommand command, CancellationToken cancellationToken)
    {
        await taskListRepository.DeleteAsync(command.TaskListId, command.UserId, cancellationToken);
    }
}
