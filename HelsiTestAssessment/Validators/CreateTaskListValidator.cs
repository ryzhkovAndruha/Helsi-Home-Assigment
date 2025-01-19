using FluentValidation;
using HelsiTestAssesment.Application;
using HelsiTestAssesment.Application.DTOs;

namespace HelsiTestAssessment.Validators;

public class CreateTaskListValidator: AbstractValidator<CreateTaskListDto>
{
    public CreateTaskListValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.ERR_001_CODE)
            .WithMessage(ErrorCodes.ERROR_IS_REQUIRED)
            .MaximumLength(255);
    }
}
