using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace HelsiTestAssesment.Infrastucture.CQRS;

public class QueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>
    {
        var handler = _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();

        if (handler == null)
        {
            throw new InvalidOperationException($"Handler for query {typeof(TQuery).Name} not found.");
        }
            

        return await handler.Handle(query, cancellationToken);
    }
}
