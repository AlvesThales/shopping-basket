using FluentValidation;

namespace ShoppingBasket.Application.Features.ProductFeature.CreateProduct;

public class CreateProductValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        ValidateName();
        ValidatePrice();
    }


    private void ValidateName()
    {
        RuleFor(c => c.Name)
            .NotEmpty();
    }
    private void ValidatePrice()
    {
        RuleFor(c => c.Price)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Price has to be greater than 0");
    }
}