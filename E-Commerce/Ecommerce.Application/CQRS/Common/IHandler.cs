namespace Ecommerce.CQRS.Common;

public interface IHandler<TRequest, TResult>
{
    Task<TResult> HandleAsync(TRequest request);
}
