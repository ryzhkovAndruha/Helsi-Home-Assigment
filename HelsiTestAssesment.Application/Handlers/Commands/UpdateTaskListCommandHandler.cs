using AutoMapper;
using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public class UpdateTaskListCommandHandler(ITaskListRepository taskListRepository, IMapper mapper) : ICommandHandler<UpdateTaskListCommand>
{
    public async Task Handle(UpdateTaskListCommand command, CancellationToken cancellationToken)
    {
        var taskList = mapper.Map<TaskList>(command.UpdateTaskListDto);

        await taskListRepository.UpdateAsync(taskList, command.UserId, cancellationToken);
    }
}
