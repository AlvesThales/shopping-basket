using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingBasket.Application.Interfaces;
using ShoppingBasket.Application.ViewModels.ProductViewModels;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;

namespace ShoppingBasket.Controllers;
[Route("products")]
public class ProductController : ApiController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService,IMediatorHandler mediator,
        INotificationHandler<DomainNotification> notifications)
    {
        _productService = productService;
    }
    
    /// <summary>
    /// Create an product
    /// </summary>
    /// <param name="createProductInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateProductOutput),201)]
    [ProducesResponseType(typeof(CreateProductOutput),400)]
    [ProducesResponseType(typeof(CreateProductOutput),500)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductInput createProductInput, CancellationToken cancellationToken)
    {
        var result = await _productService.CreateProduct(createProductInput, cancellationToken);
        return Created(result);
    }

    /// <summary>
    /// Get a specific product
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetProductSimple),200)]
    public async Task<IActionResult> GetProductById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _productService.GetProduct(id,cancellationToken);
        return Ok(result);
    }
}