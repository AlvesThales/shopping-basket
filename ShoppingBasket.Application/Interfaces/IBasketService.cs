using MediatR;
using ShoppingBasket.Application.ViewModels.BasketViewModels;

namespace ShoppingBasket.Application.Interfaces;

public interface IBasketService
{
    Task<Result<CreateBasketOutput>> CreateBasket(string customerId, CreateBasketInput input, CancellationToken cancellationToken = default);
    Task<Result<UpdateBasketOutput>> UpdateBasket(Guid basketId, UpdateBasketInput input, CancellationToken cancellationToken = default);
    Task<Result<GetBasketSimple>> GetBasketById(Guid id, CancellationToken cancellationToken = default);
    Task<Result<CreateBasketOutput>> PayBasket(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ICollection<GetBasketSimple>>> GetBaskets(bool? isPaid, string userId,
        CancellationToken cancellationToken = default);
    Task<Result<Unit>> DeleteBasket(Guid id, CancellationToken cancellationToken);
}