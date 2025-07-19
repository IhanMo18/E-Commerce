namespace Ecommerce.CQRS.Common;

public interface IMediator
{
    Task<TResult> SendAsync<TResult>(IQuery<TResult> query);
    Task<TResult> SendAsync<TResult>(ICommand<TResult> command);
}
