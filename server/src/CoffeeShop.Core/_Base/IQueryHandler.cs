using MediatR;

namespace CoffeeShop.Core
{
    public interface IQueryHandler<in TQuery, TResponse> :
        IRequestHandler<TQuery, TResponse>
           where TQuery : IQuery<TResponse>
    {
    }
}