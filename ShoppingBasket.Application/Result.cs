namespace ShoppingBasket.Application;

public readonly struct Result<TValue>
{
    private readonly TValue? _value;
    private readonly DomainError? _error;

    private Result(TValue value)
    {
        IsError = false;
        _value = value;
        _error = default;
    }
    
    private Result(DomainError error)
    {
        IsError = true;
        _value = default;
        _error = error;
    }

    private bool IsError { get; }

    public bool IsSuccess => !IsError;

    public static implicit operator Result<TValue>(TValue value) => new(value);
    public static implicit operator Result<TValue>(DomainError error) => new(error);
    public static bool operator true(Result<TValue> result) => result.IsSuccess;
    public static bool operator false(Result<TValue> result) => !result.IsSuccess;

    public TValue? GetValue() => _value;

    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<DomainError, TResult> failure) => !IsError ? success(_value!) : failure(_error!);
}