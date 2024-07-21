using FluentValidation;

namespace ShoppingBasket.Application.Features.OrderFeature.CreateOrder;

public class CreateOrderValidator: AbstractValidator<CreateOrderCommand>
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