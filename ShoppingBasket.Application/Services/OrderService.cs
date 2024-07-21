using MediatR;
using ShoppingBasket.Domain.Entities;
using ShoppingBasket.Application.Features.OrderFeature.CreateOrder;
using ShoppingBasket.Application.Features.OrderFeature.DeleteOrder;
using ShoppingBasket.Application.Features.OrderFeature.GetOrder;
using ShoppingBasket.Application.Features.OrderFeature.PayOrder;
using ShoppingBasket.Application.Features.OrderFeature.UpdateOrder;
using ShoppingBasket.Application.Interfaces;
using ShoppingBasket.Application.Mappings;
using ShoppingBasket.Application.ViewModels.OrderViewModels;
using ShoppingBasket.Domain.Common.Interfaces;

namespace ShoppingBasket.Application.Services;

public class OrderService : IOrderService
{
    private readonly IMediatorHandler _bus;
    private readonly OrderMapper _mapper;

    public OrderService(IMediatorHandler bus)
    {
        _bus = bus;
        _mapper = new OrderMapper();
    }
    
    public async Task<Result<CreateOrderOutput>> CreateOrder(string customerId, CreateOrderInput input, CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(customerId, input.OrderItems);
        var response = await _bus.SendCommand(command,cancellationToken);

        return response.Match<Result<CreateOrderOutput>>(
            order => _mapper.OrderToCreateOrderOutput(order),
            error => error);
    } 
    public async Task<Result<UpdateOrderOutput>> UpdateOrder(Guid customerId, UpdateOrderInput input, CancellationToken cancellationToken)
    {
        var command = new UpdateOrderCommand(customerId, input.OrderItems);
        var response = await _bus.SendCommand(command,cancellationToken);

        return response.Match<Result<UpdateOrderOutput>>(
            order => _mapper.OrderToUpdateOrderOutput(order),
            error => error);
    }

    public async Task<Result<Unit>> DeleteOrder(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteOrderCommand(id);
        var response = await _bus.SendCommand(command, cancellationToken);

        return response.Match<Result<Unit>>(
            order => Unit.Value,
            error => error);
    }

    public async Task<Result<GetOrderSimple>> GetOrder(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetOrderQuery(id);
        var response = await _bus.SendCommand(query, cancellationToken);
        return response.Match<Result<GetOrderSimple>>(
            order => _mapper.OrderToOrderOutput(order),
            error => error);
    }
    public async Task<Result<ICollection<GetOrderSimple>>> GetOrders(bool? isPaid, CancellationToken cancellationToken)
    {
        var query = new GetOrdersQuery(isPaid);
        var response = await _bus.SendCommand(query, cancellationToken);
        return response.Match<Result<ICollection<GetOrderSimple>>>(
            orders => orders.Select(order => _mapper.OrderToOrderOutput(order)).ToList(),
            error => error);
    }

    public async Task<Result<CreateOrderOutput>> PayOrder(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new PayOrderCommand(id);
        var response = await _bus.SendCommand(command, cancellationToken);

        return response.Match<Result<CreateOrderOutput>>(
            order => _mapper.OrderToCreateOrderOutput(order),
            error => error);
    }
}