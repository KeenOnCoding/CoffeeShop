using MediatR;

namespace CoffeeShop.Core
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}