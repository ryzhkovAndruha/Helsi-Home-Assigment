using FluentValidation;
using HelsiTestAssesment.Application.DTOs;

namespace HelsiTestAssessment.Validators;

public class UpdateTaskListUsersValidator: AbstractValidator<UpdateTaskListUsersDto>
{
    public UpdateTaskListUsersValidator()
    {
        RuleFor(dto => dto)
            .Must(dto => !string.IsNullOrEmpty(dto.UserToAdd) || !string.IsNullOrEmpty(dto.UserToRemove))
            .WithMessage("At least one of 'UserToAdd' or 'UserToRemove' must be provided.")
            .WithErrorCode("ERR_002_CODE");
    }
}
