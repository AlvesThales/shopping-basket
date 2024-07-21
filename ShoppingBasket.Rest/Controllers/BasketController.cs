using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingBasket.Application.Interfaces;
using ShoppingBasket.Application.ViewModels.BasketViewModels;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Controllers;
[Route("baskets")]
public class BasketController : ApiController
{
    private readonly IBasketService _basketService;
    private readonly UserManager<Customer> _userManager;

    public BasketController(IBasketService basketService,IMediatorHandler mediator,
        INotificationHandler<DomainNotification> notifications, UserManager<Customer> userManager)
    {
        _basketService = basketService;
        _userManager = userManager;
    }
    
    /// <summary>
    /// Create a basket
    /// </summary>
    /// <param name="createBasketInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(typeof(CreateBasketOutput),201)]
    [ProducesResponseType(typeof(CreateBasketOutput),400)]
    [ProducesResponseType(typeof(CreateBasketOutput),500)]
    public async Task<IActionResult> CreateBasket([FromBody] CreateBasketInput createBasketInput, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(User);
        var userId = user!.Id;

        var result = await _basketService.CreateBasket(userId, createBasketInput,cancellationToken);
        return Created(result);
    }

    /// <summary>
    /// Get a specific basket
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetBasketSimple),200)]
    public async Task<IActionResult> GetBasketById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _basketService.GetBasketById(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get all baskets
    /// </summary>
    /// <param name="isPaid"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetBasketSimple), 200)]
    public async Task<IActionResult> GetBaskets(CancellationToken cancellationToken, [FromQuery] bool? isPaid = null)
    {
        var result = await _basketService.GetBaskets(isPaid, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Pay for a basket
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:guid}/pay")]
    [ProducesResponseType(typeof(CreateBasketOutput), 201)]
    [ProducesResponseType(typeof(CreateBasketOutput), 400)]
    [ProducesResponseType(typeof(CreateBasketOutput), 500)]
    public async Task<IActionResult> Pay([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _basketService.PayBasket(id, cancellationToken);
        return Created(result);
    }

    /// <summary>
    /// Delete a basket
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(CreateBasketOutput), 204)]
    [ProducesResponseType(typeof(CreateBasketOutput), 400)]
    [ProducesResponseType(typeof(CreateBasketOutput), 500)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _basketService.DeleteBasket(id, cancellationToken);
        return NoContent(result);
    }

    /// <summary>
    /// Update a basket
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateBasketInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateBasketOutput), 200)]
    [ProducesResponseType(typeof(UpdateBasketOutput), 400)]
    [ProducesResponseType(typeof(UpdateBasketOutput), 500)]
    public async Task<IActionResult> UpdateBasket([FromRoute] Guid id, [FromBody] UpdateBasketInput updateBasketInput, CancellationToken cancellationToken)
    {
        var result = await _basketService.UpdateBasket(id, updateBasketInput, cancellationToken);
        return Ok(result);
    }
}