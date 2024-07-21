using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingBasket.Application.Interfaces;
using ShoppingBasket.Application.ViewModels.OrderViewModels;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Controllers;
[Route("orders")]
public class OrderController : ApiController
{
    private readonly IOrderService _orderService;
    private readonly UserManager<Customer> _userManager;

    public OrderController(IOrderService orderService,IMediatorHandler mediator,
        INotificationHandler<DomainNotification> notifications, UserManager<Customer> userManager)
    {
        _orderService = orderService;
        _userManager = userManager;
    }
    
    /// <summary>
    /// Create an order
    /// </summary>
    /// <param name="createOrderInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CreateOrderOutput),201)]
    [ProducesResponseType(typeof(CreateOrderOutput),400)]
    [ProducesResponseType(typeof(CreateOrderOutput),500)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderInput createOrderInput, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(User);
        var userId = user!.Id;

        var result = await _orderService.CreateOrder(userId, createOrderInput,cancellationToken);
        return Created(result);
    }

    /// <summary>
    /// Get a specific order
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetOrderSimple),200)]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _orderService.GetOrder(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get all orders
    /// </summary>
    /// <param name="isPaid"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetOrderSimple), 200)]
    public async Task<IActionResult> GetOrders(CancellationToken cancellationToken, [FromQuery] bool? isPaid = null)
    {
        var result = await _orderService.GetOrders(isPaid, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Pay for an order
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:guid}/pay")]
    [ProducesResponseType(typeof(CreateOrderOutput), 201)]
    [ProducesResponseType(typeof(CreateOrderOutput), 400)]
    [ProducesResponseType(typeof(CreateOrderOutput), 500)]
    public async Task<IActionResult> Pay([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _orderService.PayOrder(id, cancellationToken);
        return Created(result);
    }

    /// <summary>
    /// Pay for an order
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(CreateOrderOutput), 204)]
    [ProducesResponseType(typeof(CreateOrderOutput), 400)]
    [ProducesResponseType(typeof(CreateOrderOutput), 500)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _orderService.DeleteOrder(id, cancellationToken);
        return NoContent(result);
    }

    /// <summary>
    /// Update an order
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateOrderInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateOrderOutput), 200)]
    [ProducesResponseType(typeof(UpdateOrderOutput), 400)]
    [ProducesResponseType(typeof(UpdateOrderOutput), 500)]
    public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromBody] UpdateOrderInput updateOrderInput, CancellationToken cancellationToken)
    {
        var result = await _orderService.UpdateOrder(id, updateOrderInput, cancellationToken);
        return Ok(result);
    }
}