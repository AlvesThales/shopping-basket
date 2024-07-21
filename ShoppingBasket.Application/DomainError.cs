using System.Collections;
using ShoppingBasket.Domain.Common;

namespace ShoppingBasket.Application;

public class DomainError
{
    public ICollection<DomainNotification> Errors { get; } = new List<DomainNotification>();

    public void AddErrors(IEnumerable<DomainNotification> errors)
    {
        foreach (var error in errors)
        {
            Errors.Add(error);
        }
    }
    
    public void AddError(DomainNotification error)
    {
            Errors.Add(error);
    }
}