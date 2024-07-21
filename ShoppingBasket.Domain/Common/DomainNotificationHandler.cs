using MediatR;

namespace ShoppingBasket.Domain.Common;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
    private List<DomainNotification> _notifications;

    public DomainNotificationHandler()
    {
        _notifications = new List<DomainNotification>();
    }

    public virtual IEnumerable<DomainNotification> GetNotifications()
    {
        return _notifications;
    }

    public virtual bool HasNotificationsWithError()
    {
        return GetNotifications().Any(x => x.IsError);
    }

    public void Dispose()
    {
        _notifications = new List<DomainNotification>();
    }

    public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
    {
        _notifications.Add(notification);
        return Task.CompletedTask;
    }
}