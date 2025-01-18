using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.Handlers.Commands;
using HelsiTestAssesment.Application.Handlers.Queries;
using HelsiTestAssesment.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace HelsiTestAssesment.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        services.AddScoped<ICommandHandler<AddTaskListCommand>, AddTaskListCommandHandler>();
        services.AddScoped<ICommandHandler<AddUserToTaskListCommand>, AddUserToTaskListCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteTaskListCommand>, DeleteTaskListCommandHandler>();
        services.AddScoped<ICommandHandler<RemoveUserFromTaskListCommand>, RemoveUserFromTaskListCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateTaskListCommand>, UpdateTaskListCommandHandler>();

        services.AddScoped<IQueryHandler<GetAccesibleUsersQuery, IEnumerable<string>?>, GetAccesibleUsersQueryHandler>();
        services.AddScoped<IQueryHandler<GetTaskListByIdQuery, TaskList?>, GetTaskListByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetTaskListsQuery, IEnumerable<TaskList>?>, GetTaskListsQueryHandler>();

        return services;
    }
}
