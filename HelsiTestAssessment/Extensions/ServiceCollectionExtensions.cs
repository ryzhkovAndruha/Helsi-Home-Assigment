using FluentValidation;
using HelsiTestAssesment.Application.DTOs;
using HelsiTestAssessment.Validators;

namespace HelsiTestAssessment.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateTaskListDto>, CreateTaskListValidator>();
        services.AddScoped<IValidator<UpdateTaskListUsersDto>, UpdateTaskListUsersValidator>();
        services.AddScoped<IValidator<UpdateTaskListDto>, UpdateTaskListValidator>();

        return services;
    }
}
