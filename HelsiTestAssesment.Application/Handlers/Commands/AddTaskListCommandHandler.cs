using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public class AddTaskListCommandHandler(ITaskListRepository taskListRepository) : ICommandHandler<AddTaskListCommand>
{
    public async Task Handle(AddTaskListCommand command, CancellationToken cancellationToken)
    {
        var taskList = new TaskList()
        {
            Name = command.CreateTaskListDto.Name,
            AccessibleUserIds = command.CreateTaskListDto.AccessibleUserIds,
            Tasks = command.CreateTaskListDto.Tasks,
            OwnerId = command.UserId,
            CreatedAt = DateTime.UtcNow,
        };

        await taskListRepository.AddAsync(taskList, cancellationToken);
    }
}
