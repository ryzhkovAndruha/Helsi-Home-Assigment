using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.DTOs;
using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public record class AddUserToTaskListCommand(string TaskListId, string UserToAdd, string UserId) : ICommand;
