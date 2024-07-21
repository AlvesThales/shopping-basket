using FluentValidation;

namespace ShoppingBasket.Application.Features.BasketFeature.UpdateBasket;

public class UpdateBasketValidator: AbstractValidator<UpdateBasketCommand>
{
    public UpdateBasketValidator()
    {
        ValidateBasketId();
        ValidateBasketItems();
    }


    private void ValidateBasketId()
    {
        RuleFor(c => c.BasketId)
            .NotEmpty();
    }
    private void ValidateBasketItems()
    {
        RuleFor(c => c.BasketItems)
            .NotEmpty();
    }
}