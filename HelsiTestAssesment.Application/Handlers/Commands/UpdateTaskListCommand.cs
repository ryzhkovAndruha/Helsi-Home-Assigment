﻿using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.DTOs;

namespace HelsiTestAssesment.Application.Handlers.Commands;

public record class UpdateTaskListCommand(string Id, UpdateTaskListDto UpdateTaskListDto, string UserId) : ICommand;
