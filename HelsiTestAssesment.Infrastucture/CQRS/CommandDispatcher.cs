using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace HelsiTestAssesment.Infrastucture.CQRS;

public class CommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        if (handler == null)
        {
            throw new InvalidOperationException($"Handler for query {typeof(TCommand).Name} not found.");
        }

        await handler.Handle(command);
    }
}
