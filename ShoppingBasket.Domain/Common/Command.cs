using FluentValidation.Results;

namespace ShoppingBasket.Domain.Common;

public abstract class Command : Message
{
    public DateTime Timestamp { get; private set; }
    public ValidationResult ValidationResult { get; protected set; } = new();

    protected Command()
    {
        Timestamp = DateTime.UtcNow;
    }

    public abstract bool IsValid();
}