using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.DTOs;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public record class AddTaskListCommand(CreateTaskListDto CreateTaskListDto, string UserId) : ICommand;
