using FluentValidation;

namespace ShoppingBasket.Application.Features.BasketFeature.CreateBasket;

public class CreateOrderValidator: AbstractValidator<CreateBasketCommand>
{
    public CreateOrderValidator()
    {
        ValidateCustomerId();
        ValidateOrderItems();
    }


    private void ValidateCustomerId()
    {
        RuleFor(c => c.CustomerId)
            .NotEmpty();
    }
    private void ValidateOrderItems()
    {
        RuleFor(c => c.OrderItems)
            .NotEmpty();
    }
}