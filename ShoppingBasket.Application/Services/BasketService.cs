using MediatR;
using ShoppingBasket.Application.Features.BasketFeature.CreateBasket;
using ShoppingBasket.Application.Features.BasketFeature.DeleteBasket;
using ShoppingBasket.Application.Features.BasketFeature.GetBasket;
using ShoppingBasket.Application.Features.BasketFeature.GetBaskets;
using ShoppingBasket.Application.Features.BasketFeature.PayBasket;
using ShoppingBasket.Application.Features.BasketFeature.UpdateBasket;
using ShoppingBasket.Application.Interfaces;
using ShoppingBasket.Application.Mappings;
using ShoppingBasket.Application.ViewModels.BasketViewModels;
using ShoppingBasket.Domain.Common.Interfaces;

namespace ShoppingBasket.Application.Services;

public class BasketService : IBasketService
{
    private readonly IMediatorHandler _bus;
    private readonly BasketMapper _mapper;

    public BasketService(IMediatorHandler bus)
    {
        _bus = bus;
        _mapper = new BasketMapper();
    }
    
    public async Task<Result<CreateBasketOutput>> CreateBasket(string customerId, CreateBasketInput input, CancellationToken cancellationToken)
    {
        var command = new CreateBasketCommand(customerId, input.BasketItems);
        var response = await _bus.SendCommand(command,cancellationToken);

        return response.Match<Result<CreateBasketOutput>>(
            basket => _mapper.BasketToCreateBasketOutput(basket),
            error => error);
    } 
    public async Task<Result<UpdateBasketOutput>> UpdateBasket(Guid customerId, UpdateBasketInput input, CancellationToken cancellationToken)
    {
        var command = new UpdateBasketCommand(customerId, input.BasketItems);
        var response = await _bus.SendCommand(command,cancellationToken);

        return response.Match<Result<UpdateBasketOutput>>(
            basket => _mapper.BasketToUpdateBasketOutput(basket),
            error => error);
    }

    public async Task<Result<Unit>> DeleteBasket(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteBasketCommand(id);
        var response = await _bus.SendCommand(command, cancellationToken);

        return response.Match<Result<Unit>>(
            basket => Unit.Value,
            error => error);
    }

    public async Task<Result<GetBasketSimple>> GetBasketById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBasketQuery(id);
        var response = await _bus.SendCommand(query, cancellationToken);
        return response.Match<Result<GetBasketSimple>>(
            basket => _mapper.BasketToBasketOutput(basket),
            error => error);
    }
    public async Task<Result<ICollection<GetBasketSimple>>> GetBaskets(bool? isPaid, CancellationToken cancellationToken)
    {
        var query = new GetBasketsQuery(isPaid);
        var response = await _bus.SendCommand(query, cancellationToken);
        return response.Match<Result<ICollection<GetBasketSimple>>>(
            baskets => baskets.Select(basket => _mapper.BasketToBasketOutput(basket)).ToList(),
            error => error);
    }

    public async Task<Result<CreateBasketOutput>> PayBasket(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new PayBasketCommand(id);
        var response = await _bus.SendCommand(command, cancellationToken);

        return response.Match<Result<CreateBasketOutput>>(
            basket => _mapper.BasketToCreateBasketOutput(basket),
            error => error);
    }
}