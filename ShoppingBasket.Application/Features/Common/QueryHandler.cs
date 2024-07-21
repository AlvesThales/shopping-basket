using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;

namespace ShoppingBasket.Application.Features.Common;

public class QueryHandler
{
    private readonly IUnitOfWork _uow;
    private readonly IMediatorHandler _bus;
    private readonly DomainNotificationHandler _notifications;
    private readonly ILogger _logger;


    protected QueryHandler(ILogger logger, IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notification)
    {
        _uow = uow;
        _bus = bus;
        _notifications = (DomainNotificationHandler)notification;
        _logger = logger;
    }

    protected static DomainError NotifyValidationErrors(Command message)
    {
        var errors = new DomainError();
        foreach (var error in message.ValidationResult.Errors)
        {
            errors.AddError(new DomainNotification(message.MessageType, error.ErrorMessage, ErrorTypes.BadRequest));
        }

        return errors;
    }
    
    protected static DomainError NotifyError(string code, string message, ErrorTypes errorType = ErrorTypes.BadRequest)
    {
        var errors = new DomainError();
        errors.AddError(new DomainNotification(code, message, errorType));
        return errors;
    }

    protected async Task<bool> Commit(CancellationToken cancellationToken)
    {
        return await _uow.CommitAsync(cancellationToken);
    }
}