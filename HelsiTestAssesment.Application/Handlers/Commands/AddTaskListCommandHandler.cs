﻿using AutoMapper;
using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.DTOs;
using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public class AddTaskListCommandHandler(ITaskListRepository taskListRepository, IMapper mapper) : ICommandHandler<AddTaskListCommand>
{
    public async Task<CommandOperationResultDto> Handle(AddTaskListCommand command, CancellationToken cancellationToken)
    {
        var taskList = mapper.Map<TaskList>(command.CreateTaskListDto);

        taskList.OwnerId = command.UserId;
        taskList.CreatedAt = DateTime.UtcNow;

        await taskListRepository.AddAsync(taskList, cancellationToken);

        return new CommandOperationResultDto
        {
            Success = true,
            Data = taskList
        };
    }
}
