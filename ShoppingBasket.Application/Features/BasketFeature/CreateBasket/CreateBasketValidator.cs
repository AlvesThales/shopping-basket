using FluentValidation;

namespace ShoppingBasket.Application.Features.BasketFeature.CreateBasket;

public class CreateBasketValidator: AbstractValidator<CreateBasketCommand>
{
    public CreateBasketValidator()
    {
        //ValidateCustomerId();
        ValidateBasketItems();
    }


    private void ValidateCustomerId()
    {
        RuleFor(c => c.CustomerId)
            .NotEmpty();
    }
    private void ValidateBasketItems()
    {
        RuleFor(c => c.BasketItems)
            .NotEmpty();
    }
}