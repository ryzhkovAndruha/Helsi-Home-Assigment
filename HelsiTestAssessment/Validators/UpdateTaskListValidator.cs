using FluentValidation;
using HelsiTestAssesment.Application;
using HelsiTestAssesment.Application.DTOs;

namespace HelsiTestAssessment.Validators;

public class UpdateTaskListValidator: AbstractValidator<UpdateTaskListDto>
{
    public UpdateTaskListValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.ERR_001_CODE)
            .WithMessage(ErrorCodes.ERROR_AT_LEAST_ONE_FIELD_MUST_BE_PROVIDED)
            .MaximumLength(255);
    }
}
