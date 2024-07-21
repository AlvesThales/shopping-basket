using FluentValidation;

namespace ShoppingBasket.Application.Features.OrderFeature.UpdateOrder;

public class UpdateOrderValidator: AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        ValidateOrderId();
        ValidateOrderItems();
    }


    private void ValidateOrderId()
    {
        RuleFor(c => c.OrderId)
            .NotEmpty();
    }
    private void ValidateOrderItems()
    {
        RuleFor(c => c.OrderItems)
            .NotEmpty();
    }
}