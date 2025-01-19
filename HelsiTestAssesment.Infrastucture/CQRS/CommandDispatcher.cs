using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace HelsiTestAssesment.Infrastucture.CQRS;

public class CommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<CommandOperationResultDto> DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        if (handler == null)
        {
            throw new InvalidOperationException($"Handler for query {typeof(TCommand).Name} not found.");
        }

        return await handler.Handle(command, cancellationToken);
    }
}
