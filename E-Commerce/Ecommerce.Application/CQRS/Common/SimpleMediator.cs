using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.CQRS.Common;

public class SimpleMediator(IServiceProvider serviceProvider) : IMediator
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public Task<TResult> SendAsync<TResult>(IQuery<TResult> query)
    {
        var handlerType = typeof(IHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        dynamic? handler = _serviceProvider.GetService(handlerType);
        if (handler == null)
            throw new InvalidOperationException($"Handler for {query.GetType().Name} not found");
        return handler.HandleAsync((dynamic)query);
    }

    public Task<TResult> SendAsync<TResult>(ICommand<TResult> command)
    {
        var handlerType = typeof(IHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        dynamic? handler = _serviceProvider.GetService(handlerType);
        if (handler == null)
            throw new InvalidOperationException($"Handler for {command.GetType().Name} not found");
        return handler.HandleAsync((dynamic)command);
    }
}
