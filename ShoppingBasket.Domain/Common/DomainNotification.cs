namespace ShoppingBasket.Domain.Common;

public class DomainNotification : Event
{
    public Guid DomainNotificationId { get; private set; }
    public string Key { get; private set; }
    public dynamic Value { get; private set; }
    
    public ErrorTypes? TypeError { get; private set; }
    public int Version { get; private set; }
    public bool IsError { get; private set; }

    public DomainNotification(string key, string value, ErrorTypes typeError = ErrorTypes.BadRequest, bool isError = true)
    {
        DomainNotificationId = Guid.NewGuid();
        Version = 1;
        Key = key;
        Value = value;
        if (isError)
        {
            TypeError = typeError;
        }
        IsError = isError;
    }

    public DomainNotification(string key, int value, ErrorTypes typeError = ErrorTypes.BadRequest, bool isError = true)
    {
        DomainNotificationId = Guid.NewGuid();
        Version = 1;
        Key = key;
        Value = value;
        if (isError)
        {
            TypeError = typeError;
        }
        IsError = isError;
    }

    public DomainNotification(string key, object value, ErrorTypes typeError = ErrorTypes.BadRequest, bool isError = true)
    {
        DomainNotificationId = Guid.NewGuid();
        Version = 1;
        Key = key;
        Value = value;
        if (isError)
        {
            TypeError = typeError;
        }
        IsError = isError;
    }
}

public enum ErrorTypes
{
    BadRequest = 0,
    NotFound = 1,
    Forbidden = 2,
    ServerError = 3
}