using AutoMapper;
using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.DTOs;
using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public class UpdateTaskListCommandHandler(ITaskListRepository taskListRepository, IMapper mapper) : ICommandHandler<UpdateTaskListCommand>
{
    public async Task<CommandOperationResultDto> Handle(UpdateTaskListCommand command, CancellationToken cancellationToken)
    {
        var taskList = mapper.Map<TaskList>(command.UpdateTaskListDto);
        taskList.Id = command.Id;

        var result = await taskListRepository.UpdateAsync(taskList, command.UserId, cancellationToken);

        if (result == null)
        {
            return new CommandOperationResultDto
            {
                Success = false,
                ErrorMessage = $"Failed to update task list {command.Id} by user {command.UserId}"
            };
        }

        return new CommandOperationResultDto
        {
            Success = true,
            Data = result
        };
    }
}
