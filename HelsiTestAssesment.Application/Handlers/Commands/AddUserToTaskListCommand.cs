using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.DTOs;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public record class AddUserToTaskListCommand(ModifyTaskListUserDto AddUserToTaskListDto, string UserId) : ICommand;
