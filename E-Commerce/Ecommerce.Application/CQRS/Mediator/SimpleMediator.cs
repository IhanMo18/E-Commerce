using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application.CQRS.Mediator;

public class SimpleMediator(IServiceProvider serviceProvider) : IMediator
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic? handler = _serviceProvider.GetService(handlerType);
        if (handler is null)
        {
            throw new InvalidOperationException($"Handler for {request.GetType().Name} not registered.");
        }
        return handler.Handle((dynamic)request, cancellationToken);
    }
}
